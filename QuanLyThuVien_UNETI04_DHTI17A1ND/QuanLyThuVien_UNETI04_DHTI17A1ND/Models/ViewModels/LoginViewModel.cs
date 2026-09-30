// TẠM - File test M4, xóa sau khi Đạt fix Module 1

using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; } = string.Empty;
    }
}