use Ceres
CREATE TABLE Malls(
Num INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
Name NVARCHAR(200) NOT NULL)

CREATE TABLE Shops(
Num INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
Name NVARCHAR(200) NOT NULL,
Mall INT NOT NULL FOREIGN KEY REFERENCES Malls(Num),
ShopGrade INT NOT NULL)

CREATE TABLE Categories(
Num NVARCHAR(5) PRIMARY KEY NOT NULL,
Name NVARCHAR(200) NOT NULL,
Root NVARCHAR(5) NOT NULL FOREIGN KEY REFERENCES Categories(Num))

CREATE TABLE Products(
Num INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
Name NVARCHAR(400) NOT NULL,
Category NVARCHAR(5) NOT NULL FOREIGN KEY REFERENCES Categories(Num))

CREATE TABLE Links(
Num INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
Name NVARCHAR(400) NOT NULL,
Product INT NOT NULL FOREIGN KEY REFERENCES Products(Num),
Quality INT NOT NULL,
Unit INT NOT NULL,
Amount REAL NOT NULL,
Shop INT NOT NULL FOREIGN KEY REFERENCES Shops(Num),
Brand INT NOT NULL,
Series NVARCHAR(200) NOT NULL,
LinkDate DATETIME NOT NULL,
Freight REAL NOT NULL,
Address NVARCHAR(MAX) NOT NULL,
FullName NVARCHAR(MAX) NOT NULL,
OriginPrice REAL NOT NULL,
Discount REAL,
SDWE NVARCHAR(MAX),
MDWE  NVARCHAR(MAX),
SDWD  NVARCHAR(MAX),
MDWD  NVARCHAR(MAX),
SEDWE  NVARCHAR(MAX),
MEDWE  NVARCHAR(MAX),
TaoGold REAL,
MallVIP REAL,
Deposit REAL,
DepositDiscount REAL,
Recall REAL)

CREATE TABLE Plans(
Num INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
Name NVARCHAR(400) NOT NULL,
Links NVARCHAR(MAX) NOT NULL,
Deals NVARCHAR(MAX) NOT NULL,
PlanDate DATETIME NOT NULL,
MainLink INT)