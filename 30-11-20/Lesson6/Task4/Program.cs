using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/**
 *Задание 4
 *Создайте приложение по шаблону Console Application. 
 *Создайте асинхронный метод, который асинхронно  загружает  html  код  сайта itvdn.com.  
 *Создайте  асинхронный  метод,  который асинхронно  считает  количество  упоминаний  аббревиатуры
 *«ITVDN»  скачанного  html  кода главной страницы.
 *Выведите результат на экран консоли.
 **/

namespace ConsoleTEST
{
    class Program
    {
        static string pattern = "ITVDN";
        private static string path = "https://itvdn.com/ru";
        static void Main(string[] args)
        {
            string str = ParseHtmlPage(path).Result;
            Console.WriteLine(str);
            int count = CountTheNumberMentioned(str, pattern).Result;
            Console.WriteLine($"ITVND was found {count} times");
        }

        private static async Task<int> CountTheNumberMentioned(string text, string mentioned)
        {
            return await Task.Run(() =>
            {
                var t = text.Split(' ', ';', ',', '.', ':').Where(name => name == "ITVDN");
                
                return t.Count();
            });             
        }

        private static async Task<string> ParseHtmlPage(string path)
        {
            HttpClient httpClient = new HttpClient();
            string res = await Task.Run(() => 
            {
                return httpClient.GetStringAsync(path);
            });

            return res;
        }

        public static void PrintInfo(string text, int ManagedID, string threadName, bool isThreadPoolThread)
        {
            Console.WriteLine($"{text} ID:{ManagedID}; Name:{threadName}; From thread pool - {isThreadPoolThread}");
        }
    }
}
