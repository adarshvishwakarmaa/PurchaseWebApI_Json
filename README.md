# Purchase Invoice API (.NET Web API)

A simple **Purchase Invoice REST API** built using **ASP.NET Web API (.NET Framework)** and **SQL Server**.

This project demonstrates a **clean architecture using Models instead of JObject/JArray**, and shows how to save **nested data structures (Invoice → Product → Tax)** into a relational database.

The API accepts **JSON with multiple arrays**, processes the data using strongly typed models (`List<T>`), and inserts it into SQL tables using transactions.

---

# 🚀 Tech Stack

* ASP.NET Web API (.NET Framework 4.8)
* C#
* SQL Server
* ADO.NET
* JSON REST API

---

# 📂 Project Structure

```
PurchaseInvoiceAPI
│
├── Controllers
│      PurchaseInvoiceController.cs
│
├── Models
│      SaveVM.cs
│      Invoice.cs
│      Product.cs
│      Tax.cs
│
├── App_Start
│      WebApiConfig.cs
│
└── Web.config
```

---

# 📊 Database Structure

The database contains **three related tables**.

```
Invoice
   │
   ├── Product
   │       │
   │       └── ProductTax
```

Relationship:

```
Invoice → One invoice can have multiple products
Product → One product can have multiple taxes
```

---

# 🗄️ Database Setup

### 1️⃣ Create Database

```sql
CREATE DATABASE PurchaseDB
GO

USE PurchaseDB
GO
```

---

### 2️⃣ Create Tables

#### Invoice Table

```sql
CREATE TABLE Invoice
(
Id INT IDENTITY(1,1) PRIMARY KEY,
InvoiceNo VARCHAR(50),
InvoiceDate DATE,
SupplierCode VARCHAR(50)
)
```

---

#### Product Table

```sql
CREATE TABLE Product
(
Id INT IDENTITY(1,1) PRIMARY KEY,
InvoiceId INT,
ProductCode VARCHAR(50),
Qty INT,
Rate DECIMAL(18,2),

FOREIGN KEY (InvoiceId) REFERENCES Invoice(Id)
)
```

---

#### ProductTax Table

```sql
CREATE TABLE ProductTax
(
Id INT IDENTITY(1,1) PRIMARY KEY,
ProductId INT,
TaxType VARCHAR(20),
TaxPercent DECIMAL(18,2),

FOREIGN KEY (ProductId) REFERENCES Product(Id)
)
```

---

# 📥 Sample Data (10 Records)

### Insert Invoices

```sql
INSERT INTO Invoice VALUES ('INV001','2026-03-01','SUP01')
INSERT INTO Invoice VALUES ('INV002','2026-03-02','SUP02')
INSERT INTO Invoice VALUES ('INV003','2026-03-03','SUP03')
INSERT INTO Invoice VALUES ('INV004','2026-03-04','SUP04')
INSERT INTO Invoice VALUES ('INV005','2026-03-05','SUP05')
INSERT INTO Invoice VALUES ('INV006','2026-03-06','SUP06')
INSERT INTO Invoice VALUES ('INV007','2026-03-07','SUP07')
INSERT INTO Invoice VALUES ('INV008','2026-03-08','SUP08')
INSERT INTO Invoice VALUES ('INV009','2026-03-09','SUP09')
INSERT INTO Invoice VALUES ('INV010','2026-03-10','SUP10')
```

---

### Insert Products

```sql
INSERT INTO Product VALUES (1,'PRD01',5,100)
INSERT INTO Product VALUES (2,'PRD02',10,200)
INSERT INTO Product VALUES (3,'PRD03',7,150)
INSERT INTO Product VALUES (4,'PRD04',2,500)
INSERT INTO Product VALUES (5,'PRD05',3,300)
INSERT INTO Product VALUES (6,'PRD06',9,120)
INSERT INTO Product VALUES (7,'PRD07',4,450)
INSERT INTO Product VALUES (8,'PRD08',6,210)
INSERT INTO Product VALUES (9,'PRD09',8,190)
INSERT INTO Product VALUES (10,'PRD10',1,900)
```

---

### Insert Taxes

```sql
INSERT INTO ProductTax VALUES (1,'CGST',9)
INSERT INTO ProductTax VALUES (1,'SGST',9)
INSERT INTO ProductTax VALUES (2,'CGST',9)
INSERT INTO ProductTax VALUES (2,'SGST',9)
INSERT INTO ProductTax VALUES (3,'CGST',9)
INSERT INTO ProductTax VALUES (3,'SGST',9)
INSERT INTO ProductTax VALUES (4,'CGST',9)
INSERT INTO ProductTax VALUES (4,'SGST',9)
INSERT INTO ProductTax VALUES (5,'CGST',9)
INSERT INTO ProductTax VALUES (5,'SGST',9)
```

---

# 📡 API Endpoint

```
POST /api/PurchaseInvoice
```

---

# 📄 Sample JSON Request

```json
{
  "UserID": "admin",

  "CompInv_data": [
    {
      "Invoice_No": "INV1001",
      "Invoice_Date": "2026-03-12",
      "Supplier_Code": "SUP01",

      "Products": [
        {
          "Product_Code": "PRD01",
          "Qty": 5,
          "Rate": 100,

          "Taxes": [
            { "Tax_Type": "CGST", "Tax_Percent": 9 },
            { "Tax_Type": "SGST", "Tax_Percent": 9 }
          ]
        }
      ]
    }
  ],

  "SalesInvoice_data": [],
  "SalesReturn_data": []
}
```

---

# 🔄 API Workflow

```
Client JSON Request
        ↓
SaveVM Model
        ↓
Invoice Model
        ↓
Product Model
        ↓
Tax Model
        ↓
Controller Loops
        ↓
SQL Server Tables
```

The controller processes the nested data and inserts records into the database using **SQL transactions**.

---

# 🧪 Testing the API

You can test the API using:

* Postman
* Swagger
* Curl

Example:

```
POST http://localhost:5000/api/PurchaseInvoice
```

---

# 📌 Key Learning Points

* Strongly typed models (`List<T>`) instead of dynamic JSON
* Nested JSON processing
* SQL Server relational design
* ADO.NET database operations
* Transaction handling in Web API

---

# 👨‍💻 Author

**Adarsh Vishwakarma**

GitHub: https://github.com/adarshvishwakarmaa
