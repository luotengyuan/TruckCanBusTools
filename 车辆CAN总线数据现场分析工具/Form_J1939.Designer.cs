namespace 车辆CAN总线数据现场分析工具
{
    partial class Form_J1939
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_J1939));
            this.timer_undate_charts = new System.Windows.Forms.Timer(this.components);
            this.chart_0cfe6cee = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn_clean_listview = new System.Windows.Forms.Button();
            this.cb_visible_recv = new System.Windows.Forms.ComboBox();
            this.cb_visible_send = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_id_slt = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lv_obd_all = new System.Windows.Forms.ListView();
            this.pb_query_progress = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_start_query = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.timer_query = new System.Windows.Forms.Timer(this.components);
            this.cb_id_slt_0cfe6cee = new System.Windows.Forms.ComboBox();
            this.chart_0cf00400 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cb_id_slt_0cf00400 = new System.Windows.Forms.ComboBox();
            this.cb_id_slt_0cf00300 = new System.Windows.Forms.ComboBox();
            this.chart_0cf00300 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cb_id_slt_18fef200 = new System.Windows.Forms.ComboBox();
            this.chart_18fec1ee = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lb_period_0cfe6cee = new System.Windows.Forms.Label();
            this.lb_period_0cf00400 = new System.Windows.Forms.Label();
            this.lb_period_0cf00300 = new System.Windows.Forms.Label();
            this.lb_period_18fec1ee = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lb_value_other = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lb_other_id = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_period_other = new System.Windows.Forms.Label();
            this.cb_id_slt_other = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart_0cfe6cee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_0cf00400)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_0cf00300)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_18fec1ee)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart_0cfe6cee
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_0cfe6cee.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_0cfe6cee.Legends.Add(legend1);
            this.chart_0cfe6cee.Location = new System.Drawing.Point(8, 40);
            this.chart_0cfe6cee.Name = "chart_0cfe6cee";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_0cfe6cee.Series.Add(series1);
            this.chart_0cfe6cee.Size = new System.Drawing.Size(520, 94);
            this.chart_0cfe6cee.TabIndex = 1;
            this.chart_0cfe6cee.Text = "chart1";
            // 
            // btn_clean_listview
            // 
            this.btn_clean_listview.Location = new System.Drawing.Point(546, 30);
            this.btn_clean_listview.Name = "btn_clean_listview";
            this.btn_clean_listview.Size = new System.Drawing.Size(79, 23);
            this.btn_clean_listview.TabIndex = 116;
            this.btn_clean_listview.Text = "清除列表";
            this.btn_clean_listview.UseVisualStyleBackColor = true;
            this.btn_clean_listview.Click += new System.EventHandler(this.btn_clean_listview_Click);
            // 
            // cb_visible_recv
            // 
            this.cb_visible_recv.AutoCompleteCustomSource.AddRange(new string[] {
            "是",
            "否"});
            this.cb_visible_recv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_visible_recv.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_visible_recv.FormattingEnabled = true;
            this.cb_visible_recv.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cb_visible_recv.Location = new System.Drawing.Point(495, 32);
            this.cb_visible_recv.Name = "cb_visible_recv";
            this.cb_visible_recv.Size = new System.Drawing.Size(40, 20);
            this.cb_visible_recv.TabIndex = 115;
            this.cb_visible_recv.SelectedIndexChanged += new System.EventHandler(this.cb_visible_recv_SelectedIndexChanged);
            // 
            // cb_visible_send
            // 
            this.cb_visible_send.AutoCompleteCustomSource.AddRange(new string[] {
            "是",
            "否"});
            this.cb_visible_send.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_visible_send.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_visible_send.FormattingEnabled = true;
            this.cb_visible_send.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cb_visible_send.Location = new System.Drawing.Point(393, 32);
            this.cb_visible_send.Name = "cb_visible_send";
            this.cb_visible_send.Size = new System.Drawing.Size(40, 20);
            this.cb_visible_send.TabIndex = 113;
            this.cb_visible_send.SelectedIndexChanged += new System.EventHandler(this.cb_visible_send_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(441, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 114;
            this.label7.Text = "显示接收";
            // 
            // cb_id_slt
            // 
            this.cb_id_slt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_id_slt.FormattingEnabled = true;
            this.cb_id_slt.Items.AddRange(new object[] {
            "所有ID",
            "0x0CFE6CEE（车速转速(必须)）",
            "0x0CF00400（发动机扭矩(必须)）",
            "0x0CF00300（油门(必须)）",
            "0x18FEF200（瞬时油耗(必须)）",
            "0x18FEF100（刹车离合(可选)）",
            "0x18FEDF00（摩擦扭矩百分比(可选)）",
            "0x18FEC1EE（总里程(可选)）",
            "0x18FEE500（发动机运行时间(可选)(查询)）",
            "0x18FEE900（总油耗(可选)(查询)）",
            "0x18EBFF00（参考扭矩(可选)(查询)）",
            "0x0CF00203（电子传动控制1(可选)）",
            "0x18FEE017（总里程(可选)(用于兼容解放J6)）"});
            this.cb_id_slt.Location = new System.Drawing.Point(69, 32);
            this.cb_id_slt.Name = "cb_id_slt";
            this.cb_id_slt.Size = new System.Drawing.Size(257, 20);
            this.cb_id_slt.TabIndex = 111;
            this.cb_id_slt.SelectedIndexChanged += new System.EventHandler(this.cb_id_slt_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 112;
            this.label2.Text = "ID选择：";
            // 
            // lv_obd_all
            // 
            this.lv_obd_all.Location = new System.Drawing.Point(9, 58);
            this.lv_obd_all.Name = "lv_obd_all";
            this.lv_obd_all.Size = new System.Drawing.Size(618, 650);
            this.lv_obd_all.TabIndex = 110;
            this.lv_obd_all.UseCompatibleStateImageBehavior = false;
            // 
            // pb_query_progress
            // 
            this.pb_query_progress.Location = new System.Drawing.Point(69, 8);
            this.pb_query_progress.Name = "pb_query_progress";
            this.pb_query_progress.Size = new System.Drawing.Size(1023, 18);
            this.pb_query_progress.TabIndex = 108;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 109;
            this.label1.Text = "查询进度：";
            // 
            // btn_start_query
            // 
            this.btn_start_query.Location = new System.Drawing.Point(1094, 6);
            this.btn_start_query.Name = "btn_start_query";
            this.btn_start_query.Size = new System.Drawing.Size(75, 23);
            this.btn_start_query.TabIndex = 107;
            this.btn_start_query.Text = "开始查询";
            this.btn_start_query.UseVisualStyleBackColor = true;
            this.btn_start_query.Click += new System.EventHandler(this.btn_start_query_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(340, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 117;
            this.label6.Text = "显示发送";
            // 
            // cb_id_slt_0cfe6cee
            // 
            this.cb_id_slt_0cfe6cee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_id_slt_0cfe6cee.FormattingEnabled = true;
            this.cb_id_slt_0cfe6cee.Items.AddRange(new object[] {
            "所有ID"});
            this.cb_id_slt_0cfe6cee.Location = new System.Drawing.Point(61, 14);
            this.cb_id_slt_0cfe6cee.Name = "cb_id_slt_0cfe6cee";
            this.cb_id_slt_0cfe6cee.Size = new System.Drawing.Size(90, 20);
            this.cb_id_slt_0cfe6cee.TabIndex = 118;
            this.cb_id_slt_0cfe6cee.SelectedIndexChanged += new System.EventHandler(this.cb_id_slt_0cfe6cee_SelectedIndexChanged);
            // 
            // chart_0cf00400
            // 
            chartArea2.Name = "ChartArea1";
            this.chart_0cf00400.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart_0cf00400.Legends.Add(legend2);
            this.chart_0cf00400.Location = new System.Drawing.Point(8, 40);
            this.chart_0cf00400.Name = "chart_0cf00400";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart_0cf00400.Series.Add(series2);
            this.chart_0cf00400.Size = new System.Drawing.Size(521, 94);
            this.chart_0cf00400.TabIndex = 119;
            this.chart_0cf00400.Text = "chart1";
            // 
            // cb_id_slt_0cf00400
            // 
            this.cb_id_slt_0cf00400.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_id_slt_0cf00400.FormattingEnabled = true;
            this.cb_id_slt_0cf00400.Items.AddRange(new object[] {
            "所有ID"});
            this.cb_id_slt_0cf00400.Location = new System.Drawing.Point(61, 14);
            this.cb_id_slt_0cf00400.Name = "cb_id_slt_0cf00400";
            this.cb_id_slt_0cf00400.Size = new System.Drawing.Size(90, 20);
            this.cb_id_slt_0cf00400.TabIndex = 120;
            this.cb_id_slt_0cf00400.SelectedIndexChanged += new System.EventHandler(this.cb_id_slt_0cf00400_SelectedIndexChanged);
            // 
            // cb_id_slt_0cf00300
            // 
            this.cb_id_slt_0cf00300.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_id_slt_0cf00300.FormattingEnabled = true;
            this.cb_id_slt_0cf00300.Items.AddRange(new object[] {
            "所有ID"});
            this.cb_id_slt_0cf00300.Location = new System.Drawing.Point(61, 14);
            this.cb_id_slt_0cf00300.Name = "cb_id_slt_0cf00300";
            this.cb_id_slt_0cf00300.Size = new System.Drawing.Size(90, 20);
            this.cb_id_slt_0cf00300.TabIndex = 122;
            this.cb_id_slt_0cf00300.SelectedIndexChanged += new System.EventHandler(this.cb_id_slt_0cf00300_SelectedIndexChanged);
            // 
            // chart_0cf00300
            // 
            chartArea3.Name = "ChartArea1";
            this.chart_0cf00300.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart_0cf00300.Legends.Add(legend3);
            this.chart_0cf00300.Location = new System.Drawing.Point(8, 40);
            this.chart_0cf00300.Name = "chart_0cf00300";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart_0cf00300.Series.Add(series3);
            this.chart_0cf00300.Size = new System.Drawing.Size(521, 94);
            this.chart_0cf00300.TabIndex = 121;
            this.chart_0cf00300.Text = "chart1";
            // 
            // cb_id_slt_18fef200
            // 
            this.cb_id_slt_18fef200.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_id_slt_18fef200.FormattingEnabled = true;
            this.cb_id_slt_18fef200.Items.AddRange(new object[] {
            "所有ID"});
            this.cb_id_slt_18fef200.Location = new System.Drawing.Point(61, 16);
            this.cb_id_slt_18fef200.Name = "cb_id_slt_18fef200";
            this.cb_id_slt_18fef200.Size = new System.Drawing.Size(90, 20);
            this.cb_id_slt_18fef200.TabIndex = 124;
            this.cb_id_slt_18fef200.SelectedIndexChanged += new System.EventHandler(this.cb_id_slt_18fec1ee_SelectedIndexChanged);
            // 
            // chart_18fec1ee
            // 
            chartArea4.Name = "ChartArea1";
            this.chart_18fec1ee.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart_18fec1ee.Legends.Add(legend4);
            this.chart_18fec1ee.Location = new System.Drawing.Point(8, 42);
            this.chart_18fec1ee.Name = "chart_18fec1ee";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chart_18fec1ee.Series.Add(series4);
            this.chart_18fec1ee.Size = new System.Drawing.Size(521, 92);
            this.chart_18fec1ee.TabIndex = 123;
            this.chart_18fec1ee.Text = "chart1";
            // 
            // lb_period_0cfe6cee
            // 
            this.lb_period_0cfe6cee.AutoSize = true;
            this.lb_period_0cfe6cee.Location = new System.Drawing.Point(223, 17);
            this.lb_period_0cfe6cee.Name = "lb_period_0cfe6cee";
            this.lb_period_0cfe6cee.Size = new System.Drawing.Size(29, 12);
            this.lb_period_0cfe6cee.TabIndex = 125;
            this.lb_period_0cfe6cee.Text = "null";
            // 
            // lb_period_0cf00400
            // 
            this.lb_period_0cf00400.AutoSize = true;
            this.lb_period_0cf00400.Location = new System.Drawing.Point(223, 17);
            this.lb_period_0cf00400.Name = "lb_period_0cf00400";
            this.lb_period_0cf00400.Size = new System.Drawing.Size(29, 12);
            this.lb_period_0cf00400.TabIndex = 126;
            this.lb_period_0cf00400.Text = "null";
            // 
            // lb_period_0cf00300
            // 
            this.lb_period_0cf00300.AutoSize = true;
            this.lb_period_0cf00300.Location = new System.Drawing.Point(223, 19);
            this.lb_period_0cf00300.Name = "lb_period_0cf00300";
            this.lb_period_0cf00300.Size = new System.Drawing.Size(29, 12);
            this.lb_period_0cf00300.TabIndex = 127;
            this.lb_period_0cf00300.Text = "null";
            // 
            // lb_period_18fec1ee
            // 
            this.lb_period_18fec1ee.AutoSize = true;
            this.lb_period_18fec1ee.Location = new System.Drawing.Point(223, 19);
            this.lb_period_18fec1ee.Name = "lb_period_18fec1ee";
            this.lb_period_18fec1ee.Size = new System.Drawing.Size(29, 12);
            this.lb_period_18fec1ee.TabIndex = 128;
            this.lb_period_18fec1ee.Text = "null";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lb_value_other);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lb_other_id);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lb_period_other);
            this.groupBox1.Controls.Add(this.cb_id_slt_other);
            this.groupBox1.Location = new System.Drawing.Point(633, 619);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(535, 89);
            this.groupBox1.TabIndex = 129;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "其他值";
            // 
            // lb_value_other
            // 
            this.lb_value_other.AutoSize = true;
            this.lb_value_other.Location = new System.Drawing.Point(316, 40);
            this.lb_value_other.Name = "lb_value_other";
            this.lb_value_other.Size = new System.Drawing.Size(29, 12);
            this.lb_value_other.TabIndex = 136;
            this.lb_value_other.Text = "null";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(183, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 135;
            this.label9.Text = "周期：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 134;
            this.label8.Text = "ID选择：";
            // 
            // lb_other_id
            // 
            this.lb_other_id.AutoSize = true;
            this.lb_other_id.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_other_id.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_other_id.Location = new System.Drawing.Point(6, 17);
            this.lb_other_id.Name = "lb_other_id";
            this.lb_other_id.Size = new System.Drawing.Size(149, 12);
            this.lb_other_id.TabIndex = 133;
            this.lb_other_id.Text = "0x0CF00300（油门(必须)）";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(291, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 132;
            this.label4.Text = "值：";
            // 
            // lb_period_other
            // 
            this.lb_period_other.AutoSize = true;
            this.lb_period_other.Location = new System.Drawing.Point(223, 40);
            this.lb_period_other.Name = "lb_period_other";
            this.lb_period_other.Size = new System.Drawing.Size(29, 12);
            this.lb_period_other.TabIndex = 130;
            this.lb_period_other.Text = "null";
            // 
            // cb_id_slt_other
            // 
            this.cb_id_slt_other.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_id_slt_other.FormattingEnabled = true;
            this.cb_id_slt_other.Items.AddRange(new object[] {
            "所有ID"});
            this.cb_id_slt_other.Location = new System.Drawing.Point(61, 37);
            this.cb_id_slt_other.Name = "cb_id_slt_other";
            this.cb_id_slt_other.Size = new System.Drawing.Size(90, 20);
            this.cb_id_slt_other.TabIndex = 130;
            this.cb_id_slt_other.SelectedIndexChanged += new System.EventHandler(this.cb_id_slt_other_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.chart_0cfe6cee);
            this.groupBox3.Controls.Add(this.cb_id_slt_0cfe6cee);
            this.groupBox3.Controls.Add(this.lb_period_0cfe6cee);
            this.groupBox3.Location = new System.Drawing.Point(633, 35);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(535, 140);
            this.groupBox3.TabIndex = 138;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "0x0CFE6CEE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 135;
            this.label3.Text = "周期：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 134;
            this.label11.Text = "ID选择：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(223, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 12);
            this.label12.TabIndex = 130;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.chart_0cf00400);
            this.groupBox2.Controls.Add(this.cb_id_slt_0cf00400);
            this.groupBox2.Controls.Add(this.lb_period_0cf00400);
            this.groupBox2.Location = new System.Drawing.Point(633, 181);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(535, 140);
            this.groupBox2.TabIndex = 139;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "0x0CF00400";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 135;
            this.label5.Text = "周期：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 134;
            this.label10.Text = "ID选择：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(223, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 12);
            this.label13.TabIndex = 130;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.chart_0cf00300);
            this.groupBox4.Controls.Add(this.cb_id_slt_0cf00300);
            this.groupBox4.Controls.Add(this.lb_period_0cf00300);
            this.groupBox4.Location = new System.Drawing.Point(633, 327);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(535, 140);
            this.groupBox4.TabIndex = 140;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "0x0CF00300";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(183, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 135;
            this.label14.Text = "周期：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 134;
            this.label15.Text = "ID选择：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(223, 50);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(0, 12);
            this.label16.TabIndex = 130;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.chart_18fec1ee);
            this.groupBox5.Controls.Add(this.cb_id_slt_18fef200);
            this.groupBox5.Controls.Add(this.lb_period_18fec1ee);
            this.groupBox5.Location = new System.Drawing.Point(633, 473);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(535, 140);
            this.groupBox5.TabIndex = 141;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "0x18FEC1EE";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(183, 19);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 135;
            this.label17.Text = "周期：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 19);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 134;
            this.label18.Text = "ID选择：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(223, 50);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(0, 12);
            this.label19.TabIndex = 130;
            // 
            // Form_J1939
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 712);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_clean_listview);
            this.Controls.Add(this.cb_visible_recv);
            this.Controls.Add(this.cb_visible_send);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cb_id_slt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lv_obd_all);
            this.Controls.Add(this.pb_query_progress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_start_query);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_J1939";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "J1939协议适配";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_J1939_FormClosed);
            this.Load += new System.EventHandler(this.Form_J1939_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_0cfe6cee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_0cf00400)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_0cf00300)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_18fec1ee)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer_undate_charts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_0cfe6cee;
        private System.Windows.Forms.Button btn_clean_listview;
        private System.Windows.Forms.ComboBox cb_visible_recv;
        private System.Windows.Forms.ComboBox cb_visible_send;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_id_slt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lv_obd_all;
        private System.Windows.Forms.ProgressBar pb_query_progress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_start_query;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer_query;
        private System.Windows.Forms.ComboBox cb_id_slt_0cfe6cee;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_0cf00400;
        private System.Windows.Forms.ComboBox cb_id_slt_0cf00400;
        private System.Windows.Forms.ComboBox cb_id_slt_0cf00300;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_0cf00300;
        private System.Windows.Forms.ComboBox cb_id_slt_18fef200;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_18fec1ee;
        private System.Windows.Forms.Label lb_period_0cfe6cee;
        private System.Windows.Forms.Label lb_period_0cf00400;
        private System.Windows.Forms.Label lb_period_0cf00300;
        private System.Windows.Forms.Label lb_period_18fec1ee;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lb_value_other;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lb_other_id;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_period_other;
        private System.Windows.Forms.ComboBox cb_id_slt_other;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;

    }
}