using System;
using System.Runtime.InteropServices;
using System.Threading;


/**
 * Задание 2
 * Создайте проект по шаблону "Console Application". 
 * Создайте класс-обертку для работы с классом Thread.  
 * Класс-обертка, должен позволить выполнить 
 * экземпляр класса-делегата Action<object> в  контексте потока,
 * созданного  классом  Thread.  
 * Он  должен  быть  наделен свойствами  bool IsCompleted 
 * (для проверки на завершенность выполнения метода), 
 * bool IsSuccess (для проверки на успешность выполнения), 
 * bool IsFaulted (для проверки на провал выполнения) и 
 * Exception типа Exception (для получения исключения, 
 * которое произошло в контексте вторичного потока).
 * 
 * Реализуйтетакже методы Start и Wait. 
 * Метод Start будет запускать экземпляр класса-делегата Action<object>  
 * на  выполнение  в контексте  потока  Thread. 
 * Метод  Wait  будет  усыплять поток, который его вызвал, 
 * пока класс-делегат Action<object> не завершит свою работу. 
 * 
 * По завершению выполнения нужно присвоить свойству IsCompleted - true. 
 * Если выполнениепроизошло  без  ошибок,  свойствуIsSuccess  присвоить  true, 
 * в  противном  случае - свойству IsFaulted присвоить true и в свойство Exception записать исключение. 
 * 
 * После создания класса-обертки повторить первое задание только с использованием своего класса-обертки.
 **/

namespace ConsoleApp1
{
    class Program
    {
        const int sleep = 50;
        static void Main(string[] args)
        {
            ExtThreadPool pool = new ExtThreadPool(new Action<object>(WriteChar));

            pool.Start('*');
            pool.Start('!');

            Console.Read();
        }

        public static void WriteChar(object symbol)
        {
            char c = (char)symbol;

            for (int i = 1; i < 160; i++)
            {
                Console.Write(c);
                Thread.Sleep(sleep);
            }
        }
    }

    class ExtThreadPool
    {

        public ExtThreadPool(Action<object> action)
        {
            this.action = action;
			
			IsSuccess = false;
            IsCompleted = false;
            IsFaulted = false;
            Exception = null;
        }
        private readonly Action<object> action;

        public bool IsSuccess { get; private set; }
        public bool IsCompleted { get; private set; }
        public bool IsFaulted { get; private set; }

        public Exception Exception { get; private set; }

        public void Start(object par)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Execution), par);
        }
        public void Wait(int sleepTimer = 100)
        {
            while(IsCompleted == false)
            {
                Thread.Sleep(sleepTimer);
            }
            if (Exception != null)
            {
                throw Exception;
            }
        }

        private void Execution(object state)
        {
            try
            {
                action.Invoke(state);
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                Exception = ex;
                IsSuccess = false;
                IsFaulted = true;
            }
            finally
            {
                IsCompleted = true;
            }
        }
    }
}