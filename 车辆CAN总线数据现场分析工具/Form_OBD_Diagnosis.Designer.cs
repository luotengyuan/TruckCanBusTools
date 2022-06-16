namespace 车辆CAN总线数据现场分析工具
{
    partial class Form_OBD_Diagnosis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_OBD_Diagnosis));
            this.btn_clean_listview = new System.Windows.Forms.Button();
            this.cb_visible_recv = new System.Windows.Forms.ComboBox();
            this.cb_visible_send = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_id_slt = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lv_obd_all = new System.Windows.Forms.ListView();
            this.pb_query_progress = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_start_query = new System.Windows.Forms.Button();
            this.chart_v = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_n = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_fr = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_app = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer_query = new System.Windows.Forms.Timer(this.components);
            this.timer_undate_charts = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart_v)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_n)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_fr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_app)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_clean_listview
            // 
            this.btn_clean_listview.Location = new System.Drawing.Point(473, 30);
            this.btn_clean_listview.Name = "btn_clean_listview";
            this.btn_clean_listview.Size = new System.Drawing.Size(79, 23);
            this.btn_clean_listview.TabIndex = 106;
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
            this.cb_visible_recv.Location = new System.Drawing.Point(396, 32);
            this.cb_visible_recv.Name = "cb_visible_recv";
            this.cb_visible_recv.Size = new System.Drawing.Size(40, 20);
            this.cb_visible_recv.TabIndex = 105;
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
            this.cb_visible_send.Location = new System.Drawing.Point(274, 32);
            this.cb_visible_send.Name = "cb_visible_send";
            this.cb_visible_send.Size = new System.Drawing.Size(40, 20);
            this.cb_visible_send.TabIndex = 103;
            this.cb_visible_send.SelectedIndexChanged += new System.EventHandler(this.cb_visible_send_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(215, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 102;
            this.label6.Text = "显示发送";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(336, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 104;
            this.label7.Text = "显示接收";
            // 
            // cb_id_slt
            // 
            this.cb_id_slt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_id_slt.FormattingEnabled = true;
            this.cb_id_slt.Items.AddRange(new object[] {
            "所有ID"});
            this.cb_id_slt.Location = new System.Drawing.Point(70, 32);
            this.cb_id_slt.Name = "cb_id_slt";
            this.cb_id_slt.Size = new System.Drawing.Size(121, 20);
            this.cb_id_slt.TabIndex = 100;
            this.cb_id_slt.SelectedIndexChanged += new System.EventHandler(this.cb_id_slt_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 101;
            this.label2.Text = "ID选择：";
            // 
            // lv_obd_all
            // 
            this.lv_obd_all.Location = new System.Drawing.Point(10, 58);
            this.lv_obd_all.Name = "lv_obd_all";
            this.lv_obd_all.Size = new System.Drawing.Size(542, 551);
            this.lv_obd_all.TabIndex = 99;
            this.lv_obd_all.UseCompatibleStateImageBehavior = false;
            // 
            // pb_query_progress
            // 
            this.pb_query_progress.Location = new System.Drawing.Point(70, 8);
            this.pb_query_progress.Name = "pb_query_progress";
            this.pb_query_progress.Size = new System.Drawing.Size(942, 18);
            this.pb_query_progress.TabIndex = 97;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 98;
            this.label1.Text = "查询进度：";
            // 
            // btn_start_query
            // 
            this.btn_start_query.Location = new System.Drawing.Point(1018, 6);
            this.btn_start_query.Name = "btn_start_query";
            this.btn_start_query.Size = new System.Drawing.Size(75, 23);
            this.btn_start_query.TabIndex = 96;
            this.btn_start_query.Text = "开始查询";
            this.btn_start_query.UseVisualStyleBackColor = true;
            this.btn_start_query.Click += new System.EventHandler(this.btn_start_query_Click);
            // 
            // chart_v
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_v.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_v.Legends.Add(legend1);
            this.chart_v.Location = new System.Drawing.Point(558, 31);
            this.chart_v.Name = "chart_v";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_v.Series.Add(series1);
            this.chart_v.Size = new System.Drawing.Size(535, 140);
            this.chart_v.TabIndex = 107;
            this.chart_v.Text = "chart";
            // 
            // chart_n
            // 
            chartArea2.Name = "ChartArea1";
            this.chart_n.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart_n.Legends.Add(legend2);
            this.chart_n.Location = new System.Drawing.Point(558, 177);
            this.chart_n.Name = "chart_n";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart_n.Series.Add(series2);
            this.chart_n.Size = new System.Drawing.Size(535, 140);
            this.chart_n.TabIndex = 108;
            this.chart_n.Text = "chart2";
            // 
            // chart_fr
            // 
            chartArea3.Name = "ChartArea1";
            this.chart_fr.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart_fr.Legends.Add(legend3);
            this.chart_fr.Location = new System.Drawing.Point(558, 323);
            this.chart_fr.Name = "chart_fr";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart_fr.Series.Add(series3);
            this.chart_fr.Size = new System.Drawing.Size(535, 140);
            this.chart_fr.TabIndex = 109;
            this.chart_fr.Text = "chart3";
            // 
            // chart_app
            // 
            chartArea4.Name = "ChartArea1";
            this.chart_app.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart_app.Legends.Add(legend4);
            this.chart_app.Location = new System.Drawing.Point(558, 469);
            this.chart_app.Name = "chart_app";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chart_app.Series.Add(series4);
            this.chart_app.Size = new System.Drawing.Size(535, 140);
            this.chart_app.TabIndex = 110;
            this.chart_app.Text = "chart4";
            // 
            // Form_OBD_Diagnosis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 617);
            this.Controls.Add(this.chart_app);
            this.Controls.Add(this.chart_fr);
            this.Controls.Add(this.chart_n);
            this.Controls.Add(this.chart_v);
            this.Controls.Add(this.btn_clean_listview);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_OBD_Diagnosis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OBD诊断协议适配";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_OBD_Diagnosis_FormClosed);
            this.Load += new System.EventHandler(this.Form_OBD_Diagnosis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_v)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_n)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_fr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_app)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_clean_listview;
        private System.Windows.Forms.ComboBox cb_visible_recv;
        private System.Windows.Forms.ComboBox cb_visible_send;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_id_slt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lv_obd_all;
        private System.Windows.Forms.ProgressBar pb_query_progress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_start_query;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_v;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_n;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_fr;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_app;
        private System.Windows.Forms.Timer timer_query;
        private System.Windows.Forms.Timer timer_undate_charts;
    }
}