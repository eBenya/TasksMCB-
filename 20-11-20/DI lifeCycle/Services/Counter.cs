using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testApp.Services
{
    public interface ICounter 
    {
        int Value { get; }
    }
    //Сервис для теста. DI в метод Configure.
    public class RandomCounter : ICounter
    {
        static Random rnd = new Random();
        private int value;
        public RandomCounter()
        {
            value = rnd.Next(0, 1000);
        }

        int ICounter.Value => value;
    }
}
