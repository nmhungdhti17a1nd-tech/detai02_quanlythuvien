// Họ và tên: Nguyễn Văn Hùng
// Mã sinh viên: [MSSV Văn Hùng]
// Nội dung thực hiện: Module 3 - ViewModel danh sách độc giả (tìm kiếm, lọc, phân trang)

using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels
{
    public class DocGiaViewModel
    {
        public List<DocGia> DanhSach { get; set; } = new();
        public string? Keyword { get; set; }
        public bool? TrangThai { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages => Math.Max(1, (int)Math.Ceiling(TotalItems / (double)PageSize));
    }
}
