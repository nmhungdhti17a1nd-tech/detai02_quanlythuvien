// Họ và tên: Nguyễn Mạnh Hùng
// Mã sinh viên: 23203100075
// Nội dung thực hiện: ViewModel cho trang chủ

using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels
{
    public class HomeViewModel
    {
        // 6 sách nổi bật
        public List<Sach> SachNoiBat { get; set; } = new();

        // Thông tin thư viện
        public int TongDauSach { get; set; }
        public int TongDocGia { get; set; }
        public int TongTheLoai { get; set; }
    }
}