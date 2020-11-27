using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /**
     * Задание
     * Создайте проект по шаблону "WPF".
     * Переместите из элементов управления (ToolBox) на форму два текстовых поля (TextBox) 
     * и кнопку (Button). Дайте имена для ваших элементов управления, 
     * чтобы к ним можно было обращаться из кода. 
     * Например, текстовое поле 
     *      1 –txtResult, текстовое поле 
     *      2 -txtLoop, 
     *      3 -btnStart, кнопка.
     * Перенесите в это приложение метод FindLastFibonacciNumberиз домашнего задания #4 предыдущего урока. 
     * Создайте и зарегистрируйте обработчик события по нажатию на кнопку btnStart.
     * Он должен создать и запустить задачу, которая будет выполнять метод FindLastFibonacciNumber. 
     * Так как эта операция займет много времени, вам нужно использовать флаг TaskCreationOptions.LongRunning,
     * чтобы задача выполнялась в контексте потокавыполненияThreadи не занимала потоки из пула. 
     * Результат асинхронной задачи необходимо вывести в текстовое поле txtResult. 
     * Сделайте это с помощью продолжения. Помните, что к элементам управления можно обращаться только из потоков,
     * в которых они были созданы. 
     * Поэтому  выполните  продолжение  с  помощью  планировщика  задачSynchronizationContextTaskScheduler.
     * Его  можно  получить изстатического  метода TaskScheduler.FromCurrentSynchronizationContext().
     **/
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonStartEvent(object sender, RoutedEventArgs e)
        {
            txtResult.Clear();

            int.TryParse(txtLoop.Text, out int num);
            Task task = new Task(() =>
                {
                    Dispatcher.Invoke(() => btnStart.IsEnabled = false);

                    double d = FindLastFibonacciNumber(num);
                    Dispatcher.Invoke(() => txtResult.Text += $"{d}; ");

                    Dispatcher.Invoke(() => btnStart.IsEnabled = true);
                }, 
                TaskCreationOptions.LongRunning);
            task.Start();	//Scheduler по умолчанию.
            
        }

        private static double FindLastFibonacciNumber(int number)
        {
            Func<int, double> fib = null;
            fib = (x) => x > 1 ? fib(x - 1) + fib(x - 2) : x;
            return fib.Invoke(number);
        }
    }
}
