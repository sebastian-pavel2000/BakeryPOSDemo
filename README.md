# 🍞 BakeryPOSDemo

A simple **Point of Sale (POS)** demo application for a bakery, built with **C# (.NET WinForms)** and **SQL Server LocalDB**.  
This project was created to demonstrate practical experience with desktop application development, data handling, and basic database operations.

---

## 🧠 Overview

This application simulates a small bakery POS system with the following features:

- Displaying products from a database  
- Searching for products  
- Adding items to a shopping cart  
- Calculating order totals  
- Saving completed orders with product details  

It’s designed as a learning and demonstration project inspired by real ERP-like software used in bakeries.

---

## ⚙️ Tech Stack

- **Language:** C# (.NET Framework / WinForms)  
- **Database:** Microsoft SQL Server (LocalDB)  
- **UI:** Windows Forms (DataGridView, Buttons, Labels, etc.)  
- **IDE:** Visual Studio 2022  
- **ORM:** ADO.NET (manual SQL commands)  

---

## 🗂️ Project Structure

BackNetDemo/
│
├── BakeryDemo/ # Main application source code
│ ├── Form1.cs # Main WinForms interface (MainForm)
│ ├── ProductRepository.cs # Handles product data retrieval
│ ├── OrderRepository.cs # Handles order saving and transactions
│ ├── CartItem.cs # Data model for shopping cart items
│ └── Db.cs # Database connection helper
│
├── bakery_demo.sql # SQL script to create and seed database
├── BackNetDemo.sln # Visual Studio solution file
└── README.md # Project documentation

---

## 🧩 Database Setup

The project uses **SQL Server LocalDB** for simplicity.  
To create the database and tables, follow these steps:

1. Open **SQL Server Management Studio (SSMS)** or **Visual Studio → SQL Server Object Explorer**  
2. Connect to `(localdb)\MSSQLLocalDB`  
3. Open the file `bakery_demo.sql`  
4. Run the script to create the `BakeryDemo` database and seed sample data  

Sample products (inserted automatically):
- Croissant – 1.50 €  
- Baguette – 2.00 €  
- Pretzel – 1.20 €  
- Apple Pie Slice – 2.50 €  
- Coffee – 2.00 €

---

## 🖥️ How to Run

1. Clone the repository  
   ```bash
   git clone https://github.com/sebastian-pavel2000/BakeryPOSDemo.git
