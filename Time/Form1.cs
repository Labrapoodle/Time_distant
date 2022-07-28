using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Time
{

    public partial class Form1 : Form
    {
        
        // !!Machines[0] соответствует второй машине!! (, а Machines[12] — 14, т.е. сдвиг на два)
        List<MachineButtonLabel> Machines = new List<MachineButtonLabel>(); 
        int HighestWorkingMachineNumber = 14;
        int CurrentMachineNumber=15;
        Form2 newForm = new Form2();
        public Form1()
        {

            
            InitializeComponent();
            this.DoubleBuffered = true;
            

            Shown += Form1_Shown;
           

            for (int i = 2; i <= HighestWorkingMachineNumber; i++)
            {
                Machines.Add(new MachineButtonLabel(this, i, new Point(MachineButtonLabel.FirstIndentX + (i - 2) * (MachineButtonLabel.ButtonWidth + ((this.Width-20 - 2 * MachineButtonLabel.FirstIndentX - (HighestWorkingMachineNumber - 1) * MachineButtonLabel.ButtonWidth) / (HighestWorkingMachineNumber - 2))), chart1.Location.Y-120),
                    (machineBL) =>
                {
                    var nom = DB.LoadNominal(machineBL.MachineN);
                    var t = DB.GetCycleIntervals_Start(machineBL.MachineN);
                    machineBL.Machine.SetPeriod(t);
                    CurrentMachineNumber = machineBL.MachineN;
                    machineBL.Machine.SetNominal(nom);
                    //MessageBox.Show($"{machineBL.SelectButton.Font.Name} {machineBL.SelectButton.Font.SystemFontName}");
                    label_main.Text = $"Машина №{machineBL.MachineN}";
                    
                        
                        Plotting(machineBL.MachineN, out double _);
                        LabelColor(Machines);
                        AllEfficiency_Label(Machines);
                        chart1.Series[0].Enabled = true;
                        chart1.Series[1].Enabled = true;
                    
                    
                   
                }
                ));



            }



        }

        public void Plotting(int n, out double w)
        {
            
            List<(DateTime,double)> T;
            var cm = Machines.Find(m => m.MachineN == n);
            if (cm == null)
            {
                w = 0;
                return;
            }

            w = 0;

            chart1.Series[0].Points.Clear();
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 4;
            //chart1.ChartAreas[0].AxisX.CustomLabels.Add(cm.Machine.GetStartDate().ToOADate(),cm.Machine.GetStartDate().AddDays(1).ToOADate(),cm.Machine.GetStartDate().ToString(),1,System.Windows.Forms.DataVisualization.Charting.LabelMarkStyle.LineSideMark );
            
            //chart1.ChartAreas[0].AxisX.Title = "Дата/Время";
            chart1.ChartAreas[0].AxisY.Title = "Время цикла, сек";
            


            chart1.ChartAreas[0].AxisX.Minimum = cm.Machine.CycleBegin.ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = cm.Machine.GetEndtDate().ToOADate();

            
            
            chart1.Series[1].Points.Clear();
            chart1.Series[0].Enabled = true;
            chart1.Series[1].Enabled = true;


            double nominal = cm.Machine.GetNominal();
            T = cm.Machine.GetPeriod();
            if (T != null && T.Count != 0)
            {
                var p = T.Where(x => !(Double.IsNaN(x.Item2)));
                chart1.ChartAreas[0].AxisY.Minimum = (p.Min(x=>x.Item2) > 10) ? 10 : (Math.Round(p.Min(x => x.Item2)) - 1);
                chart1.ChartAreas[0].AxisY.Maximum = (p.Max(x=>x.Item2) < 18) ? 18 : (Math.Round(p.Max(x => x.Item2)) + 1);
                /*double l = T.Min(x => x.Item2);                
                if (!Double.IsNaN(l))
                {
                    if (l < 10)
                    {
                        chart1.ChartAreas[0].AxisY.Minimum = Math.Round(l)-1;
                        
                    }
                    else
                    {
                        chart1.ChartAreas[0].AxisY.Minimum = 10;
                    }
                    
                }
                double h = T.Max(x => x.Item2);
                if (!Double.IsNaN(h))
                {
                    if (h > 18)
                    {
                        chart1.ChartAreas[0].AxisY.Maximum = Math.Round(h) + 1;
                    }
                    else
                    {
                        chart1.ChartAreas[0].AxisY.Maximum = 18;
                    }
                    
                }*/
                
            }
            else 
            {
                chart1.ChartAreas[0].AxisY.Minimum = 10 ;
                chart1.ChartAreas[0].AxisY.Maximum = 18;
            }

            

            double A = cm.Machine.AveragePeriod();
            w = Math.Round(2 - A / nominal, 2);
            int ondex =0;
            if(T!=null && T.Count != 0)
            {
                if (cm.Machine.CycleBegin > T[0].Item1)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.Date.AddDays(1).ToOADate();
                    chart1.ChartAreas[0].AxisX.Minimum = DateTime.Now.Date.AddDays(-4).ToOADate();
                    /*if (T.FindIndex(c => c.Item1 >= cm.Machine.CycleBegin) == -1)
                    {
                        MessageBox.Show("Начало пятидневки позже времнени изменения заказа, но с начала пятидневки нет данных");
                        return;
                    }*/
                    ondex = T.FindIndex(c => c.Item1 >= DateTime.Now.AddDays(-5));
                }
            }
            
            
            

            
            
            for (int b = ondex; b < T.Count; b++)    //График времён циклов
            {
                
                //chart1.Series[0].Points.AddXY(startDate.AddHours(i).ToString(), T[i]);
                //chart1.Series[1].Points.AddXY(startDate.AddHours(i).ToString(), nominal);  //Среднее время цикла
                chart1.Series[0].Points.AddXY(T[b].Item1.ToOADate(), T[b].Item2);
                

            }
            chart1.Series[1].Points.AddXY(chart1.ChartAreas[0].AxisX.Minimum, nominal);
            chart1.Series[1].Points.AddXY(chart1.ChartAreas[0].AxisX.Maximum, nominal);




                        

            if (cm.Machine.GetNominal() == 0)
            {
                label_Nominal.Text = $"Номинальное время цикла - Не указано";
                label_Mean_Efficiency.Text = $"Средняя производительность по машине за период - Не удалось вычислить";
                label_Mean_Period.Text = $"Среднее время цикла за период - {Math.Round(A, 2)} сек";


            }
            else if (cm.Machine.GetNominal() > chart1.ChartAreas[0].AxisY.Maximum)
            {
                label_Nominal.Text = $"Номинальное время цикла - указано слишком большое значение ({cm.Machine.GetNominal()}), введите величину меньше {chart1.ChartAreas[0].AxisY.Maximum} ";
                label_Mean_Efficiency.Text = $"Средняя производительность по машине за период - Не удалось вычислить";
            }
            else
            {
                label_Mean_Period.Text = $"Среднее время цикла за период - {Math.Round(A, 2)} сек";
                label_Nominal.Text = $"Номинальное время цикла - {Math.Round(nominal, 2)} сек";

                if (cm.Machine.AveragePeriod() == 0)
                {
                    label_Mean_Efficiency.Text = $"Средняя производительность по машине за период - Не удалось вычислить";
                }
                else
                {                    
                    label_Mean_Efficiency.Text = $"Средняя производительность по машине за период - {100 * w}%";
                }
                
            }
            chart1.ChartAreas[0].AxisX.CustomLabels.Add(1, System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days, chart1.ChartAreas[0].AxisX.Minimum, chart1.ChartAreas[0].AxisX.Maximum, "dd-MM-yyyy", 1, System.Windows.Forms.DataVisualization.Charting.LabelMarkStyle.SideMark);
            //chart1.ChartAreas[0].AxisX.CustomLabels[0]
            var g = DB.GetConfsWithOrdinal(CurrentMachineNumber);
            label_Current_Configuration.Show();
            label_Current_Configuration.Text = $" {g[0].WGHT} {g[0].NCK} {g[0].MTX} ";
            
        }


        
        private void LabelColor(List<MachineButtonLabel> O)
        {
            double w;


            for (int q = 0; q < HighestWorkingMachineNumber - 1; q++)
            {
                O[q].SelectButton.Enabled = (Math.Abs((O[q].Machine.GetEndtDate() - DateTime.Now.Date).Days) <= 5);
                
                if (  O[q].Machine.GetNominal() != 0 && O[q].Machine.AveragePeriod() != 0 )
                {
                    
                    w = 2 - O[q].Machine.AveragePeriod() / O[q].Machine.GetNominal();
                    if (w >= 0.95) O[q].SelectButton.BackColor = Color.Green;
                    else if (w < 0.9) O[q].SelectButton.BackColor = Color.Red;
                    else O[q].SelectButton.BackColor = Color.Yellow;
                    O[q].EfficiencyLabel.Show();
                    if (Math.Round(100 * w) >= 100) O[q].EfficiencyLabel.Text = Math.Round(100 * w).ToString() + "%";
                    else O[q].EfficiencyLabel.Text = $" {Math.Round(100 * w)}%";

                    ;


                    w = 0;

                }
                else
                {                     
                    O[q].SelectButton.BackColor = DefaultBackColor;
                    O[q].EfficiencyLabel.Hide();
                    
                    
                }
                
                
            }
        }


        private void AllEfficiency_Label( List<MachineButtonLabel> O)
        {

            double E = 0;
            int u = 0;
            for (int q = 0; q < HighestWorkingMachineNumber - 1; q++)
            {

                if (O[q].Machine.GetNominal() != 0 && O[q].Machine.AveragePeriod() != 0)
                {   
                    u++;
                    E += 2 - O[q].Machine.AveragePeriod() / O[q].Machine.GetNominal();

                }
            }
            E = (double)E / u;
            if (Double.IsNaN(E))
            {
                label_All_Mean_Efficiency.Text = $"Общая средняя текущая проиводительность - Не удалось вычислить";
            }
            else
            {
                label_All_Mean_Efficiency.Text = $"Общая средняя текущая проиводительность - {Math.Round(100 * E)}%";
            }

        }

        

        class MachineButtonLabel : IDisposable
        {
            public Machine Machine;
            public Button SelectButton;
            public Label EfficiencyLabel;
            public int MachineN;


            public static int FirstIndentX = 21;
            public static int ButtonWidth = 75;
            public static int ButtonHeight = 65;
            public static int IndentX = 30;
            public static int IndentY = 7;
            private Form Parent;

            public MachineButtonLabel(Form form, int machineN, Point TopLeft, Action<MachineButtonLabel> Onclick)
            {

                MachineN = machineN;
                Machine = new Machine((byte)MachineN);
                SelectButton = new Button();
                SelectButton.Text = MachineN.ToString();
                SelectButton.Location = TopLeft;
                SelectButton.MouseEnter += (s, e) => SelectButton.Cursor = Cursors.Hand;
                SelectButton.MouseLeave += (s, e) => SelectButton.Cursor = Cursors.Arrow;
                SelectButton.Font = new Font("Microsoft Sans Serif", 30);
                SelectButton.Size = new Size(ButtonWidth, ButtonHeight);
                SelectButton.Click += (o, e) => { Onclick.Invoke(this); };
                //SelectButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);


                EfficiencyLabel = new Label();
                EfficiencyLabel.AutoSize = true;

                EfficiencyLabel.Location = new Point(TopLeft.X-10, TopLeft.Y + ButtonHeight + IndentY);
                EfficiencyLabel.Font = new Font("Arial", 26, FontStyle.Regular);

                form.Controls.Add(SelectButton);
                form.Controls.Add(EfficiencyLabel);
                Parent = form;
            }



            /*public void SetEfficiency()
            {
                double k = Machine.AveragePeriod();
                EfficiencyLabel.Text = k.ToString();
            }*/

            public void Dispose()
            {
                Parent.Controls.Remove(SelectButton);
                Parent.Controls.Remove(EfficiencyLabel);
            }
        }

        
        private void Form1_Shown(object sender, EventArgs e)
        {
            
            Refreshing(Machines);
            chart1.Series[0].Enabled = false;
            chart1.Series[1].Enabled = false;
            label_main.Text = "Машина №";

           

        }
        private void Refreshing(List<MachineButtonLabel> Machines)
        {
            foreach (MachineButtonLabel Mac in Machines)
            {
                var period = DB.GetCycleIntervals_Start(Mac.MachineN);
                var mominal = DB.LoadNominal(Mac.MachineN);
                var STD = DB.LoadStartDate(Mac.MachineN);
                Mac.Machine.SetPeriod(period);
                Mac.Machine.SetNominal(mominal);
                Mac.Machine.CycleBegin = STD;
                //Mac.Machine.SetEndtDate(DB.LoadEndDate(Mac.MachineN));
            }
            LabelColor(Machines);
            AllEfficiency_Label(Machines);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Refreshing(Machines);
            if(CurrentMachineNumber <= 14 && CurrentMachineNumber >= 2 && Machines[CurrentMachineNumber - 2].Machine.GetPeriod().Count != 0  )
            {
                DateTime Start = Machines[CurrentMachineNumber - 2].Machine.CycleBegin;
                DateTime Change = Machines[CurrentMachineNumber - 2].Machine.GetPeriod()[0].Item1;

                
                    Plotting(CurrentMachineNumber, out double w);
                
            }
            
            

        }

        private void Call_Second_Form_Click(object sender, EventArgs e)
        {
            if (!newForm.Visible)
            {
                newForm.Show();
            }
            
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            for (int i = 2; i <= HighestWorkingMachineNumber;i++)
            {
                Machines[i-2].SelectButton.Location = new Point(MachineButtonLabel.FirstIndentX + (i - 2) * (MachineButtonLabel.ButtonWidth + ((this.Width - 20 - 2 * MachineButtonLabel.FirstIndentX - (HighestWorkingMachineNumber - 1) * MachineButtonLabel.ButtonWidth) / (HighestWorkingMachineNumber - 2))), chart1.Location.Y - 120);
                Machines[i - 2].EfficiencyLabel.Location = new Point(Machines[i - 2].SelectButton.Location.X-10, Machines[i - 2].SelectButton.Location.Y+MachineButtonLabel.ButtonHeight+MachineButtonLabel.IndentY);
                
            }
        }

        
    }
    /*
    USE PetPro
    GO

    SELECT C.neck,C.[weight],C.matrix FROM Planning.dbo.Sets as S Right Join PetPro.[dbo].Configuration_Nominal as C ON 
	    (CAST(S.matrix AS int)=C.matrix	and S.neck=C.neck and CAST(REPLACE(S.[weight],',','.') AS float)=C.[weight])
	    WHERE (S.matrix = null or S.[weight]=null or S.neck = null or S.neck='') 
    GO
    */
}
