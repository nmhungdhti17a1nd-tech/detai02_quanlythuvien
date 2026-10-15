// Họ và tên: Vũ Duy Anh
// Mã sinh viên: 23203100073
// Nội dung thực hiện: ViewModel cho tìm kiếm, lọc, sắp xếp, phân trang sách

using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.ViewModels
{
    public class SachSearchViewModel
    {
        public List<Sach> DanhSachSach { get; set; } = new();

        // Tìm kiếm
        public string? TuKhoa { get; set; }

        // Lọc
        public int? MaTheLoai { get; set; }
        public string? NhaXuatBan { get; set; }
        public int? TrangThai { get; set; }
        public int? NamXuatBanTu { get; set; }
        public int? NamXuatBanDen { get; set; }

        // Sắp xếp
        public string? SapXep { get; set; }

        // Phân trang
        public int TrangHienTai { get; set; } = 1;
        public int KichThuocTrang { get; set; } = 10;
        public int TongSoTrang { get; set; }
        public int TongSoBanGhi { get; set; }

        // Dropdown
        public List<SelectListItem> DanhSachTheLoai { get; set; } = new();
        public List<SelectListItem> DanhSachNhaXuatBan { get; set; } = new();
    }
}