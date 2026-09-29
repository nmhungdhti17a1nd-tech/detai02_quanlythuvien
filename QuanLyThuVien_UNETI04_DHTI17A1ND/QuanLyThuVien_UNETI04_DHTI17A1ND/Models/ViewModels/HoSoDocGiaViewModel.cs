// Họ và tên: Nguyễn Văn Hùng
// Mã sinh viên: [MSSV Văn Hùng]
// Nội dung thực hiện: Module 3 - ViewModel độc giả cập nhật thông tin cá nhân

using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels
{
    // Chỉ chứa các trường độc giả ĐƯỢC PHÉP sửa (không có MaTaiKhoan, TrangThai...)
    public class HoSoDocGiaViewModel
    {
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên tối đa 100 ký tự")]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        [Display(Name = "Giới tính")]
        public string? GioiTinh { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15, ErrorMessage = "Số điện thoại tối đa 15 ký tự")]
        [Display(Name = "Số điện thoại")]
        public string? SoDienThoai { get; set; }

        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [StringLength(100, ErrorMessage = "Email tối đa 100 ký tự")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [StringLength(255, ErrorMessage = "Địa chỉ tối đa 255 ký tự")]
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }
    }
}
