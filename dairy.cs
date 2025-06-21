using System;

// Product class - represents milk or butter
public class Product
{
    // Properties - what each product has/information regarding the product-name ,quantity and price/unit
    public string Name;
    public double Quantity;     // liters for milk, kg for butter
    public double PricePerUnit; // price per liter/kg
    
    // Constructor - runs automatically  when we create a new Product/object . Constructor:basic configuration 


    public Product(string name, double quantity, double pricePerUnit)
    {
        Name = name;
        Quantity = quantity;
        PricePerUnit = pricePerUnit;
    }
    
    // Method 1: Add production (when we make more)
    public void AddProduction(double amount)
    {
        Quantity = Quantity + amount;
        Console.WriteLine($"Added {amount} units of {Name}. New total: {Quantity}");
    }
    
    // Method 2: Sell product (when customers buy)
    public bool Sell(double demand)
    {
        // Check if we have enough
        if (demand > Quantity)
        {
            Console.WriteLine($"Sorry! We only have {Quantity} units of {Name}. Cannot sell {demand}");
            return false; // Sale failed
        }
        
        // We have enough, so sell it
        Quantity = Quantity - demand;
        Console.WriteLine($"Sold {demand} units of {Name}. Remaining: {Quantity}");
        return true; // Sale successful
    }
    
    // Method 3: Calculate total value of our inventory
    public double GetValue()
    {
        return Quantity * PricePerUnit;
    }
}

// DairyFarm class - manages the whole farm
public class DairyFarm
{
    // Properties
    public string FarmName;
    public Product Milk;
    public Product Butter;
    public double DailyExpenses;
    public double TodaysIncome;
    
    // Constructor - sets up the farm
    public DairyFarm(string farmName)
    {
        FarmName = farmName;
        DailyExpenses = 200.0; // 200 DKK per day
        TodaysIncome = 0.0;
        
        // Create our two products with starting inventory
        Milk = new Product("Milk", 20, 8.0);     // 20 liters at 8 DKK each
        Butter = new Product("Butter", 3, 45.0); // 3 kg at 45 DKK each
        
        Console.WriteLine($"Welcome to {FarmName}!");
        Console.WriteLine("Starting inventory:");
        Console.WriteLine($"- {Milk.Quantity} liters of milk");
        Console.WriteLine($"- {Butter.Quantity} kg of butter");
    }
    
    // Show current inventory
    public void ShowInventory()
    {
        Console.WriteLine($"\n=== {FarmName} INVENTORY ===");
        Console.WriteLine($"Milk: {Milk.Quantity} liters (Value: {Milk.GetValue():F2} DKK)");
        Console.WriteLine($"Butter: {Butter.Quantity} kg (Value: {Butter.GetValue():F2} DKK)");
        
        double totalValue = Milk.GetValue() + Butter.GetValue();
        Console.WriteLine($"Total Inventory Value: {totalValue:F2} DKK");
    }
    
    // Record today's production
    public void RecordProduction()
    {
        Console.WriteLine("\n=== RECORD PRODUCTION ===");
        
        Console.Write("Enter milk produced today (liters): ");
        double milkAmount = double.Parse(Console.ReadLine());
        Milk.AddProduction(milkAmount);
        
        Console.Write("Enter butter produced today (kg): ");
        double butterAmount = double.Parse(Console.ReadLine());
        Butter.AddProduction(butterAmount);
        
        Console.WriteLine("Production recorded successfully!");
    }
    
    // Make a sale
    public void MakeSale()
    {
        Console.WriteLine("\n=== MAKE SALE ===");
        Console.WriteLine("What do you want to sell?");
        Console.WriteLine("1. Milk");
        Console.WriteLine("2. Butter");
        Console.Write("Choose (1-2): ");
        
        string productChoice = Console.ReadLine();
        
        if (productChoice == "1")
        {
            Console.Write($"How many liters of milk to sell? (Available: {Milk.Quantity}): ");
            double amount = double.Parse(Console.ReadLine());
            if (Milk.Sell(amount))
            {
                double income = amount * Milk.PricePerUnit;
                TodaysIncome += income;
                Console.WriteLine($"Sale successful! Income: {income:F2} DKK");
            }
        }
        else if (productChoice == "2")
        {
            Console.Write($"How many kg of butter to sell? (Available: {Butter.Quantity}): ");
            double amount = double.Parse(Console.ReadLine());
            if (Butter.Sell(amount))
            {
                double income = amount * Butter.PricePerUnit;
                TodaysIncome += income;
                Console.WriteLine($"Sale successful! Income: {income:F2} DKK");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice!");
        }
    }
    
    // Show daily report
    public void ShowDailyReport()
    {
        Console.WriteLine($"\n=== DAILY REPORT FOR {FarmName} ===");
        Console.WriteLine($"Today's Income: {TodaysIncome:F2} DKK");
        Console.WriteLine($"Daily Expenses: {DailyExpenses:F2} DKK");
        
        double profit = TodaysIncome - DailyExpenses;
        Console.WriteLine($"Today's Profit: {profit:F2} DKK");
        
        if (profit > 0)
        {
            Console.WriteLine(" Good day! Farm made profit!");
        }
        else if (profit == 0)
        {
            Console.WriteLine(" Break even day");
        }
        else
        {
            Console.WriteLine(" Loss today. Need more sales!");
        }
        
        ShowInventory();
    }
}

// Main program - this runs first
class Program
{
    static void Main()
    {
        Console.WriteLine("=== DAIRY FARM MANAGEMENT SYSTEM ===");
        
        // Create our farm
        DairyFarm myFarm = new DairyFarm("Dairy Calc");
        
        // Main menu loop
        while (true)
        {
            Console.WriteLine("\n=== MAIN MENU ===");
            Console.WriteLine("1. View Inventory");
            Console.WriteLine("2. Record Production");
            Console.WriteLine("3. Make Sale");
            Console.WriteLine("4. Daily Report");
            Console.WriteLine("5. Exit");
            Console.Write("Choose option (1-5): ");
            
            string choice = Console.ReadLine();
            
            if (choice == "1")
            {
                myFarm.ShowInventory();
            }
            else if (choice == "2")
            {
                myFarm.RecordProduction();
            }
            else if (choice == "3")
            {
                myFarm.MakeSale();
            }
            else if (choice == "4")
            {
                myFarm.ShowDailyReport();
            }
            else if (choice == "5")
            {
                Console.WriteLine("Goodbye from Dairy calc! 🐄");
                Console.WriteLine("Thanks for managing the dairy today!");
                break; // Exit the loop
            }
            else
            {
                Console.WriteLine("Invalid choice! Please enter 1, 2, 3, 4, or 5.");
            }
        }
        
        Console.WriteLine("\nPress any key to close...");
        Console.ReadKey();
    }
}
