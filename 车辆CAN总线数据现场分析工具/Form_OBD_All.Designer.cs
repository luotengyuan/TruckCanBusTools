namespace 车辆CAN总线数据现场分析工具
{
    partial class Form_OBD_All
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_OBD_All));
            this.btn_start_query = new System.Windows.Forms.Button();
            this.pb_query_progress = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lv_obd_all = new System.Windows.Forms.ListView();
            this.timer_query = new System.Windows.Forms.Timer(this.components);
            this.cb_id_slt = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_visible_recv = new System.Windows.Forms.ComboBox();
            this.cb_visible_send = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_query_cur_dtc = new System.Windows.Forms.Button();
            this.btn_query_his_dtc = new System.Windows.Forms.Button();
            this.btn_clean_his_dtc = new System.Windows.Forms.Button();
            this.btn_clean_listview = new System.Windows.Forms.Button();
            this.tb_cur_dtc = new System.Windows.Forms.TextBox();
            this.tb_his_dtc = new System.Windows.Forms.TextBox();
            this.tb_clean_his_dtc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_start_query
            // 
            this.btn_start_query.Location = new System.Drawing.Point(972, 12);
            this.btn_start_query.Name = "btn_start_query";
            this.btn_start_query.Size = new System.Drawing.Size(75, 23);
            this.btn_start_query.TabIndex = 0;
            this.btn_start_query.Text = "开始查询";
            this.btn_start_query.UseVisualStyleBackColor = true;
            this.btn_start_query.Click += new System.EventHandler(this.btn_start_query_Click);
            // 
            // pb_query_progress
            // 
            this.pb_query_progress.Location = new System.Drawing.Point(74, 15);
            this.pb_query_progress.Name = "pb_query_progress";
            this.pb_query_progress.Size = new System.Drawing.Size(892, 18);
            this.pb_query_progress.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "查询进度：";
            // 
            // lv_obd_all
            // 
            this.lv_obd_all.Location = new System.Drawing.Point(14, 65);
            this.lv_obd_all.Name = "lv_obd_all";
            this.lv_obd_all.Size = new System.Drawing.Size(817, 506);
            this.lv_obd_all.TabIndex = 3;
            this.lv_obd_all.UseCompatibleStateImageBehavior = false;
            // 
            // cb_id_slt
            // 
            this.cb_id_slt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_id_slt.FormattingEnabled = true;
            this.cb_id_slt.Items.AddRange(new object[] {
            "所有ID"});
            this.cb_id_slt.Location = new System.Drawing.Point(74, 39);
            this.cb_id_slt.Name = "cb_id_slt";
            this.cb_id_slt.Size = new System.Drawing.Size(121, 20);
            this.cb_id_slt.TabIndex = 4;
            this.cb_id_slt.SelectedIndexChanged += new System.EventHandler(this.cb_id_slt_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "ID选择：";
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
            this.cb_visible_recv.Location = new System.Drawing.Point(400, 39);
            this.cb_visible_recv.Name = "cb_visible_recv";
            this.cb_visible_recv.Size = new System.Drawing.Size(40, 20);
            this.cb_visible_recv.TabIndex = 91;
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
            this.cb_visible_send.Location = new System.Drawing.Point(278, 39);
            this.cb_visible_send.Name = "cb_visible_send";
            this.cb_visible_send.Size = new System.Drawing.Size(40, 20);
            this.cb_visible_send.TabIndex = 89;
            this.cb_visible_send.SelectedIndexChanged += new System.EventHandler(this.cb_visible_send_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(219, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 88;
            this.label6.Text = "显示发送";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(340, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 90;
            this.label7.Text = "显示接收";
            // 
            // btn_query_cur_dtc
            // 
            this.btn_query_cur_dtc.Location = new System.Drawing.Point(839, 37);
            this.btn_query_cur_dtc.Name = "btn_query_cur_dtc";
            this.btn_query_cur_dtc.Size = new System.Drawing.Size(125, 23);
            this.btn_query_cur_dtc.TabIndex = 92;
            this.btn_query_cur_dtc.Text = "查询当前故障码";
            this.btn_query_cur_dtc.UseVisualStyleBackColor = true;
            this.btn_query_cur_dtc.Click += new System.EventHandler(this.btn_query_cur_dtc_Click);
            // 
            // btn_query_his_dtc
            // 
            this.btn_query_his_dtc.Location = new System.Drawing.Point(839, 257);
            this.btn_query_his_dtc.Name = "btn_query_his_dtc";
            this.btn_query_his_dtc.Size = new System.Drawing.Size(125, 23);
            this.btn_query_his_dtc.TabIndex = 93;
            this.btn_query_his_dtc.Text = "查询历史故障码";
            this.btn_query_his_dtc.UseVisualStyleBackColor = true;
            this.btn_query_his_dtc.Click += new System.EventHandler(this.btn_query_his_dtc_Click);
            // 
            // btn_clean_his_dtc
            // 
            this.btn_clean_his_dtc.Location = new System.Drawing.Point(839, 478);
            this.btn_clean_his_dtc.Name = "btn_clean_his_dtc";
            this.btn_clean_his_dtc.Size = new System.Drawing.Size(125, 23);
            this.btn_clean_his_dtc.TabIndex = 94;
            this.btn_clean_his_dtc.Text = "清除历史故障码";
            this.btn_clean_his_dtc.UseVisualStyleBackColor = true;
            this.btn_clean_his_dtc.Click += new System.EventHandler(this.btn_clean_his_dtc_Click);
            // 
            // btn_clean_listview
            // 
            this.btn_clean_listview.Location = new System.Drawing.Point(752, 37);
            this.btn_clean_listview.Name = "btn_clean_listview";
            this.btn_clean_listview.Size = new System.Drawing.Size(79, 23);
            this.btn_clean_listview.TabIndex = 95;
            this.btn_clean_listview.Text = "清除列表";
            this.btn_clean_listview.UseVisualStyleBackColor = true;
            this.btn_clean_listview.Click += new System.EventHandler(this.btn_clean_listview_Click);
            // 
            // tb_cur_dtc
            // 
            this.tb_cur_dtc.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_cur_dtc.ForeColor = System.Drawing.Color.Blue;
            this.tb_cur_dtc.Location = new System.Drawing.Point(839, 65);
            this.tb_cur_dtc.Multiline = true;
            this.tb_cur_dtc.Name = "tb_cur_dtc";
            this.tb_cur_dtc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_cur_dtc.Size = new System.Drawing.Size(208, 186);
            this.tb_cur_dtc.TabIndex = 100;
            // 
            // tb_his_dtc
            // 
            this.tb_his_dtc.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_his_dtc.ForeColor = System.Drawing.Color.Blue;
            this.tb_his_dtc.Location = new System.Drawing.Point(839, 286);
            this.tb_his_dtc.Multiline = true;
            this.tb_his_dtc.Name = "tb_his_dtc";
            this.tb_his_dtc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_his_dtc.Size = new System.Drawing.Size(208, 186);
            this.tb_his_dtc.TabIndex = 101;
            // 
            // tb_clean_his_dtc
            // 
            this.tb_clean_his_dtc.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_clean_his_dtc.Location = new System.Drawing.Point(839, 507);
            this.tb_clean_his_dtc.Multiline = true;
            this.tb_clean_his_dtc.Name = "tb_clean_his_dtc";
            this.tb_clean_his_dtc.Size = new System.Drawing.Size(208, 64);
            this.tb_clean_his_dtc.TabIndex = 102;
            // 
            // Form_OBD_All
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1059, 583);
            this.Controls.Add(this.tb_clean_his_dtc);
            this.Controls.Add(this.tb_his_dtc);
            this.Controls.Add(this.tb_cur_dtc);
            this.Controls.Add(this.btn_clean_listview);
            this.Controls.Add(this.btn_clean_his_dtc);
            this.Controls.Add(this.btn_query_his_dtc);
            this.Controls.Add(this.btn_query_cur_dtc);
            this.Controls.Add(this.cb_visible_recv);
            this.Controls.Add(this.cb_visible_send);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cb_id_slt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lv_obd_all);
            this.Controls.Add(this.pb_query_progress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_start_query);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_OBD_All";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OBD诊断协议完整查询";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_OBD_All_FormClosed);
            this.Load += new System.EventHandler(this.Form_OBD_All_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_start_query;
        private System.Windows.Forms.ProgressBar pb_query_progress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lv_obd_all;
        private System.Windows.Forms.Timer timer_query;
        private System.Windows.Forms.ComboBox cb_id_slt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_visible_recv;
        private System.Windows.Forms.ComboBox cb_visible_send;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_query_cur_dtc;
        private System.Windows.Forms.Button btn_query_his_dtc;
        private System.Windows.Forms.Button btn_clean_his_dtc;
        private System.Windows.Forms.Button btn_clean_listview;
        private System.Windows.Forms.TextBox tb_cur_dtc;
        private System.Windows.Forms.TextBox tb_his_dtc;
        private System.Windows.Forms.TextBox tb_clean_his_dtc;
    }
}