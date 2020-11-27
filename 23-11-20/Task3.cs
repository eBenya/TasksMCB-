using System;
using System.Runtime.InteropServices;
using System.Threading;


/**
 * Задание3
 * Создайте   проект   по   шаблону   "Console   Application".
 * Создайте класс-обертку,параметризированный указателем местазаполнения
 * типом TResult для работы с классом Thread. 
 * Класс-обертка должен позволить выполнить метод, 
 * сообщенный с экземпляромкласса-делегата Func<object, TResult> в контексте потока,
 * созданного классом Thread. 
 * Реализоватьсвойства: bool IsCompleted,  bool  IsSuccess,  bool  IsFaulted,
 * ExceptionтипаException каквтретьемзадании. 
 * Добавить свойство TResult Result, 
 * которое будет отдавать результат выполнения класса-делегата Func<object, TResult>
 * в контексте вторичного потока. Если результат еще не готов, 
 * то нужно усыплять поток, который его запросил, пока результат не станет  доступным. 
 * Если  выполнение  было  выполнено  с  ошибкой,  свойство  Result  должно вернуть ошибку,
 * а не результат. Проверьте  работу  вашего  класса-обертки,
 * создав  в  классе  Program  метод  Calculate  с возвращаемым значением типа int 
 * и входящим параметром типа int с названием sleepTime.
 * В теле метода Calculate в цикле for, размерностью в 10 итераций,
 * проинкрементируйтезначение переменной  итерации  цикла.  
 * На каждой итерации вызвать метод Sleep, усыпляя поток на значение входящего параметра sleepTime. 
 * Верните из метода Calculate результат сложения в цикле. 
 * Выполните метод Calculate в контексте вашего класса-обертки.
 * Пока результат не готов-выводите из метода Main на экран консоли знаки восклицания. 
 * Воспользуйтесь  свойством  IsCompleted  чтобы  узнать  готов  ли  результат. 
 * Когда результатбудетготов-выведите его на экран консоли
 **/

namespace ConsoleApp1
{
    class Program
    {
        const int sleep = 50;
        static void Main(string[] args)
        {
            ExtThreadPool<int> pool = new ExtThreadPool<int>(Calculate);
            pool.Start(100);

            while (!pool.IsCompleted)
            {
                Console.Write("!");
                Thread.Sleep(50);
            }

            Console.WriteLine($"\nPool resault: = {pool.Result}");
        }

        public static int Calculate(object sleepTime)
        {
            int wait = (int)sleepTime;
            int sum = 0;

            for (int i = 1; i < 10; i++)
            {
                sum += i;
                Thread.Sleep(wait);
            }

            return sum;
        }
    }

    class ExtThreadPool<TResult>
    {

        public ExtThreadPool(Func<object, TResult> func)
        {
            this.func = func;
            result = default;

            IsSuccess = false;
            IsCompleted = false;
            IsFaulted = false;
            Exception = null;
        }

        private readonly Func<object, TResult> func;
        private TResult result;

        public TResult Result
        {
            get
            {
                while (!IsCompleted)
                {
                    Thread.Sleep(100);
                }
                return IsSuccess && Exception == null ? result : throw Exception;
            }
        }

        public bool IsSuccess { get; private set; }
        public bool IsCompleted { get; private set; }
        public bool IsFaulted { get; private set; }     //Нахрена требовать еще и этот параметр в задаче?!

        public Exception Exception { get; private set; }

        public void Start(object par)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Execution), par);
        }

        private void Execution(object state)
        {
            try
            {
                result = func.Invoke(state);
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