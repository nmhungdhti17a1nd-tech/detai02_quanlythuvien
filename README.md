# QuanLyThuVien_UNETI04_DHTI17A1ND

Bài tập lớn môn **Thực hành lập trình .NET** — Đề tài 02: Xây dựng hệ thống quản lý thư viện và mượn trả sách.

## 1. Giới thiệu

Ứng dụng web quản lý thư viện, hỗ trợ 2 nhóm người dùng:

- **Admin**: quản lý thể loại, sách, độc giả, phiếu mượn, xác nhận mượn/trả, xem thống kê và Dashboard.
- **Độc giả**: đăng nhập, tra cứu sách, đăng ký mượn sách, xem sách đang mượn và lịch sử mượn trả.

## 2. Công nghệ sử dụng

| Thành phần | Công nghệ |
|-----------|-----------|
| Nền tảng | .NET 10 SDK |
| Framework Web | ASP.NET Core 10 MVC |
| Ngôn ngữ | C# |
| ORM | Entity Framework Core 10 (Code First) |
| Cơ sở dữ liệu | SQL Server |
| Truy vấn dữ liệu | LINQ |
| Giao diện | Razor View, Tag Helper/HTML Helper, HTML/CSS, JavaScript |
| Quản lý mã nguồn | Git/GitHub |

## 3. Thành viên nhóm & phân công Module

| STT | Họ và tên | Mã sinh viên | Module phụ trách |
|-----|-----------|--------------|------------------|
| 1 | Chu Công Đạt | | Module 1 — Tài khoản, Đăng nhập, Phân quyền, Quản lý thể loại |
| 2 | Vũ Duy Anh | | Module 2 — Quản lý và tra cứu sách |
| 3 | Nguyễn Văn Hùng | | Module 3 — Quản lý độc giả và đăng ký mượn sách |
| 4 | Nguyễn Mạnh Hùng | 23203100075 | Module 4 — Quản lý mượn trả, Dashboard, Thống kê |

## 4. Cấu trúc thư mục

    QuanLyThuVien_UNETI04_DHTI17A1ND/
    ├── Controllers/        # Xử lý request, điều phối nghiệp vụ
    ├── Models/             # Entity, ViewModel
    ├── Views/              # Giao diện Razor
    ├── wwwroot/            # CSS, JS, thư viện tĩnh
    ├── Program.cs          # Cấu hình ứng dụng
    ├── appsettings.json    # Cấu hình chung (connection string...)
    └── .gitignore

## 5. Hướng dẫn cài đặt và chạy chương trình

### Yêu cầu môi trường

- .NET 10 SDK
- SQL Server (LocalDB hoặc SQL Server Express)
- Visual Studio 2026 hoặc Visual Studio Code

### Các bước thực hiện

1. **Clone repository**

    ```bash
    git clone https://github.com/nmhungdhti17a1nd-tech/detai02_quanlythuvien.git
    cd detai02_quanlythuvien
