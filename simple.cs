using System;
using System.Collections.Generic;

// The class Categories is a shared container to store fixed product categories.
// These string values are constants and can be used everywhere without creating instances.
public static class Categories
{
    public const string Bread = "Brød";    
    public const string Cake = "Kage";     
    public const string Cookie = "Småkage"; 
    public const string Muffin = "Muffin"; 
}

// Product class stores details about each bakery item
public class Product
{
    public string Name;
    public decimal Price;
    public string Category;
    public int Stock;

    // Constructor sets initial values when a new Product is created
    public Product(string name, decimal price, string category, int initialStock)
    {
        Name = name;
        Price = price;
        Category = category;
        Stock = initialStock;
    }

    // Check if product is in stock
    public bool IsAvailable() => Stock > 0;

    // Reduce stock by one if available, return true if successful
    public bool TakeFromStock()
    {
        if (Stock > 0)
        {
            Stock -= 1 ;
            return true;
        }
        return false;
    }

    // Display product info with price and stock status
    public void Show()
    {
        if (IsAvailable())
        {
            Console.WriteLine($"{Name} - {Price} DKK ({Category}) - {Stock} left");
        }
        else
        {
            Console.WriteLine($"{Name}  - OUT OF STOCK");
        }
    }
}

// Customer class stores customer's name and  phone number
public class Customer
{
    public string Name;
    public string Phone;

    // Constructor initializes customer details
    public Customer(string name, string phone )
    {
        Name = name;
        Phone = phone;
    }
}

// Order class tracks a customer's order with products and time
public class Order
{
    public Customer Buyer;
    public List<Product> Items;
    public DateTime When;

    // Create an order for the given customer, set order time, initialize empty product list
    public Order(Customer customer)
    {
        Buyer = customer;
        Items = new List<Product>();
        When = DateTime.Now;
    }

    // Try to add a product to the order if it is in stock
    public bool TryAdd(Product item)
    {
        if (!item.IsAvailable())
        {
            Console.WriteLine($"Sorry! {item.Name} is OUT OF STOCK");
            return false;
        }

        if (item.TakeFromStock())
        {
            Items.Add(item);
            Console.WriteLine($" {item.Name} (In Stock: {item.Stock})");
            return true;
        }

        Console.WriteLine($"Sorry! {item.Name}  sold out!");
        return false;
    }

    // Calculate total price of the order
    public decimal Total()
    {
        decimal sum = 0;
        foreach (var item in Items)
        {
            sum += item.Price;
        }
        return sum;
    }

    // Print a receipt showing all items, prices, and total
    public void PrintReceipt()
    {
        if (Items.Count == 0)
        {
            Console.WriteLine($"\nNo items purchased for {Buyer.Name}");
            return;
        }

        Console.WriteLine($"\nORDER FOR: {Buyer.Name}");
        Console.WriteLine($"TIME: {When:dd/MM/yyyy HH:mm}");
        Console.WriteLine("----------------------------");
        foreach (var item in Items)
        {
            Console.WriteLine($"{item.Name} .... {item.Price} DKK");
        }
        Console.WriteLine("----------------------------");
        Console.WriteLine($"TOTAL: {Total()} DKK");
        Console.WriteLine();
    }
}

// Bakery class manages products, orders, and daily business logic
public class Bakery
{
    public string Name;
    private List<Product> products;
    private List<Order> todaysOrders;

    // Initialize bakery with a name and empty lists for products and orders
    public Bakery(string bakeryName)
    {
        Name = bakeryName;
        products = new List<Product>();
        todaysOrders = new List<Order>();
    }

    // Add a new product to the bakery menu with name, price, category and stock
    public void AddToMenu(string name, decimal price, string category, int initialStock)
    {
        Product newItem = new Product(name, price, category, initialStock);
        products.Add(newItem);
        Console.WriteLine($"Added {name} to menu with {initialStock} items in stock");
    }

    // Show all products on the menu with details
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

    // Get a product by its number on the menu (1-based)
    public Product GetItem(int menuNumber)
    {
        if (menuNumber >= 1 && menuNumber <= products.Count)
        {
            return products[menuNumber - 1];//index number 
        }
        return null;
    }

    // Create a new order for a customer and add it to today's orders
    public Order NewOrder(string customerName, string phone )
    {
        Customer customer = new Customer(customerName, phone);
        Order order = new Order(customer);
        todaysOrders.Add(order);
        Console.WriteLine($"New order for {customerName}");
        return order;
    }

    // Add more stock to a product by its menu number
    public void Restock(int menuNumber, int amount)
    {
        Product item = GetItem(menuNumber);
        if (item != null)
        {
            item.Stock += amount;
            Console.WriteLine($"Restocked {item.Name}. New stock: {item.Stock}");
        }
        else
        {
            Console.WriteLine("Invalid menu number for restocking");
        }
    }

    // Show summary of all orders, sales, and inventory status for the day
    public void DailySummary()
    {
        Console.WriteLine($"\n=== TODAY'S SUMMARY FOR {Name} ===");
        Console.WriteLine($"Total Orders: {todaysOrders.Count}");

        decimal totalSales = 0;
        foreach (var order in todaysOrders)
        {
            totalSales += order.Total();
        }

        Console.WriteLine($"Total Sales: {totalSales} DKK");

        decimal averageOrder = todaysOrders.Count > 0 ? totalSales / todaysOrders.Count : 0;
        Console.WriteLine($"Average Order: {averageOrder:F2} DKK");

        Console.WriteLine("\n--- INVENTORY STATUS ---");
        foreach (var product in products)
        {
            if (product.Stock == 0)
            {
                Console.WriteLine($"  {product.Name}: OUT OF STOCK!");
            }
            else if (product.Stock <= 2)
            {
                Console.WriteLine($" {product.Name}: LOW STOCK ({product.Stock} left)");
            }
            else
            {
                Console.WriteLine($"{product.Name}: {product.Stock} in stock");
            }
        }
    }
}

// Main program to run the bakery simulation
class Program
{
   static void Main()
{
    Bakery myShop = new Bakery("Hjem Bageri");

    // Preload menu with some items
    myShop.AddToMenu("Rugbrød", 25.00m, Categories.Bread, 6);
    myShop.AddToMenu("Drømmekage", 32.00m, Categories.Cake, 4);
    myShop.AddToMenu("Hindbærsnitte", 12.00m, Categories.Cookie, 3);
    myShop.AddToMenu("BlåbærMuffin", 20.00m, Categories.Muffin, 5);

    while (true)
    {
        Console.WriteLine("\n=== HJEM BAGERI MENU ===");
        Console.WriteLine("1. Show Product Menu");
        Console.WriteLine("2. Make New Order");
        Console.WriteLine("3. Restock Item");
        Console.WriteLine("4. Daily Summary");
        Console.WriteLine("5. Exit");
        Console.Write("Choose an option (1-5): ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                myShop.ShowMenu();
                break;

            case "2":
                Console.Write("Enter customer name: ");
                string name = Console.ReadLine();
                Console.Write("Enter phone number: ");
                string phone = Console.ReadLine();
                Order order = myShop.NewOrder(name, phone);

                while (true)
                {
                    myShop.ShowMenu();
                    Console.Write("Enter item number to add to order (0 to finish): ");
                    if (!int.TryParse(Console.ReadLine(), out int itemNumber) || itemNumber < 0) continue;
                    if (itemNumber == 0) break;
                    var item = myShop.GetItem(itemNumber);
                    if (item != null)
                        order.TryAdd(item);
                    else
                        Console.WriteLine("Invalid item number.");
                }

                order.PrintReceipt();
                break;

            case "3":
                myShop.ShowMenu();
                Console.Write("Enter menu number to restock: ");
                int restockNum = int.Parse(Console.ReadLine());
                Console.Write("Enter amount to add: ");
                int amount = int.Parse(Console.ReadLine());
                myShop.Restock(restockNum, amount);
                break;

            case "4":
                myShop.DailySummary();
                break;

            case "5":
                Console.WriteLine("Goodbye! Closing shop.");
                return;

            default:
                Console.WriteLine("Invalid option. Try again.");
                break;
        }
    }
 } 
}
