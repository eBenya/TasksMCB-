using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class TimeService : ITimeService
    {
        public string GetTimeOfDay()
        {
            var tod = DateTime.Now.TimeOfDay;
            if (tod.Hours >= 12 && tod.Hours < 16)
                return "Day now";
            if (tod.Hours >= 16 && tod.Hours < 24)
                return "Evening now";
            if (tod.Hours >= 0 && tod.Hours < 4)
                return "Nght now";
            if (tod.Hours >= 4 && tod.Hours < 12)
                return "Morning now";


            return "Error";
        }
    }
}
