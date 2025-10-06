-- ===================================================
-- BakeryPOSDemo Database Initialization Script
-- Author: Sebastian Pavel
-- Date: October 2025
-- ===================================================

-- Create database
CREATE DATABASE BakeryDemo;
GO

USE BakeryDemo;
GO

-- =======================
-- Table: Produkte
-- =======================
CREATE TABLE Produkte (
    ProduktID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Preis DECIMAL(10,2) NOT NULL
);
GO

-- Insert sample products
INSERT INTO Produkte (Name, Preis) VALUES
('Croissant', 1.50),
('Baguette', 2.00),
('Pretzel', 1.20),
('Apple Pie Slice', 2.50),
('Coffee', 2.00);
GO

-- =======================
-- Table: Bestellungen
-- =======================
CREATE TABLE Bestellungen (
    BestellungID INT IDENTITY(1,1) PRIMARY KEY,
    Datum DATETIME DEFAULT GETDATE(),
    Gesamtpreis DECIMAL(10,2) NOT NULL
);
GO

-- =======================
-- Table: Bestellpositionen
-- =======================
CREATE TABLE Bestellpositionen (
    PositionID INT IDENTITY(1,1) PRIMARY KEY,
    BestellungID INT NOT NULL,
    ProduktID INT NOT NULL,
    Menge INT NOT NULL,
    Preis DECIMAL(10,2) NOT NULL,

    FOREIGN KEY (BestellungID) REFERENCES Bestellungen(BestellungID),
    FOREIGN KEY (ProduktID) REFERENCES Produkte(ProduktID)
);
GO
