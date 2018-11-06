using System;
using System.Linq;
using SampleSupport;
using Task.Data;

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {
        private DataSource dataSource = new DataSource();

        [Category("Restriction Operators")]
        [Title("Where - Task 1")]
        [Description("Amount of customers whose Total turnover is greater than X")]
        public void Linq1()
        {
            int totalTurnover = 0;

            var customersWithTotalAmount = dataSource.Customers
                .Select(c => new
                {
                    Customer = c,
                    TotalAmount = c.Orders.Sum(o => o.Total)
                })
                .Where(o => o.TotalAmount > totalTurnover);

            while (customersWithTotalAmount.Count() > 5)
            {
                Console.WriteLine($"Amount of customers whose Total turnover is greater than {totalTurnover}: {customersWithTotalAmount.Count()}");
                foreach (var item in customersWithTotalAmount)
                {
                    Console.WriteLine($"{item.Customer.CustomerID} has total turnover equal to {item.TotalAmount}");
                }
                totalTurnover += 4000;
                Console.WriteLine();
            }
        }

        [Category("Grouping operators")]
        [Title("GroupBy - Task 2")]
        [Description("Customer's list of suppliers from same location")]
        public void Linq2()
        {
            var customers = dataSource.Customers
                .Select(c => new
                {
                    Customer = c,
                    Suppliers = dataSource.Suppliers
                        .Where(supplier => supplier.Country == c.Country && supplier.City == c.City)
                });

            Console.WriteLine("Where usage: ");
            foreach (var item in customers)
            {
                if (item.Suppliers.Count() > 0)
                {
                    Console.WriteLine($"Customer's ID is {item.Customer.CustomerID}, customer's country is {item.Customer.Country}, customer's city is {item.Customer.City}");
                    Console.WriteLine($"Supplier's name | Supplier's country | Supplier's city");
                    foreach (var supplier in item.Suppliers)
                    {
                        Console.WriteLine($"{supplier.SupplierName} | {supplier.Country} | {supplier.City}");
                    }
                    Console.WriteLine();
                }
            }

            var suppliers = dataSource.Suppliers
                .GroupBy(s => new
                {
                    s.City,
                    s.Country
                });

            var customersExtended = dataSource.Customers
                .Select(c => new
                {
                    Customer = c,
                    Suppliers = suppliers
                        .FirstOrDefault(s => s.Key.City == c.City && s.Key.Country == c.Country)
                });

            Console.WriteLine("GroupBy usage: ");
            foreach (var item in customersExtended)
            {
                if (item.Suppliers != null)
                {
                    Console.WriteLine($"Customer's ID is {item.Customer.CustomerID}, customer's country is {item.Customer.Country}, customer's city is {item.Customer.City}");
                    Console.WriteLine("Supplier's name | Supplier's country | Supplier's city");
                    foreach (var supplier in item.Suppliers)
                    {
                        Console.WriteLine($"{supplier.SupplierName} | {supplier.Country} | {supplier.City}");
                    }
                    Console.WriteLine();
                }
            }
        }

        [Category("Quantifiers")]
        [Title("Any - Task 3")]
        [Description("Customers who had at least one order which cost was more than X")]
        public void Linq3()
        {
            int cost = 5000;

            var result = dataSource.Customers
                .Select(c => new
                {
                    Customer = c,
                    Order = c.Orders.FirstOrDefault(o => o.Total > cost)
                })
                .Where(t => t.Order != null);

            foreach (var item in result)
            {
                Console.WriteLine($"Customer's id: {item.Customer.CustomerID}, first order with cost > {cost}: {item.Order.OrderID}");
            }
        }

        [Category("Quantifiers")]
        [Title("Any - Task 4")]
        [Description("Customers with first order date")]
        public void Linq4()
        {
            var result = dataSource.Customers
                .Where(c => c.Orders.Any())
                .Select(c => new
                {
                    Customer = c,
                    FirstOrderDate = c.Orders.Min(o => o.OrderDate)
                });

            foreach (var item in result)
            {
                Console.WriteLine($"Customer's id: {item.Customer.CustomerID}, first order date {item.FirstOrderDate}");
            }
        }
    }
}
