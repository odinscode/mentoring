﻿using System;
using System.Collections.Generic;
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
                    Console.WriteLine($"{item.Customer.CompanyName} has total turnover equal to {item.TotalAmount}");
                }
                totalTurnover += 4000;
                Console.WriteLine();
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 6")]
        [Description("Customers with nondigital postal code or with empty region or with non-valid telephone number")]
        public void Linq6()
        {
            var digitalCharList = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            var result = dataSource.Customers
                .Where(c => c.PostalCode?.ToList()?.Except(digitalCharList).Count() != 0
                    || string.IsNullOrWhiteSpace(c.Region)
                    || !c.Phone.StartsWith("("));

            foreach (var item in result)
            {
                Console.WriteLine($"Company name: {item.CompanyName}, postal code: {item.PostalCode}, region: {item.Region}, phone: {item.Phone}");
            }
        }

        [Category("Grouping Operators")]
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
                    Console.WriteLine($"Customer's name is {item.Customer.CompanyName}, customer's country is {item.Customer.Country}, customer's city is {item.Customer.City}");
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
                    Console.WriteLine($"Customer's name is {item.Customer.CompanyName}, customer's country is {item.Customer.Country}, customer's city is {item.Customer.City}");
                    Console.WriteLine("Supplier's name | Supplier's country | Supplier's city");
                    foreach (var supplier in item.Suppliers)
                    {
                        Console.WriteLine($"{supplier.SupplierName} | {supplier.Country} | {supplier.City}");
                    }
                    Console.WriteLine();
                }
            }
        }

        [Category("Grouping Operators")]
        [Title("GroupBy - Task 7")]
        [Description("Products grouped by avaliability in stock and sorted by cost")]
        public void Linq7()
        {
            var result = dataSource.Products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    AvailableProducts = new
                    {
                        AvailabilityInStock = g
                            .GroupBy(p => p.UnitsInStock != 0)
                            .Select(a => new
                            {
                                isAvailable = a.Key,
                                Products = a.OrderBy(_ => _.UnitPrice)
                            }),
                    }
                });

            foreach (var item in result)
            {
                string category = item.Category;
                var productsGroup = item.AvailableProducts;
                Console.WriteLine($"Category: {category}");

                foreach (var group in productsGroup.AvailabilityInStock)
                {
                    Console.WriteLine(group.isAvailable ? "Available products in group" : "Non-Available products in group");
                    foreach (var product in group.Products)
                    {
                        Console.WriteLine($"Product id: {product.ProductID}, Name: {product.ProductName}, Price: {product.UnitPrice}");
                    }
                }
                Console.WriteLine();
            }
        }

        [Category("Grouping Operators")]
        [Title("GroupBy - Task 8")]
        [Description("Products grouped by expensive/average/cheap prices")]
        public void Linq8()
        {
            var result = dataSource.Products
                .OrderBy(p => p.UnitPrice)
                .GroupBy(p => GetGroupName(p.UnitPrice));

            foreach (var item in result)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine();
                foreach (var temp in item)
                {
                    Console.WriteLine($"{temp.ProductName}, {temp.UnitPrice}");
                }
                Console.WriteLine();
            }
        }

        private string GetGroupName(decimal productCost)
        {
            int expensiveProductCost = 80;
            int cheapProductCost = 20;

            return productCost <= cheapProductCost
                ? "Cheap"
                : (productCost > cheapProductCost && productCost < expensiveProductCost)
                    ? "Average"
                    : "Expensive";
        }

        [Category("Grouping Operators")]
        [Title("GroupBy - Task 9")]
        [Description("Average profit and average intensity for each city")]
        public void Linq9()
        {
            var result = dataSource.Customers
                .GroupBy(c => c.City)
                .Select(g => new
                {
                    CityName = g.Key,
                    CustomersCount = g.Count(),
                    CustomerOrderAmount = g.Sum(c => c.Orders.Count()),
                    SummaryProfit = g.Sum(c => c.Orders.Sum(o => o.Total)),
                    AverageProfit = g.Average(c => c.Orders.Sum(o => o.Total)),
                    Intensity = g.Average(c => c.Orders.Count())
                });

            foreach (var item in result)
            {
                Console.WriteLine($"City: {item.CityName}");
                Console.WriteLine($"Amount of customers: {item.CustomersCount}");
                Console.WriteLine($"Customers amount of orders: {item.CustomerOrderAmount}");
                Console.WriteLine($"Summary profit: {item.SummaryProfit}");
                Console.WriteLine($"Average profit: {item.AverageProfit}");
                Console.WriteLine($"Intensity: {item.Intensity}");
                Console.WriteLine();
            }
        }

        [Category("Grouping Operators")]
        [Title("GroupBy - Task 10")]
        [Description("Average statistics (amount of purchases) for each customer per year, per month and per year and month")]
        public void Linq10()
        {
            var result = dataSource.Customers
                .Select(c => new
                {
                    Customer = c,
                    PurchasesPerYear = c.Orders
                        .GroupBy(o => o.OrderDate.ToString("yyyy"))
                        .Select(g => new
                        {
                            Year = g.Key,
                            OrdersAmount = g.Count()
                        }),
                    PurchasesPerMonth = c.Orders
                        .GroupBy(o => o.OrderDate.ToString("MMMM"))
                        .Select(g => new
                        {
                            Month = g.Key,
                            OrdersAmount = g.Count()
                        }),
                    PurchasesPerYearAndMonth = c.Orders
                        .GroupBy(o => o.OrderDate.ToString("yyyy MMMM"))
                        .Select(g => new
                        {
                            YearAndMonth = g.Key,
                            OrdersAmount = g.Count()
                        })
                });

            foreach (var item in result)
            {
                Console.WriteLine($"Customer: {item.Customer.CompanyName}");

                Console.WriteLine("Intensity per year");
                foreach (var temp in item.PurchasesPerYear)
                {
                    Console.WriteLine($"During {temp.Year} the customer made {temp.OrdersAmount} orders");
                }

                Console.WriteLine();

                Console.WriteLine("Intensity per month");
                foreach (var temp in item.PurchasesPerMonth)
                {
                    Console.WriteLine($"During {temp.Month} the customer made {temp.OrdersAmount} orders");
                }

                Console.WriteLine();

                Console.WriteLine("Intensity per year and PurchasesPerYearAndMonth");
                foreach (var temp in item.PurchasesPerYearAndMonth)
                {
                    Console.WriteLine($"During {temp.YearAndMonth} the customer made {temp.OrdersAmount} orders");
                }

                Console.WriteLine();
                Console.WriteLine();
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
                Console.WriteLine($"Customer's name: {item.Customer.CompanyName}, first order with cost > {cost}: {item.Order.OrderID}");
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
                Console.WriteLine($"Customer's name: {item.Customer.CompanyName}, first order date: {item.FirstOrderDate}");
            }
        }

        [Category("Ordering Operators")]
        [Title("OrderBy - Task 5")]
        [Description("Customers with first order date but filtered by age, month, total turnover and client name")]
        public void Linq5()
        {
            var result = dataSource.Customers
                .Where(c => c.Orders.Any())
                .Select(c => new
                {
                    Customer = c,
                    FirstOrderDate = c.Orders.Min(o => o.OrderDate)
                })
                .OrderBy(r => r.FirstOrderDate.Year)
                .ThenBy(r => r.FirstOrderDate.Month)
                .ThenByDescending(r => r.Customer.Orders.Sum(o => o.Total))
                .ThenBy(r => r.Customer.CompanyName);

            foreach (var item in result)
            {
                Console.WriteLine($"Customer's name: {item.Customer.CompanyName}, first order date: {item.FirstOrderDate}");
            }
        }
    }
}
