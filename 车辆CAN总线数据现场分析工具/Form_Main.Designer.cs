namespace 车辆CAN总线数据现场分析工具
{
    partial class Form_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.mySkinEngine = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cb_rate_2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_on_off = new System.Windows.Forms.Button();
            this.cb_rate_1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_devtype = new System.Windows.Forms.ComboBox();
            this.cb_devidx = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tc_can_channel = new System.Windows.Forms.TabControl();
            this.tp_can_channel1 = new System.Windows.Forms.TabPage();
            this.lb_err_count_1 = new System.Windows.Forms.Label();
            this.cb_visible_err_1 = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.btn_obd_adapter_1 = new System.Windows.Forms.Button();
            this.btn_obd_all_1 = new System.Windows.Forms.Button();
            this.btn_j1939_adapter_1 = new System.Windows.Forms.Button();
            this.cb_frame_format_1 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cb_frame_type_1 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cb_send_format_1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_send_1 = new System.Windows.Forms.Button();
            this.tb_send_data_1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_send_id_1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lv_can_data_1 = new System.Windows.Forms.ListView();
            this.lb_count_1 = new System.Windows.Forms.Label();
            this.btn_clean_1 = new System.Windows.Forms.Button();
            this.cb_visible_recv_1 = new System.Windows.Forms.ComboBox();
            this.cb_visible_send_1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tp_can_channel2 = new System.Windows.Forms.TabPage();
            this.lb_err_count_2 = new System.Windows.Forms.Label();
            this.btn_obd_adapter_2 = new System.Windows.Forms.Button();
            this.btn_j1939_adapter_2 = new System.Windows.Forms.Button();
            this.cb_visible_err_2 = new System.Windows.Forms.ComboBox();
            this.btn_obd_all_2 = new System.Windows.Forms.Button();
            this.cb_frame_format_2 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cb_frame_type_2 = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cb_send_format_2 = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btn_send_2 = new System.Windows.Forms.Button();
            this.tb_send_data_2 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tb_send_id_2 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.lv_can_data_2 = new System.Windows.Forms.ListView();
            this.lb_count_2 = new System.Windows.Forms.Label();
            this.btn_clean_2 = new System.Windows.Forms.Button();
            this.cb_visible_recv_2 = new System.Windows.Forms.ComboBox();
            this.cb_visible_send_2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_open_app_dir = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.tc_can_channel.SuspendLayout();
            this.tp_can_channel1.SuspendLayout();
            this.tp_can_channel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mySkinEngine
            // 
            this.mySkinEngine.SerialNumber = "";
            this.mySkinEngine.SkinFile = null;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.cb_rate_2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.btn_on_off);
            this.groupBox3.Controls.Add(this.cb_rate_1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cb_devtype);
            this.groupBox3.Controls.Add(this.cb_devidx);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(779, 60);
            this.groupBox3.TabIndex = 87;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "打开CAN通道";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(499, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 12);
            this.label11.TabIndex = 14;
            this.label11.Text = "通道2波特率";
            // 
            // cb_rate_2
            // 
            this.cb_rate_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_rate_2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_rate_2.FormattingEnabled = true;
            this.cb_rate_2.Location = new System.Drawing.Point(574, 24);
            this.cb_rate_2.Name = "cb_rate_2";
            this.cb_rate_2.Size = new System.Drawing.Size(72, 20);
            this.cb_rate_2.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(333, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "通道1波特率";
            // 
            // btn_on_off
            // 
            this.btn_on_off.Location = new System.Drawing.Point(674, 22);
            this.btn_on_off.Name = "btn_on_off";
            this.btn_on_off.Size = new System.Drawing.Size(75, 23);
            this.btn_on_off.TabIndex = 75;
            this.btn_on_off.Text = "开启";
            this.btn_on_off.UseVisualStyleBackColor = true;
            this.btn_on_off.Click += new System.EventHandler(this.btn_on_off_Click);
            // 
            // cb_rate_1
            // 
            this.cb_rate_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_rate_1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_rate_1.FormattingEnabled = true;
            this.cb_rate_1.Location = new System.Drawing.Point(408, 24);
            this.cb_rate_1.Name = "cb_rate_1";
            this.cb_rate_1.Size = new System.Drawing.Size(72, 20);
            this.cb_rate_1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "设备索引";
            // 
            // cb_devtype
            // 
            this.cb_devtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_devtype.Enabled = false;
            this.cb_devtype.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_devtype.FormattingEnabled = true;
            this.cb_devtype.Items.AddRange(new object[] {
            "DEV_USBCAN",
            "DEV_USBCAN2"});
            this.cb_devtype.Location = new System.Drawing.Point(67, 24);
            this.cb_devtype.Name = "cb_devtype";
            this.cb_devtype.Size = new System.Drawing.Size(121, 20);
            this.cb_devtype.TabIndex = 11;
            // 
            // cb_devidx
            // 
            this.cb_devidx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_devidx.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_devidx.FormattingEnabled = true;
            this.cb_devidx.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cb_devidx.Location = new System.Drawing.Point(265, 24);
            this.cb_devidx.Name = "cb_devidx";
            this.cb_devidx.Size = new System.Drawing.Size(50, 20);
            this.cb_devidx.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "设备类型";
            // 
            // tc_can_channel
            // 
            this.tc_can_channel.Controls.Add(this.tp_can_channel1);
            this.tc_can_channel.Controls.Add(this.tp_can_channel2);
            this.tc_can_channel.ItemSize = new System.Drawing.Size(453, 20);
            this.tc_can_channel.Location = new System.Drawing.Point(12, 78);
            this.tc_can_channel.Multiline = true;
            this.tc_can_channel.Name = "tc_can_channel";
            this.tc_can_channel.SelectedIndex = 0;
            this.tc_can_channel.ShowToolTips = true;
            this.tc_can_channel.Size = new System.Drawing.Size(912, 499);
            this.tc_can_channel.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tc_can_channel.TabIndex = 88;
            // 
            // tp_can_channel1
            // 
            this.tp_can_channel1.Controls.Add(this.lb_err_count_1);
            this.tp_can_channel1.Controls.Add(this.cb_visible_err_1);
            this.tp_can_channel1.Controls.Add(this.label20);
            this.tp_can_channel1.Controls.Add(this.btn_obd_adapter_1);
            this.tp_can_channel1.Controls.Add(this.btn_obd_all_1);
            this.tp_can_channel1.Controls.Add(this.btn_j1939_adapter_1);
            this.tp_can_channel1.Controls.Add(this.cb_frame_format_1);
            this.tp_can_channel1.Controls.Add(this.label12);
            this.tp_can_channel1.Controls.Add(this.cb_frame_type_1);
            this.tp_can_channel1.Controls.Add(this.label13);
            this.tp_can_channel1.Controls.Add(this.cb_send_format_1);
            this.tp_can_channel1.Controls.Add(this.label8);
            this.tp_can_channel1.Controls.Add(this.btn_send_1);
            this.tp_can_channel1.Controls.Add(this.tb_send_data_1);
            this.tp_can_channel1.Controls.Add(this.label5);
            this.tp_can_channel1.Controls.Add(this.tb_send_id_1);
            this.tp_can_channel1.Controls.Add(this.label3);
            this.tp_can_channel1.Controls.Add(this.lv_can_data_1);
            this.tp_can_channel1.Controls.Add(this.lb_count_1);
            this.tp_can_channel1.Controls.Add(this.btn_clean_1);
            this.tp_can_channel1.Controls.Add(this.cb_visible_recv_1);
            this.tp_can_channel1.Controls.Add(this.cb_visible_send_1);
            this.tp_can_channel1.Controls.Add(this.label6);
            this.tp_can_channel1.Controls.Add(this.label7);
            this.tp_can_channel1.Location = new System.Drawing.Point(4, 24);
            this.tp_can_channel1.Name = "tp_can_channel1";
            this.tp_can_channel1.Padding = new System.Windows.Forms.Padding(3);
            this.tp_can_channel1.Size = new System.Drawing.Size(904, 471);
            this.tp_can_channel1.TabIndex = 0;
            this.tp_can_channel1.Text = "通道1";
            this.tp_can_channel1.UseVisualStyleBackColor = true;
            // 
            // lb_err_count_1
            // 
            this.lb_err_count_1.AutoSize = true;
            this.lb_err_count_1.Location = new System.Drawing.Point(480, 10);
            this.lb_err_count_1.Name = "lb_err_count_1";
            this.lb_err_count_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lb_err_count_1.Size = new System.Drawing.Size(11, 12);
            this.lb_err_count_1.TabIndex = 110;
            this.lb_err_count_1.Text = "0";
            this.lb_err_count_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_visible_err_1
            // 
            this.cb_visible_err_1.AutoCompleteCustomSource.AddRange(new string[] {
            "是",
            "否"});
            this.cb_visible_err_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_visible_err_1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_visible_err_1.FormattingEnabled = true;
            this.cb_visible_err_1.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cb_visible_err_1.Location = new System.Drawing.Point(427, 7);
            this.cb_visible_err_1.Name = "cb_visible_err_1";
            this.cb_visible_err_1.Size = new System.Drawing.Size(40, 20);
            this.cb_visible_err_1.TabIndex = 109;
            this.cb_visible_err_1.SelectedIndexChanged += new System.EventHandler(this.cb_visible_err_1_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(367, 10);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 12);
            this.label20.TabIndex = 108;
            this.label20.Text = "显示错误";
            // 
            // btn_obd_adapter_1
            // 
            this.btn_obd_adapter_1.Location = new System.Drawing.Point(601, 5);
            this.btn_obd_adapter_1.Name = "btn_obd_adapter_1";
            this.btn_obd_adapter_1.Size = new System.Drawing.Size(95, 23);
            this.btn_obd_adapter_1.TabIndex = 107;
            this.btn_obd_adapter_1.Text = "OBD协议适配";
            this.btn_obd_adapter_1.UseVisualStyleBackColor = true;
            this.btn_obd_adapter_1.Click += new System.EventHandler(this.btn_obd_adapter_1_Click);
            // 
            // btn_obd_all_1
            // 
            this.btn_obd_all_1.Location = new System.Drawing.Point(526, 5);
            this.btn_obd_all_1.Name = "btn_obd_all_1";
            this.btn_obd_all_1.Size = new System.Drawing.Size(69, 23);
            this.btn_obd_all_1.TabIndex = 106;
            this.btn_obd_all_1.Text = "OBD查询";
            this.btn_obd_all_1.UseVisualStyleBackColor = true;
            this.btn_obd_all_1.Click += new System.EventHandler(this.btn_obd_all_1_Click);
            // 
            // btn_j1939_adapter_1
            // 
            this.btn_j1939_adapter_1.Location = new System.Drawing.Point(702, 5);
            this.btn_j1939_adapter_1.Name = "btn_j1939_adapter_1";
            this.btn_j1939_adapter_1.Size = new System.Drawing.Size(115, 23);
            this.btn_j1939_adapter_1.TabIndex = 105;
            this.btn_j1939_adapter_1.Text = "J1939协议适配";
            this.btn_j1939_adapter_1.UseVisualStyleBackColor = true;
            this.btn_j1939_adapter_1.Click += new System.EventHandler(this.btn_j1939_adapter_1_Click);
            // 
            // cb_frame_format_1
            // 
            this.cb_frame_format_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_frame_format_1.Enabled = false;
            this.cb_frame_format_1.FormattingEnabled = true;
            this.cb_frame_format_1.Items.AddRange(new object[] {
            "数据帧",
            "远程帧"});
            this.cb_frame_format_1.Location = new System.Drawing.Point(374, 441);
            this.cb_frame_format_1.Name = "cb_frame_format_1";
            this.cb_frame_format_1.Size = new System.Drawing.Size(74, 20);
            this.cb_frame_format_1.TabIndex = 104;
            this.cb_frame_format_1.SelectedIndexChanged += new System.EventHandler(this.cb_frame_format_1_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(327, 449);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 103;
            this.label12.Text = "帧格式:";
            // 
            // cb_frame_type_1
            // 
            this.cb_frame_type_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_frame_type_1.Enabled = false;
            this.cb_frame_type_1.FormattingEnabled = true;
            this.cb_frame_type_1.Items.AddRange(new object[] {
            "标准帧",
            "扩展帧"});
            this.cb_frame_type_1.Location = new System.Drawing.Point(224, 441);
            this.cb_frame_type_1.Name = "cb_frame_type_1";
            this.cb_frame_type_1.Size = new System.Drawing.Size(74, 20);
            this.cb_frame_type_1.TabIndex = 102;
            this.cb_frame_type_1.SelectedIndexChanged += new System.EventHandler(this.cb_frame_type_1_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(178, 449);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 101;
            this.label13.Text = "帧类型:";
            // 
            // cb_send_format_1
            // 
            this.cb_send_format_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_send_format_1.Enabled = false;
            this.cb_send_format_1.FormattingEnabled = true;
            this.cb_send_format_1.Items.AddRange(new object[] {
            "正常发送",
            "自发自收"});
            this.cb_send_format_1.Location = new System.Drawing.Point(65, 441);
            this.cb_send_format_1.Name = "cb_send_format_1";
            this.cb_send_format_1.Size = new System.Drawing.Size(93, 20);
            this.cb_send_format_1.TabIndex = 100;
            this.cb_send_format_1.SelectedIndexChanged += new System.EventHandler(this.cb_send_format_1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 449);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 96;
            this.label8.Text = "发送格式:";
            // 
            // btn_send_1
            // 
            this.btn_send_1.Location = new System.Drawing.Point(823, 439);
            this.btn_send_1.Name = "btn_send_1";
            this.btn_send_1.Size = new System.Drawing.Size(75, 23);
            this.btn_send_1.TabIndex = 95;
            this.btn_send_1.Text = "发送";
            this.btn_send_1.UseVisualStyleBackColor = true;
            this.btn_send_1.Click += new System.EventHandler(this.btn_send_1_Click);
            // 
            // tb_send_data_1
            // 
            this.tb_send_data_1.Location = new System.Drawing.Point(639, 441);
            this.tb_send_data_1.Name = "tb_send_data_1";
            this.tb_send_data_1.Size = new System.Drawing.Size(163, 21);
            this.tb_send_data_1.TabIndex = 94;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(605, 449);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 93;
            this.label5.Text = "DATA:";
            // 
            // tb_send_id_1
            // 
            this.tb_send_id_1.Location = new System.Drawing.Point(517, 441);
            this.tb_send_id_1.Name = "tb_send_id_1";
            this.tb_send_id_1.Size = new System.Drawing.Size(65, 21);
            this.tb_send_id_1.TabIndex = 92;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(476, 449);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 91;
            this.label3.Text = "ID: 0x";
            // 
            // lv_can_data_1
            // 
            this.lv_can_data_1.Location = new System.Drawing.Point(8, 33);
            this.lv_can_data_1.Name = "lv_can_data_1";
            this.lv_can_data_1.Size = new System.Drawing.Size(890, 402);
            this.lv_can_data_1.TabIndex = 90;
            this.lv_can_data_1.UseCompatibleStateImageBehavior = false;
            // 
            // lb_count_1
            // 
            this.lb_count_1.AutoSize = true;
            this.lb_count_1.Location = new System.Drawing.Point(265, 10);
            this.lb_count_1.Name = "lb_count_1";
            this.lb_count_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lb_count_1.Size = new System.Drawing.Size(47, 12);
            this.lb_count_1.TabIndex = 89;
            this.lb_count_1.Text = "收0/发0";
            this.lb_count_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_clean_1
            // 
            this.btn_clean_1.Location = new System.Drawing.Point(823, 5);
            this.btn_clean_1.Name = "btn_clean_1";
            this.btn_clean_1.Size = new System.Drawing.Size(75, 23);
            this.btn_clean_1.TabIndex = 88;
            this.btn_clean_1.Text = "清空";
            this.btn_clean_1.UseVisualStyleBackColor = true;
            this.btn_clean_1.Click += new System.EventHandler(this.btn_clean_1_Click);
            // 
            // cb_visible_recv_1
            // 
            this.cb_visible_recv_1.AutoCompleteCustomSource.AddRange(new string[] {
            "是",
            "否"});
            this.cb_visible_recv_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_visible_recv_1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_visible_recv_1.FormattingEnabled = true;
            this.cb_visible_recv_1.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cb_visible_recv_1.Location = new System.Drawing.Point(187, 7);
            this.cb_visible_recv_1.Name = "cb_visible_recv_1";
            this.cb_visible_recv_1.Size = new System.Drawing.Size(40, 20);
            this.cb_visible_recv_1.TabIndex = 87;
            this.cb_visible_recv_1.SelectedIndexChanged += new System.EventHandler(this.cb_visible_recv_1_SelectedIndexChanged);
            // 
            // cb_visible_send_1
            // 
            this.cb_visible_send_1.AutoCompleteCustomSource.AddRange(new string[] {
            "是",
            "否"});
            this.cb_visible_send_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_visible_send_1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_visible_send_1.FormattingEnabled = true;
            this.cb_visible_send_1.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cb_visible_send_1.Location = new System.Drawing.Point(65, 7);
            this.cb_visible_send_1.Name = "cb_visible_send_1";
            this.cb_visible_send_1.Size = new System.Drawing.Size(40, 20);
            this.cb_visible_send_1.TabIndex = 85;
            this.cb_visible_send_1.SelectedIndexChanged += new System.EventHandler(this.cb_visible_send_1_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 84;
            this.label6.Text = "显示发送";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(127, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 86;
            this.label7.Text = "显示接收";
            // 
            // tp_can_channel2
            // 
            this.tp_can_channel2.Controls.Add(this.lb_err_count_2);
            this.tp_can_channel2.Controls.Add(this.btn_obd_adapter_2);
            this.tp_can_channel2.Controls.Add(this.btn_j1939_adapter_2);
            this.tp_can_channel2.Controls.Add(this.cb_visible_err_2);
            this.tp_can_channel2.Controls.Add(this.btn_obd_all_2);
            this.tp_can_channel2.Controls.Add(this.cb_frame_format_2);
            this.tp_can_channel2.Controls.Add(this.label21);
            this.tp_can_channel2.Controls.Add(this.label14);
            this.tp_can_channel2.Controls.Add(this.cb_frame_type_2);
            this.tp_can_channel2.Controls.Add(this.label15);
            this.tp_can_channel2.Controls.Add(this.cb_send_format_2);
            this.tp_can_channel2.Controls.Add(this.label16);
            this.tp_can_channel2.Controls.Add(this.btn_send_2);
            this.tp_can_channel2.Controls.Add(this.tb_send_data_2);
            this.tp_can_channel2.Controls.Add(this.label17);
            this.tp_can_channel2.Controls.Add(this.tb_send_id_2);
            this.tp_can_channel2.Controls.Add(this.label18);
            this.tp_can_channel2.Controls.Add(this.lv_can_data_2);
            this.tp_can_channel2.Controls.Add(this.lb_count_2);
            this.tp_can_channel2.Controls.Add(this.btn_clean_2);
            this.tp_can_channel2.Controls.Add(this.cb_visible_recv_2);
            this.tp_can_channel2.Controls.Add(this.cb_visible_send_2);
            this.tp_can_channel2.Controls.Add(this.label9);
            this.tp_can_channel2.Controls.Add(this.label10);
            this.tp_can_channel2.Location = new System.Drawing.Point(4, 24);
            this.tp_can_channel2.Name = "tp_can_channel2";
            this.tp_can_channel2.Padding = new System.Windows.Forms.Padding(3);
            this.tp_can_channel2.Size = new System.Drawing.Size(904, 471);
            this.tp_can_channel2.TabIndex = 1;
            this.tp_can_channel2.Text = "通道2";
            this.tp_can_channel2.UseVisualStyleBackColor = true;
            // 
            // lb_err_count_2
            // 
            this.lb_err_count_2.AutoSize = true;
            this.lb_err_count_2.Location = new System.Drawing.Point(480, 10);
            this.lb_err_count_2.Name = "lb_err_count_2";
            this.lb_err_count_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lb_err_count_2.Size = new System.Drawing.Size(11, 12);
            this.lb_err_count_2.TabIndex = 113;
            this.lb_err_count_2.Text = "0";
            this.lb_err_count_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_obd_adapter_2
            // 
            this.btn_obd_adapter_2.Location = new System.Drawing.Point(601, 5);
            this.btn_obd_adapter_2.Name = "btn_obd_adapter_2";
            this.btn_obd_adapter_2.Size = new System.Drawing.Size(95, 23);
            this.btn_obd_adapter_2.TabIndex = 109;
            this.btn_obd_adapter_2.Text = "OBD协议适配";
            this.btn_obd_adapter_2.UseVisualStyleBackColor = true;
            this.btn_obd_adapter_2.Click += new System.EventHandler(this.btn_obd_adapter_2_Click);
            // 
            // btn_j1939_adapter_2
            // 
            this.btn_j1939_adapter_2.Location = new System.Drawing.Point(702, 5);
            this.btn_j1939_adapter_2.Name = "btn_j1939_adapter_2";
            this.btn_j1939_adapter_2.Size = new System.Drawing.Size(115, 23);
            this.btn_j1939_adapter_2.TabIndex = 108;
            this.btn_j1939_adapter_2.Text = "J1939协议适配";
            this.btn_j1939_adapter_2.UseVisualStyleBackColor = true;
            this.btn_j1939_adapter_2.Click += new System.EventHandler(this.btn_j1939_adapter_2_Click);
            // 
            // cb_visible_err_2
            // 
            this.cb_visible_err_2.AutoCompleteCustomSource.AddRange(new string[] {
            "是",
            "否"});
            this.cb_visible_err_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_visible_err_2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_visible_err_2.FormattingEnabled = true;
            this.cb_visible_err_2.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cb_visible_err_2.Location = new System.Drawing.Point(427, 7);
            this.cb_visible_err_2.Name = "cb_visible_err_2";
            this.cb_visible_err_2.Size = new System.Drawing.Size(40, 20);
            this.cb_visible_err_2.TabIndex = 112;
            this.cb_visible_err_2.SelectedIndexChanged += new System.EventHandler(this.cb_visible_err_2_SelectedIndexChanged);
            // 
            // btn_obd_all_2
            // 
            this.btn_obd_all_2.Location = new System.Drawing.Point(526, 5);
            this.btn_obd_all_2.Name = "btn_obd_all_2";
            this.btn_obd_all_2.Size = new System.Drawing.Size(69, 23);
            this.btn_obd_all_2.TabIndex = 107;
            this.btn_obd_all_2.Text = "OBD查询";
            this.btn_obd_all_2.UseVisualStyleBackColor = true;
            this.btn_obd_all_2.Click += new System.EventHandler(this.btn_obd_all_2_Click);
            // 
            // cb_frame_format_2
            // 
            this.cb_frame_format_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_frame_format_2.Enabled = false;
            this.cb_frame_format_2.FormattingEnabled = true;
            this.cb_frame_format_2.Items.AddRange(new object[] {
            "数据帧",
            "远程帧"});
            this.cb_frame_format_2.Location = new System.Drawing.Point(374, 441);
            this.cb_frame_format_2.Name = "cb_frame_format_2";
            this.cb_frame_format_2.Size = new System.Drawing.Size(74, 20);
            this.cb_frame_format_2.TabIndex = 115;
            this.cb_frame_format_2.SelectedIndexChanged += new System.EventHandler(this.cb_frame_format_2_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(367, 10);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 12);
            this.label21.TabIndex = 111;
            this.label21.Text = "显示错误";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(327, 449);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 12);
            this.label14.TabIndex = 114;
            this.label14.Text = "帧格式:";
            // 
            // cb_frame_type_2
            // 
            this.cb_frame_type_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_frame_type_2.Enabled = false;
            this.cb_frame_type_2.FormattingEnabled = true;
            this.cb_frame_type_2.Items.AddRange(new object[] {
            "标准帧",
            "扩展帧"});
            this.cb_frame_type_2.Location = new System.Drawing.Point(224, 441);
            this.cb_frame_type_2.Name = "cb_frame_type_2";
            this.cb_frame_type_2.Size = new System.Drawing.Size(74, 20);
            this.cb_frame_type_2.TabIndex = 113;
            this.cb_frame_type_2.SelectedIndexChanged += new System.EventHandler(this.cb_frame_type_2_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(178, 449);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 12);
            this.label15.TabIndex = 112;
            this.label15.Text = "帧类型:";
            // 
            // cb_send_format_2
            // 
            this.cb_send_format_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_send_format_2.Enabled = false;
            this.cb_send_format_2.FormattingEnabled = true;
            this.cb_send_format_2.Items.AddRange(new object[] {
            "正常发送",
            "自发自收"});
            this.cb_send_format_2.Location = new System.Drawing.Point(65, 441);
            this.cb_send_format_2.Name = "cb_send_format_2";
            this.cb_send_format_2.Size = new System.Drawing.Size(93, 20);
            this.cb_send_format_2.TabIndex = 111;
            this.cb_send_format_2.SelectedIndexChanged += new System.EventHandler(this.cb_send_format_2_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 449);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 12);
            this.label16.TabIndex = 110;
            this.label16.Text = "发送格式:";
            // 
            // btn_send_2
            // 
            this.btn_send_2.Location = new System.Drawing.Point(823, 439);
            this.btn_send_2.Name = "btn_send_2";
            this.btn_send_2.Size = new System.Drawing.Size(75, 23);
            this.btn_send_2.TabIndex = 109;
            this.btn_send_2.Text = "发送";
            this.btn_send_2.UseVisualStyleBackColor = true;
            this.btn_send_2.Click += new System.EventHandler(this.btn_send_2_Click);
            // 
            // tb_send_data_2
            // 
            this.tb_send_data_2.Location = new System.Drawing.Point(639, 441);
            this.tb_send_data_2.Name = "tb_send_data_2";
            this.tb_send_data_2.Size = new System.Drawing.Size(163, 21);
            this.tb_send_data_2.TabIndex = 108;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(605, 449);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 12);
            this.label17.TabIndex = 107;
            this.label17.Text = "DATA:";
            // 
            // tb_send_id_2
            // 
            this.tb_send_id_2.Location = new System.Drawing.Point(517, 441);
            this.tb_send_id_2.Name = "tb_send_id_2";
            this.tb_send_id_2.Size = new System.Drawing.Size(65, 21);
            this.tb_send_id_2.TabIndex = 106;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(476, 449);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 105;
            this.label18.Text = "ID: 0x";
            // 
            // lv_can_data_2
            // 
            this.lv_can_data_2.Location = new System.Drawing.Point(8, 33);
            this.lv_can_data_2.Name = "lv_can_data_2";
            this.lv_can_data_2.Size = new System.Drawing.Size(890, 402);
            this.lv_can_data_2.TabIndex = 90;
            this.lv_can_data_2.UseCompatibleStateImageBehavior = false;
            // 
            // lb_count_2
            // 
            this.lb_count_2.AutoSize = true;
            this.lb_count_2.Location = new System.Drawing.Point(265, 10);
            this.lb_count_2.Name = "lb_count_2";
            this.lb_count_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lb_count_2.Size = new System.Drawing.Size(47, 12);
            this.lb_count_2.TabIndex = 89;
            this.lb_count_2.Text = "收0/发0";
            this.lb_count_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_clean_2
            // 
            this.btn_clean_2.Location = new System.Drawing.Point(823, 5);
            this.btn_clean_2.Name = "btn_clean_2";
            this.btn_clean_2.Size = new System.Drawing.Size(75, 23);
            this.btn_clean_2.TabIndex = 88;
            this.btn_clean_2.Text = "清空";
            this.btn_clean_2.UseVisualStyleBackColor = true;
            this.btn_clean_2.Click += new System.EventHandler(this.btn_clean_2_Click);
            // 
            // cb_visible_recv_2
            // 
            this.cb_visible_recv_2.AutoCompleteCustomSource.AddRange(new string[] {
            "是",
            "否"});
            this.cb_visible_recv_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_visible_recv_2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_visible_recv_2.FormattingEnabled = true;
            this.cb_visible_recv_2.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cb_visible_recv_2.Location = new System.Drawing.Point(187, 7);
            this.cb_visible_recv_2.Name = "cb_visible_recv_2";
            this.cb_visible_recv_2.Size = new System.Drawing.Size(40, 20);
            this.cb_visible_recv_2.TabIndex = 87;
            this.cb_visible_recv_2.SelectedIndexChanged += new System.EventHandler(this.cb_visible_recv_2_SelectedIndexChanged);
            // 
            // cb_visible_send_2
            // 
            this.cb_visible_send_2.AutoCompleteCustomSource.AddRange(new string[] {
            "是",
            "否"});
            this.cb_visible_send_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_visible_send_2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_visible_send_2.FormattingEnabled = true;
            this.cb_visible_send_2.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cb_visible_send_2.Location = new System.Drawing.Point(65, 7);
            this.cb_visible_send_2.Name = "cb_visible_send_2";
            this.cb_visible_send_2.Size = new System.Drawing.Size(40, 20);
            this.cb_visible_send_2.TabIndex = 85;
            this.cb_visible_send_2.SelectedIndexChanged += new System.EventHandler(this.cb_visible_send_2_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 84;
            this.label9.Text = "显示发送";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(127, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 86;
            this.label10.Text = "显示接收";
            // 
            // btn_open_app_dir
            // 
            this.btn_open_app_dir.Location = new System.Drawing.Point(797, 19);
            this.btn_open_app_dir.Name = "btn_open_app_dir";
            this.btn_open_app_dir.Size = new System.Drawing.Size(117, 53);
            this.btn_open_app_dir.TabIndex = 76;
            this.btn_open_app_dir.Text = "打开程序目录";
            this.btn_open_app_dir.UseVisualStyleBackColor = true;
            this.btn_open_app_dir.Click += new System.EventHandler(this.btn_open_app_dir_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 589);
            this.Controls.Add(this.btn_open_app_dir);
            this.Controls.Add(this.tc_can_channel);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "车辆CAN总线数据现场分析工具";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Main_FormClosed);
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tc_can_channel.ResumeLayout(false);
            this.tp_can_channel1.ResumeLayout(false);
            this.tp_can_channel1.PerformLayout();
            this.tp_can_channel2.ResumeLayout(false);
            this.tp_can_channel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunisoft.IrisSkin.SkinEngine mySkinEngine;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.ComboBox cb_rate_2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_on_off;
        public System.Windows.Forms.ComboBox cb_rate_1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_devtype;
        private System.Windows.Forms.ComboBox cb_devidx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tc_can_channel;
        private System.Windows.Forms.TabPage tp_can_channel1;
        private System.Windows.Forms.TabPage tp_can_channel2;
        private System.Windows.Forms.ListView lv_can_data_1;
        private System.Windows.Forms.Label lb_count_1;
        private System.Windows.Forms.Button btn_clean_1;
        private System.Windows.Forms.ComboBox cb_visible_recv_1;
        private System.Windows.Forms.ComboBox cb_visible_send_1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView lv_can_data_2;
        private System.Windows.Forms.Label lb_count_2;
        private System.Windows.Forms.Button btn_clean_2;
        private System.Windows.Forms.ComboBox cb_visible_recv_2;
        private System.Windows.Forms.ComboBox cb_visible_send_2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_send_1;
        private System.Windows.Forms.TextBox tb_send_data_1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_send_id_1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_frame_format_1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cb_frame_type_1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cb_send_format_1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_frame_format_2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cb_frame_type_2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cb_send_format_2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btn_send_2;
        private System.Windows.Forms.TextBox tb_send_data_2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tb_send_id_2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btn_j1939_adapter_1;
        private System.Windows.Forms.Button btn_obd_all_1;
        private System.Windows.Forms.Button btn_obd_adapter_1;
        private System.Windows.Forms.Button btn_obd_adapter_2;
        private System.Windows.Forms.Button btn_j1939_adapter_2;
        private System.Windows.Forms.Button btn_obd_all_2;
        private System.Windows.Forms.Label lb_err_count_1;
        private System.Windows.Forms.ComboBox cb_visible_err_1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lb_err_count_2;
        private System.Windows.Forms.ComboBox cb_visible_err_2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btn_open_app_dir;
    }
}

