// Import necessary system libraries for basic functionality
using System; // For Console input/output and basic operations
using System.Collections.Generic; // For using List<T> collections

// CLASS DEFINITION: Product
// Purpose: Represents individual items sold in the bakery with inventory tracking
// Real-world equivalent: A physical product like bread, cake, etc. with stock count
public class Product
{
    // PROPERTIES: Store the essential information about each product
    public string Name;        // What the product is called (e.g., "Chocolate Cake")
    public decimal Price;      // How much it costs (using decimal for money precision)
    public string Category;    // What type of item it is (bread, cake, pastry, etc.)
    public int Stock;          // How many items we have left in inventory
    
    // CONSTRUCTOR: Special method that runs when creating a new Product object
    // Purpose: Initialize a new product with its basic information and starting inventory
    public Product(string name, decimal price, string category, int initialStock)
    {
        this.Name = name;           // Store the product name in this object
        this.Price = price;         // Store the price in this object
        this.Category = category;   // Store the category in this object
        this.Stock = initialStock;  // Store how many we have in stock
        // Note: "this." refers to the current object being created
    }
    
    // METHOD: Check if product is available for purchase
    // Purpose: See if we have any items left in stock
    public bool IsAvailable()
    {
        return Stock > 0;  // Returns true if we have stock, false if sold out
    }
    
    // METHOD: Reduce stock when item is sold
    // Purpose: Update inventory when customer buys this product
    public bool TakeFromStock()
    {
        if (Stock > 0)      // Check if we have any left
        {
            Stock--;        // Reduce stock by 1
            return true;    // Successfully took item from stock
        }
        return false;       // No stock available
    }
    
    // METHOD: Display product information with stock status
    // Purpose: Print the product details including availability
    public void Show()
    {
        if (IsAvailable())  // If we have stock
        {
            // Show product with stock count: "Chocolate Cake - $25.99 (Cake) - 5 left"
            Console.WriteLine($"{Name} - ${Price} ({Category}) - {Stock} left");
        }
        else  // If sold out
        {
            // Show product as out of stock: "Chocolate Cake - $25.99 (Cake) - OUT OF STOCK"
            Console.WriteLine($"{Name} - ${Price} ({Category}) - OUT OF STOCK");
        }
    }
}

// CLASS DEFINITION: Customer
// Purpose: Represents people who buy from the bakery
// Real-world equivalent: A person walking into the bakery
public class Customer
{
    // PROPERTIES: Store customer information
    public string Name;  // Customer's name
    public string Phone; // Customer's phone number (optional)
    
    // CONSTRUCTOR: Create a new customer
    // Purpose: Initialize customer information when they place an order
    public Customer(string name, string phone = "")
    {
        this.Name = name;   // Store customer name
        this.Phone = phone; // Store phone number (default to empty string if not provided)
        // Note: phone = "" means phone is optional when creating a Customer
    }
}

// CLASS DEFINITION: Order
// Purpose: Represents what a customer wants to purchase
// Real-world equivalent: A receipt or order form
public class Order
{
    // PROPERTIES: Store order information
    public Customer Buyer;        // Who is making this order
    public List<Product> Items;   // List of products they want to buy
    public DateTime When;         // When the order was placed
    
    // CONSTRUCTOR: Create a new order for a customer
    // Purpose: Start a new order and record when it was made
    public Order(Customer customer)
    {
        this.Buyer = customer;                    // Remember who this order belongs to
        this.Items = new List<Product>();         // Create empty list to store products
        this.When = DateTime.Now;                 // Record current date/time
        // Note: List<Product>() creates a new empty list that can hold Product objects
    }
    
    // METHOD: Try to add a product to this order
    // Purpose: When customer selects an item, check stock and add it to their order
    public bool TryAdd(Product item)
    {
        if (!item.IsAvailable())  // Check if item is out of stock
        {
            Console.WriteLine($"Sorry! {item.Name} is OUT OF STOCK");
            return false;  // Could not add item
        }
        
        if (item.TakeFromStock())  // Try to take item from inventory
        {
            Items.Add(item);                          // Add the product to the order's item list
            Console.WriteLine($"Added {item.Name} (Stock remaining: {item.Stock})");
            return true;   // Successfully added item
        }
        else
        {
            Console.WriteLine($"Sorry! {item.Name} just sold out!");
            return false;  // Could not add item
        }
    }
    
    // METHOD: Calculate total cost of all items in order
    // Purpose: Add up the price of everything the customer is buying
    public decimal Total()
    {
        decimal sum = 0;              // Start with zero total
        foreach(var item in Items)    // Loop through each product in the order
        {
            sum += item.Price;        // Add each product's price to the running total
        }
        return sum;                   // Return the final total amount
        // Note: foreach loops through each item in the Items list automatically
    }
    
    // METHOD: Print a receipt for this order
    // Purpose: Show customer what they bought and how much it costs
    public void PrintReceipt()
    {
        if (Items.Count == 0)  // Check if order is empty
        {
            Console.WriteLine($"\nNo items purchased for {Buyer.Name}");
            return;
        }
        
        Console.WriteLine();          // Print empty line for spacing
        
        // Print receipt header with customer name
        Console.WriteLine($"ORDER FOR: {Buyer.Name}");
        
        // Print when the order was placed, formatted nicely
        Console.WriteLine($"TIME: {When.ToString("MM/dd/yyyy HH:mm")}");
        
        // Print separator line to make receipt look professional
        Console.WriteLine("----------------------------");
        
        // Loop through each item and print it with price
        foreach(var item in Items)
        {
            // Print each item in format: "Chocolate Cake .... $25.99"
            Console.WriteLine($"{item.Name} .... ${item.Price}");
        }
        
        // Print bottom separator line
        Console.WriteLine("----------------------------");
        
        // Print total amount due
        Console.WriteLine($"TOTAL: ${Total()}");
        
        Console.WriteLine(); // Print empty line for spacing
    }
}

// CLASS DEFINITION: Bakery
// Purpose: The main business system that manages everything
// Real-world equivalent: The bakery business itself
public class Bakery
{
    // PROPERTIES: Store bakery business information
    public string Name;                    // Name of the bakery
    private List<Product> products;        // All products available for sale
    private List<Order> todaysOrders;      // All orders placed today
    // Note: "private" means only this class can access these variables directly
    
    // CONSTRUCTOR: Create a new bakery business
    // Purpose: Set up a new bakery with empty inventory and no orders
    public Bakery(string bakeryName)
    {
        this.Name = bakeryName;                          // Store the bakery's name
        this.products = new List<Product>();             // Create empty product list
        this.todaysOrders = new List<Order>();           // Create empty order list
    }
    
    // METHOD: Add new products to the bakery's menu with initial stock
    // Purpose: Stock the bakery with items to sell
    public void AddToMenu(string name, decimal price, string category, int initialStock)
    {
        Product newItem = new Product(name, price, category, initialStock);  // Create new product with stock
        products.Add(newItem);                                               // Add it to the menu
        Console.WriteLine($"Added {name} to menu with {initialStock} items in stock");
    }
    
    // METHOD: Display all available products with stock status
    // Purpose: Show customers what they can buy and what's available
    public void ShowMenu()
    {
        // Print menu header with bakery name in uppercase
        Console.WriteLine($"\n=== {Name.ToUpper()} MENU ===");
        
        // Loop through all products and number them
        for(int i = 0; i < products.Count; i++)
        {
            Console.Write($"{i + 1}. ");     // Print menu number (1, 2, 3, etc.)
            products[i].Show();              // Print product details with stock info
            // Note: i + 1 because arrays start at 0 but we want menu to start at 1
        }
        
        Console.WriteLine(); // Empty line for spacing
    }
    
    // METHOD: Get a specific product by menu number
    // Purpose: Let customers select items by number (1, 2, 3, etc.)
    public Product GetItem(int menuNumber)
    {
        // Check if the menu number is valid (between 1 and total products)
        if(menuNumber >= 1 && menuNumber <= products.Count)
        {
            return products[menuNumber - 1];  // Return the product (subtract 1 for array index)
            // Note: Subtract 1 because arrays start at 0 but menu numbers start at 1
        }
        return null;  // Return null if invalid menu number
        // Note: null means "nothing" - indicates the menu number was invalid
    }
    
    // METHOD: Start a new order for a customer
    // Purpose: Begin the ordering process for someone who wants to buy something
    public Order NewOrder(string customerName, string phone = "")
    {
        Customer customer = new Customer(customerName, phone);  // Create customer object
        Order order = new Order(customer);                      // Create order for this customer
        todaysOrders.Add(order);                               // Add order to today's list
        
        // Confirm that we started the order
        Console.WriteLine($"Started order for {customerName}");
        
        return order;  // Return the order so we can add items to it
    }
    
    // METHOD: Restock a product by menu number
    // Purpose: Add more inventory when we get new deliveries
    public void Restock(int menuNumber, int amount)
    {
        Product item = GetItem(menuNumber);  // Get the product
        if (item != null)
        {
            item.Stock += amount;  // Add to existing stock
            Console.WriteLine($"Restocked {item.Name}. New stock: {item.Stock}");
        }
        else
        {
            Console.WriteLine("Invalid menu number for restocking");
        }
    }
    
    // METHOD: Show summary of today's business including inventory status
    // Purpose: See how well the bakery did today and what needs restocking
    public void DailySummary()
    {
        // Print summary header
        Console.WriteLine($"\n=== TODAY'S SUMMARY FOR {Name} ===");
        
        // Show how many orders we took today
        Console.WriteLine($"Total Orders: {todaysOrders.Count}");
        
        decimal totalSales = 0;               // Variable to track total money made
        foreach(var order in todaysOrders)    // Loop through each order from today
        {
            totalSales += order.Total();      // Add each order's total to overall sales
        }
        
        // Show total money made today
        Console.WriteLine($"Total Sales: ${totalSales}");
        
        // Calculate and show average order size
        // Note: Check if we have orders to avoid dividing by zero
        decimal averageOrder = todaysOrders.Count > 0 ? totalSales / todaysOrders.Count : 0;
        Console.WriteLine($"Average Order: ${averageOrder:F2}");
        // Note: :F2 formats the number to show exactly 2 decimal places
        
        // Show inventory status
        Console.WriteLine("\n--- INVENTORY STATUS ---");
        foreach(var product in products)
        {
            if (product.Stock == 0)
            {
                Console.WriteLine($"⚠️  {product.Name}: OUT OF STOCK!");
            }
            else if (product.Stock <= 2)  // Low stock warning
            {
                Console.WriteLine($"⚠️  {product.Name}: LOW STOCK ({product.Stock} left)");
            }
            else
            {
                Console.WriteLine($"✅ {product.Name}: {product.Stock} in stock");
            }
        }
    }
}

// MAIN PROGRAM CLASS
// Purpose: Contains the Main method where the program starts
class Program
{
    // MAIN METHOD: Entry point of the program
    // Purpose: This is where the program begins running
    static void Main()
    {
        // STEP 1: Create the bakery business
        Bakery myShop = new Bakery("Mom's Bakery");  // Create bakery with name
        
        // STEP 2: Stock the bakery with products (name, price, category, initial stock)
        // Add various bakery items with their prices, categories, and how many we have
        myShop.AddToMenu("Chocolate Chip Cookies", 2.50m, "Cookie", 10);  // 10 cookies in stock
        myShop.AddToMenu("Sourdough Bread", 4.00m, "Bread", 5);           // 5 loaves in stock
        myShop.AddToMenu("Birthday Cake", 28.00m, "Cake", 2);             // 2 cakes in stock
        myShop.AddToMenu("Apple Danish", 3.25m, "Pastry", 8);             // 8 pastries in stock
        myShop.AddToMenu("Blueberry Muffin", 2.75m, "Muffin", 1);         // Only 1 muffin left!
        
        // STEP 3: Show the menu to customers
        myShop.ShowMenu();  // Display all available products with stock counts
        
        // STEP 4: Simulate customers coming in and placing orders
        
        // FIRST CUSTOMER: Sarah Wilson
        Console.WriteLine("--- CUSTOMER 1: Sarah Wilson ---");
        Order order1 = myShop.NewOrder("Sarah Wilson", "555-0123");
        order1.TryAdd(myShop.GetItem(1));  // Try to add Chocolate Chip Cookies
        order1.TryAdd(myShop.GetItem(2));  // Try to add Sourdough Bread
        order1.TryAdd(myShop.GetItem(5));  // Try to add Blueberry Muffin (only 1 left!)
        order1.PrintReceipt();
        
        // SECOND CUSTOMER: Mike Johnson (wants the same muffin!)
        Console.WriteLine("--- CUSTOMER 2: Mike Johnson ---");
        Order order2 = myShop.NewOrder("Mike Johnson");
        order2.TryAdd(myShop.GetItem(3));  // Try to add Birthday Cake
        order2.TryAdd(myShop.GetItem(5));  // Try to add Blueberry Muffin (should be sold out now!)
        order2.TryAdd(myShop.GetItem(4));  // Try to add Apple Danish instead
        order2.PrintReceipt();
        
        // THIRD CUSTOMER: Little Timmy (wants lots of cookies)
        Console.WriteLine("--- CUSTOMER 3: Little Timmy ---");
        Order order3 = myShop.NewOrder("Little Timmy");
        // Try to buy 3 cookies (we should have enough)
        order3.TryAdd(myShop.GetItem(1));  // Cookie #1
        order3.TryAdd(myShop.GetItem(1));  // Cookie #2  
        order3.TryAdd(myShop.GetItem(1));  // Cookie #3
        order3.PrintReceipt();
        
        // Show updated menu with current stock levels
        Console.WriteLine("\n--- UPDATED MENU AFTER SALES ---");
        myShop.ShowMenu();
        
        // STEP 5: Restock some items
        Console.WriteLine("\n--- RESTOCKING ---");
        myShop.Restock(5, 12);  // Restock muffins (item #5) with 12 more
        myShop.Restock(1, 20);  // Restock cookies (item #1) with 20 more
        
        // Show menu after restocking
        Console.WriteLine("\n--- MENU AFTER RESTOCKING ---");
        myShop.ShowMenu();
        
        // STEP 6: End of day - show business summary with inventory status
        myShop.DailySummary();
        
        // STEP 7: Wait for user to press Enter before closing program
        Console.WriteLine("\nPress Enter to close...");
        Console.ReadLine();  // Wait for user input before ending program
    }
}
