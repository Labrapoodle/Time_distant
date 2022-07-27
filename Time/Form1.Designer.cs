namespace Time
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label_main = new System.Windows.Forms.Label();
            this.label_Nominal = new System.Windows.Forms.Label();
            this.label_Mean_Period = new System.Windows.Forms.Label();
            this.label_Mean_Efficiency = new System.Windows.Forms.Label();
            this.label_All_Mean_Efficiency = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Call_Second_Form = new System.Windows.Forms.Button();
            this.label_Current_Configuration = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.LabelStyle.Angle = -90;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisX.LabelStyle.Format = "hh:mm";
            chartArea1.AxisX.LabelStyle.Interval = 2D;
            chartArea1.AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.MajorTickMark.Interval = 1D;
            chartArea1.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea1.AxisX.MajorTickMark.Size = 3F;
            chartArea1.AxisX.MinorTickMark.Enabled = true;
            chartArea1.AxisX.MinorTickMark.Interval = 2D;
            chartArea1.AxisX.MinorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
            chartArea1.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisY.Interval = 1D;
            chartArea1.AxisY.MajorTickMark.Size = 0.4F;
            chartArea1.AxisY.Maximum = 18D;
            chartArea1.AxisY.Minimum = 10D;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.chart1.Location = new System.Drawing.Point(0, 139);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Black;
            series1.MarkerColor = System.Drawing.Color.Black;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Series1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.LimeGreen;
            series2.LabelBorderWidth = 3;
            series2.Name = "Series2";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(1235, 258);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // label_main
            // 
            this.label_main.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_main.AutoSize = true;
            this.label_main.BackColor = System.Drawing.SystemColors.Control;
            this.label_main.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_main.Location = new System.Drawing.Point(589, 9);
            this.label_main.Name = "label_main";
            this.label_main.Size = new System.Drawing.Size(90, 20);
            this.label_main.TabIndex = 19;
            this.label_main.Text = "Машина №";
            this.label_main.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Nominal
            // 
            this.label_Nominal.AllowDrop = true;
            this.label_Nominal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Nominal.AutoSize = true;
            this.label_Nominal.Font = new System.Drawing.Font("Square721 BT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Nominal.Location = new System.Drawing.Point(18, 410);
            this.label_Nominal.Name = "label_Nominal";
            this.label_Nominal.Size = new System.Drawing.Size(237, 16);
            this.label_Nominal.TabIndex = 33;
            this.label_Nominal.Text = "Среднее время цикла за период - ";
            // 
            // label_Mean_Period
            // 
            this.label_Mean_Period.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Mean_Period.AutoSize = true;
            this.label_Mean_Period.Font = new System.Drawing.Font("Square721 BT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Mean_Period.Location = new System.Drawing.Point(18, 439);
            this.label_Mean_Period.Name = "label_Mean_Period";
            this.label_Mean_Period.Size = new System.Drawing.Size(198, 16);
            this.label_Mean_Period.TabIndex = 34;
            this.label_Mean_Period.Text = "Номинальное время цикла - ";
            // 
            // label_Mean_Efficiency
            // 
            this.label_Mean_Efficiency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Mean_Efficiency.AutoSize = true;
            this.label_Mean_Efficiency.Font = new System.Drawing.Font("Square721 BT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Mean_Efficiency.Location = new System.Drawing.Point(18, 468);
            this.label_Mean_Efficiency.Name = "label_Mean_Efficiency";
            this.label_Mean_Efficiency.Size = new System.Drawing.Size(361, 16);
            this.label_Mean_Efficiency.TabIndex = 35;
            this.label_Mean_Efficiency.Text = "Средняя производительность по машине за период -";
            // 
            // label_All_Mean_Efficiency
            // 
            this.label_All_Mean_Efficiency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_All_Mean_Efficiency.AutoSize = true;
            this.label_All_Mean_Efficiency.Font = new System.Drawing.Font("Square721 BT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_All_Mean_Efficiency.Location = new System.Drawing.Point(18, 497);
            this.label_All_Mean_Efficiency.Name = "label_All_Mean_Efficiency";
            this.label_All_Mean_Efficiency.Size = new System.Drawing.Size(321, 16);
            this.label_All_Mean_Efficiency.TabIndex = 36;
            this.label_All_Mean_Efficiency.Text = "Общая средняя текущая производительность - ";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 15000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Call_Second_Form
            // 
            this.Call_Second_Form.Font = new System.Drawing.Font("Square721 BT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Call_Second_Form.Location = new System.Drawing.Point(21, 25);
            this.Call_Second_Form.Name = "Call_Second_Form";
            this.Call_Second_Form.Size = new System.Drawing.Size(159, 42);
            this.Call_Second_Form.TabIndex = 37;
            this.Call_Second_Form.Text = "Открыть таблицу";
            this.Call_Second_Form.UseVisualStyleBackColor = true;
            this.Call_Second_Form.Click += new System.EventHandler(this.Call_Second_Form_Click);
            // 
            // label_Current_Configuration
            // 
            this.label_Current_Configuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Current_Configuration.AutoSize = true;
            this.label_Current_Configuration.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label_Current_Configuration.Font = new System.Drawing.Font("Square721 BT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Current_Configuration.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label_Current_Configuration.Location = new System.Drawing.Point(867, 468);
            this.label_Current_Configuration.Name = "label_Current_Configuration";
            this.label_Current_Configuration.Size = new System.Drawing.Size(55, 20);
            this.label_Current_Configuration.TabIndex = 38;
            this.label_Current_Configuration.Text = "label1";
            this.label_Current_Configuration.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1235, 520);
            this.Controls.Add(this.label_Current_Configuration);
            this.Controls.Add(this.Call_Second_Form);
            this.Controls.Add(this.label_All_Mean_Efficiency);
            this.Controls.Add(this.label_Mean_Efficiency);
            this.Controls.Add(this.label_Mean_Period);
            this.Controls.Add(this.label_Nominal);
            this.Controls.Add(this.label_main);
            this.Controls.Add(this.chart1);
            this.MinimumSize = new System.Drawing.Size(1251, 559);
            this.Name = "Form1";
            this.Text = "Циклизатор";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        
        private System.Windows.Forms.Label label_main;
       
        private System.Windows.Forms.Label label_Nominal;
        private System.Windows.Forms.Label label_Mean_Period;
        private System.Windows.Forms.Label label_Mean_Efficiency;
        private System.Windows.Forms.Label label_All_Mean_Efficiency;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button Call_Second_Form;
        private System.Windows.Forms.Label label_Current_Configuration;
    }
}

