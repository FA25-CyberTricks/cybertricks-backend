# 🎮 CyberTricks  
> Hệ thống **booking quán game trực tuyến** – đặt chỗ, quản lý & trải nghiệm net-cafe thông minh  

![CyberTricks Logo](https://via.placeholder.com/600x200?text=CyberTricks+Logo)  

[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)  
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)  
[![MySQL](https://img.shields.io/badge/Database-MySQL-blue?logo=mysql)](https://www.mysql.com/)  
[![Bootstrap](https://img.shields.io/badge/Frontend-Bootstrap_5-7952B3?logo=bootstrap)](https://getbootstrap.com/)  

---

## 📖 Giới thiệu
**CyberTricks** là một nền tảng **đặt chỗ quán game trực tuyến** (net-cafe booking system) hỗ trợ:
- Người chơi đặt chỗ trước, chọn máy/ghế theo sơ đồ.  
- Chủ quán quản lý cơ sở, nhân viên, và thanh toán.  
- Hướng đến **trải nghiệm hiện đại, minh bạch & tiện lợi** cho cả khách hàng và chủ quán.  

---

## ✨ Tính năng nổi bật
- 🖥 **Booking máy/ghế trực tuyến** theo thời gian thực.  
- 💳 **Thanh toán online & QR code**.  
- 🍔 **Đặt đồ ăn/uống trước**.  
- 🔐 **Xác thực & phân quyền nhiều vai trò** (Admin, Owner, Staff, Client, Guest).  
- 📊 **Báo cáo doanh thu & quản lý vận hành**.  

---

## 🏗 Kiến trúc hệ thống (Web)

![System Diagram](https://via.placeholder.com/800x400?text=System+Architecture+Diagram)  

---

## 🛠 Tech Stack
- **Backend**: ASP.NET Core 8, EF Core, Identity, AutoMapper  
- **Frontend**: Bootstrap 5, Razor Views  
- **Database**: MySQL, EF Core Code First  
- **Storage**: MinIO (S3-compatible)  
- **Messaging**: RabbitMQ  
- **CI/CD**: GitHub Actions, Docker  

---

## 🚀 Hướng dẫn cài đặt

### Backend & Web
```bash
# Clone repo
git clone https://github.com/your-org/cybertricks.git
cd cybertricks

# Cấu hình appsettings.json (DB, JWT, MinIO, RabbitMQ)
# Chạy migration
dotnet ef database update

# Run Web + API
dotnet run

