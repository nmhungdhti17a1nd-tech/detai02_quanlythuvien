// Họ và tên: [Vũ Duy Anh]
// Mã sinh viên: [MSSV Duy Anh]
// Nội dung thực hiện: Module 2 - Quản lý và tra cứu sách

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities
{
    [Table("Sach")]
    public class Sach
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaSach { get; set; }

        [Required(ErrorMessage = "Tên sách không được để trống")]
        [StringLength(200)]
        [Display(Name = "Tên sách")]
        public string TenSach { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        [Display(Name = "Thể loại")]
        public int MaTheLoai { get; set; }

        [Required(ErrorMessage = "Tác giả không được để trống")]
        [StringLength(150)]
        [Display(Name = "Tác giả")]
        public string TacGia { get; set; } = string.Empty;

        [StringLength(150)]
        [Display(Name = "Nhà xuất bản")]
        public string? NhaXuatBan { get; set; }

        [Display(Name = "Năm xuất bản")]
        [Range(1900, 2100, ErrorMessage = "Năm xuất bản không hợp lệ")]
        public int? NamXuatBan { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải >= 0")]
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng còn phải >= 0")]
        [Display(Name = "Số lượng còn")]
        public int SoLuongCon { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá sách phải > 0")]
        [Display(Name = "Giá sách")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaSach { get; set; }

        [Display(Name = "Mô tả")]
        [Column(TypeName = "nvarchar(1000)")]
        public string? MoTa { get; set; }

        // 0: Đang phục vụ, 1: Tạm ngừng, 2: Ngừng phục vụ
        [Display(Name = "Trạng thái")]
        public int TrangThai { get; set; } = 0;

        [ForeignKey("MaTheLoai")]
        public TheLoai? TheLoai { get; set; }

        public ICollection<ChiTietPhieuMuon>? ChiTietPhieuMuons { get; set; }
    }
}