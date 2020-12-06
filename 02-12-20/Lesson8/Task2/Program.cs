﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/**
 *Задание 2
 * Создайте приложение по шаблону Console Application. 
 * Создайте следующий класс:
 * internal class Product
 * {
 *   public string Name { get; set; }
 *   public int Quantity { get; set; }
 * }
 * Создайте класс Shop.
 * Внутри него создайте:
 *  •Коллекцию для хранения элементов типа Product. 
 *  •Метод с названием MakeAnOrder, 
 * в теле которого должен создаваться новый экземпляр класса Product и добавлять в коллекцию.
 *  •Метод с названием ProcessOrders, в теле которого вы должны изымать из коллекции продукты 
 * и выводить на экран консоли название продукта и сколько единиц было куплено.
 * В классе Program используя задачи создайте несколько покупателей, 
 * которые будут делать несколько заказов, а также создайте одного сотрудника,
 * которыйбудет обрабатывать заказы.
 **/

namespace ConsoleTEST
{
    class Program
    {
        static async Task Main()
        {
            string ceiling = new string('-', 40);

            Shop shop = new Shop();
            await Task.Run(async () =>
            {
                await shop.MakeAnOrderAsync("Apple", 1234);
                await shop.MakeAnOrderAsync("Orange", 1658);
                await shop.MakeAnOrderAsync("Peach", 1245);
            });

            ConcurrentBag<Product> byer1 = new ConcurrentBag<Product>() {
                new Product { Name = "Apple", Quantity = 10},
                new Product { Name = "Orange", Quantity = 7}
                };
            ConcurrentBag<Product> byer2 = new ConcurrentBag<Product>() {
                new Product { Name = "Peach", Quantity = 34},
                new Product { Name = "Orange", Quantity = 17}
                };
            ConcurrentBag<Product> byer3 = new ConcurrentBag<Product>() {
                new Product { Name = "Apple", Quantity = 32},
                new Product { Name = "Peach", Quantity = 14}
                };

            shop.PrintAllToComsole();
            Console.WriteLine(ceiling);

            Console.WriteLine("byer1 by:");
            await Task.Run(() =>
            {
                Parallel.ForEach(byer1, i => shop.ProcessOrders(i));
            });

            Console.WriteLine(ceiling);
            shop.PrintAllToComsole();
            Console.WriteLine(ceiling);

            Console.WriteLine("byer2 by:");
            await Task.Run(() =>
            {
                Parallel.ForEach(byer2, i => shop.ProcessOrders(i));
            });

            Console.WriteLine(ceiling);
            shop.PrintAllToComsole();
            Console.WriteLine(ceiling);

            Console.WriteLine("byer3 by:");
            await Task.Run(() =>
            {
                Parallel.ForEach(byer3, i => shop.ProcessOrders(i));
            });

            Console.WriteLine(ceiling);
            shop.PrintAllToComsole();
            Console.WriteLine(ceiling);
        }
    }

    internal class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
    internal class Shop
    {
        public ConcurrentBag<Product> products = new ConcurrentBag<Product>();
        
        public void MakeAnOrder(string name, int count)
        {
            products.Add(new Product()
            {
                Name = name,
                Quantity = count
            });
        }
        public async Task MakeAnOrderAsync(string name, int count)
        {
            await Task.Run(() => MakeAnOrder(name, count));
        }

        public void ProcessOrders(Product prod)
        {
            Product product = products.FirstOrDefault(pr => pr.Name == prod.Name);
            if (product?.Quantity >= prod.Quantity)
            {
                product.Quantity -= prod.Quantity;
                Console.WriteLine($"{prod.Name} - {prod.Quantity}шт");
            }
        }
        public async Task ProcessOrdersAsync(Product prod)
        {
            await Task.Run(() => ProcessOrders(prod));
        }
        public void ProcessOrders(string name, int count)
        {
            Product product = products.FirstOrDefault(pr => pr.Name == name);
            if (product?.Quantity >= count)
            {
                product.Quantity -= count;
                Console.WriteLine($"{name} - {count}шт");
            }
        }

        public void PrintAllToComsole()
        {
            foreach (var item in products)
            {
                Console.WriteLine($"{item.Name} - {item.Quantity}шт");
            }
        }
    }
}
