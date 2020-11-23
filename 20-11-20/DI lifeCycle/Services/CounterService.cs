using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testApp.Services
{
    //Сервис для теста. DI в конструктор.
    public class CounterService
    {        
        protected internal ICounter Counter { get; }
        public CounterService(ICounter counter)
        {
            Counter = counter;
        }
    }
}
