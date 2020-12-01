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
     *Задание 
     *Создайте приложение по шаблону WPFApplication. 
     *Переместите из элементов управления (ToolBox) на форму два текстовых поля, 
     *две кнопки и прогресс бар.
     *Сделайте приложение, которое считает факториал числа, 
     *введенного пользователем в одно из текстовых полей. 
     *Второе текстовое поле показывает результат. 
     *Приложение должно поддерживать отмену выполнения и, 
     *с помощью прогресс бара, демонстрировать прогресс операции. 
     *При этом, вы должны обеспечить повторный запуск приложения
     *с полной работоспособностью вычислений, отмены и прогресса операции.
     *
     *Примечание: если операция выполняется слишком быстро,
     *для наглядности добавьте задержку между вычислениями следующего промежуточного значения факториала.
     **/
    public partial class MainWindow : Window
    {
        IProgress<int> progress;
        CancellationTokenSource cts;
        public MainWindow()
        {
            InitializeComponent();
            cts = new CancellationTokenSource();
            progress = new Progress<int>(PBUpdate);
        }

        private void PBUpdate(int value)
        {
            ProgressBar.Value = value;
        }

        private async void BtnStartEvent(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtIn.Text, out int num))
            {
                btnStart.IsEnabled = false;

                try
                {                    
                    txtResault.Text = (await FactorialAsync(num, cts.Token, progress)).ToString();
                }
                catch (OperationCanceledException ex)
                {
                    txtResault.Text = $"Operation is canceled. {ex.Message}{Environment.NewLine}";
                }
                catch(Exception ex)
                {
                    txtResault.Text = $"WTF!!! {ex.Message}";
                }
                finally
                {
                    btnStart.IsEnabled = true;
                }
            }            
        }

        private Task<int> FactorialAsync(int n, CancellationToken token = default, IProgress<int> pr = null)
        {
            return Task.Run(() => Factorial(n, token, pr));
        }
        private int Factorial(int n, CancellationToken token, IProgress<int> pr)
        {
            token.ThrowIfCancellationRequested();

            int fac = 1;

            for (int i = 2; i <= n; i++)
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }

                fac *= i;                

                Task.Delay(100).Wait();

                pr?.Report(i * 100 / n);
            }
            return fac;
        }

        private void BtnCanceledEvent(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
            cts = new CancellationTokenSource();
        }

    }
}
