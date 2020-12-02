using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/**
 *Задание 3
 *Выполните задание под номером 2. 
 *Переделайте пример, используя шаблон Producer-Consumer. 
 *Вам необходимо использовать потокобезопасную оболочку BlockingCollection. 
 *Метод ProcessOrders должен работать пока работа с оболочкой не завершена. 
 *Когда покупатели завершат покупку своих товаров, они должны об этом указать.
 **/

namespace ConsoleTEST
{
    class Program
    {
        static async Task Main()
        {
            string ceiling = new string('-', 40);
            Shop shop = new Shop();

            Console.WriteLine("Attention! Delivery is expected:\n\tApple: 1234\n\tOrange:1658\n\tPeach:1245");
            Console.WriteLine(ceiling);

            ConcurrentBag<Product> list1 = new ConcurrentBag<Product>() {
                new Product { Name = "Apple", Quantity = 10},
                new Product { Name = "Orange", Quantity = 7}
            };
            ConcurrentBag<Product> list2 = new ConcurrentBag<Product>() {
                new Product { Name = "Peach", Quantity = 34},
                new Product { Name = "Orange", Quantity = 17}
            };
            ConcurrentBag<Product> list3 = new ConcurrentBag<Product>() {
                new Product { Name = "Apple", Quantity = 32},
                new Product { Name = "Peach", Quantity = 14}
            };

            using (BlockingCollection<Product> collection = new BlockingCollection<Product>(shop.products))
            {
                //Create action for filling shop
                Action<string, int> action = (name, count) =>
                {
                    shop.MakeAnOrder(name, count);
                };

                //Filling shop
                var producer1 = Task.Run(() => action.Invoke("Apple", 1234));
                var producer2 = Task.Run(() => action.Invoke("Orange", 1658));
                var producer3 = Task.Run(() => action.Invoke("Peach", 1245));

                //The store opened, customers arrived
                var byer1 = Task.Run(() =>
                {
                    try
                    {
                        Parallel.ForEach(list1, i => shop.ProcessOrders(i, "byer1"));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Aaaaaaaaaaaaaaaaaaaaaaaaaaa!!!!!!!");                        
                    }
                    finally
                    {
                        Console.WriteLine("byer1: I`m done!");
                    }
                });
                var byer2 = Task.Run(() =>
                {
                    try
                    {
                        Parallel.ForEach(list2, i => shop.ProcessOrders(i, "byer2"));
                    }
                    catch (Exception)
                    {

                        Console.WriteLine("Aaaaaaaaaaaaaaaaaaaaaaaaaaa!!!!!!!");
                    }
                    finally
                    {
                        Console.WriteLine("byer2: I`m done!");
                    }
                });
                var byer3 = Task.Run(() =>
                {
                    try
                    {
                        Parallel.ForEach(list3, i => shop.ProcessOrders(i, "byer3"));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Aaaaaaaaaaaaaaaaaaaaaaaaaaa!!!!!!!");
                    }
                    finally
                    {
                        Console.WriteLine("byer3: I`m done!");
                    }
                });

                //Waiting for delivery
                await Task.WhenAll(producer1, producer2, producer3);

                collection.CompleteAdding();

                //Waiting for customers
                await Task.WhenAll(byer1, byer2, byer3);
            }

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

        public void ProcessOrders(Product prod, string who = default)
        {
            Product product = products.FirstOrDefault(pr => pr.Name == prod.Name);
            if (product?.Quantity >= prod.Quantity)
            {
                product.Quantity -= prod.Quantity;
                Console.WriteLine($"{prod.Name} - {prod.Quantity}шт");
            }
        }
        public async Task ProcessOrdersAsync(Product prod, string who = default)
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
