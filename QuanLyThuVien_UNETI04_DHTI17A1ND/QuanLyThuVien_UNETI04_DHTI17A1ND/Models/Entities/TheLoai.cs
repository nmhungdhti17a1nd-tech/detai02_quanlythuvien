// Họ và tên: [Chu Công Đạt]
// Mã sinh viên: [MSSV Đạt]
// Nội dung thực hiện: Module 1 - Quản lý thể loại sách

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities
{
    [Table("TheLoai")]
    public class TheLoai
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTheLoai { get; set; }

        [Required(ErrorMessage = "Tên thể loại không được để trống")]
        [StringLength(100)]
        [Display(Name = "Tên thể loại")]
        public string TenTheLoai { get; set; } = string.Empty;

        [Display(Name = "Mô tả")]
        [Column(TypeName = "nvarchar(500)")]
        public string? MoTa { get; set; }

        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; } = true;

        public ICollection<Sach>? Sachs { get; set; }
    }
}