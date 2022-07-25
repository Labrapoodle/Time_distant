using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time
{
    class Machine
    {

        private List<(DateTime,double)> T_;
        private int k_;
        private double nominal_;
        private DateTime StartD_;
        private DateTime EndD_;


        public Machine(int k)
        {
            this.k_ = k;

        }
        
        public DateTime CycleBegin
        {
            get
            {
                return StartD_;
            }
            set
            {
                StartD_ = value;
                EndD_ = value.AddDays(5);
            }
        }
        
       
        
        
        public DateTime GetEndtDate()
        {
            return EndD_;
        }
        public List<(DateTime, double)> GetPeriod()
        {
            return T_;
        }
        public void SetPeriod(List<(DateTime, double)> t)
        {            
            T_ = t;
        }
        public void SetNominal(double P)
        {
            
            nominal_ = P;
        }
        public double GetNominal()
        {
            return nominal_;
        }
        
        public int GetNumber()
            
            {
                return k_;
            }
        public double AveragePeriod()
        {
            
            
            double p = new double();
            if (T_ != null && T_.Count>0)
            {

                int StartNdex;
                int EndNdex;

                if (StartD_ >= T_[0].Item1)
                {
                    if (!T_.Any(x => x.Item1.Date == DateTime.Now.AddDays(-1).Date))
                    {
                        T_.Add((DateTime.Now.Date.AddHours(-1),Double.NaN));
                    }
                    if(!T_.Any(x=>x.Item1 == DateTime.Now.AddDays(-4).Date))
                    {
                        T_.Insert(0, (DateTime.Now.AddDays(-4).Date, Double.NaN));
                    }
                    StartNdex = T_.FindIndex(x => x.Item1 >= DateTime.Now.Date.AddDays(-4));
                    EndNdex = T_.FindIndex(x => x.Item1 >= DateTime.Now.AddDays(1));
                }

                else
                {
                    StartNdex = 0;
                    EndNdex = T_.Count - 1;
                }               
                
                
                
                List<double> V = T_.Select(l => l.Item2).ToList();
                var h = V.GetRange(StartNdex,EndNdex-StartNdex);
                p = V.Where(x => !(Double.IsNaN(x))).Average();

               

            }
            return p;
            /*private double Efficiency()
            {
                return 0;
            }*/
        }
    }
}
