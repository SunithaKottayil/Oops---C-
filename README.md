
# Bakery Management System
## A Danish-Inspired Console App to Learn C# OOP Concepts

Welcome to the **Bakery Management System**—a simple C# console application designed to simulate a real bakery's ordering and inventory system with a Danish cultural flavor.

---

### Why This Project?
When I first started learning C# and Object-Oriented Programming (OOP), I struggled to apply theoretical concepts in code. To bridge this gap, I decided to build a hands-on project. Developing this small system not only enhanced my understanding of OOP but also made learning enjoyable.

---

## Features

- **Local Danish Bakery Products**  
  Sample items include Wienerbrød, Rugbrød, Drømmekage, Kanelsnegl, and Tebirkes.

- **Inventory Tracking**  
  Real-time stock updates, restocking, and out-of-stock notifications.

- **Order Management with Receipt Generation**  
  Simulate live orders, generate receipts, and deduct stock accordingly.

- **Daily Summary Report**  
  Get a summary at the end of the day with total sales, average order size, and current stock.

- **Prices in DKK**  
  All prices are shown in Danish Krone.

---

## Sample Products & Prices

| Product        | Price (DKK) |
| -------------- | ----------: |
| Wienerbrød     |      22.00  |
| Rugbrød        |      30.00  |
| Drømmekage     |      35.00  |
| Kanelsnegl     |      18.50  |
| Tebirkes       |      15.00  |

---

## Sample Output

```plaintext
===== Welcome to the Danish Bakery =====
Product Menu:
1. Wienerbrød    (Stock: 10) - 22.00 DKK
2. Rugbrød       (Stock: 8)  - 30.00 DKK
3. Drømmekage    (Stock: 5)  - 35.00 DKK
4. Kanelsnegl    (Stock: 12) - 18.50 DKK
5. Tebirkes      (Stock: 15) - 15.00 DKK

Enter product number to order (0 to finish): 1
Enter quantity: 2

Added 2 x Wienerbrød to your order.

Enter product number to order (0 to finish): 5
Enter quantity: 3

Added 3 x Tebirkes to your order.

Enter product number to order (0 to finish): 0

--- Order Receipt ---
2 x Wienerbrød   @ 22.00 DKK = 44.00 DKK
3 x Tebirkes     @ 15.00 DKK = 45.00 DKK
Total: 89.00 DKK

Thank you for your order!

===== End of Day Summary =====
Total Orders: 4
Total Sales: 256.00 DKK
Average Order Size: 64.00 DKK
Remaining Stock:
Wienerbrød: 8
Rugbrød: 8
Drømmekage: 5
Kanelsnegl: 12
Tebirkes: 12
