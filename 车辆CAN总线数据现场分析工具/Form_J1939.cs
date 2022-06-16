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
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace 车辆CAN总线数据现场分析工具
{
    public partial class Form_J1939 : Form
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

        private UInt32 send_id = 0x18db33f1;

        bool is_visible_send = false;
        bool is_visible_recv = false;

        int id_idx = 0;
        // 通用查询ID集合
        private UInt32[] reqIds = new UInt32[]{0x18EA0000, 0x18EA0003, 0x18EA000B, 0x18EA000F, 0x18EA0010, 0x18EA0017, 0x18EA001D,
            0x18EA0021, 0x18EA0024, 0x18EA0027, 0x18EA0029, 0x18EA00EE, 0x18EA00F9};

        int data_idx = 0;
        private byte[][] reqDatas = { new byte[]{ (byte)0xE5, (byte)0xFE, (byte)0x00, (byte)0xFF, (byte)0xFF, (byte)0xFF, (byte)0xFF, (byte)0xFF }, // 发动机运行时间(可选)(查询)
                              new byte[]{(byte)0xE9, (byte)0xFE, (byte)0x00, (byte)0xFF, (byte)0xFF, (byte)0xFF, (byte)0xFF, (byte)0xFF}, // 总油耗(可选)(查询)
                              new byte[]{(byte)0xFF, (byte)0xEB, (byte)0x00, (byte)0xFF, (byte)0xFF, (byte)0xFF, (byte)0xFF, (byte)0xFF} // 参考扭矩(可选)(查询)
                              };

        _SendCanMsg sendMsgHandle;

        DateTime recv_t_0cfe6cee = DateTime.Now;
        UInt32 slt_id_0cfe6cee = 0;
        private List<UInt32> idList_0cfe6cee = new List<UInt32>();
        double cur_val_v = 0;
        double cur_val_oss = 0;
        private Queue<double> dataQueue_v = new Queue<double>(100);
        private Queue<double> dataQueue_oss = new Queue<double>(100);
        DateTime start_t_0cfe6cee = DateTime.Now;
        long count_0cfe6cee = 0;

        DateTime recv_t_0cf00400 = DateTime.Now;
        UInt32 slt_id_0cf00400 = 0;
        private List<UInt32> idList_0cf00400 = new List<UInt32>();
        double cur_val_n = 0;
        double cur_val_act = 0;
        private Queue<double> dataQueue_n = new Queue<double>(100);
        private Queue<double> dataQueue_act = new Queue<double>(100);
        DateTime start_t_0cf00400 = DateTime.Now;
        long count_0cf00400 = 0;

        DateTime recv_t_0cf00300 = DateTime.Now;
        UInt32 slt_id_0cf00300 = 0;
        private List<UInt32> idList_0cf00300 = new List<UInt32>();
        double cur_val_app = 0;
        private Queue<double> dataQueue_app = new Queue<double>(100);
        DateTime start_t_0cf00300 = DateTime.Now;
        long count_0cf00300 = 0;

        DateTime recv_t_18fef200 = DateTime.Now;
        UInt32 slt_id_18fef200 = 0;
        private List<UInt32> idList_18fef200 = new List<UInt32>();
        double cur_val_fr = 0;
        private Queue<double> dataQueue_fr = new Queue<double>(100);
        DateTime start_t_18fef200 = DateTime.Now;
        long count_18fef200 = 0;

        DateTime recv_t_other = DateTime.Now;
        UInt32 slt_id_other = 0;
        private List<UInt32> idList_other = new List<UInt32>();
        DateTime start_t_other = DateTime.Now;
        long count_other = 0;
        
        public Form_J1939(Form_Main mainForm, int channel)
        {
            this.mainForm = mainForm;
            this.channel = channel;
            InitializeComponent();
            ListView.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form_J1939_Load(object sender, EventArgs e)
        {
            sendMsgHandle = new _SendCanMsg(RecvCanDataCallback);
            mainForm.SendCanMsg += sendMsgHandle;

            cb_id_slt.SelectedIndex = 0;
            cb_id_slt_0cfe6cee.SelectedIndex = 0;
            cb_id_slt_0cf00400.SelectedIndex = 0;
            cb_id_slt_0cf00300.SelectedIndex = 0;
            cb_id_slt_18fef200.SelectedIndex = 0;
            cb_id_slt_other.SelectedIndex = 0;

            cb_visible_recv.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.J1939, ConfigUtils.Keys.visible_recv_j1939, "0", -1));
            is_visible_recv = cb_visible_recv.SelectedIndex == 0;
            cb_visible_send.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.J1939, ConfigUtils.Keys.visible_send_j1939, "0", -1));
            is_visible_send = cb_visible_send.SelectedIndex == 0;

            pb_query_progress.Maximum = reqIds.Length;
            pb_query_progress.Value = 0;
            InitListView(lv_obd_all);
            timer_query.Tick += new EventHandler(Query_Tick);
            timer_query.Interval = 500;

            this.timer_undate_charts.Tick += new EventHandler(Undate_Charts_Tick);
            this.timer_undate_charts.Interval = 500;
            this.timer_undate_charts.Start();
            InitChart();

            hanleRecvDataThread = new Thread(HanleRecvDataThread);
            hanleRecvDataThread.Start();
        }

        private void Form_J1939_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.SendCanMsg -= sendMsgHandle;
            hanleRecvDataThread.Abort();
            if (mIsQueryStart)
            {
                timer_query.Stop();
            }
            this.timer_undate_charts.Stop();
        }

        public void RecvCanDataCallback(int devInx, uint id, byte[] data)
        {
            if (data == null || channel != devInx)
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
                byte[] can_data = can_data_obj.data;
                switch (id & 0x00ffff00)
                {
                    case 0x00fe6c00:
                        if (!idList_0cfe6cee.Contains(id))
                        {
                            idList_0cfe6cee.Add(id);
                            string id_str = id.ToString("X2");
                            if (id_str.Length % 2 == 1)
                            {
                                id_str = "0x0" + id_str;
                            }
                            else
                            {
                                id_str = "0x" + id_str;
                            }
                            cb_id_slt_0cfe6cee.Items.Add(id_str);
                        }
                        if (slt_id_0cfe6cee == 0 || slt_id_0cfe6cee == id)
                        {
                            int v = ConvertUtils.combind(can_data[7], can_data[6]);
                            int oss = ConvertUtils.combind(can_data[5], can_data[4]);
                            cur_val_oss = oss * 0.125;
                            cur_val_v = v * 0.00390625;
                            //Console.WriteLine("cur_val_oss = " + cur_val_oss + "  cur_val_v = " + cur_val_v);
                            DateTime ct = DateTime.Now;
                            string period_str = "null";
                            count_0cfe6cee++;
                            if (count_0cfe6cee != 0)
                            {
                                period_str = (int)((ct - start_t_0cfe6cee).TotalMilliseconds / count_0cfe6cee) + " ms";
                            }
                            lb_period_0cfe6cee.Text = period_str;
                            recv_t_0cfe6cee = ct;
                        }
                        break;
                    case 0x00f00400:
                        if (!idList_0cf00400.Contains(id))
                        {
                            idList_0cf00400.Add(id);
                            string id_str = id.ToString("X2");
                            if (id_str.Length % 2 == 1)
                            {
                                id_str = "0x0" + id_str;
                            }
                            else
                            {
                                id_str = "0x" + id_str;
                            }
                            cb_id_slt_0cf00400.Items.Add(id_str);
                        }
                        if (slt_id_0cf00400 == 0 || slt_id_0cf00400 == id)
                        {
                            int n = ConvertUtils.combind(can_data[4], can_data[3]);
                            cur_val_n = n >> 3;
                            cur_val_act = ConvertUtils.getshort(can_data[2]) - 125;
                            //Console.WriteLine("cur_val_n = " + cur_val_n + "  cur_val_act = " + cur_val_act);
                            DateTime ct = DateTime.Now;
                            string period_str = "null";
                            count_0cf00400++;
                            if (count_0cf00400 != 0)
                            {
                                period_str = (int)((ct - start_t_0cf00400).TotalMilliseconds / count_0cf00400) + " ms";
                            }
                            lb_period_0cf00400.Text = period_str;
                            recv_t_0cf00400 = ct;
                        }
                        break;
                    case 0x00f00300:
                        if (!idList_0cf00300.Contains(id))
                        {
                            idList_0cf00300.Add(id);
                            string id_str = id.ToString("X2");
                            if (id_str.Length % 2 == 1)
                            {
                                id_str = "0x0" + id_str;
                            }
                            else
                            {
                                id_str = "0x" + id_str;
                            }
                            cb_id_slt_0cf00300.Items.Add(id_str);
                        }
                        if (slt_id_0cf00300 == 0 || slt_id_0cf00300 == id)
                        {
                            short app = ConvertUtils.getshort(can_data[1]);
                            cur_val_app = app * 0.4;
                            //Console.WriteLine("cur_val_app = " + cur_val_app);
                            DateTime ct = DateTime.Now;
                            string period_str = "null";
                            count_0cf00300++;
                            if (count_0cf00300 != 0)
                            {
                                period_str = (int)((ct - start_t_0cf00300).TotalMilliseconds / count_0cf00300) + " ms";
                            }
                            lb_period_0cf00300.Text = period_str;
                            recv_t_0cf00300 = ct;
                        }
                        break;
                    case 0x00fef200:
                        if (!idList_18fef200.Contains(id))
                        {
                            idList_18fef200.Add(id);
                            string id_str = id.ToString("X2");
                            if (id_str.Length % 2 == 1)
                            {
                                id_str = "0x0" + id_str;
                            }
                            else
                            {
                                id_str = "0x" + id_str;
                            }
                            cb_id_slt_18fef200.Items.Add(id_str);
                        }
                        if (slt_id_18fef200 == 0 || slt_id_18fef200 == id)
                        {
                            int fr_tmp = ConvertUtils.combind(can_data[1], can_data[0]);
                            cur_val_fr = fr_tmp * 0.05;
                            //Console.WriteLine("cur_val_fr = " + cur_val_fr);
                            DateTime ct = DateTime.Now;
                            string period_str = "null";
                            count_18fef200++;
                            if (count_18fef200 != 0)
                            {
                                period_str = (int)((ct - start_t_18fef200).TotalMilliseconds / count_18fef200) + " ms";
                            }
                            lb_period_18fec1ee.Text = period_str;
                            recv_t_18fef200 = ct;
                        }
                        break;
                    case 0x00fef100:
                        if (slt_id != 0 && (slt_id & 0x00ffff00) == (id & 0x00ffff00) && (slt_id_other == 0 || slt_id_other == id))
                        {
                            if (!idList_other.Contains(id))
                            {
                                idList_other.Add(id);
                                string id_str = id.ToString("X2");
                                if (id_str.Length % 2 == 1)
                                {
                                    id_str = "0x0" + id_str;
                                }
                                else
                                {
                                    id_str = "0x" + id_str;
                                }
                                cb_id_slt_other.Items.Add(id_str);
                            }
                            byte b4 = can_data[3];
                            byte cs = (byte)((b4 >> 6) & 0x03);
                            byte brk = (byte)((b4 >> 4) & 0x03);
                            lb_value_other.Text = "刹车 = " + brk + "\n离合 = " + cs;
                            DateTime ct = DateTime.Now;
                            string period_str = "null";
                            count_other++;
                            if (count_other != 0)
                            {
                                period_str = (int)((ct - start_t_other).TotalMilliseconds / count_other) + " ms";
                            }
                            lb_period_other.Text = period_str;
                            recv_t_other = ct;
                        }
                        break;
                    case 0x00fedf00:
                        if (slt_id != 0 && (slt_id & 0x00ffff00) == (id & 0x00ffff00) && (slt_id_other == 0 || slt_id_other == id))
                        {
                            if (!idList_other.Contains(id))
                            {
                                idList_other.Add(id);
                                string id_str = id.ToString("X2");
                                if (id_str.Length % 2 == 1)
                                {
                                    id_str = "0x0" + id_str;
                                }
                                else
                                {
                                    id_str = "0x" + id_str;
                                }
                                cb_id_slt_other.Items.Add(id_str);
                            }
                            int fct = ConvertUtils.getshort(can_data[0]) - 125;
                            lb_value_other.Text = "摩擦扭矩百分百 = " + fct;
                            DateTime ct = DateTime.Now;
                            string period_str = "null";
                            count_other++;
                            if (count_other != 0)
                            {
                                period_str = (int)((ct - start_t_other).TotalMilliseconds / count_other) + " ms";
                            }
                            lb_period_other.Text = period_str;
                            recv_t_other = ct;
                        }
                        break;
                    case 0x00fec100:
                        if (slt_id != 0 && (slt_id & 0x00ffff00) == (id & 0x00ffff00) && (slt_id_other == 0 || slt_id_other == id))
                        {
                            if (!idList_other.Contains(id))
                            {
                                idList_other.Add(id);
                                string id_str = id.ToString("X2");
                                if (id_str.Length % 2 == 1)
                                {
                                    id_str = "0x0" + id_str;
                                }
                                else
                                {
                                    id_str = "0x" + id_str;
                                }
                                cb_id_slt_other.Items.Add(id_str);
                            }
                            int fr_tmp = ConvertUtils.combind(can_data[1], can_data[0]);
                            double fr = fr_tmp * 0.05;
                            lb_value_other.Text = "总里程 = " + fr;
                            DateTime ct = DateTime.Now;
                            string period_str = "null";
                            count_other++;
                            if (count_other != 0)
                            {
                                period_str = (int)((ct - start_t_other).TotalMilliseconds / count_other) + " ms";
                            }
                            lb_period_other.Text = period_str;
                            recv_t_other = ct;
                        }
                        break;
                    case 0x00fee500:
                        if (slt_id != 0 && (slt_id & 0x00ffff00) == (id & 0x00ffff00) && (slt_id_other == 0 || slt_id_other == id))
                        {
                            if (!idList_other.Contains(id))
                            {
                                idList_other.Add(id);
                                string id_str = id.ToString("X2");
                                if (id_str.Length % 2 == 1)
                                {
                                    id_str = "0x0" + id_str;
                                }
                                else
                                {
                                    id_str = "0x" + id_str;
                                }
                                cb_id_slt_other.Items.Add(id_str);
                            }
                            //发动机累计运行时间 单位: 小时
                            long temp1 = (((can_data[3] << 24) & 0xFF000000)
                                  | ((can_data[2] << 16) & 0x00FF0000)
                                  | ((can_data[1] << 8) & 0x0000FF00)
                                  | (can_data[0] & 0x000000FF)) & 0xFFFFFFFFL;
                            double engine_run_time = temp1 * 0.05;

                            //发动机累计转速 单位: 转
                            long temp2 = (((can_data[7] << 24) & 0xFF000000) | ((can_data[6] << 16) & 0x00FF0000)
                                  | ((can_data[5] << 8) & 0x0000FF00) | (can_data[4] & 0x000000FF)) & 0xFFFFFFFFL;
                            long engine_run_speed = temp2 * 1000;

                            lb_value_other.Text = "发动机累计运行时间 = " + temp1 + "\n发动机累计转速 = " + temp2;
                            DateTime ct = DateTime.Now;
                            string period_str = "null";
                            count_other++;
                            if (count_other != 0)
                            {
                                period_str = (int)((ct - start_t_other).TotalMilliseconds / count_other) + " ms";
                            }
                            lb_period_other.Text = period_str;
                            recv_t_other = ct;
                        }
                        break;
                    case 0x00fee900:
                        if (slt_id != 0 && (slt_id & 0x00ffff00) == (id & 0x00ffff00) && (slt_id_other == 0 || slt_id_other == id))
                        {
                            if (!idList_other.Contains(id))
                            {
                                idList_other.Add(id);
                                string id_str = id.ToString("X2");
                                if (id_str.Length % 2 == 1)
                                {
                                    id_str = "0x0" + id_str;
                                }
                                else
                                {
                                    id_str = "0x" + id_str;
                                }
                                cb_id_slt_other.Items.Add(id_str);
                            }
                            long oil = (((can_data[7] << 24) & 0xFF000000) | ((can_data[6] << 16) & 0x00FF0000)
                                    | ((can_data[5] << 8) & 0x0000FF00) | (can_data[4] & 0x000000FF)) & 0xFFFFFFFFL;
                            double oil_total = oil * 0.5;
                            lb_value_other.Text = "总油耗 = " + oil;
                            DateTime ct = DateTime.Now;
                            string period_str = "null";
                            count_other++;
                            if (count_other != 0)
                            {
                                period_str = (int)((ct - start_t_other).TotalMilliseconds / count_other) + " ms";
                            }
                            lb_period_other.Text = period_str;
                            recv_t_other = ct;
                        }
                        break;
                    case 0x00ebff00:
                        if (slt_id != 0 && (slt_id & 0x00ffff00) == (id & 0x00ffff00) && (slt_id_other == 0 || slt_id_other == id))
                        {
                            if (!idList_other.Contains(id))
                            {
                                idList_other.Add(id);
                                string id_str = id.ToString("X2");
                                if (id_str.Length % 2 == 1)
                                {
                                    id_str = "0x0" + id_str;
                                }
                                else
                                {
                                    id_str = "0x" + id_str;
                                }
                                cb_id_slt_other.Items.Add(id_str);
                            }
                            int rft = ConvertUtils.combind(can_data[7], can_data[6]);
                            lb_value_other.Text = "参考扭矩 = " + rft;
                            DateTime ct = DateTime.Now;
                            string period_str = "null";
                            count_other++;
                            if (count_other != 0)
                            {
                                period_str = (int)((ct - start_t_other).TotalMilliseconds / count_other) + " ms";
                            }
                            lb_period_other.Text = period_str;
                            recv_t_other = ct;
                        }
                        break;
                    case 0x00f00200:
                        if (slt_id != 0 && (slt_id & 0x00ffff00) == (id & 0x00ffff00) && (slt_id_other == 0 || slt_id_other == id))
                        {
                            if (!idList_other.Contains(id))
                            {
                                idList_other.Add(id);
                                string id_str = id.ToString("X2");
                                if (id_str.Length % 2 == 1)
                                {
                                    id_str = "0x0" + id_str;
                                }
                                else
                                {
                                    id_str = "0x" + id_str;
                                }
                                cb_id_slt_other.Items.Add(id_str);
                            }
                            int oss = ConvertUtils.combind(can_data[2], can_data[1]);
                            double ossACT = oss * 0.125;
                            lb_value_other.Text = "变速箱输出轴转速 = " + ossACT;
                            DateTime ct = DateTime.Now;
                            string period_str = "null";
                            count_other++;
                            if (count_other != 0)
                            {
                                period_str = (int)((ct - start_t_other).TotalMilliseconds / count_other) + " ms";
                            }
                            lb_period_other.Text = period_str;
                            recv_t_other = ct;
                        }
                        break;
                    case 0x00fee000:
                        if (slt_id != 0 && (slt_id & 0x00ffff00) == (id & 0x00ffff00) && (slt_id_other == 0 || slt_id_other == id))
                        {
                            if (!idList_other.Contains(id))
                            {
                                idList_other.Add(id);
                                string id_str = id.ToString("X2");
                                if (id_str.Length % 2 == 1)
                                {
                                    id_str = "0x0" + id_str;
                                }
                                else
                                {
                                    id_str = "0x" + id_str;
                                }
                                cb_id_slt_other.Items.Add(id_str);
                            }
                            long temp = (((can_data[7] << 24) & 0xFF000000) | ((can_data[6] << 16) & 0x00FF0000)
                                        | ((can_data[5] << 8) & 0x0000FF00) | (can_data[4] & 0x000000FF)) & 0xFFFFFFFFL;
                            double mileage = temp * 0.125;
                            lb_value_other.Text = "总里程 = " + mileage;
                            DateTime ct = DateTime.Now;
                            string period_str = "null";
                            count_other++;
                            if (count_other != 0)
                            {
                                period_str = (int)((ct - start_t_other).TotalMilliseconds / count_other) + " ms";
                            }
                            lb_period_other.Text = period_str;
                            recv_t_other = ct;
                        }
                        break;
                    default:
                        break;
                }
                // 显示
                if (slt_id == 0 || (slt_id != 0 && (id & 0x00ffff00) == slt_id))
                {
                    if (is_visible_recv)
                    {
                        String frameID = "0x" + Convert.ToString(id, 16).ToUpper();
                        //Console.WriteLine("frameID = " + frameID);

                        frameStr.Remove(0, frameStr.Length);
                        //frameStr.Append(frameID + ":");
                        int len = can_data.Length;
                        for (byte j = 0; j < len; j++)
                        {
                            frameStr.Append(ConvertToHex(can_data[j]));
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
                }
            }
        }

        private string ConvertToHex(UInt32 x)
        {
            string hex = Convert.ToString(x, 16).PadLeft(2, '0').ToUpper();
            return hex + " ";
        }

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Undate_Charts_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now - recv_t_0cfe6cee).TotalMilliseconds < 1000)
            {
                if (dataQueue_v.Count > 30)
                {
                    dataQueue_v.Dequeue();
                    dataQueue_v.Dequeue();
                }
                dataQueue_v.Enqueue(cur_val_v);
                this.chart_0cfe6cee.Series[0].Points.Clear();
                for (int i = 0; i < dataQueue_v.Count; i++)
                {
                    this.chart_0cfe6cee.Series[0].Points.AddXY((i+1), dataQueue_v.ElementAt(i));
                }

                if (dataQueue_oss.Count > 30)
                {
                    dataQueue_oss.Dequeue();
                    dataQueue_oss.Dequeue();
                }
                dataQueue_oss.Enqueue(cur_val_oss);
                this.chart_0cfe6cee.Series[1].Points.Clear();
                for (int i = 0; i < dataQueue_oss.Count; i++)
                {
                    this.chart_0cfe6cee.Series[1].Points.AddXY((i + 1), dataQueue_oss.ElementAt(i));
                }
            }

            if ((DateTime.Now - recv_t_0cf00400).TotalMilliseconds < 1000)
            {
                if (dataQueue_n.Count > 30)
                {
                    dataQueue_n.Dequeue();
                    dataQueue_n.Dequeue();
                }
                dataQueue_n.Enqueue(cur_val_n);
                this.chart_0cf00400.Series[0].Points.Clear();
                for (int i = 0; i < dataQueue_n.Count; i++)
                {
                    this.chart_0cf00400.Series[0].Points.AddXY((i + 1), dataQueue_n.ElementAt(i));
                }

                if (dataQueue_act.Count > 30)
                {
                    dataQueue_act.Dequeue();
                    dataQueue_act.Dequeue();
                }
                dataQueue_act.Enqueue(cur_val_act);
                this.chart_0cf00400.Series[1].Points.Clear();
                for (int i = 0; i < dataQueue_act.Count; i++)
                {
                    this.chart_0cf00400.Series[1].Points.AddXY((i + 1), dataQueue_act.ElementAt(i));
                }
            }

            if ((DateTime.Now - recv_t_0cf00300).TotalMilliseconds < 1000)
            {
                if (dataQueue_app.Count > 30)
                {
                    dataQueue_app.Dequeue();
                    dataQueue_app.Dequeue();
                }
                dataQueue_app.Enqueue(cur_val_app);
                this.chart_0cf00300.Series[0].Points.Clear();
                for (int i = 0; i < dataQueue_app.Count; i++)
                {
                    this.chart_0cf00300.Series[0].Points.AddXY((i + 1), dataQueue_app.ElementAt(i));
                }

            }

            if ((DateTime.Now - recv_t_18fef200).TotalMilliseconds < 1000)
            {
                if (dataQueue_fr.Count > 30)
                {
                    dataQueue_fr.Dequeue();
                    dataQueue_fr.Dequeue();
                }
                dataQueue_fr.Enqueue(cur_val_fr);
                this.chart_18fec1ee.Series[0].Points.Clear();
                for (int i = 0; i < dataQueue_fr.Count; i++)
                {
                    this.chart_18fec1ee.Series[0].Points.AddXY((i + 1), dataQueue_fr.ElementAt(i));
                }

            }
        }

        /// <summary>
        /// 初始化图表
        /// </summary>
        private void InitChart()
        {
            //定义图表区域
            chart_0cfe6cee.ChartAreas.Clear();
            //定义存储和显示点的容器
            chart_0cfe6cee.Series.Clear();
            //设置标题
            chart_0cfe6cee.Titles.Clear();
            //chart_0cfe6cee.Titles.Add("0CFE6CEE");
            ChartArea ca1 = new ChartArea("0CFE6CEE");
            //设置图表显示样式
            ca1.AxisY.Minimum = 0;
            ca1.AxisY.Maximum = 150;
            ca1.AxisX.Interval = 5;
            ca1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F); ;
            ca1.AxisY.Interval = 30;
            ca1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            ca1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chart_0cfe6cee.ChartAreas.Add(ca1);
            //this.chart_0cfe6cee.Titles[0].ForeColor = Color.RoyalBlue;
            //this.chart_0cfe6cee.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);

            Series s1 = new Series("Speed");
            //设置图表显示样式
            s1.Color = Color.Red;
            //this.chart1.Titles[0].Text = "折线图";
            s1.ChartType = SeriesChartType.Spline;
            s1.Points.Clear();
            s1.MarkerBorderWidth = 2;
            s1.MarkerSize = 4;
            s1.MarkerStyle = MarkerStyle.Diamond;
            s1.ToolTip = s1.Name + "：#VAL \r\n #AXISLABEL";
            s1.YAxisType = AxisType.Primary;
            chart_0cfe6cee.Series.Add(s1);

            //定义存储和显示点的容器
            Series s2 = new Series("OSS");
            //设置图表显示样式
            s2.Color = Color.Blue;
            s2.ChartType = SeriesChartType.Spline;
            s2.Points.Clear();
            s2.MarkerBorderWidth = 2;
            s2.MarkerSize = 4;
            s2.MarkerStyle = MarkerStyle.Diamond;
            s2.ToolTip = s2.Name + "：#VAL \r\n #AXISLABEL";
            s2.YAxisType = AxisType.Secondary;
            chart_0cfe6cee.Series.Add(s2);

            //定义图表区域
            chart_0cf00400.ChartAreas.Clear();
            //定义存储和显示点的容器
            chart_0cf00400.Series.Clear();
            //设置标题
            chart_0cf00400.Titles.Clear();
            //chart_0cf00400.Titles.Add("0cf00400");
            ca1 = new ChartArea("0cf00400");
            //设置图表显示样式
            ca1.AxisY.Minimum = 0;
            ca1.AxisY.Maximum = 3000;
            ca1.AxisX.Interval = 5;
            ca1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F); ;
            ca1.AxisY.Interval = 500;
            ca1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            ca1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chart_0cf00400.ChartAreas.Add(ca1);
            //this.chart_0cf00400.Titles[0].ForeColor = Color.RoyalBlue;
            //this.chart_0cf00400.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);

            s1 = new Series("N");
            //设置图表显示样式
            s1.Color = Color.Red;
            //this.chart1.Titles[0].Text = "折线图";
            s1.ChartType = SeriesChartType.Spline;
            s1.Points.Clear();
            s1.MarkerBorderWidth = 2;
            s1.MarkerSize = 4;
            s1.MarkerStyle = MarkerStyle.Diamond;
            s1.ToolTip = s1.Name + "：#VAL \r\n #AXISLABEL";
            s1.YAxisType = AxisType.Primary;
            chart_0cf00400.Series.Add(s1);

            //定义存储和显示点的容器
            s2 = new Series("Act");
            //设置图表显示样式
            s2.Color = Color.Blue;
            s2.ChartType = SeriesChartType.Spline;
            s2.Points.Clear();
            s2.MarkerBorderWidth = 2;
            s2.MarkerSize = 4;
            s2.MarkerStyle = MarkerStyle.Diamond;
            s2.ToolTip = s2.Name + "：#VAL \r\n #AXISLABEL";
            s2.YAxisType = AxisType.Secondary;
            chart_0cf00400.Series.Add(s2);

            //定义图表区域
            chart_0cf00300.ChartAreas.Clear();
            //定义存储和显示点的容器
            chart_0cf00300.Series.Clear();
            //设置标题
            chart_0cf00300.Titles.Clear();
            //chart_0cf00300.Titles.Add("0cf00300");
            ca1 = new ChartArea("0cf00300");
            //设置图表显示样式
            ca1.AxisY.Minimum = 0;
            ca1.AxisY.Maximum = 100;
            ca1.AxisX.Interval = 5;
            ca1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F); ;
            ca1.AxisY.Interval = 20;
            ca1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            ca1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chart_0cf00300.ChartAreas.Add(ca1);
            //this.chart_0cf00300.Titles[0].ForeColor = Color.RoyalBlue;
            //this.chart_0cf00300.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);

            s1 = new Series("APP");
            //设置图表显示样式
            s1.Color = Color.Red;
            //this.chart1.Titles[0].Text = "折线图";
            s1.ChartType = SeriesChartType.Spline;
            s1.Points.Clear();
            s1.MarkerBorderWidth = 2;
            s1.MarkerSize = 4;
            s1.MarkerStyle = MarkerStyle.Diamond;
            s1.ToolTip = s1.Name + "：#VAL \r\n #AXISLABEL";
            s1.YAxisType = AxisType.Primary;
            chart_0cf00300.Series.Add(s1);

            //定义图表区域
            chart_18fec1ee.ChartAreas.Clear();
            //定义存储和显示点的容器
            chart_18fec1ee.Series.Clear();
            //设置标题
            chart_18fec1ee.Titles.Clear();
            //chart_18fec1ee.Titles.Add("18fec1ee");
            ca1 = new ChartArea("18fec1ee");
            //设置图表显示样式
            //ca1.AxisY.Minimum = 0;
            //ca1.AxisY.Maximum = 50;
            ca1.AxisX.Interval = 5;
            ca1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F); ;
            //ca1.AxisY.Interval = 10;
            ca1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            ca1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chart_18fec1ee.ChartAreas.Add(ca1);
            //this.chart_18fec1ee.Titles[0].ForeColor = Color.RoyalBlue;
            //this.chart_18fec1ee.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);

            s1 = new Series("FR");
            //设置图表显示样式
            s1.Color = Color.Red;
            //this.chart1.Titles[0].Text = "折线图";
            s1.ChartType = SeriesChartType.Spline;
            s1.Points.Clear();
            s1.MarkerBorderWidth = 2;
            s1.MarkerSize = 4;
            s1.MarkerStyle = MarkerStyle.Diamond;
            s1.ToolTip = s1.Name + "：#VAL \r\n #AXISLABEL";
            s1.YAxisType = AxisType.Primary;
            chart_18fec1ee.Series.Add(s1);

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

        private void btn_clean_listview_Click(object sender, EventArgs e)
        {
            is_cleanning = true;
            lv_obd_all.Clear();
            InitListView(lv_obd_all);
            is_cleanning = false;
            // 清除周期统计
            start_t_0cfe6cee = DateTime.Now;
            count_0cfe6cee = 0;
            start_t_0cf00400 = DateTime.Now;
            count_0cf00400 = 0;
            start_t_0cf00300 = DateTime.Now;
            count_0cf00300 = 0;
            start_t_18fef200 = DateTime.Now;
            count_18fef200 = 0;
            start_t_other = DateTime.Now;
            count_other = 0;
        }

        private void Query_Tick(object sender, EventArgs e)
        {
            UInt32 reqId = reqIds[id_idx];
            byte[] send_data = reqDatas[data_idx++];
            sendCanData(new SendObj(reqId, send_data));
            if (data_idx == reqDatas.Length)
            {
                data_idx = 0;
                id_idx++;
                pb_query_progress.Value = id_idx;
                if (id_idx == reqIds.Length)
                {
                    id_idx = 0;
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
                string id_str = cb_id_slt.Text.ToString().Replace("0x", "").Split('（')[0];
                slt_id = Convert.ToUInt32(id_str, 16) & 0x00ffff00;
            }
            if (cb_id_slt.SelectedIndex <= 4)
            {
                lb_other_id.Text = "未选中需要显示数据";
            }
            else
            {
                lb_other_id.Text = cb_id_slt.Text;
            }
            cb_id_slt_other.Items.Clear();
            cb_id_slt_other.Items.Add("所有ID");
            cb_id_slt_other.SelectedIndex = 0;
            lb_value_other.Text = "null";
            idList_other.Clear();
            slt_id_other = 0;
            lb_period_other.Text = "null";
            start_t_other = DateTime.Now;
            count_other = 0;
        }

        private void cb_visible_send_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.J1939, ConfigUtils.Keys.visible_send_j1939, cb_visible_send.SelectedIndex.ToString());
            is_visible_send = cb_visible_send.SelectedIndex == 0;
        }

        private void cb_visible_recv_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.J1939, ConfigUtils.Keys.visible_recv_j1939, cb_visible_recv.SelectedIndex.ToString());
            is_visible_recv = cb_visible_recv.SelectedIndex == 0;
        }

        private void cb_id_slt_0cfe6cee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_id_slt_0cfe6cee.SelectedIndex == 0)
            {
                slt_id_0cfe6cee = 0;
            }
            else
            {
                slt_id_0cfe6cee = idList_0cfe6cee.ToArray()[cb_id_slt_0cfe6cee.SelectedIndex - 1];
            }
        }

        private void cb_id_slt_0cf00400_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_id_slt_0cf00400.SelectedIndex == 0)
            {
                slt_id_0cf00400 = 0;
            }
            else
            {
                slt_id_0cf00400 = idList_0cf00400.ToArray()[cb_id_slt_0cf00400.SelectedIndex - 1];
            }
        }

        private void cb_id_slt_0cf00300_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_id_slt_0cf00300.SelectedIndex == 0)
            {
                slt_id_0cf00300 = 0;
            }
            else
            {
                slt_id_0cf00300 = idList_0cf00300.ToArray()[cb_id_slt_0cf00300.SelectedIndex - 1];
            }
        }

        private void cb_id_slt_18fec1ee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_id_slt_18fef200.SelectedIndex == 0)
            {
                slt_id_18fef200 = 0;
            }
            else
            {
                slt_id_18fef200 = idList_18fef200.ToArray()[cb_id_slt_18fef200.SelectedIndex - 1];
            }
        }

        private void cb_id_slt_other_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_id_slt_other.SelectedIndex == 0)
            {
                slt_id_other = 0;
            }
            else
            {
                slt_id_other = idList_other.ToArray()[cb_id_slt_other.SelectedIndex - 1];
            }
        }

    }
}
