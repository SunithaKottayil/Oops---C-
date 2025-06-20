using System;
using System.Collections.Generic;// gives access to generic data structures fx . lists and dictionaries. here  we use  List<Product> to store multiple products.

/// <summary>
/// Enum for product categories, making it robust and type-safe.
/// </summary>
public enum ProductCategory
{
    Bread,
    Cake,
    Pastry,
    Cookie,
    Muffin,
    Other
}

/// <summary>
/// Product class: represents a bakery product, with encapsulation and validation.
/// </summary>
public class Product
{
    public Guid ProductId { get; } = Guid.NewGuid();
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public ProductCategory Category { get; private set; }
    private int stock;
    public int Stock
    {
        get => stock;
        private set => stock = (value >= 0) ? value : 0;
    }

    public Product(string name, decimal price, ProductCategory category, int initialStock)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name can't be empty.");
        if (price < 0) throw new ArgumentOutOfRangeException(nameof(price), "Price can't be negative.");
        Name = name;
        Price = price;
        Category = category;
        Stock = initialStock;
    }

    public bool IsAvailable(int quantity = 1) => Stock >= quantity;

    public bool ReduceStock(int quantity)
    {
        if (quantity <= 0) return false;
        if (Stock >= quantity)
        {
            Stock -= quantity;
            return true;
        }
        return false;
    }

    public void Restock(int quantity)
    {
        if (quantity > 0) Stock += quantity;
    }

    public void Show()
    {
        string status = Stock > 0 ? $"{Stock} left" : "OUT OF STOCK";
        Console.WriteLine($"{Name} - {Price:C} ({Category}) - {status}");
    }
}

/// <summary>
/// Customer class: now includes a unique ID for tracking.
/// </summary>
public class Customer
{
    public Guid CustomerId { get; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Phone { get; private set; }

    public Customer(string name, string phone = "")
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Customer name required.");
        Name = name;
        Phone = phone ?? "";
    }
}

/// <summary>
/// OrderItem: One line in an order, decouples order from product inventory.
/// </summary>
public class OrderItem
{
    public Product Product { get; }
    public int Quantity { get; }
    public decimal LineTotal => Product.Price * Quantity;

    public OrderItem(Product product, int quantity)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");

        Product = product;
        Quantity = quantity;
    }
}

/// <summary>
/// Order class: stores customer, items, and timestamp. Uses OrderItem for quantities.
/// </summary>
public class Order
{
    public Guid OrderId { get; } = Guid.NewGuid();
    public Customer Buyer { get; }
    public List<OrderItem> Items { get; }
    public DateTime When { get; }

    public Order(Customer customer)
    {
        Buyer = customer ?? throw new ArgumentNullException(nameof(customer));
        Items = new List<OrderItem>();
        When = DateTime.Now;
    }

    public bool TryAdd(Product product, int quantity)
    {
        if (!product.IsAvailable(quantity))
        {
            Console.WriteLine($"Sorry! Only {product.Stock} of {product.Name} available.");
            return false;
        }
        if (product.ReduceStock(quantity))
        {
            Items.Add(new OrderItem(product, quantity));
            Console.WriteLine($"Added {quantity} x {product.Name} (Stock remaining: {product.Stock})");
            return true;
        }
        return false;
    }

    public decimal Total() => Items.Sum(item => item.LineTotal);

    public void PrintReceipt()
    {
        if (!Items.Any())
        {
            Console.WriteLine($"\nNo items purchased for {Buyer.Name}");
            return;
        }
        Console.WriteLine($"\nORDER FOR: {Buyer.Name}");
        Console.WriteLine($"TIME: {When:MM/dd/yyyy HH:mm}");
        Console.WriteLine("----------------------------");
        foreach (var item in Items)
            Console.WriteLine($"{item.Quantity} x {item.Product.Name} .... {item.LineTotal:C}");
        Console.WriteLine("----------------------------");
        Console.WriteLine($"TOTAL: {Total():C}\n");
    }
}

/// <summary>
/// The core Bakery business logic: manages products, orders, and reports.
/// </summary>
public class Bakery
{
    public string Name { get; }
    private readonly List<Product> products = new();
    private readonly List<Order> todaysOrders = new();

    public Bakery(string bakeryName)
    {
        if (string.IsNullOrWhiteSpace(bakeryName)) throw new ArgumentException("Bakery name required.");
        Name = bakeryName;
    }

    public void AddToMenu(string name, decimal price, ProductCategory category, int initialStock)
    {
        var newProduct = new Product(name, price, category, initialStock);
        products.Add(newProduct);
        Console.WriteLine($"Added {name} to menu with {initialStock} items in stock.");
    }

    public void ShowMenu()
    {
        Console.WriteLine($"\n=== {Name.ToUpper()} MENU ===");
        for (int i = 0; i < products.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            products[i].Show();
        }
        Console.WriteLine();
    }

    public Product GetItem(int menuNumber)
    {
        if (menuNumber >= 1 && menuNumber <= products.Count)
            return products[menuNumber - 1];
        return null;
    }

    public Order NewOrder(string customerName, string phone = "")
    {
        var customer = new Customer(customerName, phone);
        var order = new Order(customer);
        todaysOrders.Add(order);
        Console.WriteLine($"Started order for {customer.Name}");
        return order;
    }

    public void Restock(int menuNumber, int amount)
    {
        var item = GetItem(menuNumber);
        if (item != null)
        {
            item.Restock(amount);
            Console.WriteLine($"Restocked {item.Name}. New stock: {item.Stock}");
        }
        else
        {
            Console.WriteLine("Invalid menu number for restocking.");
        }
    }

    /// <summary>
    /// More detailed daily summary: includes best sellers, low stock, and total sales.
    /// </summary>
    public void DailySummary()
    {
        Console.WriteLine($"\n=== TODAY'S SUMMARY FOR {Name} ===");
        Console.WriteLine($"Total Orders: {todaysOrders.Count}");

        decimal totalSales = todaysOrders.Sum(o => o.Total());
        Console.WriteLine($"Total Sales: {totalSales:C}");

        decimal averageOrder = todaysOrders.Any() ? totalSales / todaysOrders.Count : 0;
        Console.WriteLine($"Average Order: {averageOrder:C2}");

        // Best seller
        var bestSeller = todaysOrders
            .SelectMany(o => o.Items)
            .GroupBy(i => i.Product.Name)
            .OrderByDescending(g => g.Sum(x => x.Quantity))
            .FirstOrDefault();

        if (bestSeller != null)
            Console.WriteLine($"Best Seller: {bestSeller.Key} ({bestSeller.Sum(x => x.Quantity)} sold)");

        // Inventory status
        Console.WriteLine("\n--- INVENTORY STATUS ---");
        foreach (var product in products)
        {
            if (product.Stock == 0)
                Console.WriteLine($"⚠️  {product.Name}: OUT OF STOCK!");
            else if (product.Stock <= 2)
                Console.WriteLine($"⚠️  {product.Name}: LOW STOCK ({product.Stock} left)");
            else
                Console.WriteLine($"✅ {product.Name}: {product.Stock} in stock");
        }
    }
}

/// <summary>
/// Main entry point. Shows improved input validation and order logic.
/// </summary>
class Program
{
    static void Main()
    {
        Bakery myShop = new Bakery("Mom's Bakery");

        // Add menu items
        myShop.AddToMenu("Chocolate Chip Cookies", 2.50m, ProductCategory.Cookie, 10);
        myShop.AddToMenu("Sourdough Bread", 4.00m, ProductCategory.Bread, 5);
        myShop.AddToMenu("Birthday Cake", 28.00m, ProductCategory.Cake, 2);
        myShop.AddToMenu("Apple Danish", 3.25m, ProductCategory.Pastry, 8);
        myShop.AddToMenu("Blueberry Muffin", 2.75m, ProductCategory.Muffin, 1);

        myShop.ShowMenu();

        // Order 1: Sarah Wilson
        Console.WriteLine("--- CUSTOMER 1: Sarah Wilson ---");
        var order1 = myShop.NewOrder("Sarah Wilson", "555-0123");
        order1.TryAdd(myShop.GetItem(1), 2); // 2 Cookies
        order1.TryAdd(myShop.GetItem(2), 1); // 1 Bread
        order1.TryAdd(myShop.GetItem(5), 1); // 1 Muffin
        order1.PrintReceipt();

        // Order 2: Mike Johnson
        Console.WriteLine("--- CUSTOMER 2: Mike Johnson ---");
        var order2 = myShop.NewOrder("Mike Johnson");
        order2.TryAdd(myShop.GetItem(3), 1); // 1 Cake
        order2.TryAdd(myShop.GetItem(5), 1); // 1 Muffin (should be sold out)
        order2.TryAdd(myShop.GetItem(4), 2); // 2 Danish
        order2.PrintReceipt();

        // Order 3: Little Timmy
        Console.WriteLine("--- CUSTOMER 3: Little Timmy ---");
        var order3 = myShop.NewOrder("Little Timmy");
        order3.TryAdd(myShop.GetItem(1), 3); // 3 Cookies
        order3.PrintReceipt();

        Console.WriteLine("\n--- UPDATED MENU AFTER SALES ---");
        myShop.ShowMenu();

        // Restock
        Console.WriteLine("\n--- RESTOCKING ---");
        myShop.Restock(5, 12); // Muffins
        myShop.Restock(1, 20); // Cookies

        Console.WriteLine("\n--- MENU AFTER RESTOCKING ---");
        myShop.ShowMenu();

        // Daily Summary
        myShop.DailySummary();

        Console.WriteLine("\nPress Enter to close...");
        Console.ReadLine();
    }
}
