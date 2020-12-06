using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Threading;

namespace WPF_TEST
{
    /**
     * Задание 3
     * Создайте приложение по шаблону WPF. 
     * Переместите из элементов управления (ToolBox) на форму два текстовых поля и три кнопки.
     * Создайте асинхронный метод CreateObjectsAsync(int objectCount).
     * Который в своем теле создает массив типа object с количествомэлементов, 
     * равным параметру objectCount. 
     * Далее, с помощью цикла инициализируйте каждый элемент массива новым экземпляром класса object.
     * При этом, на каждой итерации сделайте асинхронную задержку на 1 секунду. 
     * По завершении инициализации массива выведите в текстовое поле количество созданных объектов в массиве.
     * Создайте три асинхронных обработчика события для каждой из кнопок.
     * Каждый обработчик внутри себя неблокирующим образом вызывает метод CreateObjectsAsync.
     * Первый обработчик передает значение 250, второй –400, третий –1000. 
     * Создайте асинхронный метод,
     * который каждые 500 миллисекунд будет замерять количество занятых байт на куче (GC.GetTotalMemory())
     * и выводить во второе текстовое поле. Запустите метод при старте приложения.
     **/
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(CheckGCTotalMemory);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Start();
        }

        private async void ButoonEvenHandler(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.IsEnabled = false;
            string res = string.Empty;

            switch (button.Name)
            {
                case "btnStart_250":
                    res = await CreateObjectsAsync(250);
                    break;
                case "btnStart_400":
                    res = await CreateObjectsAsync(400);
                    break;
                case "btnStart_1000":
                    res = await CreateObjectsAsync(1000);
                    break;
                default:
                    res = "Something went wrong";
                    break;
            }

            txtResault.Text += $"Button {button.Name} - {res}\n";

            button.IsEnabled = true;
        }
        private async Task<string> CreateObjectsAsync(int objectCount)
        {
            await Task.Run(() =>
            {
                object[] arr = new object[objectCount];
                for (int i = 0; i < objectCount; i++)
                {
                    arr[i] = new object();
                    Thread.Sleep(10);
                }
            });

            return $"Create {objectCount} elements";
        }

        private void CheckGCTotalMemory(object sender, EventArgs e)
        {
            txtByteUsed.Text = $"GC total memory check: {GC.GetTotalMemory(true)}\n";
        }
    }
}
