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

namespace WPF_TEST
{
    /**
     * Задание 
     * Создайте приложение по шаблону WPFApplication. 
     * Переместите из элементов управления (ToolBox) на форму текстовое поле и кнопку. 
     * Создайте асинхронный обработчик события по нажатию на кнопку.
     * Используя класс HttpClient загрузите из Интернета html код любой страницы.
     * Выведите его в текстовое поле.
     **/
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButoonEvenHandler(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;

            await Task.Run(()=>Thread.Sleep(3000));

            HttpClient httpClient = new HttpClient();
            string res = await httpClient.GetStringAsync(@"https://zxpress.ru/article.php?id=8482");
            
            txtResault.Text = res.ToString();
            
            btnStart.IsEnabled = true;
        }
    }
}
