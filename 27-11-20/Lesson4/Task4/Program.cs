using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

/**
 *Задание 4
 *Создайте приложение по шаблону ConsoleApplication. 
 *Создайте метод с названием ParseAsync, он должен возвращать Task<IList<string>> 
 *и приниматьстроковой параметр с названием inputData.
 *В теле метода разбейте все содержимое строки inputData на отдельные слова по разделителям:
 *      пробел, запятая, точка. 
 *Полученные слова запишите в строковой массив. Создайте коллекцию List<string>,
 *в которую запишите слова из массива без повторений. 
 *В методе Main считайте текст из файла или введите несколько десятков слов через клавиатуру(на ваше усмотрение).
 *Запишите это в строковую переменную. Вызовите метод ParseAsync, куда передайте строковую переменную.
 *Возвращаемое значение метода в виде задачи запишите в переменную.
 *Пока выполняется метод ParseAsync, сделайте следующее:
 *
 *      1) Выведите на экран консоли строку «Введите свое имя». 
 *      2) Примите ввод данных пользователя с клавиатуры в строковую переменную name. 
 *      3) Создайте экземпляр класса FileStream. 
 *      На его основе создайте экземпляр класса StreamWriter
 *      (НЕ ИСПОЛЬЗОВАТЬ АСИНХРОННЫЕ МЕТОДЫ ДЛЯ ЗАПИСИ В ФАЙЛ!).
 *      На этом моменте вызовите ожидание завершения полученной задачи от вызова метода ParseAsync,
 *      используя оператор await! Результат оператора await запишите в переменную с названием parseResult.
 *      
 *Запишите в файл первую строку: «{name} нашел {parseResult.Count} уникальных слов. Перечисление слов: ». 
 *После, запишите через запятую (исключая последнее слово, там необходимо поставить точку)
 *все найденные слова методом ParseAsync. 
 **/


namespace ConsoleTEST
{
    class Program
    {
        static string path = "G:\\File.txt";
        static void Main(string[] args)
        {
            MainAsync(args);
        }
        static async void MainAsync(string[] args)
        {
            string s = "Some string. It`s separated by symbols.";

            var res = ParseAsync(s);

            Console.WriteLine("Write you name.");
            string name = Console.ReadLine();

            if (!File.Exists(path))
            {
                Console.WriteLine("File not found.");
                Console.ReadKey();
                return;
            }
            using (FileStream fs = File.Create(path))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    var parseResult = await res;
                    sw.Write($"{name} find {parseResult.Count} unique words.\n");

                    var r = res.Result;
                    for (int i = 0; i < r.Count - 1; i++)
                    {
                        sw.Write($"{r[i]}, ");
                    }
                    sw.Write($"{r.Count - 1}.");
                }
            }
        }

        private static Task<IList<string>> ParseAsync (string inputData)
        {
            return Task.Run(() => Parse(inputData));
        }
        private static IList<string> Parse(string inputData)
        {
            return inputData.Split(' ', ',', '.').Distinct().ToList();
        }
    }
}
