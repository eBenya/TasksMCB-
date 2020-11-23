﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncProgramming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int counter;
        public MainWindow()
        {
            InitializeComponent();
            counter = 0;
        }

        private void BtnClick_Click(object sender, RoutedEventArgs e)
        {
            txtClick.Text = Convert.ToString(++counter);
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            DownloadStringAsync("http://microsoft.com/");
        }


        private void DownloadStringAsync(string url)
        {
            Task.Run(() =>
            {
                Dispatcher.Invoke(() => btnDownload.IsEnabled = false);

                string res = DownloadString(url);
                Dispatcher.Invoke(() =>
                {
                    txtDownload.Text = res;
                    btnDownload.IsEnabled = true;
                });               
            });

        }
        private string DownloadString(string url)
        {
            Thread.Sleep(5000);

            HttpClient httpClient = new HttpClient();

            return httpClient.GetStringAsync(url).Result;
        }

    }
}
