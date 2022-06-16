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
    public partial class Form_OBD_Diagnosis : Form
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

        byte[] pids = new byte[] { 0x04, 0x0b, 0x0c, 0x0d, 0x0f, 0x10 };
        int idx = 0;

        _SendCanMsg sendMsgHandle;

        Dictionary<byte, int> pidMaps = new Dictionary<byte, int>();
        AlignDataInfo alignData = new AlignDataInfo();

        private Queue<double> dataQueue_v = new Queue<double>(100);
        private Queue<double> dataQueue_n = new Queue<double>(100);
        private Queue<double> dataQueue_fr = new Queue<double>(100);
        private Queue<double> dataQueue_app = new Queue<double>(100);

        private int mVmap0 = 100;         // 怠速时基础进气压力
        private double modifVal = 6;        // 瞬时油耗计算修正值

        public Form_OBD_Diagnosis(Form_Main mainForm, int channel)
        {
            this.mainForm = mainForm;
            this.channel = channel;
            InitializeComponent();
            ListView.CheckForIllegalCrossThreadCalls = false;
            initPidMaps();
        }

        private void Form_OBD_Diagnosis_Load(object sender, EventArgs e)
        {
            sendMsgHandle = new _SendCanMsg(RecvCanDataCallback);
            mainForm.SendCanMsg += sendMsgHandle;

            hanleRecvDataThread = new Thread(HanleRecvDataThread);
            hanleRecvDataThread.Start();

            cb_id_slt.SelectedIndex = 0;

            cb_visible_recv.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.OBD_DIAGNOSIS, ConfigUtils.Keys.visible_recv_obd_all, "0", -1));
            is_visible_recv = cb_visible_recv.SelectedIndex == 0;
            cb_visible_send.SelectedIndex = Convert.ToInt32(ConfigUtils.getConfigString(ConfigUtils.Sections.OBD_DIAGNOSIS, ConfigUtils.Keys.visible_send_obd_all, "0", -1));
            is_visible_send = cb_visible_send.SelectedIndex == 0;

            pb_query_progress.Maximum = pids.Length;
            pb_query_progress.Value = 0;
            InitListView(lv_obd_all);
            timer_query.Tick += new EventHandler(Query_Tick);
            timer_query.Interval = 500;
            timer_undate_charts.Tick += new EventHandler(Undate_Charts_Tick);
            timer_undate_charts.Interval = 500;
            timer_undate_charts.Start();

            alignData.Vair = 14.3;
            alignData.Vfuel = 840;
            alignData.Vcc = 4.26;

            InitChart();
        }

        /// <summary>
        /// 初始化图表
        /// </summary>
        private void InitChart()
        {
            //定义图表区域
            chart_v.ChartAreas.Clear();
            //定义存储和显示点的容器
            chart_v.Series.Clear();
            //设置标题
            chart_v.Titles.Clear();
            chart_v.Titles.Add("速度");
            ChartArea ca1 = new ChartArea("V");
            //设置图表显示样式
            ca1.AxisY.Minimum = 0;
            ca1.AxisY.Maximum = 150;
            ca1.AxisX.Interval = 5;
            ca1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F); ;
            ca1.AxisY.Interval = 30;
            ca1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            ca1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chart_v.ChartAreas.Add(ca1);
            Series s1 = new Series("V");
            //设置图表显示样式
            s1.Color = Color.Red;
            s1.ChartType = SeriesChartType.Spline;
            s1.Points.Clear();
            s1.MarkerBorderWidth = 2;
            s1.MarkerSize = 4;
            s1.MarkerStyle = MarkerStyle.Diamond;
            s1.ToolTip = s1.Name + "：#VAL \r\n #AXISLABEL";
            s1.YAxisType = AxisType.Primary;
            chart_v.Series.Add(s1);


            //定义图表区域
            chart_n.ChartAreas.Clear();
            //定义存储和显示点的容器
            chart_n.Series.Clear();
            //设置标题
            chart_n.Titles.Clear();
            chart_n.Titles.Add("转速");
            ca1 = new ChartArea("N");
            //设置图表显示样式
            ca1.AxisY.Minimum = 0;
            ca1.AxisY.Maximum = 3000;
            ca1.AxisX.Interval = 5;
            ca1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F); ;
            ca1.AxisY.Interval = 500;
            ca1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            ca1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chart_n.ChartAreas.Add(ca1);
            s1 = new Series("N");
            //设置图表显示样式
            s1.Color = Color.Blue;
            s1.ChartType = SeriesChartType.Spline;
            s1.Points.Clear();
            s1.MarkerBorderWidth = 2;
            s1.MarkerSize = 4;
            s1.MarkerStyle = MarkerStyle.Diamond;
            s1.ToolTip = s1.Name + "：#VAL \r\n #AXISLABEL";
            s1.YAxisType = AxisType.Primary;
            chart_n.Series.Add(s1);


            //定义图表区域
            chart_fr.ChartAreas.Clear();
            //定义存储和显示点的容器
            chart_fr.Series.Clear();
            //设置标题
            chart_fr.Titles.Clear();
            chart_fr.Titles.Add("瞬时油耗");
            ca1 = new ChartArea("FR");
            //设置图表显示样式
            ca1.AxisY.Minimum = 0;
            ca1.AxisY.Maximum = 50;
            ca1.AxisX.Interval = 5;
            ca1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F); ;
            ca1.AxisY.Interval = 10;
            ca1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            ca1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chart_fr.ChartAreas.Add(ca1);
            s1 = new Series("FR");
            //设置图表显示样式
            s1.Color = Color.DarkGreen;
            s1.ChartType = SeriesChartType.Spline;
            s1.Points.Clear();
            s1.MarkerBorderWidth = 2;
            s1.MarkerSize = 4;
            s1.MarkerStyle = MarkerStyle.Diamond;
            s1.ToolTip = s1.Name + "：#VAL \r\n #AXISLABEL";
            s1.YAxisType = AxisType.Primary;
            chart_fr.Series.Add(s1);


            //定义图表区域
            chart_app.ChartAreas.Clear();
            //定义存储和显示点的容器
            chart_app.Series.Clear();
            //设置标题
            chart_app.Titles.Clear();
            chart_app.Titles.Add("油门开度");
            ca1 = new ChartArea("APP");
            //设置图表显示样式
            ca1.AxisY.Minimum = 0;
            ca1.AxisY.Maximum = 100;
            ca1.AxisX.Interval = 5;
            ca1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F); ;
            ca1.AxisY.Interval = 20;
            ca1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            ca1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chart_app.ChartAreas.Add(ca1);
            s1 = new Series("APP");
            //设置图表显示样式
            s1.Color = Color.Black;
            s1.ChartType = SeriesChartType.Spline;
            s1.Points.Clear();
            s1.MarkerBorderWidth = 2;
            s1.MarkerSize = 4;
            s1.MarkerStyle = MarkerStyle.Diamond;
            s1.ToolTip = s1.Name + "：#VAL \r\n #AXISLABEL";
            s1.YAxisType = AxisType.Primary;
            chart_app.Series.Add(s1);


        }

        private void Form_OBD_Diagnosis_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.SendCanMsg -= sendMsgHandle;
            if (mIsQueryStart)
            {
                timer_query.Stop();
            }
            timer_undate_charts.Stop();
        }

        private void Undate_Charts_Tick(object sender, EventArgs e)
        {
            if (!mIsQueryStart)
            {
                return;
            }
            if ((DateTime.Now - alignData.V_obd_t).TotalMilliseconds < 1000)
            {
                if (dataQueue_v.Count > 40) {
                    for (int i = 0; i < 2; i++)
                    {
                        dataQueue_v.Dequeue(); 
                    }
                }
                dataQueue_v.Enqueue(alignData.V_obd);
            } 
            this.chart_v.Series[0].Points.Clear();
            for (int i = 0; i < dataQueue_v.Count; i++)
            {
                this.chart_v.Series[0].Points.AddXY((i + 1), dataQueue_v.ElementAt(i));
            }

            if ((DateTime.Now - alignData.N_obd_t).TotalMilliseconds < 1000)
            {
                if (dataQueue_n.Count > 40)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dataQueue_n.Dequeue();
                    }
                }
                dataQueue_n.Enqueue(alignData.N_obd);
            }
            this.chart_n.Series[0].Points.Clear();
            for (int i = 0; i < dataQueue_n.Count; i++)
            {
                this.chart_n.Series[0].Points.AddXY((i + 1), dataQueue_n.ElementAt(i));
            }

            if ((DateTime.Now - alignData.Fr_obd_t).TotalMilliseconds < 1000)
            {
                if (dataQueue_fr.Count > 40)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dataQueue_fr.Dequeue();
                    }
                }
                dataQueue_fr.Enqueue(alignData.Fr_obd);
            }
            this.chart_fr.Series[0].Points.Clear();
            for (int i = 0; i < dataQueue_fr.Count; i++)
            {
                this.chart_fr.Series[0].Points.AddXY((i + 1), dataQueue_fr.ElementAt(i));
            }

            if ((DateTime.Now - alignData.App_obd_t).TotalMilliseconds < 1000)
            {
                if (dataQueue_app.Count > 40)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dataQueue_app.Dequeue();
                    }
                }
                dataQueue_app.Enqueue(alignData.App_obd);
            }
            this.chart_app.Series[0].Points.Clear();
            for (int i = 0; i < dataQueue_app.Count; i++)
            {
                this.chart_app.Series[0].Points.AddXY((i + 1), dataQueue_app.ElementAt(i));
            }
        }

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

                    // 解析数据
                    if (data[1] == 0x41 && data.Length >= 8)
                    {
                        int len = getPidDataLen(data[2]);
                        if (len <= 0 || data.Length < len + 3)
                        {
                            Console.WriteLine("长度错误：");
                            return;
                        }
                        byte[] datas = new byte[len];
                        for (int i = 3; i < data.Length && i - 3 < len; i++)
                        {
                            datas[i - 3] = data[i];
                        }
                        ObdDataInfo info = new ObdDataInfo(data[2], datas);

                        this.Invoke((EventHandler)(delegate
                        {
                            handlePidData(info);
                        }));
                    }
                }
            }
        }
        private string ConvertToHex(UInt32 x)
        {
            string hex = Convert.ToString(x, 16).PadLeft(2, '0').ToUpper();
            return hex + " ";
        }

        private int getPidDataLen(byte pid)
        {
            if (!pidMaps.ContainsKey(pid))
            {
                return -1;
            }
            int len = 0;
            pidMaps.TryGetValue(pid, out len);
            return len;
        }

        private void initPidMaps()
        {
            pidMaps.Add(0x01, 4);
            pidMaps.Add(0x02, 2);
            pidMaps.Add(0x03, 2);
            pidMaps.Add(0x04, 1);
            pidMaps.Add(0x05, 1);
            pidMaps.Add(0x06, 2);
            pidMaps.Add(0x07, 2);
            pidMaps.Add(0x08, 2);
            pidMaps.Add(0x09, 2);
            pidMaps.Add(0x0A, 1);
            pidMaps.Add(0x0B, 1);
            pidMaps.Add(0x0C, 2);
            pidMaps.Add(0x0D, 1);
            pidMaps.Add(0x0E, 1);
            pidMaps.Add(0x0F, 1);
            pidMaps.Add(0x10, 2);
            pidMaps.Add(0x11, 1);
            pidMaps.Add(0x12, 1);
            pidMaps.Add(0x13, 1);
            pidMaps.Add(0x14, 2);
            pidMaps.Add(0x15, 2);
            pidMaps.Add(0x16, 2);
            pidMaps.Add(0x17, 2);
            pidMaps.Add(0x18, 2);
            pidMaps.Add(0x19, 2);
            pidMaps.Add(0x1A, 2);
            pidMaps.Add(0x1B, 2);
            pidMaps.Add(0x1C, 1);
            pidMaps.Add(0x1D, 1);
            pidMaps.Add(0x1E, 1);
            pidMaps.Add(0x1F, 2);
            pidMaps.Add(0x21, 2);
            pidMaps.Add(0x22, 2);
            pidMaps.Add(0x23, 2);
            pidMaps.Add(0x24, 4);
            pidMaps.Add(0x25, 4);
            pidMaps.Add(0x26, 4);
            pidMaps.Add(0x27, 4);
            pidMaps.Add(0x28, 4);
            pidMaps.Add(0x29, 2);
            pidMaps.Add(0x2A, 2);
            pidMaps.Add(0x2B, 2);
            pidMaps.Add(0x2C, 1);
            pidMaps.Add(0x2D, 1);
            pidMaps.Add(0x2E, 1);
            pidMaps.Add(0x2F, 1);
            pidMaps.Add(0x30, 1);
            pidMaps.Add(0x31, 2);
            pidMaps.Add(0x32, 2);
            pidMaps.Add(0x33, 1);
            pidMaps.Add(0x34, 4);
            pidMaps.Add(0x35, 4);
            pidMaps.Add(0x36, 4);
            pidMaps.Add(0x37, 4);
            pidMaps.Add(0x38, 4);
            pidMaps.Add(0x39, 2);
            pidMaps.Add(0x3A, 2);
            pidMaps.Add(0x3B, 2);
            pidMaps.Add(0x3C, 2);
            pidMaps.Add(0x3D, 2);
            pidMaps.Add(0x3E, 2);
            pidMaps.Add(0x3F, 2);
            pidMaps.Add(0x41, 4);
            pidMaps.Add(0x42, 2);
            pidMaps.Add(0x43, 2);
            pidMaps.Add(0x44, 2);
            pidMaps.Add(0x45, 1);
            pidMaps.Add(0x46, 1);
            pidMaps.Add(0x47, 1);
            pidMaps.Add(0x48, 1);
            pidMaps.Add(0x49, 1);
            pidMaps.Add(0x4A, 1);
            pidMaps.Add(0x4B, 1);
            pidMaps.Add(0x4C, 1);
            pidMaps.Add(0x4D, 2);
            pidMaps.Add(0x4E, 2);
            pidMaps.Add(0x4F, 4);
            pidMaps.Add(0x50, 4);
            pidMaps.Add(0x51, 1);
            pidMaps.Add(0x52, 1);
            pidMaps.Add(0x53, 2);
            pidMaps.Add(0x54, 2);
            pidMaps.Add(0x55, 2);
            pidMaps.Add(0x56, 2);
            pidMaps.Add(0x57, 2);
            pidMaps.Add(0x58, 2);
            pidMaps.Add(0x59, 2);
            pidMaps.Add(0x5A, 1);
        }

        private void handlePidData(ObdDataInfo info)
        {
            switch (info.Pid)
            {
                case 0x04:  // 计算负荷值(Calculated Load Value)
                    if (info.Data.Length == 1)
                    {
                        double load = info.Data[0] * 100.0 / 255;
                        //Console.WriteLine("load = " + load);
                        alignData.Vload = load;
                        // 估算油门踏板
                        double app = handleAppClc();
                        alignData.App_obd = app;
                        alignData.App_obd_t = DateTime.Now;
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x0F:  // 进气温度
                    if (info.Data.Length == 1)
                    {
                        double jqwd = info.Data[0] - 40;
                        //Console.WriteLine("jqwd = " + jqwd);
                        alignData.Vjqwd = jqwd;
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x4C:  // 节气阀执行器控制指令Commanded Throttle Actuator Control
                    if (info.Data.Length == 1)
                    {
                        double ctac = info.Data[0] * 100.0 / 255;
                        //Console.WriteLine("ctac = " + ctac);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x0D:  // 车速
                    if (info.Data.Length == 1)
                    {
                        int v = info.Data[0];
                        //Console.WriteLine("v = " + v);
                        alignData.V_obd = v;
                        alignData.V_obd_t = DateTime.Now;
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x0B:  // 进气歧管绝对压力
                    if (info.Data.Length == 1)
                    {
                        int jqyl = info.Data[0];
                        //Console.WriteLine("jqyl = " + jqyl);
                        alignData.Vjqyl = jqyl;
                        double fc = handleFuelClc();
                        alignData.Fr_obd = fc;
                        alignData.Fr_obd_t = DateTime.Now;
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x0C:  // 发动机转速
                    if (info.Data.Length == 2)
                    {
                        double n = (info.Data[0] * 256 + info.Data[1]) / 4.0;
                        //Console.WriteLine("n = " + n);
                        alignData.N_obd = n;
                        alignData.N_obd_t = DateTime.Now;
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x1F:  // 离启动运行时间
                    if (info.Data.Length == 2)
                    {
                        int startTime = info.Data[0] * 256 + info.Data[1];
                        //Console.WriteLine("startTime = " + startTime);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x49:  // 油门开度
                    if (info.Data.Length == 1)
                    {
                        double app_49 = info.Data[0] * 100.0 / 255;
                        //Console.WriteLine("app_49 = " + app_49);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x4A:  // 油门开度
                    if (info.Data.Length == 1)
                    {
                        double app_4A = info.Data[0] * 100.0 / 255;
                        //Console.WriteLine("app_4A = " + app_4A);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x4B:  // 油门开度
                    if (info.Data.Length == 1)
                    {
                        double app_4B = info.Data[0] * 100.0 / 255;
                        //Console.WriteLine("app_4B = " + app_4B);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x5A:  // 油门开度
                    if (info.Data.Length == 1)
                    {
                        double app_5A = info.Data[0] * 100.0 / 255;
                        //Console.WriteLine("app_5A = " + app_5A);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x11:  // 节气门开度
                    if (info.Data.Length == 1)
                    {
                        double app_11 = info.Data[0] * 100.0 / 255;
                        //Console.WriteLine("app_11 = " + app_11);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x45:  // 节气门开度
                    if (info.Data.Length == 1)
                    {
                        double app_45 = info.Data[0] * 100.0 / 255;
                        //Console.WriteLine("app_45 = " + app_45);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x47:  // 节气门开度
                    if (info.Data.Length == 1)
                    {
                        double app_47 = info.Data[0] * 100.0 / 255;
                        //Console.WriteLine("app_47 = " + app_47);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x10:  // 瞬时油耗估算 空气流量传感器的空气流量
                    if (info.Data.Length == 2)
                    {
                        double airFlow = (info.Data[0] * 256 + info.Data[1]) * 0.01;
                        //Console.WriteLine("airFlow = " + airFlow);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x14:  // 瞬时油耗估算
                    if (info.Data.Length == 2)
                    {
                        double outPutV_14 = info.Data[0] * 0.005;//0到1V氧传感器输出电压
                        double fuelMidf_s_14 = (info.Data[1] - 128) * 100.0 / 128;//短时燃油修正
                        //Console.WriteLine("outPutV_14 = " + outPutV_14 + "  fuelMidf_s_14 = " + fuelMidf_s_14);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x15:  // 瞬时油耗估算
                    if (info.Data.Length == 2)
                    {
                        double outPutV_15 = info.Data[0] * 0.005;//0到1V氧传感器输出电压
                        double fuelMidf_s_15 = (info.Data[1] - 128) * 100.0 / 128;//短时燃油修正
                        //Console.WriteLine("outPutV_15 = " + outPutV_15 + "  fuelMidf_s_15 = " + fuelMidf_s_15);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x06:  // 瞬时油耗估算
                    if (info.Data.Length == 2)
                    {
                        double fuelMidf_s_1_06 = (info.Data[0] - 128) * 100.0 / 128;//气缸1短时燃油修正
                        double fuelMidf_s_3_06 = (info.Data[1] - 128) * 100.0 / 128;//气缸3短时燃油修正
                        //Console.WriteLine("fuelMidf_s_1_06 = " + fuelMidf_s_1_06 + "  fuelMidf_s_3_06 = " + fuelMidf_s_3_06);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x08:  // 瞬时油耗估算
                    if (info.Data.Length == 2)
                    {
                        double fuelMidf_s_2_08 = (info.Data[0] - 128) * 100.0 / 128;//气缸2短时燃油修正
                        double fuelMidf_s_4_08 = (info.Data[1] - 128) * 100.0 / 128;//气缸4短时燃油修正
                        //Console.WriteLine("fuelMidf_s_2_08 = " + fuelMidf_s_2_08 + "  fuelMidf_s_4_08 = " + fuelMidf_s_4_08);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x07:  // 瞬时油耗估算
                    if (info.Data.Length == 2)
                    {
                        double fuelMidf_l_1_07 = (info.Data[0] - 128) * 100.0 / 128;//气缸1短时燃油修正
                        double fuelMidf_l_3_07 = (info.Data[1] - 128) * 100.0 / 128;//气缸3短时燃油修正
                        //Console.WriteLine("fuelMidf_l_1_07 = " + fuelMidf_l_1_07 + "  fuelMidf_l_3_07 = " + fuelMidf_l_3_07);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x09:  // 瞬时油耗估算
                    if (info.Data.Length == 2)
                    {
                        double fuelMidf_l_2_09 = (info.Data[0] - 128) * 100.0 / 128;//气缸2短时燃油修正
                        double fuelMidf_l_4_09 = (info.Data[1] - 128) * 100.0 / 128;//气缸4短时燃油修正
                        //Console.WriteLine("fuelMidf_l_2_09 = " + fuelMidf_l_2_09 + "  fuelMidf_l_4_09 = " + fuelMidf_l_4_09);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x24:  // 瞬时油耗估算
                    if (info.Data.Length == 4)
                    {
                        double lambda_24 = (info.Data[0] * 256 + info.Data[1]) * 0.0000305;// 线性或宽带式氧传感器的等效比(lambda)
                        double lambda_v_24 = (info.Data[2] * 256 + info.Data[3]) * 0.000122;// 线性或宽带式氧传感器的电压
                        //Console.WriteLine("lambda_24 = " + lambda_24 + "  lambda_v_24 = " + lambda_v_24);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x34:  // 瞬时油耗估算
                    if (info.Data.Length == 4)
                    {
                        double lambda_34 = (info.Data[0] * 256 + info.Data[1]) * 0.0000305;// 线性或宽带式氧传感器的等效比(lambda)
                        double lambda_v_34 = (info.Data[2] * 256 + info.Data[3]) * 0.000122;// 线性或宽带式氧传感器的电压
                        //Console.WriteLine("lambda_34 = " + lambda_34 + "  lambda_v_34 = " + lambda_v_34);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x52:  // 瞬时油耗估算 酒精燃料比例
                    if (info.Data.Length == 1)
                    {
                        double AlcoholFuelPercentage = info.Data[0] * 100.0 / 255;
                        //Console.WriteLine("AlcoholFuelPercentage = " + AlcoholFuelPercentage);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                case 0x58:  // 瞬时油耗估算
                    if (info.Data.Length == 2)
                    {
                        double fuelMidf_58_1 = (info.Data[0] - 128) * 100.0 / 128;//长期第二氧传感器燃料修正-列1(short term secondary O2 sensor fuel Trim--Bank2)
                        double fuelMidf_58_2 = (info.Data[1] - 128) * 100.0 / 128;//长期第二氧传感器燃料修正-列1(short term secondary O2 sensor fuel Trim--Bank2)

                        //Console.WriteLine("fuelMidf_58_1 = " + fuelMidf_58_1 + "  fuelMidf_58_2 = " + fuelMidf_58_2);
                    }
                    else
                    {
                        Console.WriteLine("数据长度错误：" + info.Pid.ToString("X2"));
                    }
                    break;
                default:
                    //Console.WriteLine("未处理的PID：" + info.Pid.ToString("X2"));
                    break;
            }
        }

        private double handleFuelClc()
        {
            //((0.85*4.26*29*3600)/(1*8.3145*120*14.3*725))*Q3*F3/(273.5+45)-7
            //((0.85*Vcc*29*3600)/(Vlambda*8.3145*120*Vair*Vfuel))*Vmap*Vrpm/(273.5+Viat)-7
            double fr = ((0.75 * alignData.Vcc * 29 * 3600) / (1 * 8.3145 * 120 * alignData.Vair * alignData.Vfuel)) * alignData.Vjqyl * alignData.N_obd / (273.5 + alignData.Vjqwd) - modifVal;
            if (fr < 0) fr = 0;
            if (fr > 100) fr = 100;
            return fr;
        }

        /**
         * 计算油门开度
         * @return
         */
        private double handleAppClc()
        {
            if (alignData.Vjqyl < mVmap0)
            {
                mVmap0 = (int)alignData.Vjqyl;
            }
            // 油门 = 0.7*(Vmap-Vmap0)+0.3*Load   发动机进气压力 Vmap： 0x0B  发动机负荷 Load: 0x04    Vmap0怠速时的基础进气压力
            double app = 0.7 * (alignData.Vjqyl - mVmap0) + 0.3 * alignData.Vload;
            if (app < 0) app = 0;
            if (app > 100) app = 100;
            return app;
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
            byte[] send_data = new byte[] { 0x02, 0x01, pids[idx++], 0x00, 0x00, 0x00, 0x00, 0x00 };
            sendCanData(new SendObj(send_id, send_data));
            pb_query_progress.Value = idx;
            if (idx == pids.Length)
            {
                idx = 0;
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
            ConfigUtils.setConfigString(ConfigUtils.Sections.OBD_DIAGNOSIS, ConfigUtils.Keys.visible_send_obd_all, cb_visible_send.SelectedIndex.ToString());
            is_visible_send = cb_visible_send.SelectedIndex == 0;
        }

        private void cb_visible_recv_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUtils.setConfigString(ConfigUtils.Sections.OBD_DIAGNOSIS, ConfigUtils.Keys.visible_recv_obd_all, cb_visible_recv.SelectedIndex.ToString());
            is_visible_recv = cb_visible_recv.SelectedIndex == 0;
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
