
# Bakery Management System
## A Danish-Inspired Console App to Learn C# OOP Concepts

Welcome to the **Bakery Management System**—a simple C# console application designed to simulate a real bakery's ordering and inventory system with a Danish cultural flavor.

---

### Why This Project?
When I first started learning C# and Object-Oriented Programming (OOP), I struggled to apply theoretical concepts in code. To bridge this gap, I decided to build a hands-on project. Developing this small system not only enhanced my understanding of OOP but also made learning enjoyable.

---


#  Hjem Bageri -  Bakery Management System
## A Danish-Inspired Console App to Learn C# OOP Concepts

A simple console-based bakery ordering system in C# by using Oop Concepts, designed to help manage products, customer orders, restocking, and daily summaries.

## 📋 Features

- Add and manage bakery products
- Create customer orders with stock checking
- Automatically updates stock levels
- Restock products
- View detailed receipts
- Show daily sales summary
- Menu-based interface for better user experience

---

##  How to Run

1. **Clone the repository:**

   ```bash
   git clone https/github.com/SunithaKottayil/Sunitha-Kottayil.github.io
   cd hjem-bageri
   ```

2. **Build and Run the Project:**

   - Open the solution in **Visual Studio**, or
   - Compile from terminal:

     ```bash
     dotnet build
     dotnet run
     ```

---

##  Project Structure

```plaintext
Bakery.cs       # Core classes: Product, Customer, Order, Bakery
Program.cs      # Contains Main() and the interactive menu
Categories.cs   # Contains fixed product category names (e.g., Cake, Bread)
```

---

## 🧁 Example Menu Flow

When you run the app, you'll see:

```
=== HJEM BAGERI MENU ===
1. Show Product Menu
2. Make New Order
3. Restock Item
4. Daily Summary
5. Exit
```

---

## ✅ Sample Product List

| Name           | Category | Price (DKK) | Stock |
|----------------|----------|-------------|--------|
| Rugbrød        | Brød     | 25.00       | 6      |
| Drømmekage     | Kage     | 32.00       | 4      |
| Hindbærsnitte  | Småkage  | 12.00       | 3      |
| BlåbærMuffin   | Muffin   | 20.00       | 5      |

---

## 🛠 Technologies Used

- C# (.NET 6+)
- Console-based UI

---

##  Future Improvements

- Save/load orders to a file or database
- Support for removing products
- Admin login for restocking
- Unit tests for key features

---

## 📄 License

MIT License — free to use and modify. Attribution appreciated. 😊
