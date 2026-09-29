// Họ và tên: Nguyễn Văn Hùng
// Mã sinh viên: [MSSV Văn Hùng]
// Nội dung thực hiện: Module 3 - Hàm hỗ trợ đọc thông tin đăng nhập từ Session

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Helpers
{
    public static class PhienDangNhap
    {
        // CHỈ CẦN SỬA 3 HẰNG SỐ NÀY cho khớp cách Module 1 lưu Session khi đăng nhập
        public const string KhoaMaTaiKhoan = "MaTaiKhoan";
        public const string KhoaHoTen = "HoTen";
        public const string KhoaVaiTro = "VaiTro";

        public const string VaiTroAdmin = "Admin";
        public const string VaiTroDocGia = "DocGia";

        public static int? LayMaTaiKhoan(this HttpContext context)
            => context.Session.GetInt32(KhoaMaTaiKhoan);

        public static bool LaAdmin(this HttpContext context)
            => context.Session.GetString(KhoaVaiTro) == VaiTroAdmin;

        public static bool LaDocGia(this HttpContext context)
            => context.LayMaTaiKhoan() != null
               && context.Session.GetString(KhoaVaiTro) == VaiTroDocGia;
    }
}
