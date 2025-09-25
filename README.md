# 🎮 CyberTricks – Backend (API)  
> Online **gaming cafe booking system** – backend API service for seat booking, management & operations  

[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)  
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)  
[![MySQL](https://img.shields.io/badge/Database-MySQL-blue?logo=mysql)](https://www.mysql.com/)  
[![RabbitMQ](https://img.shields.io/badge/Messaging-RabbitMQ-FF6600?logo=rabbitmq)](https://www.rabbitmq.com/)  
[![MinIO](https://img.shields.io/badge/Storage-MinIO-EE1C25?logo=minio)](https://min.io/)  

---

## 📖 Introduction
**CyberTricks Backend** provides the API services for the online gaming cafe booking system:  
- Real-time seat & machine booking management.  
- Authentication & role-based authorization (Admin, Owner, Staff, Client, Guest).  
- Online payments & QR code processing.  
- Store, machine, staff, and product management.  
- Loyalty system, tournaments, and dashboard analytics integration.  

---

## ✨ Core Modules
- **Authentication & Authorization**: ASP.NET Identity + JWT.  
- **Booking Management**: APIs for booking & cancelling seats, tracking machine status.  
- **Store & Staff Management**: Manage stores, rooms, machines, and staff members.  
- **Payment Integration**: Online payments, QR code checkout.  
- **Product & Order**: Food & drink ordering along with bookings.  
- **Reporting & Analytics**: APIs for dashboards and insights.  

---

## 🏗 Backend Architecture
```plaintext
📦 ct.backend
├─ Domain/           # Entities, Enums
├─ Infrastructure/   # EF Core, Identity, ExternalServices
├─ Features/         # Controllers, DTOs (Vertical Slice Architecture)
└─ Common/           # Helpers, Validators
```

## 🛠 Tech Stack
- **Framework**: ASP.NET Core 8  
- **Database**: MySQL (EF Core Code First)  
- **Identity**: ASP.NET Identity + JWT  
- **CI/CD**: GitHub Actions, Docker  

---

## 🚀 Getting Started

### 1️⃣ Clone & Setup
```bash
git clone https://github.com/your-org/cybertricks-backend.git
cd cybertricks-backend

# Cấu hình appsettings.json (DB, JWT, MinIO, RabbitMQ)
# Chạy migration
dotnet ef database update

# Run API
dotnet run
```
