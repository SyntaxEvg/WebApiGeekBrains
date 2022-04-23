using System;

namespace WebApiGeekBrains.BLL
{
    public class RandomDateTime
    {
        Random gen;
       int range;
        readonly static DateTime start = new DateTime(2021, 1, 1);

        public RandomDateTime(Random random)
        {          
            gen = random;
            range = (DateTime.Today - start).Days;
        }
        private DateTime _GeNData;

        public DateTime GeNData
        {
            get {
                return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
            }
            set { _GeNData = value; }
        }
    }
}
