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
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.AxisX.Interval = 12D;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.MajorTickMark.Interval = 0D;
            chartArea1.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
            chartArea1.AxisX.MajorTickMark.Size = 1.5F;
            chartArea1.AxisX.Maximum = 120D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.MinorTickMark.Enabled = true;
            chartArea1.AxisX.MinorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea1.AxisY.Interval = 1D;
            chartArea1.AxisY.MajorTickMark.Size = 0.4F;
            chartArea1.AxisY.Maximum = 18D;
            chartArea1.AxisY.Minimum = 10D;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(0, 95);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Black;
            series1.MarkerSize = 7;
            series1.Name = "Series1";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            series2.LabelBorderWidth = 3;
            series2.Name = "Series2";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(911, 212);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // label_main
            // 
            this.label_main.AutoSize = true;
            this.label_main.Location = new System.Drawing.Point(427, 9);
            this.label_main.Name = "label_main";
            this.label_main.Size = new System.Drawing.Size(62, 13);
            this.label_main.TabIndex = 19;
            this.label_main.Text = "Машина №";
            // 
            // label_Nominal
            // 
            this.label_Nominal.AllowDrop = true;
            this.label_Nominal.AutoSize = true;
            this.label_Nominal.Location = new System.Drawing.Point(18, 320);
            this.label_Nominal.Name = "label_Nominal";
            this.label_Nominal.Size = new System.Drawing.Size(181, 13);
            this.label_Nominal.TabIndex = 33;
            this.label_Nominal.Text = "Среднее время цикла за период - ";
            // 
            // label_Mean_Period
            // 
            this.label_Mean_Period.AutoSize = true;
            this.label_Mean_Period.Location = new System.Drawing.Point(18, 349);
            this.label_Mean_Period.Name = "label_Mean_Period";
            this.label_Mean_Period.Size = new System.Drawing.Size(154, 13);
            this.label_Mean_Period.TabIndex = 34;
            this.label_Mean_Period.Text = "Номинальное время цикла - ";
            // 
            // label_Mean_Efficiency
            // 
            this.label_Mean_Efficiency.AutoSize = true;
            this.label_Mean_Efficiency.Location = new System.Drawing.Point(18, 378);
            this.label_Mean_Efficiency.Name = "label_Mean_Efficiency";
            this.label_Mean_Efficiency.Size = new System.Drawing.Size(277, 13);
            this.label_Mean_Efficiency.TabIndex = 35;
            this.label_Mean_Efficiency.Text = "Средняя производительность по машине за период -";
            // 
            // label_All_Mean_Efficiency
            // 
            this.label_All_Mean_Efficiency.AutoSize = true;
            this.label_All_Mean_Efficiency.Location = new System.Drawing.Point(18, 407);
            this.label_All_Mean_Efficiency.Name = "label_All_Mean_Efficiency";
            this.label_All_Mean_Efficiency.Size = new System.Drawing.Size(251, 13);
            this.label_All_Mean_Efficiency.TabIndex = 36;
            this.label_All_Mean_Efficiency.Text = "Общая средняя текущая производительность - ";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Call_Second_Form
            // 
            this.Call_Second_Form.Location = new System.Drawing.Point(766, 378);
            this.Call_Second_Form.Name = "Call_Second_Form";
            this.Call_Second_Form.Size = new System.Drawing.Size(124, 23);
            this.Call_Second_Form.TabIndex = 37;
            this.Call_Second_Form.Text = "Открыть таблицу";
            this.Call_Second_Form.UseVisualStyleBackColor = true;
            this.Call_Second_Form.Click += new System.EventHandler(this.Call_Second_Form_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 430);
            this.Controls.Add(this.Call_Second_Form);
            this.Controls.Add(this.label_All_Mean_Efficiency);
            this.Controls.Add(this.label_Mean_Efficiency);
            this.Controls.Add(this.label_Mean_Period);
            this.Controls.Add(this.label_Nominal);
            this.Controls.Add(this.label_main);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Циклизатор";
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
    }
}

