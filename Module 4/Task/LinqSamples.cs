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

            var customers = dataSource.Customers
                .ToDictionary(
                    k => k,
                    v => v.Orders.Sum(o => o.Total))
                .Where(f => f.Value > totalTurnover);

            while (customers.Count() > 5)
            {
                Console.WriteLine($"Amount of customers whose Total turnover is greater than {totalTurnover} = {customers.Count()}");
                foreach (var item in customers)
                {
                    Console.WriteLine($"{item.Key.CustomerID} has total turnover equal to {item.Value}");
                }
                totalTurnover += 4000;
                Console.WriteLine();
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 2")]
        [Description("Customer's list of suppliers from same location")]
        public void Linq2()
        {
            var customers = dataSource.Customers
                .Select(customer => new { customer, suppliers = dataSource.Suppliers.Where(supplier => supplier.Country == customer.Country && supplier.City == customer.City) });

            foreach (var item in customers)
            {
                Console.WriteLine($"Customer's ID is - {item.customer.CustomerID}, customer's country is - {item.customer.Country}, customer's city is {item.customer.City}");

                if (item.suppliers.Count() > 0)
                {
                    Console.WriteLine($"Supplier's name | Supplier's country | Supplier's city");

                    foreach (var supplier in item.suppliers)
                    {
                        Console.WriteLine($"{supplier.SupplierName} | {supplier.Country} | {supplier.City}");
                    }

                }
                Console.WriteLine();
            }
        }

        [Category("Heh")]
        [Title("Any - Task 3")]
        [Description("Customers who had at least one order which cost was more than X")]
        public void Linq3()
        {
            int cost = 5000;

            var result = dataSource.Customers
                .Select(c => new { customer = c, order = c.Orders.FirstOrDefault(o => o.Total > cost) });

            foreach (var item in result)
            {
                if (item.order != null)
                {
                    Console.WriteLine($"Customer's id: {item.customer.CustomerID}, first order with cost > {cost}: {item.order.OrderID}");
                }
            }
        }

        [Category("Heh")]
        [Title("Any - Task 4")]
        [Description("Customers who had at least one order which cost was more than X")]
        public void Linq4()
        {
            var result = dataSource.Customers
                .Select(c => new { customer = c, firstOrderDate = c?.Orders?.Min(o => o.OrderDate) });

            foreach (var item in result)
            {
                if (item.firstOrderDate != null)
                {
                    Console.WriteLine($"Customer's id: {item.customer.CustomerID}, first order date {item.firstOrderDate}");
                }
            }
        }
    }
}
