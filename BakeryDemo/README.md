# Bakery Demo (WinForms + SQL Server)

This is a small demonstration project built in **C# WinForms** with a **SQL Server** backend.  
It simulates a simple **Point of Sale (POS) system for a bakery**, where users can search products, add them to a cart, and save an order to the database.

---

## Features
- **Product List**: Browse bakery products with search functionality.  
- **Cart System**: Add products with quantities, update or clear the cart.  
- **Order Saving**: Save an order (with line items) into the database.  
- **Total Calculation**: Automatically updates the total price when items are added or quantities changed.  

---

## Technologies Used
- **C# WinForms (.NET Framework)**  
- **SQL Server Express (LocalDB)**  
- **ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter)**  

---

## Project Structure
- `MainForm.cs` → Main UI (products grid, cart grid, buttons for add, clear, save).  
- `ProductRepository.cs` → Loads product data from SQL Server.  
- `OrderRepository.cs` → Saves orders and order lines to SQL Server.  
- `CartItem.cs` → Model for items in the shopping cart.  
- `Db.cs` → Connection helper (returns an open SQL connection).  

---

## Database Schema
The database uses three simple tables:

```sql
CREATE TABLE Produkte (
    ProduktID INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Preis DECIMAL(10,2) NOT NULL
);

CREATE TABLE Bestellungen (
    BestellungID INT IDENTITY PRIMARY KEY,
    Gesamtpreis DECIMAL(10,2) NOT NULL,
    Bestelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Bestellpositionen (
    PositionID INT IDENTITY PRIMARY KEY,
    BestellungID INT NOT NULL FOREIGN KEY REFERENCES Bestellungen(BestellungID),
    ProduktID INT NOT NULL FOREIGN KEY REFERENCES Produkte(ProduktID),
    Menge INT NOT NULL,
    Preis DECIMAL(10,2) NOT NULL
);
