// Họ và tên: Nguyễn Văn Hùng
// Mã sinh viên: [MSSV Văn Hùng]
// Nội dung thực hiện: Module 3 - ViewModel đăng ký mượn sách và xem thông tin mượn

using System.ComponentModel.DataAnnotations;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels
{
    public class DangKyMuonViewModel
    {
        public int MaSach { get; set; }

        // Các trường chỉ để hiển thị, luôn nạp lại từ CSDL (để nullable để không bị báo bắt buộc)
        public string? TenSach { get; set; }
        public string? TacGia { get; set; }
        public string? TenTheLoai { get; set; }
        public int SoLuongCon { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng mượn")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng mượn phải lớn hơn 0")]
        [Display(Name = "Số lượng mượn")]
        public int SoLuongMuon { get; set; } = 1;

        [Required(ErrorMessage = "Vui lòng nhập số ngày mượn")]
        [Range(1, 30, ErrorMessage = "Thời gian mượn từ 1 đến 30 ngày")]
        [Display(Name = "Số ngày mượn")]
        public int SoNgayMuon { get; set; } = 14;
    }

    // Trang "Đang mượn": yêu cầu chờ xác nhận + sách đang mượn
    public class DangMuonViewModel
    {
        public List<PhieuMuon> ChoXacNhan { get; set; } = new();
        public List<PhieuMuon> DangMuon { get; set; } = new();
    }
}
