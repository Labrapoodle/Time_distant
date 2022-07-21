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
        static List<MachineButtonLabel> Machines = new List<MachineButtonLabel>();
        static int HighestWorkingMachineNumber = 14;
        static int CurrentMachineNumber=0;
        public Form1()
        {


            InitializeComponent();
            Shown += Form1_Shown;
           

            for (int i = 2; i <= HighestWorkingMachineNumber; i++)
            {
                Machines.Add(new MachineButtonLabel(this, i, new Point(MachineButtonLabel.FirstIndentX + (i - 2) * (MachineButtonLabel.ButtonWidth + ((902 - 2 * MachineButtonLabel.FirstIndentX - (HighestWorkingMachineNumber - 1) * MachineButtonLabel.ButtonWidth) / (HighestWorkingMachineNumber - 2))), 46),
                    (machineBL) =>
                {
                    var nom = DB.LoadNominal(machineBL.MachineN);
                    var t = DB.RandomPeriod(machineBL.MachineN);
                    machineBL.Machine.SetPeriod(t);
                    CurrentMachineNumber = machineBL.MachineN;
                    machineBL.Machine.SetNominal(nom);
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
            List<double> T;
            var cm = Machines.Find(m => m.MachineN == n);
            if (cm == null)
            {
                w = 0;
                return;
            }

            w = 0;

            chart1.Series[0].Points.Clear();



           
            
            chart1.Series[1].Points.Clear();
            chart1.Series[0].Enabled = true;
            chart1.Series[1].Enabled = true;


            double nominal = cm.Machine.GetNominal();
            T = cm.Machine.GetPeriod();
            double A = cm.Machine.AveragePeriod();
            w = Math.Round(2 - A / nominal, 2);

            var startDate = cm.Machine.GetStartDate();

            for (int i = 0; i < 120; i++)    //График времён циклов
            {
                
                chart1.Series[0].Points.AddXY(startDate.AddHours(i).ToString(), T[i]);
                chart1.Series[1].Points.AddXY(startDate.AddHours(i).ToString(), nominal);  //Среднее время цикла
                
            }
            
            
            
            label_Mean_Period.Text = $"Среднее время цикла за период - {Math.Round(A, 2)} сек";            

            if (cm.Machine.GetNominal() == 0)
            {                         
                label_Nominal.Text = $"Номинальное время цикла -  Не указано ";
                label_Mean_Efficiency.Text = $"Средняя производительность по машине за период - Не удалось вычислить";                
            }
            else
            {
                label_Nominal.Text = $"Номинальное время цикла - {Math.Round(nominal, 2)} сек";
                label_Mean_Efficiency.Text = $"Средняя производительность по машине за период - {100 * w}%";
            }
            


        }


        
        private void LabelColor(List<MachineButtonLabel> O)
        {
            double w;


            for (int q = 0; q < HighestWorkingMachineNumber - 1; q++)
            {
                if (O[q].Machine.GetNominal() != 0)
                {
                    w = 2 - O[q].Machine.AveragePeriod() / O[q].Machine.GetNominal();
                    if (w >= 0.95) O[q].SelectButton.BackColor = Color.Green;
                    else if (w < 0.9) O[q].SelectButton.BackColor = Color.Red;
                    else O[q].SelectButton.BackColor = Color.Yellow;
                    O[q].EfficiencyLabel.Show();
                    if (w > 1) O[q].EfficiencyLabel.Text = Math.Round(100 * w).ToString() + "%";
                    else O[q].EfficiencyLabel.Text = $" {Math.Round(100 * w)} %";


                    w = 0;

                }
                else
                {                     
                    O[q].SelectButton.BackColor = DefaultBackColor;
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
            public static int ButtonWidth = 27;
            public static int ButtonHeight = 23;
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
                SelectButton.Size = new Size(ButtonWidth, ButtonHeight);
                SelectButton.Click += (o, e) => { Onclick.Invoke(this); };



                EfficiencyLabel = new Label();
                EfficiencyLabel.AutoSize = true;

                EfficiencyLabel.Location = new Point(TopLeft.X, TopLeft.Y + ButtonHeight + IndentY);
                
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
                Mac.Machine.SetPeriod(DB.RandomPeriod(Mac.MachineN));
                Mac.Machine.SetNominal(DB.LoadNominal(Mac.MachineN));
                Mac.Machine.SetStartDate(DB.LoadStartDate(Mac.MachineN));
                Mac.Machine.SetEndtDate(DB.LoadEndDate(Mac.MachineN));
            }
            LabelColor(Machines);
            AllEfficiency_Label(Machines);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            Refreshing(Machines);
            Plotting(CurrentMachineNumber, out double w);

        }

        private void Call_Second_Form_Click(object sender, EventArgs e)
        {
            
            Form2 newForm = new Form2();
            newForm.Show();
        }

        
    }


}
