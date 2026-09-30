// Họ và tên: Nguyễn Mạnh Hùng
// Mã sinh viên: 23203100075
// Nội dung thực hiện: Module 4 - ViewModel cho chức năng trả sách

using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels
{
    public class TraSachViewModel
    {
        public int MaPhieuMuon { get; set; }
        public string TenDocGia { get; set; } = string.Empty;
        public DateTime NgayMuon { get; set; }
        public DateTime HanTra { get; set; }

        public List<ChiTietTraViewModel> DanhSachSach { get; set; } = new();
    }

    public class ChiTietTraViewModel
    {
        public int MaChiTiet { get; set; }
        public string TenSach { get; set; } = string.Empty;
        public string TacGia { get; set; } = string.Empty;
        public int SoLuongMuon { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày trả")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày trả")]
        public DateTime NgayTra { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Vui lòng chọn tình trạng")]
        [Display(Name = "Tình trạng trả")]
        public string TinhTrangTra { get; set; } = "Tốt";
    }
}