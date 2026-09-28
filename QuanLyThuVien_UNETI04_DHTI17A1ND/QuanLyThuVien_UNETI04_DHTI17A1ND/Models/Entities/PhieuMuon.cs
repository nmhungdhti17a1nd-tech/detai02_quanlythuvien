// Họ và tên: [Nguyễn Văn Hùng] + [Nguyễn Mạnh Hùng]
// Mã sinh viên: [MSSV Văn Hùng] + 23203100075
// Nội dung thực hiện: Module 3 - Phiếu mượn | Module 4 - Quản lý mượn trả

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities
{
    [Table("PhieuMuon")]
    public class PhieuMuon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaPhieuMuon { get; set; }

        [Required]
        [Display(Name = "Độc giả")]
        public int MaDocGia { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày mượn")]
        public DateTime NgayMuon { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "Hạn trả")]
        public DateTime HanTra { get; set; }

        // 0: Chờ xác nhận, 1: Đang mượn, 2: Đã trả, 3: Đã hủy
        [Display(Name = "Trạng thái")]
        public int TrangThai { get; set; } = 0;

        [ForeignKey("MaDocGia")]
        public DocGia? DocGia { get; set; }

        public ICollection<ChiTietPhieuMuon>? ChiTietPhieuMuons { get; set; }
    }
}