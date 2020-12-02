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
 *Задание 2
 *Создайте приложение по шаблону Console Application. 
 *Создайте следующиеклассы:
 * internal class Product
 * {
 *   public string Name { get; set; }
 *   public int Quantity { get; set; }
 * }
 * 
 * internal class Customer
 * {
 *   publicstringName { get; set; }
 *   publicstringPhone { get; set; }
 *   publicstringAddress { get; set; }
 * }
 * 
 * internal class Order
 * {
 *   public Customer Customer { get; set; }
 *   public List<Product> Products { get; set; }
 * }
 * Создайте класс Shop. 
 * Внутри него создайте:
 *      Коллекцию ConcurrentDictionary, 
 *   которая по имени покупателя будет хранить его заказы. 
 *      Метод с названием MakeAnOrder, 
 *   в теле которого должен создаваться новый экземпляр класса Product,
 *   и добавляться в коллекцию. Если там такой продукт уже есть, 
 *   необходимо изменить его количество.
 *      Метод с названием ProcessOrders, 
 *   в теле которого вы должны изымать из коллекции продукты,
 *   и выводить на экран консоли название продукта,
 *   и сколько единиц было куплено.
 * В классе Program используя задачи создайте несколько покупателей,
 * которые будут делать несколько заказов,
 * а также создайте одного сотрудника, который будет обрабатывать заказы.
 **/

namespace ConsoleTEST
{
    class Program
    {
        static async Task Main()
        {
            string ceiling = new string('-', 40);

            Shop shop = new Shop();

            shop.PrintAllToComsole();

            Order customer1 = new Order
            {
                Customer = new Customer { Name = "Kek" },
                Products = new List<Product>
                {
                    new Product { Name = "Apple", Quantity = 10},
                    new Product { Name = "Orange", Quantity = 7}
                }
            };
            foreach (var item in customer1.Products)
            {
                shop.MakeAnOrder(customer1.Customer, item);
            }

            Order customer2 = new Order
            {
                Customer = new Customer { Name = "Puc" },
                Products = new List<Product>
                {
                    new Product { Name = "Peach", Quantity = 34},
                    new Product { Name = "Orange", Quantity = 17}
                }
            };
            foreach (var item in customer2.Products)
            {
                shop.MakeAnOrder(customer2.Customer, item);
            }

            Order customer3 = new Order
            {
                Customer = new Customer { Name = "Lol" },
                Products = new List<Product>
                {
                    new Product { Name = "Apple", Quantity = 32},
                    new Product { Name = "Peach", Quantity = 14}
                }
            };
            foreach (var item in customer3.Products)
            {
                shop.MakeAnOrder(customer3.Customer, item);
            }
            shop.PrintAllToComsole();

            Console.WriteLine(ceiling);
            Console.WriteLine("Sale process:");
            await Task.Run(() =>
            {
                Parallel.ForEach(shop.orders, 
                    order => Parallel.ForEach(order.Value, prod => shop.ProcessOrders(order.Key, prod)));
            });
            Console.WriteLine(ceiling);
            shop.PrintAllToComsole();

            Console.WriteLine("Main end.");
            /*Теперь я аще не пойму задачу!!!*/
        }
    }

    internal class Shop
    {
        public ConcurrentDictionary<Customer, List<Product>> orders = new ConcurrentDictionary<Customer, List<Product>>();
        
        public void ProcessOrders(Customer customer, Product product)
        {
            bool isTry = orders.TryGetValue(customer, out List<Product> products);
            if(isTry)
            {
                Product old = products.FirstOrDefault(pr => pr.Name == product.Name);
                if (old?.Quantity >= product.Quantity)
                {
                    int count = product.Quantity;
                    old.Quantity -= product.Quantity;
                    Console.WriteLine($"The seller sold:\n\t{product.Name} - {count}pcs");
                }
            }
        }

        public void MakeAnOrder(Customer customer , Product product)
        {
            if (orders.TryGetValue(customer, out var products))
            {
                var pr = products.FirstOrDefault(pr => pr == product);

                if (pr is null)
                {
                    products.Add(product);
                }
                else
                {
                    pr.Quantity += product.Quantity;
                }
            }
            else
            {
                var list = new List<Product>();
                list.Add(product);
                orders.TryAdd(customer, list);
            }
        }

        public void PrintAllToComsole()
        {
            foreach (var item in orders)
            {
                Console.WriteLine($"Orders {item.Key.Name}:");
                
                foreach (var prod in item.Value)
                {
                    Console.WriteLine($"\t{prod.Name} - {prod.Quantity}pcs");
                }
            }
        }
    }

    internal class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public override bool Equals(object obj)
        {
            var pr = obj as Product;
            if (this.Name == pr.Name)
                return true;
            return false;
        }
    }

    internal class Customer
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public override bool Equals(object obj)
        {
            var c = obj as Customer;
            if (this.Name == c.Name && this.Address == c.Address && this.Phone == c.Phone)
                return true;
            return false;
        }
    }

    internal class Order
    {
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
    }
}
