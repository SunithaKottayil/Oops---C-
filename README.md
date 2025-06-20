# Oops---C# !!!!!!!!!!!!!
Bakery Management System

I was struggling with OOP concepts while learning C#. Even though it seemed easy while reading the theoretical concepts, it was too difficult to write code to use and explain these concepts. So I decided to build something I could actually visualize - a bakery system (got inspired from one of the videos I've seen).

Turns out it really helped me understand

What it does

Manages bakery products (bread, cake and Pastries.)
Takes customer orders
Prints receipts
Shows daily sales summary
Shows when an items is out of stock 

Why I built this
I was struggling with OOP concepts in my CS class. Abstract examples didn't click for me, so I decided to build something I could actually visualize - a bakery system.
Turns out it really helped me understand:

How to design classes
When to use methods vs properties
How objects interact with each other

Running it
bashgit clone https://github.com/yourusername/bakery-system.git
cd bakery-system
dotnet run
Or just open it in Visual Studio and hit F5.
Sample output
=== MOM'S BAKERY MENU ===
1. Chocolate Chip Cookies - $2.50 (Cookie)
2. Sourdough Bread - $4.00 (Bread)
3. Birthday Cake - $28.00 (Cake)

--- CUSTOMER 1 ---
Started order for Sarah Wilson
Added Chocolate Chip Cookies
Added Sourdough Bread

ORDER FOR: Sarah Wilson
TIME: 06/20/2025 10:30
----------------------------
Chocolate Chip Cookies .... $2.50
Sourdough Bread .... $4.00
----------------------------
TOTAL: $6.50
What I learned

Planning before coding saves time (wish I learned this earlier!)
Simple, readable code is better than "clever" code
Real-world examples make abstract concepts click
Comments should explain WHY, not WHAT

Code structure

Product.cs - represents bakery items
Customer.cs - customer info
Order.cs - handles orders and receipts
Bakery.cs - main business logic
Program.cs - runs everything

Things I'd add next

Database instead of in-memory storage
Web interface (maybe Blazor?)
Inventory tracking
Multiple locations

Notes
The code is heavily commented because I wanted to document my learning process. Probably over-commented for a real project, but helpful if you're learning OOP.
Feel free to use this for your own learning or homework (but understand it first!).
Questions?
Open an issue or message me. Always happy to help fellow learners.

Built while procrastinating on my data structures homework
