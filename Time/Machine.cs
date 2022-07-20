using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time
{
    class Machine
    {

        private List<double> T_;
        private int k_;
        private double nominal_;
        private DateTime StartD_;
        private DateTime EndD_;


        public Machine(int k)
        {
            this.k_ = k;

        }
        
        public void SetStartDate(DateTime D)
        {
            StartD_ = D;
        }
        public DateTime GetStartDate()
        {
            return StartD_;
        }
        public void SetEndtDate(DateTime D)
        {
            EndD_ = D;
        }
        public DateTime GetEndtDate()
        {
            return EndD_;
        }
        public List<double> GetPeriod()
        {
            return T_;
        }
        public void SetPeriod(List<double> t)
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
            double l = new double();
            if (!(T_ == null))
            {
                l = T_.Where(x => !(Double.IsNaN(x))).Average();

               

            }
            return l;
            /*private double Efficiency()
            {
                return 0;
            }*/
        }
    }
}
