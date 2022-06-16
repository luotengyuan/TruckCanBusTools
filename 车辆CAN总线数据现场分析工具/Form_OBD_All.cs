using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 车辆CAN总线数据现场分析工具.Objs;
using 车辆CAN总线数据现场分析工具.Utils;
using System.Threading;

namespace 车辆CAN总线数据现场分析工具
{
    public partial class Form_OBD_All : Form
    {
        Form_Main mainForm;
        int channel;

        Queue<can_data_t> recvQueue = new Queue<can_data_t>();
        private object recvQueueLock = new object();
        Thread hanleRecvDataThread = null;

        bool mIsQueryStart = false;

        private int candata_serial;

        private UInt32 slt_id = 0;
        private List<UInt32> idList = new List<UInt32>();

        StringBuilder frameStr = new StringBuilder();
        StringBuilder sendFrameStr = new StringBuilder();
        private bool is_cleanning;

        private uint send_id = 0x18db33f1;

        bool is_visible_send = false;
        bool is_visible_recv = false;

        byte pid = 0x00;
        byte sid = 0x01;

        _SendCanMsg sendMsgHandle;

        public Form_OBD_All(Form_Main mainForm, int channel)
        {
            this.mainForm = mainForm;
            this.channel = channel;
            InitializeComponent();
            ListView.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form_OBD_All_Load(object sender, EventArgs e)
        {
            sendMsgHandle = new _SendCanMsg(RecvCanDataCallback);
            mainForm.SendCanMsg += sendMsgHandle;
            cb_id_slt.SelectedIndex = 0;

            cb_visible_recv.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.OBD_ALL, ConfigUtils.Keys.visible_recv_obd_all, "0", -1));
            is_visible_recv = cb_visible_recv.SelectedIndex == 0;
            cb_visible_send.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.OBD_ALL, ConfigUtils.Keys.visible_send_obd_all, "0", -1));
            is_visible_send = cb_visible_send.SelectedIndex == 0;

            pb_query_progress.Maximum = 255 * 2;
            pb_query_progress.Value = 0;
            InitListView(lv_obd_all);
            timer_query.Tick += new EventHandler(Query_Tick);
            timer_query.Interval = 500;

            hanleRecvDataThread = new Thread(HanleRecvDataThread);
            hanleRecvDataThread.Start();
        }

        private void Form_OBD_All_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.SendCanMsg -= sendMsgHandle;
            if (mIsQueryStart)
            {
                timer_query.Stop();
            }

        }

        //【4】事件响应方法
        public void RecvCanDataCallback(int devInx, uint id, byte[] data)
        {
            if ((id & 0x00ffff00) != 0x00daf100 || data == null || channel != devInx)
            {
                return;
            }

            can_data_t can_data_obj = new can_data_t();
            can_data_obj.id = id;
            can_data_obj.data = data;
            lock (recvQueueLock)
            {
                recvQueue.Enqueue(can_data_obj);
            }

        }

        private void HanleRecvDataThread()
        {
            while (true)
            {
                if (recvQueue.Count <= 0)
                {
                    Thread.Sleep(10);
                    continue;
                }
                can_data_t can_data_obj;
                lock (recvQueueLock)
                {
                    can_data_obj = recvQueue.Dequeue();
                }
                uint id = can_data_obj.id;
                byte[] data = can_data_obj.data;

                if (!idList.Contains(id))
                {
                    idList.Add(id);
                    string id_str = id.ToString("X2");
                    if (id_str.Length % 2 == 1)
                    {
                        id_str = "0x0" + id_str;
                    }
                    else
                    {
                        id_str = "0x" + id_str;
                    }
                    cb_id_slt.Items.Add(id_str);
                }
                if (slt_id == 0 || (slt_id != 0 && id == slt_id))
                {
                    if (is_visible_recv)
                    {
                        String frameID = "0x" + Convert.ToString(id, 16).ToUpper();
                        //Console.WriteLine("frameID = " + frameID);

                        frameStr.Remove(0, frameStr.Length);
                        //frameStr.Append(frameID + ":");
                        int len = data.Length;
                        for (byte j = 0; j < len; j++)
                        {
                            frameStr.Append(ConvertToHex(data[j]));
                        }
                        candata_serial++;
                        //创建行对象
                        ListViewItem li = new ListViewItem(candata_serial + "");
                        //添加同一行的数据
                        li.SubItems.Add("接收");
                        li.SubItems.Add(DateTime.Now.ToString("HH:mm:ss:fff"));
                        li.SubItems.Add(frameID);
                        li.SubItems.Add(len + "");
                        li.SubItems.Add(frameStr.ToString());
                        if (!is_cleanning)
                        {
                            this.Invoke((EventHandler)(delegate
                            {
                                InsertListView(lv_obd_all, li);
                            }));
                        }
                        if (candata_serial % 100 == 0)
                        {
                            GC.Collect();
                        }
                    }
                    if (data[1] == 0x43)
                    {
                        if (data[2] > 2)
                        {
                            data[2] = 2;
                        }
                        this.Invoke((EventHandler)(delegate
                        {
                            tb_cur_dtc.Text = "";
                            for (int i = 0; i < data[2]; i++)
                            {
                                tb_cur_dtc.Text += parseDTC(data[3 + i * 2], data[4 + i * 2]);
                            }
                        }));
                    }
                    if (data[1] == 0x47)
                    {
                        if (data[2] > 2)
                        {
                            data[2] = 2;
                        }
                        this.Invoke((EventHandler)(delegate
                        {
                            tb_his_dtc.Text = "";
                            for (int i = 0; i < data[2]; i++)
                            {
                                tb_his_dtc.Text += parseDTC(data[3 + i * 2], data[4 + i * 2]);
                            }
                        }));
                    }
                    if (data[1] == 0x44)
                    {
                        if (data[2] == 0x00)
                        {
                            this.Invoke((EventHandler)(delegate
                            {
                                tb_clean_his_dtc.Text = "执行成功";
                                tb_clean_his_dtc.ForeColor = System.Drawing.Color.Green;
                            }));
                        }
                        else
                        {
                            this.Invoke((EventHandler)(delegate
                            {
                                tb_clean_his_dtc.Text = "执行失败";
                                tb_clean_his_dtc.ForeColor = System.Drawing.Color.Red;
                            }));
                        }
                    }
                }
            }
        }

        private string parseDTC(byte higthByte, byte LowByte)
        {
            StringBuilder sb = new StringBuilder();
            int c1 = higthByte >> 6 & 0x03;
            switch (c1)
            {
                case 0x00:
                    // 动力系统（P）
                    sb.Append("P");
                    break;
                case 0x01:
                    // 底盘（C）
                    sb.Append("C");
                    break;
                case 0x02:
                    // 车身（B）
                    sb.Append("B");
                    break;
                case 0x03:
                    // 网络系统（U）
                    sb.Append("U");
                    break;
                default:
                    return "";
            }
            int c2 = higthByte >> 4 & 0x03;
            sb.Append(c2.ToString("X1"));
            int c3 = higthByte & 0x0F;
            sb.Append(c3.ToString("X1"));
            int c4 = LowByte >> 4 & 0x0F;
            sb.Append(c4.ToString("X1"));
            int c5 = LowByte & 0x0F;
            sb.Append(c5.ToString("X1"));
            sb.Append("\r\n");
            return sb.ToString();
        }

        private string ConvertToHex(UInt32 x)
        {
            string hex = Convert.ToString(x, 16).PadLeft(2, '0').ToUpper();
            return hex + " ";
        }

        private void btn_start_query_Click(object sender, EventArgs e)
        {
            if (mIsQueryStart)
            {
                // 停止查询
                timer_query.Stop();
                mIsQueryStart = false;
                btn_start_query.Text = "开始查询";
            }
            else
            {
                // 开始查询
                timer_query.Start();
                mIsQueryStart = true;
                btn_start_query.Text = "停止查询";
            }
        }

        private void Query_Tick(object sender, EventArgs e)
        {
            byte len;
            if (sid == 0x01)
            {
                len = 0x02;
            }
            else if (sid == 0x02)
            {
                len = 0x03;
            }
            else
            {
                len = 0x00;
            }
            byte[] send_data = new byte[] { len, sid, pid++, 0x00, 0x00, 0x00, 0x00, 0x00 };
            sendCanData(new SendObj(send_id, send_data));
            pb_query_progress.Value = pid + (255 * (sid - 1));
            if (pid == 0xff)
            {
                if (sid == 0x01)
                {
                    sid = 0x02;
                }
                else
                {
                    sid = 0x01;
                }
            }
        }

        public void sendCanData(SendObj send_obj)
        {
            if (channel == 1)
            {
                mainForm.sendCanData_1(send_obj);
            }
            else
            {
                mainForm.sendCanData_2(send_obj);
            }
            if (is_visible_send)
            {
                String frameID = "0x" + Convert.ToString(send_obj.getId(), 16).ToUpper();
                sendFrameStr.Remove(0, sendFrameStr.Length);
                for (byte j = 0; j < 8; j++)
                {
                    sendFrameStr.Append(ConvertToHex(send_obj.getData()[j]));
                }

                candata_serial++;
                //创建行对象
                ListViewItem li = new ListViewItem(candata_serial + "");
                //添加同一行的数据
                li.SubItems.Add("发送");
                li.SubItems.Add(DateTime.Now.ToString("HH:mm:ss:fff"));
                li.SubItems.Add(frameID);
                li.SubItems.Add(8 + "");
                li.SubItems.Add(sendFrameStr.ToString().Trim());
                this.Invoke((EventHandler)(delegate
                {
                    InsertListView(lv_obd_all, li);
                }));
                if (candata_serial % 100 == 0)
                {
                    GC.Collect();
                }
            }
        }

        /// <summary>
        /// 初始化ListView的方法
        /// </summary>
        /// <param name="lv"></param>
        public void InitListView(ListView lv)
        {
            candata_serial = 0;
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
            return lv;
        }

        private void cb_id_slt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_id_slt.SelectedIndex == 0)
            {
                slt_id = 0;
            }
            else
            {
                slt_id = idList.ToArray()[cb_id_slt.SelectedIndex - 1];
            }
        }

        private void cb_visible_send_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.OBD_ALL, ConfigUtils.Keys.visible_send_obd_all, cb_visible_send.SelectedIndex.ToString());
            is_visible_send = cb_visible_send.SelectedIndex == 0;
        }

        private void cb_visible_recv_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.OBD_ALL, ConfigUtils.Keys.visible_recv_obd_all, cb_visible_recv.SelectedIndex.ToString());
            is_visible_recv = cb_visible_recv.SelectedIndex == 0;
        }

        private void btn_query_cur_dtc_Click(object sender, EventArgs e)
        {
            tb_cur_dtc.Text = "";
            byte[] send_data = new byte[] { 0x01, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            sendCanData(new SendObj(send_id, send_data));
        }

        private void btn_query_his_dtc_Click(object sender, EventArgs e)
        {
            tb_his_dtc.Text = "";
            byte[] send_data = new byte[] { 0x01, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            sendCanData(new SendObj(send_id, send_data));
        }

        private void btn_clean_his_dtc_Click(object sender, EventArgs e)
        {
            tb_clean_his_dtc.Text = "";
            byte[] send_data = new byte[] { 0x01, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            sendCanData(new SendObj(send_id, send_data));
        }

        private void btn_clean_listview_Click(object sender, EventArgs e)
        {
            is_cleanning = true;
            lv_obd_all.Clear();
            InitListView(lv_obd_all);
            is_cleanning = false;
        }
    }
}
