QuanLyThuVien_UNETI04_DHTI17A1ND

Bài tập lớn môn Thực hành lập trình .NET — Đề tài 02: Xây dựng hệ thống quản lý thư viện và mượn trả sách.

Giới thiệu

Ứng dụng web quản lý thư viện, hỗ trợ 2 nhóm người dùng:

Admin: quản lý thể loại, sách, độc giả, phiếu mượn, xác nhận mượn/trả, xem thống kê và Dashboard.

Độc giả: đăng nhập, tra cứu sách, đăng ký mượn sách, xem sách đang mượn và lịch sử mượn trả.

Công nghệ sử dụng

Nền tảng: .NET 10 SDK

Framework Web: ASP.NET Core 10 MVC

Ngôn ngữ: C#

ORM: Entity Framework Core 10 (Code First)

Cơ sở dữ liệu: SQL Server

Truy vấn dữ liệu: LINQ

Giao diện: Razor View, Tag Helper/HTML Helper, HTML/CSS, JavaScript

Quản lý mã nguồn: Git/GitHub

Thành viên nhóm & phân công Module

Chu Công Đạt - MSSV: 23203100049 - Module 1: Tài khoản, Đăng nhập, Phân quyền, Quản lý thể loại

Vũ Duy Anh - MSSV: 23203100073 - Module 2: Quản lý và tra cứu sách

Nguyễn Văn Hùng - MSSV: 23203100079 - Module 3: Quản lý độc giả và đăng ký mượn sách

Nguyễn Mạnh Hùng - MSSV: 23203100075 - Module 4: Quản lý mượn trả, Dashboard, Thống kê

Cấu trúc thư mục

QuanLyThuVien_UNETI04_DHTI17A1ND/

Controllers/ : Xử lý request, điều phối nghiệp vụ

Models/ : Entity, ViewModel

Views/ : Giao diện Razor

wwwroot/ : CSS, JS, thư viện tĩnh, hình ảnh

Program.cs : Cấu hình ứng dụng

appsettings.json : Cấu hình chung (connection string...)

.gitignore

Hướng dẫn cài đặt và chạy chương trình

Yêu cầu môi trường:

.NET 10 SDK

SQL Server (LocalDB hoặc SQL Server Express)

Visual Studio 2026 hoặc Visual Studio Code

Các bước thực hiện:

Clone repository
git clone https://github.com/nmhungdhti17a1nd-tech/detai02_quanlythuvien.git
cd detai02_quanlythuvien/QuanLyThuVien_UNETI04_DHTI17A1ND/QuanLyThuVien_UNETI04_DHTI17A1ND

Cấu hình cơ sở dữ liệu
Mở file appsettings.json, kiểm tra chuỗi kết nối DefaultConnection:

Nếu dùng SQL Server Express: Server=.\SQLEXPRESS;...

Nếu dùng LocalDB: Server=(localdb)\MSSQLLocalDB;...

Cập nhật Database & Chạy ứng dụng
dotnet ef database update
dotnet run

Truy cập ứng dụng
Mở trình duyệt và truy cập: https://localhost:7105

Tài khoản mẫu để kiểm tra

Admin: Tên đăng nhập: admin | Mật khẩu: 123456

Độc giả: Tên đăng nhập: docgia01 | Mật khẩu: 123456

