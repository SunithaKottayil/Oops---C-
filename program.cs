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
