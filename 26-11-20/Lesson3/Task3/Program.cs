using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /**
     * # Задание 3 
     * Создайте проект по шаблону "Console Application". 
     * Создайте свой планировщик задач, производный от класса TaskSchedulerс названием DelayTaskScheduler.
     * Ваш планировщик будет выполнять задачи с задержкой в 2 секунды. 
     * То есть, при запуске задачи, она должна подождать 2 секунды прежде, чем запустится.
     * Для решения такой ситуации можно воспользоваться классом Timer 
     * или методом ThreadPool.RegisterWaitForSingleObject(), 
     * которые позволят вам выполнить вашу задачу в контексте пула потоков, 
     * но при этом с задержкой, указанной вами. 
     * 
     * Не забудьте заглушить абстрактный метод TryExecuteTaskInline(необходимо просто всегда возвращать false). 
     * Из-за задержки в выполнении, задачаможет часто пытаться выполнится синхронно к потоку вызова.
     * 
     * Создайте задачу, которая выведет на экран консоли в каком потоке она отработала и являлся ли он потоком 
     * из пула потоков (для этого используйте свойство -Thread.CurrentThread.IsThreadPoolThread).
     * После этого запустите задачу в контексте вашего планировщика DelayTaskScheduler. 
     * После создайте цикл whileи,при условии, чтосвойство IsCompletedвашей задачи возвращает false,
     * выводите на экран консоли звездочку с задержкой в 100 миллисекунд. 
     * Кодпримернотакой:
     *      while(task.IsCompleted == false)
     *      {
     *          Console.Write($"* ");
     *          Thread.Sleep(100);
     *      }
     * Если вы указали, что задачи должны выполнятся с задержкой в 2 секунды,
     * то у вас должно быть выведено на экран консоли 20 звездочек.
     **/
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Main thread Id = {Thread.CurrentThread.ManagedThreadId}");
            
            DelayTaskScheduler scheduler = new DelayTaskScheduler();
            Task task =  new Task(() =>
                {
                    Console.WriteLine($"\nTask ID = {Task.CurrentId} completed in thread {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"\t\t IsThreadPoolThread - {Thread.CurrentThread.IsThreadPoolThread}\n");
                });
            task.Start(scheduler);

            int i = 0;
            while (task.IsCompleted == false) 
            { 
                Console.Write($"{++i} "); 
                Thread.Sleep(100); 
            }

            Console.WriteLine("End Main.");
            Console.ReadKey();
        }
    }

    class DelayTaskScheduler : TaskScheduler
    {
        private readonly Stack<Task> tasks = new Stack<Task>();
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return tasks;
        }

        protected override void QueueTask(Task task)
        {
            tasks.Push(task);
            
            ThreadPool.RegisterWaitForSingleObject(new AutoResetEvent(false), ExecuteTask, task, 2000, false);            
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return false;
        }

        private void ExecuteTask(object _, bool timeout)
        {
            while (true)
            {
                Task task = null;

                lock (tasks)
                {
                    if (tasks.Count == 0)
                    {
                        break;
                    }
                    task = tasks.Pop();
                }

                if (task == null)
                {
                    break;
                }

                base.TryExecuteTask(task);
            }
        }
    }
}