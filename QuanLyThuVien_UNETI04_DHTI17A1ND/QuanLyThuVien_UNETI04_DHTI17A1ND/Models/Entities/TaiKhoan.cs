// Họ và tên: [Chu Công Đạt]
// Mã sinh viên: [MSSV Đạt]
// Nội dung thực hiện: Module 1 - Tài khoản, Đăng nhập, Phân quyền

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTaiKhoan { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(50)]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(255)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Display(Name = "Vai trò")]
        public string VaiTro { get; set; } = "DocGia";

        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; } = true;

        public DocGia? DocGia { get; set; }
    }
}