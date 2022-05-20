using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioactivityDecayCalculator
{
    public class Nuclide
    {
        public string Name { get; set; }
        public float HalfLife { get; set; }
        public float StartActivity { get; }

        public float Day1 { get; set; }

        public float Day10 { get; set; }

        public float Day100 { get; set; }

        public float Day1000 { get; set; }

        public int FinishedDecay { get; }

        private static int[] DaysToCalculate = { 1, 10, 100, 1000 };

        public Nuclide(string Name, float StartActivity, float HalfLife)
        {
            this.Name = Name;
            this.HalfLife = HalfLife;
            this.StartActivity = StartActivity;

            foreach(int TimeDays in DaysToCalculate)
            {
                this.GetType().GetProperty("Day" + TimeDays).SetValue(this, CalculateDecay(TimeDays));
            }

            FinishedDecay = FindDayDecayFinished();
        }

        private float CalculateDecay(int TimeDays)
        {
            return (float) Math.Round(StartActivity * Math.Pow(0.5, TimeDays / HalfLife), 2);
        }

        private int FindDayDecayFinished()
        {
            int TimeSpanLimit = 1000000;
            int MaxDayDecay = 1;
            while (MaxDayDecay < TimeSpanLimit)
            {
                if (CalculateDecay(MaxDayDecay) < 1)
                {
                    break;
                }
                else
                {
                    MaxDayDecay *= 10;
                }
            }

            int low = 0, high = MaxDayDecay;
            while (low != high)
            {
                int mid = (low + high) / 2;
                if (CalculateDecay(mid) >= 1)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid;
                }
            }

            return low;
        }
    }
}
