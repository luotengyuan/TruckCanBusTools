using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.Concurrent;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using 车辆CAN总线数据现场分析工具.Utils;
using 车辆CAN总线数据现场分析工具.Objs;
using System.IO;

namespace 车辆CAN总线数据现场分析工具
{

    //【1】声明委托
    public delegate void _SendCanMsg(int devInx, uint id, byte[] data);

    public partial class Form_Main : Form
    {

        //【2】定义事件
        public event _SendCanMsg SendCanMsg;

        string rootPath = "";

        Queue<can_data_t> recvQueue_1 = new Queue<can_data_t>();
        private object recvQueueLock_1 = new object();
        Thread hanleRecvDataThread_1 = null;
        Queue<can_data_t> recvQueue_2 = new Queue<can_data_t>();
        private object recvQueueLock_2 = new object();
        Thread hanleRecvDataThread_2 = null;

        ConcurrentQueue<SaveObj> saveQueue_1 = new ConcurrentQueue<SaveObj>();
        private object saveQueueLock_1 = new object();
        ConcurrentQueue<SaveObj> saveQueue_2 = new ConcurrentQueue<SaveObj>();
        private object saveQueueLock_2 = new object();

        FileStream saveFileStream_1 = null;
        FileStream saveFileStream_2 = null;
        Thread saveCanDataThread_1 = null;
        Thread saveCanDataThread_2 = null;
        long saveSerial_1 = 0;
        long saveSerial_2 = 0;

        #region  CAN相关变量定义
        //定义CAN设备类型
        bool mIsCanOpen = false;    //打开CAN标志
        public UInt32 m_devtype = (UInt32)CanUtils.DeviceType.VCI_USBCAN2;//USBCAN2
        public UInt32 m_devind = 0;
        public UInt32 m_canind_1 = 0;
        public UInt32 m_canind_2 = 1;
        public byte m_filter = 1;
        public byte m_mode = 0;
        UInt32[] m_arrdevtype = new UInt32[20];
        CanUtils.VCI_CAN_OBJ[] m_recFrames = new CanUtils.VCI_CAN_OBJ[1000];

        int frameType = 1;
        int frameFormat = 0;

        //定义Timer类
        System.Timers.Timer rec_timer_1;
        CanUtils.VCI_CAN_OBJ[] m_recFrames_1 = new CanUtils.VCI_CAN_OBJ[1000];
        long candata_serial_1 = 0;

        //定义Timer类
        System.Timers.Timer rec_timer_2;
        CanUtils.VCI_CAN_OBJ[] m_recFrames_2 = new CanUtils.VCI_CAN_OBJ[1000];
        long candata_serial_2 = 0;

        Thread sendThread_1 = null;
        Queue<SendObj> sendQueue_1 = new Queue<SendObj>();
        private object sendQueueLock_1 = new object();
        CanUtils.VCI_CAN_OBJ sendobj_1 = new CanUtils.VCI_CAN_OBJ();

        Thread sendThread_2 = null;
        Queue<SendObj> sendQueue_2 = new Queue<SendObj>();
        private object sendQueueLock_2 = new object();
        CanUtils.VCI_CAN_OBJ sendobj_2 = new CanUtils.VCI_CAN_OBJ();

        Thread readErrInfoThread = null;
        CanUtils.VCI_ERR_INFO m_errInfo = new CanUtils.VCI_ERR_INFO();

        ListViewItem li_1 = null;
        ListViewItem li_2 = null;
        bool is_sep = false;

        bool is_visible_send_1 = false;
        bool is_visible_recv_1 = false;
        bool is_visible_err_1 = false;

        int total_send_1 = 0;
        int total_recv_1 = 0;
        int total_err_1 = 0;


        bool is_visible_send_2 = false;
        bool is_visible_recv_2 = false;
        bool is_visible_err_2 = false;

        int total_send_2 = 0;
        int total_recv_2 = 0;
        int total_err_2 = 0;

        bool is_cleanning_1 = false;
        bool is_cleanning_2 = false;

        #endregion

        /// <summary>
        /// 界面构造函数
        /// </summary>
        public Form_Main()
        {
            InitializeComponent();
            ListView.CheckForIllegalCrossThreadCalls = false;
            rootPath = Environment.CurrentDirectory.ToString();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            InitViews();
            InitTimer_1();
            InitTimer_2();
            hanleRecvDataThread_1 = new Thread(HandleRecvDataThread_1);
            hanleRecvDataThread_1.Start();
            hanleRecvDataThread_2 = new Thread(HandleRecvDataThread_2);
            hanleRecvDataThread_2.Start();
            sendThread_1 = new Thread(SendDataThread_1);
            sendThread_1.Start();
            sendThread_2 = new Thread(SendDataThread_2);
            sendThread_2.Start();
            readErrInfoThread = new Thread(ReadErrInfoThread);
            readErrInfoThread.Start();
            saveCanDataThread_1 = new Thread(SaveCanDataThread_1);
            saveCanDataThread_1.Start();
            saveCanDataThread_2 = new Thread(SaveCanDataThread_2);
            saveCanDataThread_2.Start();
        }

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            rec_timer_1.Close();
            rec_timer_2.Close();
            hanleRecvDataThread_1.Abort();
            hanleRecvDataThread_2.Abort();
            sendThread_1.Abort();
            sendThread_2.Abort();
            readErrInfoThread.Abort();
            saveCanDataThread_1.Abort();
            saveCanDataThread_2.Abort();
            if (saveFileStream_1 != null)
            {
                saveFileStream_1.Close();
            }
            if (saveFileStream_2 != null)
            {
                saveFileStream_2.Close();
            }
            Environment.Exit(0);
        }

        /// <summary>
        /// 初始化界面布局
        /// </summary>
        private void InitViews()
        {
            // 设置皮肤
            this.mySkinEngine.SkinFile = "Wave.ssk";
            // 初始化CAN相关控件
            cb_rate_1.DataSource = CanUtils.getRateTable();
            cb_rate_1.DisplayMember = "Rate";   // Text，即显式的文本
            cb_rate_1.ValueMember = "Timing";    // Value，即实际的值
            cb_rate_2.DataSource = CanUtils.getRateTable();
            cb_rate_2.DisplayMember = "Rate";   // Text，即显式的文本
            cb_rate_2.ValueMember = "Timing";    // Value，即实际的值
            try
            {
                //CAN_PARAM
                cb_devtype.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.devtype, "1", -1));
                cb_devidx.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.devidx, "0", -1));
                cb_rate_1.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.rate_1, "8", -1));
                cb_rate_2.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.rate_2, "8", -1));
                cb_visible_recv_1.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.visible_recv_1, "0", -1));
                is_visible_recv_1 = cb_visible_recv_1.SelectedIndex == 0;
                cb_visible_send_1.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.visible_send_1, "0", -1));
                is_visible_send_1 = cb_visible_send_1.SelectedIndex == 0;
                cb_visible_recv_2.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.visible_recv_2, "0", -1));
                is_visible_recv_2 = cb_visible_recv_2.SelectedIndex == 0;
                cb_visible_send_2.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.visible_send_2, "0", -1));
                is_visible_send_2 = cb_visible_send_2.SelectedIndex == 0;
                cb_visible_err_1.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.visible_err_1, "0", -1));
                is_visible_err_1 = cb_visible_err_1.SelectedIndex == 0;
                cb_visible_err_2.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.visible_err_2, "0", -1));
                is_visible_err_2 = cb_visible_err_2.SelectedIndex == 0;
                cb_send_format_1.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.send_format_1, "0", -1));
                cb_frame_type_1.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.frame_type_1, "1", -1));
                cb_frame_format_1.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.frame_format_1, "0", -1));
                tb_send_id_1.Text = ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.send_id_1, "", -1);
                tb_send_data_1.Text = ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.send_data_1, "", -1);
                cb_send_format_2.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.send_format_2, "0", -1));
                cb_frame_type_2.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.frame_type_2, "1", -1));
                cb_frame_format_2.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.frame_format_2, "0", -1));
                tb_send_id_2.Text = ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.send_id_2, "", -1);
                tb_send_data_2.Text = ConfigUtils.getConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.send_data_2, "", -1);
            }
            catch (Exception)
            {
                MessageBox.Show("加载配置出错！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            InitListView_1(this.lv_can_data_1);
            InitListView_2(this.lv_can_data_2);
        }

        /// <summary>
        /// 初始化Timer控件
        /// </summary>
        private void InitTimer_1()
        {
            //设置定时间隔(毫秒为单位)
            int interval = 500;
            rec_timer_1 = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            rec_timer_1.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            //rec_timer.Enabled = true;
            //绑定Elapsed事件
            rec_timer_1.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp_1);
            rec_timer_1.Start();
        }

        byte[] contents_1 = new byte[8];
        StringBuilder frameStr_1 = new StringBuilder();

        /// <summary>
        /// Timer类执行定时到点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        unsafe private void TimerUp_1(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Console.WriteLine("--------timer--------");
            if (mIsCanOpen)
            {

                UInt32 resFrameNum = CanUtils.VCI_Receive(m_devtype, m_devind, m_canind_1, ref m_recFrames_1[0], 1000, 100);
                //Console.WriteLine("resFrameNum = " + resFrameNum);
                for (UInt32 i = 0; i < resFrameNum; i++)
                {
                    if (m_recFrames_1[i].RemoteFlag == 1)
                        continue;//跳过远程帧
                    byte len = (byte)(m_recFrames_1[i].DataLen % 9);
                    contents_1 = new byte[8];
                    fixed (CanUtils.VCI_CAN_OBJ* temp = &m_recFrames_1[i])
                    {
                        for (byte j = 0; j < len; j++)
                        {
                            contents_1[j] = temp->Data[j];//读取1939协议单帧
                        }
                    }
                    can_data_t can_data = new can_data_t();
                    can_data.id = m_recFrames_1[i].ID;
                    can_data.data = contents_1;
                    lock (recvQueueLock_1)
                    {
                        recvQueue_1.Enqueue(can_data);
                    }
                }
                
            }
        }

        private void HandleRecvDataThread_1()
        {
            while (true)
            {
                if (recvQueue_1.Count <= 0)
                {
                    Thread.Sleep(10);
                    continue;
                }
                can_data_t can_data;
                lock (recvQueueLock_1)
                {
                    can_data = recvQueue_1.Dequeue();
                }

                String frameID = "0x" + Convert.ToString(can_data.id, 16).ToUpper();
                //Console.WriteLine("frameID = " + frameID);

                int len = can_data.data.Length;
                frameStr_1 = new StringBuilder();
                for (byte j = 0; j < len; j++)
                {
                    frameStr_1.Append(ConvertToHex(can_data.data[j]));
                }

                total_recv_1++;
                lb_count_1.Text = "收" + total_recv_1 + "/发" + total_send_1;

                DateTime ct = DateTime.Now;
                saveCanDataToFile_1("接收", ct, can_data.id, can_data.data);

                if (is_visible_recv_1)
                {
                    candata_serial_1++;
                    //创建行对象
                    ListViewItem li = new ListViewItem(candata_serial_1 + "");
                    //添加同一行的数据
                    li.SubItems.Add("接收");
                    li.SubItems.Add(ct.ToString("HH:mm:ss:fff"));
                    li.SubItems.Add(frameID);
                    li.SubItems.Add(len + "");
                    li.SubItems.Add(frameStr_1.ToString());
                    if (!is_cleanning_1)
                    {
                        this.Invoke((EventHandler)(delegate
                        {
                            InsertListView(this.lv_can_data_1, li);
                        }));
                    }
                    if (candata_serial_1 % 100 == 0)
                    {
                        GC.Collect();
                    }
                }

                //【3】激发事件
                if (SendCanMsg != null)
                {
                    SendCanMsg(1, can_data.id, can_data.data);
                }
            }
        }

        /// <summary>
        /// 初始化Timer控件
        /// </summary>
        private void InitTimer_2()
        {
            //设置定时间隔(毫秒为单位)
            int interval = 500;
            rec_timer_2 = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            rec_timer_2.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            //rec_timer.Enabled = true;
            //绑定Elapsed事件
            rec_timer_2.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp_2);
            rec_timer_2.Start();
        }

        byte[] contents_2 = new byte[8];
        StringBuilder frameStr_2 = new StringBuilder();

        /// <summary>
        /// Timer类执行定时到点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        unsafe private void TimerUp_2(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Console.WriteLine("--------timer--------");
            if (mIsCanOpen)
            {

                UInt32 resFrameNum = CanUtils.VCI_Receive(m_devtype, m_devind, m_canind_2, ref m_recFrames_2[0], 1000, 100);
                //Console.WriteLine("resFrameNum = " + resFrameNum);
                for (UInt32 i = 0; i < resFrameNum; i++)
                {
                    if (m_recFrames_2[i].RemoteFlag == 1)
                        continue;//跳过远程帧
                    byte len = (byte)(m_recFrames_2[i].DataLen % 9);
                    contents_2 = new byte[8];
                    fixed (CanUtils.VCI_CAN_OBJ* temp = &m_recFrames_2[i])
                    {
                        for (byte j = 0; j < len; j++)
                        {
                            contents_2[j] = temp->Data[j];//读取1939协议单帧
                        }
                    }
                    can_data_t can_data = new can_data_t();
                    can_data.id = m_recFrames_2[i].ID;
                    can_data.data = contents_2;
                    lock (recvQueueLock_2)
                    {
                        recvQueue_2.Enqueue(can_data);
                    }

                }
                
            }
        }

        private void HandleRecvDataThread_2()
        {
            while (true)
            {
                if (recvQueue_2.Count <= 0)
                {
                    Thread.Sleep(10);
                    continue;
                }
                can_data_t can_data;
                lock (recvQueueLock_2)
                {
                    can_data = recvQueue_2.Dequeue();
                }

                String frameID = "0x" + Convert.ToString(can_data.id, 16).ToUpper();
                //Console.WriteLine("frameID = " + frameID);

                int len = can_data.data.Length;
                frameStr_2 = new StringBuilder();
                for (byte j = 0; j < len; j++)
                {
                    frameStr_2.Append(ConvertToHex(can_data.data[j]));
                }

                total_recv_2++;
                lb_count_2.Text = "收" + total_recv_2 + "/发" + total_send_2;

                DateTime ct = DateTime.Now;
                saveCanDataToFile_2("接收", ct, can_data.id, can_data.data);

                if (is_visible_recv_2)
                {
                    candata_serial_2++;
                    //创建行对象
                    ListViewItem li = new ListViewItem(candata_serial_2 + "");
                    //添加同一行的数据
                    li.SubItems.Add("接收");
                    li.SubItems.Add(ct.ToString("HH:mm:ss:fff"));
                    li.SubItems.Add(frameID);
                    li.SubItems.Add(len + "");
                    li.SubItems.Add(frameStr_2.ToString());
                    if (!is_cleanning_2)
                    {
                        this.Invoke((EventHandler)(delegate
                        {
                            InsertListView(this.lv_can_data_2, li);
                        }));
                    }
                    if (candata_serial_2 % 100 == 0)
                    {
                        GC.Collect();
                    }
                }

                //【3】激发事件
                if (SendCanMsg != null)
                {
                    SendCanMsg(2, can_data.id, can_data.data);
                }
            }
        }

        private string ConvertToHex(UInt32 x)
        {
            string hex = Convert.ToString(x, 16).PadLeft(2, '0').ToUpper();
            return hex + " ";
        }

        private void btn_on_off_Click(object sender, EventArgs e)
        {
            if (mIsCanOpen)
            {
                if (CanUtils.VCI_CloseDevice(m_devtype, m_devind) != 1)
                {
                    MessageBox.Show("关闭设备失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                updateViews(0);
                MessageBox.Show("关闭设备成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                m_devtype = (UInt32)(cb_devtype.SelectedIndex + 3);
                m_devind = (UInt32)cb_devidx.SelectedIndex;

                if (CanUtils.VCI_OpenDevice(m_devtype, m_devind, 0) != 1)
                {
                    MessageBox.Show("打开设备失败,请检查设备类型和设备索引号是否正确！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                CanUtils.VCI_INIT_CONFIG config_1 = new CanUtils.VCI_INIT_CONFIG();
                config_1.AccCode = Convert.ToUInt32("0x00000000", 16);
                config_1.AccMask = Convert.ToUInt32("0xFFFFFFFF", 16);
                String[] strs_1 = cb_rate_1.SelectedValue.ToString().Split(',');
                config_1.Timing0 = Convert.ToByte(strs_1[0], 16);
                config_1.Timing1 = Convert.ToByte(strs_1[1], 16);
                config_1.Filter = m_filter;
                config_1.Mode = m_mode;
                if (CanUtils.VCI_InitCAN(m_devtype, m_devind, m_canind_1, ref config_1) != 1)
                {
                    MessageBox.Show("初始化CAN1失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (CanUtils.VCI_StartCAN(m_devtype, m_devind, m_canind_1) != 1)
                {
                    MessageBox.Show("启动CAN1失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                CanUtils.VCI_INIT_CONFIG config_2 = new CanUtils.VCI_INIT_CONFIG();
                config_2.AccCode = Convert.ToUInt32("0x00000000", 16);
                config_2.AccMask = Convert.ToUInt32("0xFFFFFFFF", 16);
                String[] strs_2 = cb_rate_2.SelectedValue.ToString().Split(',');
                config_2.Timing0 = Convert.ToByte(strs_2[0], 16);
                config_2.Timing1 = Convert.ToByte(strs_2[1], 16);
                config_2.Filter = m_filter;
                config_2.Mode = m_mode;
                if (CanUtils.VCI_InitCAN(m_devtype, m_devind, m_canind_2, ref config_2) != 1)
                {
                    MessageBox.Show("初始化CAN2失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (CanUtils.VCI_StartCAN(m_devtype, m_devind, m_canind_2) != 1)
                {
                    MessageBox.Show("启动CAN2失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.devtype, cb_devtype.SelectedIndex.ToString());
                ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.devidx, cb_devidx.SelectedIndex.ToString());
                ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.rate_1, cb_rate_1.SelectedIndex.ToString());
                ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.rate_2, cb_rate_2.SelectedIndex.ToString());

                updateViews(1);

                UInt32 ret = CanUtils.VCI_ClearBuffer(m_devtype, m_devind, m_canind_1);
                if (ret != 1)
                {
                    Console.WriteLine("清除CAN1缓冲区失败！");
                }

                ret = CanUtils.VCI_ClearBuffer(m_devtype, m_devind, m_canind_2);
                if (ret != 1)
                {
                    Console.WriteLine("清除CAN2缓冲区失败！");
                }
                MessageBox.Show("开启设备成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /**
         * tag: 0 CAN未开启  1 CAN已开启
         */
        private void updateViews(int tag)
        {
            switch (tag)
            {
                case 0:
                    mIsCanOpen = false;
                    btn_on_off.Text = "开启";
                    cb_devtype.Enabled = true;
                    cb_devidx.Enabled = true;
                    cb_rate_1.Enabled = true;
                    cb_rate_2.Enabled = true;
                    break;
                case 1:
                    mIsCanOpen = true;
                    btn_on_off.Text = "关闭";
                    cb_devtype.Enabled = false;
                    cb_devidx.Enabled = false;
                    cb_rate_1.Enabled = false;
                    cb_rate_2.Enabled = false;
                    break;
                default:
                    break;
            }
        }


        StringBuilder sendFrameStr_1 = new StringBuilder();
        unsafe private void SendDataThread_1()
        {
            while (true)
            {
                if (sendQueue_1.Count <= 0)
                {
                    Thread.Sleep(1);
                    continue;
                }
                try
                {
                    SendObj send_obj = sendQueue_1.Dequeue();

                    sendobj_1 = new CanUtils.VCI_CAN_OBJ();
                    sendobj_1.RemoteFlag = (byte)frameFormat;
                    sendobj_1.ExternFlag = (byte)frameType;

                    //bytes = new String[] { "0cfe6cee", "00", "01", "02", "03", "04", "05", "06", "07" };
                    //bytes = new String[] { "12345678", "00", "01", "02", "03", "04", "05", "06", "07" };
                    sendobj_1.ID = send_obj.getId();//设置帧ID
                    sendobj_1.DataLen = 8;
                    fixed (CanUtils.VCI_CAN_OBJ* temp = &sendobj_1)
                    {
                        for (int j = 0; j < 8; j++)
                            temp->Data[j] = send_obj.getData()[j];//设置帧具体内容
                    }
                    //long st = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
                    if (CanUtils.VCI_Transmit(m_devtype, m_devind, m_canind_1, ref sendobj_1, 1) != 1)
                    {
                        //updateViews(1);
                        //MessageBox.Show("发送失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        continue;
                    }
                    //long et = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
                    //Console.WriteLine("dift = " + (et-st));

                    total_send_1++;
                    lb_count_1.Text = "收" + total_recv_1 + "/发" + total_send_1;

                    if (is_visible_send_1 && !is_cleanning_1)
                    {
                        String frameID = "0x" + Convert.ToString(send_obj.getId(), 16).ToUpper();
                        sendFrameStr_1.Remove(0, sendFrameStr_1.Length);
                        for (byte j = 0; j < 8; j++)
                        {
                            sendFrameStr_1.Append(ConvertToHex(send_obj.getData()[j]));
                        }

                        candata_serial_1++;
                        //创建行对象
                        li_1 = new ListViewItem(candata_serial_1 + "");
                        //添加同一行的数据
                        li_1.SubItems.Add("发送");
                        li_1.SubItems.Add(DateTime.Now.ToString("HH:mm:ss:fff"));
                        li_1.SubItems.Add(frameID);
                        li_1.SubItems.Add(8 + "");
                        li_1.SubItems.Add(sendFrameStr_1.ToString().Trim());
                        this.Invoke((EventHandler)(delegate
                        {
                            InsertListView(this.lv_can_data_1, li_1);
                        }));
                        if (candata_serial_1 % 100 == 0)
                        {
                            GC.Collect();
                        }
                    }

                    //Console.WriteLine(send_period + "  " + DateTime.Now.ToString("HH:mm:ss:fff"));
                    Thread.Sleep(1);

                }
                catch (Exception)
                {
                    //MessageBox.Show("发送异常", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("发送异常");
                    continue;
                }
            }
        }

        StringBuilder sendFrameStr_2 = new StringBuilder();
        unsafe private void SendDataThread_2()
        {
            while (true)
            {
                if (sendQueue_2.Count <= 0)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                try
                {
                    SendObj send_obj = sendQueue_2.Dequeue();
                    sendobj_2 = new CanUtils.VCI_CAN_OBJ();
                    sendobj_2.RemoteFlag = (byte)frameFormat;
                    sendobj_2.ExternFlag = (byte)frameType;

                    //bytes = new String[] { "0cfe6cee", "00", "01", "02", "03", "04", "05", "06", "07" };
                    //bytes = new String[] { "12345678", "00", "01", "02", "03", "04", "05", "06", "07" };
                    sendobj_2.ID = send_obj.getId();//设置帧ID
                    sendobj_2.DataLen = 8;
                    fixed (CanUtils.VCI_CAN_OBJ* temp = &sendobj_2)
                    {
                        for (int j = 0; j < 8; j++)
                            temp->Data[j] = send_obj.getData()[j];//设置帧具体内容
                    }
                    //long st = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
                    if (CanUtils.VCI_Transmit(m_devtype, m_devind, m_canind_2, ref sendobj_2, 1) != 1)
                    {
                        //updateViews(1);
                        //MessageBox.Show("发送失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        continue;
                    }
                    //long et = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
                    //Console.WriteLine("dift = " + (et - st));

                    total_send_2++;
                    lb_count_2.Text = "收" + total_recv_2 + "/发" + total_send_2;

                    if (is_visible_send_2 && !is_cleanning_2)
                    {
                        String frameID = "0x" + Convert.ToString(send_obj.getId(), 16).ToUpper();
                        sendFrameStr_2.Remove(0, sendFrameStr_2.Length);
                        for (byte j = 0; j < 8; j++)
                        {
                            sendFrameStr_2.Append(ConvertToHex(send_obj.getData()[j]));
                        }

                        candata_serial_2++;
                        //创建行对象
                        li_2 = new ListViewItem(candata_serial_2 + "");
                        //添加同一行的数据
                        li_2.SubItems.Add("发送");
                        li_2.SubItems.Add(DateTime.Now.ToString("HH:mm:ss:fff"));
                        li_2.SubItems.Add(frameID);
                        li_2.SubItems.Add(8 + "");
                        li_2.SubItems.Add(sendFrameStr_2.ToString().Trim());
                        this.Invoke((EventHandler)(delegate
                        {
                            InsertListView(this.lv_can_data_2, li_2);
                        }));
                        if (candata_serial_2 % 100 == 0)
                        {
                            GC.Collect();
                        }
                    }

                    //Console.WriteLine(send_period + "  " + DateTime.Now.ToString("HH:mm:ss:fff"));
                    Thread.Sleep(1);

                }
                catch (Exception)
                {
                    //MessageBox.Show("发送异常", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("发送异常");
                    continue;
                }
            }
        }

        private void ReadErrInfoThread()
        {
            while (true)
            {
                if (mIsCanOpen)
                {
                    UInt32 ret = CanUtils.VCI_ReadErrInfo(m_devtype, m_devind, m_canind_1, ref m_errInfo);
                    if (ret == 1 && m_errInfo.ErrCode > 0)
                    {
                        total_err_1++;
                        lb_err_count_1.Text = total_err_1.ToString();

                        byte[] tempData = new byte[4];
                        tempData[0] = m_errInfo.Passive_ErrData[0];
                        tempData[1] = m_errInfo.Passive_ErrData[1];
                        tempData[2] = m_errInfo.Passive_ErrData[2];
                        tempData[3] = m_errInfo.ArLost_ErrData;
                        DateTime ct = DateTime.Now;
                        saveCanDataToFile_1("错误", ct, m_errInfo.ErrCode, tempData);

                        StringBuilder sb = new StringBuilder();
                        if (is_visible_err_1 && !is_cleanning_1)
                        {
                            String frameID = "0x" + Convert.ToString(m_errInfo.ErrCode, 16).ToUpper();
                            for (int i = 0; i < tempData.Length; i++)
                            {
                                sb.Append(ConvertToHex(tempData[i]));
                            }

                            candata_serial_1++;
                            //创建行对象
                            li_1 = new ListViewItem(candata_serial_1 + "");
                            //添加同一行的数据
                            li_1.SubItems.Add("错误");
                            li_1.SubItems.Add(ct.ToString("HH:mm:ss:fff"));
                            li_1.SubItems.Add(frameID);
                            li_1.SubItems.Add(4 + "");
                            li_1.SubItems.Add(sb.ToString().Trim());
                            this.Invoke((EventHandler)(delegate
                            {
                                InsertListView(this.lv_can_data_1, li_1);
                            }));
                            if (candata_serial_1 % 100 == 0)
                            {
                                GC.Collect();
                            }
                        }
                        //Console.WriteLine("m_canind_1 = " + m_canind_1 + "  ErrCode = " + m_errInfo.ErrCode + "  Passive_ErrData = " + sb.ToString() + "  ArLost_ErrData = " + m_errInfo.ArLost_ErrData);
                    }
                    Thread.Sleep(100); 
                    
                    ret = CanUtils.VCI_ReadErrInfo(m_devtype, m_devind, m_canind_2, ref m_errInfo);
                    if (ret == 1 && m_errInfo.ErrCode > 0)
                    {
                        total_err_2++;
                        lb_err_count_2.Text = total_err_2.ToString();

                        byte[] tempData = new byte[4];
                        tempData[0] = m_errInfo.Passive_ErrData[0];
                        tempData[1] = m_errInfo.Passive_ErrData[1];
                        tempData[2] = m_errInfo.Passive_ErrData[2];
                        tempData[3] = m_errInfo.ArLost_ErrData;
                        DateTime ct = DateTime.Now;
                        saveCanDataToFile_2("错误", ct, m_errInfo.ErrCode, tempData);

                        if (is_visible_err_2 && !is_cleanning_2)
                        {
                            String frameID = "0x" + Convert.ToString(m_errInfo.ErrCode, 16).ToUpper();
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < tempData.Length; i++)
                            {
                                sb.Append(ConvertToHex(tempData[i]));
                            }

                            candata_serial_2++;
                            //创建行对象
                            li_2 = new ListViewItem(candata_serial_2 + "");
                            //添加同一行的数据
                            li_2.SubItems.Add("错误");
                            li_2.SubItems.Add(ct.ToString("HH:mm:ss:fff"));
                            li_2.SubItems.Add(frameID);
                            li_2.SubItems.Add(4 + "");
                            li_2.SubItems.Add(sb.ToString().Trim());
                            this.Invoke((EventHandler)(delegate
                            {
                                InsertListView(this.lv_can_data_2, li_2);
                            }));
                            if (candata_serial_2 % 100 == 0)
                            {
                                GC.Collect();
                            }
                        }
                        //Console.WriteLine("m_canind_2 = " + m_canind_2 + "  ErrCode = " + m_errInfo.ErrCode + "  Passive_ErrData = " + sb.ToString() + "  ArLost_ErrData = " + m_errInfo.ArLost_ErrData);
                    }
                    Thread.Sleep(100);
                }

            }
        }

        /// <summary>
        /// 初始化ListView的方法
        /// </summary>
        /// <param name="lv"></param>
        public void InitListView_1(ListView lv)
        {
            candata_serial_1 = 0;
            //设置属性
            lv.GridLines = true;  //显示网格线
            lv.FullRowSelect = true;  //显示全行
            lv.MultiSelect = false;  //设置只能单选
            lv.View = View.Details;  //设置显示模式为详细
            lv.HoverSelection = true;  //当鼠标停留数秒后自动选择
            //把列名添加到listview中
            lv.Columns.Add("序号", 40);
            lv.Columns.Add("方向", 40);
            lv.Columns.Add("时间", 100);
            lv.Columns.Add("帧ID", 80);
            lv.Columns.Add("长度", 40);
            lv.Columns.Add("数据(Hex)", 250);
        }

        /// <summary>
        /// 初始化ListView的方法
        /// </summary>
        /// <param name="lv"></param>
        public void InitListView_2(ListView lv)
        {
            candata_serial_2 = 0;
            //设置属性
            lv.GridLines = true;  //显示网格线
            lv.FullRowSelect = true;  //显示全行
            lv.MultiSelect = false;  //设置只能单选
            lv.View = View.Details;  //设置显示模式为详细
            lv.HoverSelection = true;  //当鼠标停留数秒后自动选择
            //把列名添加到listview中
            lv.Columns.Add("序号", 40);
            lv.Columns.Add("方向", 40);
            lv.Columns.Add("时间", 100);
            lv.Columns.Add("帧ID", 80);
            lv.Columns.Add("长度", 40);
            lv.Columns.Add("数据(Hex)", 250);
        }

        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        public ListView InsertListView(ListView lv, ListViewItem li)
        {
            //将行对象绑定在listview对象中
            lv.Items.Add(li);
            if (lv.Items.Count - 1 >= 0)
            {
                lv.EnsureVisible(lv.Items.Count - 1);
            }
            //lv.Items[lv.Items.Count - 1].Checked = true;


            //MessageBox.Show("新增数据成功！");
            return lv;
        }

        private void btn_clean_1_Click(object sender, EventArgs e)
        {
            is_cleanning_1 = true;
            lv_can_data_1.Clear();
            InitListView_1(lv_can_data_1);
            total_recv_1 = 0;
            total_send_1 = 0;
            lb_count_1.Text = "收" + total_recv_1 + "/发" + total_send_1;
            total_err_1 = 0;
            lb_err_count_1.Text = total_err_1.ToString();
            is_cleanning_1 = false;
        }

        private void btn_clean_2_Click(object sender, EventArgs e)
        {
            is_cleanning_2 = true;
            lv_can_data_2.Clear();
            InitListView_2(lv_can_data_2);
            total_recv_2 = 0;
            total_send_2 = 0;
            lb_count_2.Text = "收" + total_recv_2 + "/发" + total_send_2;
            total_err_2 = 0;
            lb_err_count_2.Text = total_err_2.ToString();
            is_cleanning_2 = false;
        }

        private void cb_visible_send_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.visible_send_1, cb_visible_send_1.SelectedIndex.ToString());
            is_visible_send_1 = cb_visible_send_1.SelectedIndex == 0;
        }

        private void cb_visible_recv_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.visible_recv_1, cb_visible_recv_1.SelectedIndex.ToString());
            is_visible_recv_1 = cb_visible_recv_1.SelectedIndex == 0;
        }

        private void cb_visible_send_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.visible_send_2, cb_visible_send_2.SelectedIndex.ToString());
            is_visible_send_2 = cb_visible_send_2.SelectedIndex == 0;
        }

        private void cb_visible_recv_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.visible_recv_2, cb_visible_recv_2.SelectedIndex.ToString());
            is_visible_recv_2 = cb_visible_recv_2.SelectedIndex == 0;
        }

        private void cb_send_format_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.send_format_1, cb_send_format_1.SelectedIndex.ToString());
        }

        private void cb_frame_type_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.frame_type_1, cb_frame_type_1.SelectedIndex.ToString());
        }

        private void cb_frame_format_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.frame_format_1, cb_frame_format_1.SelectedIndex.ToString());
        }

        private void cb_send_format_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.send_format_2, cb_send_format_2.SelectedIndex.ToString());
        }

        private void cb_frame_type_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.frame_type_2, cb_frame_type_2.SelectedIndex.ToString());
        }

        private void cb_frame_format_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.frame_format_2, cb_frame_format_2.SelectedIndex.ToString());
        }

        private void cb_visible_err_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.visible_err_1, cb_visible_err_1.SelectedIndex.ToString());
            is_visible_err_1 = cb_visible_err_1.SelectedIndex == 0;
        }

        private void cb_visible_err_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.visible_err_2, cb_visible_err_2.SelectedIndex.ToString());
            is_visible_err_2 = cb_visible_err_2.SelectedIndex == 0;
        }

        private void btn_send_1_Click(object sender, EventArgs e)
        {
            if (!mIsCanOpen)
            {
                MessageBox.Show("请先开启CAN工具！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            UInt32 id;
            try
            {
                id = Convert.ToUInt32(tb_send_id_1.Text, 16);
                if (id > 0x7effffff)
                {
                    MessageBox.Show("ID值超过0x7EFFFFFF异常！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("ID格式未知错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            byte[] data = new byte[8];
            try
            {
                string data_str = tb_send_data_1.Text;
                if (data_str == null || data_str.Trim().Equals(""))
                {
                    MessageBox.Show("DATA数据不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    data_str = data_str.Trim();
                    while (data_str.Contains("  "))
                    {
                        data_str = data_str.Replace("  ", " ");
                    }
                    char[] s = new char[] { ' ' };
                    string[] arrs = data_str.Split(s);
                    if (arrs == null || arrs.Length != 8)
                    {
                        MessageBox.Show("DATA数据长度必须8字节！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    for (int i = 0; i < arrs.Length; i++)
                    {
                        data[i] = Convert.ToByte(arrs[i], 16);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("DATA格式未知错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            sendCanData_1(new SendObj(id, data));
            // 保存数据
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.send_id_1, tb_send_id_1.Text);
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.send_data_1, tb_send_data_1.Text);
        }

        public void sendCanData_1(SendObj obj)
        {
            if (obj == null || obj.getData() == null)
            {
                return;
            }
            lock(sendQueue_1){
                sendQueue_1.Enqueue(obj);
            }
            saveCanDataToFile_1("发送", DateTime.Now, obj.getId(), obj.getData());
        }

        private void btn_send_2_Click(object sender, EventArgs e)
        {
            if (!mIsCanOpen)
            {
                MessageBox.Show("请先开启CAN工具！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            UInt32 id;
            try
            {
                id = Convert.ToUInt32(tb_send_id_2.Text, 16);
                if (id > 0x7effffff)
                {
                    MessageBox.Show("ID值超过0x7EFFFFFF异常！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("ID格式未知错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            byte[] data = new byte[8];
            try
            {
                string data_str = tb_send_data_2.Text;
                if (data_str == null || data_str.Trim().Equals(""))
                {
                    MessageBox.Show("DATA数据不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    data_str = data_str.Trim();
                    while (data_str.Contains("  "))
                    {
                        data_str = data_str.Replace("  ", " ");
                    }
                    char[] s = new char[] { ' ' };
                    string[] arrs = data_str.Split(s);
                    if (arrs == null || arrs.Length != 8)
                    {
                        MessageBox.Show("DATA数据长度必须8字节！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    for (int i = 0; i < arrs.Length; i++)
                    {
                        data[i] = Convert.ToByte(arrs[i], 16);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("DATA格式未知错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            sendCanData_2(new SendObj(id, data));
            // 保存数据
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.send_id_2, tb_send_id_2.Text);
            ConfigUtils.setConfigString(ConfigUtils.Sections.CAN_PARAM, ConfigUtils.Keys.send_data_2, tb_send_data_2.Text);
        }

        public void sendCanData_2(SendObj obj)
        {
            if (obj == null || obj.getData() == null)
            {
                return;
            }
            lock (sendQueue_2)
            {
                sendQueue_2.Enqueue(obj);
            }
            saveCanDataToFile_2("发送", DateTime.Now, obj.getId(), obj.getData());
        }

        private void btn_obd_all_1_Click(object sender, EventArgs e)
        {
            if (mIsCanOpen)
            {
                Form_OBD_All obd_all = new Form_OBD_All(this, 1);
                obd_all.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先打开CAN设备！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_obd_adapter_1_Click(object sender, EventArgs e)
        {
            if (mIsCanOpen)
            {
                Form_OBD_Diagnosis obd_diagnosis = new Form_OBD_Diagnosis(this, 1);
                obd_diagnosis.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先打开CAN设备！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_j1939_adapter_1_Click(object sender, EventArgs e)
        {
            if (mIsCanOpen)
            {
                Form_J1939 j1939 = new Form_J1939(this, 1);
                j1939.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先打开CAN设备！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_obd_all_2_Click(object sender, EventArgs e)
        {
            if (mIsCanOpen)
            {
                Form_OBD_All obd_all = new Form_OBD_All(this, 2);
                obd_all.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先打开CAN设备！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_obd_adapter_2_Click(object sender, EventArgs e)
        {
            if (mIsCanOpen)
            {
                Form_OBD_Diagnosis obd_diagnosis = new Form_OBD_Diagnosis(this, 2);
                obd_diagnosis.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先打开CAN设备！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_j1939_adapter_2_Click(object sender, EventArgs e)
        {
            if (mIsCanOpen)
            {
                Form_J1939 j1939 = new Form_J1939(this, 2);
                j1939.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先打开CAN设备！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void saveCanDataToFile_1(string dir, DateTime time, UInt32 id, byte[] data)
        {
            saveQueue_1.Enqueue(new SaveObj(dir, time, id, data));
        }

        private void saveCanDataToFile_2(string dir, DateTime time, UInt32 id, byte[] data)
        {
                saveQueue_2.Enqueue(new SaveObj(dir, time, id, data));
        }

        private void SaveCanDataThread_1()
        {
            while (true)
            {
                if (saveQueue_1.IsEmpty)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                try
                {
                    if (saveFileStream_1 == null)
                    {
                        string rootPath = Environment.CurrentDirectory.ToString();
                        //判断文件路径是否存在，不存在则创建文件夹
                        if (!System.IO.Directory.Exists(rootPath+"\\CanData"))
                        {
                            System.IO.Directory.CreateDirectory(rootPath + "\\CanData");//不存在就创建目录
                        }
                        saveFileStream_1 = new FileStream(rootPath + "\\CanData\\" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff") + "_CANDATA_1.CSV", FileMode.Create, FileAccess.Write);//创建写入文件
                        appendToFile(saveFileStream_1, "序号,传输方向,时间标识,名称,帧ID(靠右对齐),帧格式,帧类型,数据长度,数据(HEX)\r\n");
                    }
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < saveQueue_1.Count; i++)
                    {
                        SaveObj obj;
                        saveQueue_1.TryDequeue(out obj);
                        // 序号	传输方向	时间标识	名称	帧ID(靠右对齐)	帧格式	帧类型	数据长度	数据(HEX)
                        // 0	  发送	  15:07:39:123		      0x18db33f1	数据帧	扩展帧	   8	    02 06 01 00 00 00 00 00 
                        saveSerial_1++;
                        sb.Append(saveSerial_1);
                        sb.Append(",");
                        sb.Append(obj.getTransfer_dir());
                        sb.Append(",");
                        sb.Append(obj.getTime().ToString("HH:mm:ss:fff"));
                        sb.Append(",");
                        sb.Append("");
                        sb.Append(",");
                        sb.Append("0x" + Convert.ToString(obj.getId(), 16).ToUpper());
                        sb.Append(",");
                        sb.Append("数据帧");
                        sb.Append(",");
                        sb.Append("扩展帧");
                        sb.Append(",");
                        sb.Append(obj.getData().Length);
                        sb.Append(",");
                        foreach (var item in obj.getData())
                        {
                            sb.Append(ConvertToHex(item));
                        }
                        sb.Append("\r\n");
                    }
                    //Console.WriteLine(sb.ToString());
                    appendToFile(saveFileStream_1, sb.ToString());
                        
                }
                catch (Exception)
                {
                }
                Thread.Sleep(1000);
            }
        }

        private void SaveCanDataThread_2()
        {
            while (true)
            {
                if (saveQueue_2.IsEmpty)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                try
                {
                    if (saveFileStream_2 == null)
                    {
                        //判断文件路径是否存在，不存在则创建文件夹
                        if (!System.IO.Directory.Exists(rootPath + "\\CanData"))
                        {
                            System.IO.Directory.CreateDirectory(rootPath + "\\CanData");//不存在就创建目录
                        }
                        saveFileStream_2 = new FileStream(rootPath + "\\CanData\\" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff") + "_CANDATA_2.CSV", FileMode.Create, FileAccess.Write);//创建写入文件
                        appendToFile(saveFileStream_2, "序号,传输方向,时间标识,名称,帧ID(靠右对齐),帧格式,帧类型,数据长度,数据(HEX)\r\n");
                    }
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < saveQueue_2.Count; i++)
                    {
                        SaveObj obj;
                        saveQueue_2.TryDequeue(out obj);
                        // 序号	传输方向	时间标识	名称	帧ID(靠右对齐)	帧格式	帧类型	数据长度	数据(HEX)
                        // 0	  发送	  15:07:39:123		      0x18db33f1	数据帧	扩展帧	   8	    02 06 01 00 00 00 00 00 
                        saveSerial_2++;
                        sb.Append(saveSerial_2);
                        sb.Append(",");
                        sb.Append(obj.getTransfer_dir());
                        sb.Append(",");
                        sb.Append(obj.getTime().ToString("HH:mm:ss:fff"));
                        sb.Append(",");
                        sb.Append("");
                        sb.Append(",");
                        sb.Append("0x" + Convert.ToString(obj.getId(), 16).ToUpper());
                        sb.Append(",");
                        sb.Append("数据帧");
                        sb.Append(",");
                        sb.Append("扩展帧");
                        sb.Append(",");
                        sb.Append(obj.getData().Length);
                        sb.Append(",");
                        foreach (var item in obj.getData())
                        {
                            sb.Append(ConvertToHex(item));
                        }
                        sb.Append("\r\n");
                    }
                    //Console.WriteLine(sb.ToString());
                    appendToFile(saveFileStream_2, sb.ToString());
                }
                catch (Exception)
                {
                }
                Thread.Sleep(1000);
            }
        }

        private void appendToFile(FileStream fs, string txt)
        {
            byte[] buf = Encoding.Default.GetBytes(txt);
            fs.Write(buf, 0, buf.Length);
            fs.Flush();
        }

        private void btn_open_app_dir_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", rootPath);
        }
    }
}
