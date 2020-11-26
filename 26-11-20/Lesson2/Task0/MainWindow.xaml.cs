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
     * Создайте проектпо шаблону "WPF". 
     * Переместите из элементов управления (ToolBox) на форму текстовое поле (TextBox) и кнопку (Button). 
     * Дайте имена для ваших элементов управления, чтобы к ним можно было обращаться из кода. 
     * Например, текстовое поле –txtResult, а кнопка –btnStart. 
     * Создайте и зарегистрируйте обработчик события понажатию на кнопку btnStart. 
     * Он должен в цикле выводить в текстовое поле звездочки с задержкой в 300 миллисекунд. 
     * В текстовое поле должно быть выведено 100 звездочек.  
     * Чтобы форма не зависла на время вывода звездочек и могла отвечать на действия пользователя,
     * реализуйте выполнение с помощью задач.
     **/
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonStartEvent(object sender, RoutedEventArgs e)
        {
            txtResult.Clear();
            btnStart.IsEnabled = false;
            await Task.Run(()=> WriteCharTextBox('*', 100, txtResult));
            btnStart.IsEnabled = true;
        }

        private void WriteCharTextBox(char c, int count, TextBox textBox)
        {
            TextBox tb = textBox as TextBox;
            if (tb != null)
            {
                for (int i = 0; i < count; i++)
                {
                    Dispatcher.Invoke( ()=> tb.Text += c.ToString());
                    Thread.Sleep(100);
                }
            }            
        }
    }
}
