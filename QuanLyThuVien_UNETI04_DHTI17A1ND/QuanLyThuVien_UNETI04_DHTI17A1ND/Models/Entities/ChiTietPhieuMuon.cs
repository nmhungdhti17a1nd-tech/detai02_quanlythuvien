// Họ và tên: [Nguyễn Văn Hùng] + [Nguyễn Mạnh Hùng]
// Mã sinh viên: [MSSV Văn Hùng] + 23203100075
// Nội dung thực hiện: Module 3 - Chi tiết phiếu mượn | Module 4 - Trả sách

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities
{
    [Table("ChiTietPhieuMuon")]
    public class ChiTietPhieuMuon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaChiTiet { get; set; }

        [Required]
        [Display(Name = "Phiếu mượn")]
        public int MaPhieuMuon { get; set; }

        [Required]
        [Display(Name = "Sách")]
        public int MaSach { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng mượn phải > 0")]
        [Display(Name = "Số lượng mượn")]
        public int SoLuongMuon { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày trả")]
        public DateTime? NgayTra { get; set; }

        [StringLength(255)]
        [Display(Name = "Tình trạng trả")]
        public string? TinhTrangTra { get; set; }

        [ForeignKey("MaPhieuMuon")]
        public PhieuMuon? PhieuMuon { get; set; }

        [ForeignKey("MaSach")]
        public Sach? Sach { get; set; }
    }
}