# 🎮 CyberTricks – Backend (API)  
> Hệ thống **booking quán game trực tuyến** – backend dịch vụ API cho đặt chỗ, quản lý & vận hành net-cafe thông minh  

[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)  
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)  
[![MySQL](https://img.shields.io/badge/Database-MySQL-blue?logo=mysql)](https://www.mysql.com/)  
[![RabbitMQ](https://img.shields.io/badge/Messaging-RabbitMQ-FF6600?logo=rabbitmq)](https://www.rabbitmq.com/)  
[![MinIO](https://img.shields.io/badge/Storage-MinIO-EE1C25?logo=minio)](https://min.io/)  

---

## 📖 Giới thiệu
**CyberTricks Backend** cung cấp các API cho hệ thống đặt chỗ quán game:  
- Quản lý booking máy/ghế theo thời gian thực.  
- Xác thực & phân quyền (Admin, Owner, Staff, Client, Guest).  
- Xử lý thanh toán & QR code.  
- Quản lý cơ sở, phòng máy, nhân viên, sản phẩm.  
- Hỗ trợ tích hợp loyalty, giải đấu, và dashboard phân tích.  

---

## ✨ Modules chính
- **Authentication & Authorization**: ASP.NET Identity + JWT.  
- **Booking Management**: API đặt & hủy chỗ, theo dõi trạng thái ghế/máy.  
- **Store & Staff Management**: Quản lý cơ sở, phòng, máy, nhân viên.  
- **Payment Integration**: Thanh toán online, QR code.  
- **Product & Order**: Đặt món ăn/uống kèm theo booking.  
- **Reporting & Analytics**: API cho dashboard.  

---

## 🏗 Kiến trúc Backend
```plaintext
📦 ct.backend
├─ Domain/           # Entities, Enums
├─ Infrastructure/   # EF Core, Identity, ExternalServices
├─ Features/         # Controllers, Dtos (Vertical Slice Architecture)
└─ Common/           # Helpers, Validators
```

## 🛠 Tech Stack
- **Framework**: ASP.NET Core 8  
- **Database**: MySQL (EF Core Code First)  
- **Identity**: ASP.NET Identity + JWT  
- **CI/CD**: GitHub Actions, Docker  

---

## 🚀 Hướng dẫn cài đặt

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
