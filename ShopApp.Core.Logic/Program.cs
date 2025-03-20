//Console.WriteLine("Program!");

//using System;
//using System.Collections.Generic;
//using System.Linq;

//public class Product
//{
//    public string Name { get; set; }
//    public decimal Price { get; set; }
//}

//public class PriceThenNameDescendingComparer : IComparer<Product>
//{
//    public int Compare(Product x, Product y)
//    {
//        // Compare prices first
//        int priceComparison = x.Price.CompareTo(y.Price);

//        // If prices are equal, compare names in descending order
//        if (priceComparison == 0)
//        {
//            return y.Name.CompareTo(x.Name); // Descending order
//        }
//        else
//        {
//            return priceComparison;
//        }
//    }
//}

//public class Example
//{
//    public static void Main(string[] args)
//    {
//        List<Product> products = new List<Product>
//        {
//            new Product { Name = "Apple", Price = 1.00m },
//            new Product { Name = "Banana", Price = 0.50m },
//            new Product { Name = "Orange", Price = 1.00m },
//            new Product { Name = "Grape", Price = 0.75m },
//            new Product { Name = "Watermelon", Price = 2.50m }
//        };

//        // Explicitly declare the key selector function
//        Func<Product, decimal> keySelector = p => p.Price;

//        // Explicitly declare the comparer
//        IComparer<Product> comparer = new PriceThenNameDescendingComparer();

//        // Sort using OrderBy and ThenBy (without explicit type parameters)
//        var sortedProducts = products.OrderBy<Product,Decimal>(keySelector,comparer);

//        // Print the sorted results
//        foreach (var product in sortedProducts)
//        {
//            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}");
//        }
//    }
//}
