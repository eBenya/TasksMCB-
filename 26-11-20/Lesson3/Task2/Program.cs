using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /**
     * # Задание 2
     * Создайте проект по шаблону "Console Application". 
     * Создайте свой планировщик задач, производный от класса TaskSchedulerс названием StackTaskScheduler.
     * Ваш планировщик будет выполнять первоочередно только поступившие задачи, 
     * то есть самые «свежие». Поэтому, внутри него используйте для хранения задач коллекцию Stack<T>.
     * Реализуйте добавление задачи при запуске в вашу коллекцию. 
     * Также, вам необходимо создать метод, который будет перебирать коллекцию задач и изымать задачи на выполнение.
     * Создайте коллекцию задач из 40 задач. Каждая из задач должна вывести на экран консоли, 
     * что она выполнилась и свой порядковый номер при запуске. Запустите все задачи в цикле с вашим планировщиком.
     * Посмотрите на результат работы
     **/
    class Program
    {
        static void Main(string[] args)
        {
            Task[] tasks = new Task[40];
            Console.WriteLine($"Main thread Id = {Thread.CurrentThread.ManagedThreadId}");

            StackTaskScheduler scheduler = new StackTaskScheduler();

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() =>
                {
                    Thread.Sleep(100);
                    Console.WriteLine($"Task {i}, ID = {Task.CurrentId} completed in thread {Thread.CurrentThread.ManagedThreadId}\n");
                });
                tasks[i].Start(scheduler);
            }


            Console.WriteLine("End Main.");
            Console.ReadKey();
        }
    }

    class StackTaskScheduler : TaskScheduler
    {
        private readonly Stack<Task> tasks = new Stack<Task>();
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return tasks;
        }

        protected override void QueueTask(Task task)
        {
            tasks.Push(task);
            ExecuteTask();
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return false;
        }

        private void ExecuteTask()
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