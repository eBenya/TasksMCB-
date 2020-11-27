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
     * Создайте приложение по шаблону WindowsForm.
     * Переместите из элементов управления (ToolBox) на форму текстовое поле и кнопку.
     * Создайте закрытый метод GenerateAnswer, который ничего не принимает и возвращает string.
     * В теле метода вызовите метод Sleep,передав туда значение 10000,
     * после с помощью оператора return верните строку «Привет мир!».
     * Создайте обработчик события для добавленной вами кнопки. 
     * Сделайте обработчик события асинхронным, добавив модификатор asyncк методу. 
     * Далее, создайте следующий код в указанномпорядке:
     *      1.      Сделайте кнопку неактивной на время асинхронной операции.
     *      2.      Вызовите через задачу (Task.Run) метод GenerateAnswer. 
     *          Примените оператор awaitк запущенной задаче, чтобы не блокировать UIинтерфейс.
     *          Возвращаемое значение оператора awaitзапишите в переменную.
     *      3.      Значение переменной запишите в текстовое поле, добавив к строке 
     *          $« Выполнено в потоке {Thread.CurrentThread.ManagedThreadId}».
     *      4.      Сделайте кнопку вновь активной для нажатий.
     *Посмотрите на результаты работы.
     *Каковыотличия работы ключевых слов async await здесь от консольных приложений.
     **/
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonStartEvent(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            string res = await Task.Run(() => GenerateAnswer());
            txtResult.Text = $"{res}\nВыполнено в потоке {Thread.CurrentThread.ManagedThreadId}";
            btnStart.IsEnabled = true;
        }

        private string GenerateAnswer()
        {
            Thread.Sleep(5000);
            return "«Привет мир!";
        }
    }
}
