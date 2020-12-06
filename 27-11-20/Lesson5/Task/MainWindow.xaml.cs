using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
     * Создайте асинхронный обработчик события по нажатию на кнопку. Создайте метод Addition,
     * который принимает два параметра и возвращает результат их сложениях. 
     * Из асинхронного обработчика события вызовите этот метод через Task.Run<int>.
     * На возвращаемом значении метода Run вызовите метод ConfigureAwait с указанием параметра false.
     * Примените оператор await к этому выражению и примите результат работы задачи в целочисленнуюпеременную.
     * Ее значение выведите в текстовое поле.
     * 
     * Посмотрите на результаты работы.
     **/
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButoonEvenHandler(object sender, RoutedEventArgs e)
        {
            int res = await Task.Run(() => Addition(7, 9)).ConfigureAwait(false);
            txtResault.Text = res.ToString();
        }

        private int Addition(int x, int y) => x + y;
    }
}
