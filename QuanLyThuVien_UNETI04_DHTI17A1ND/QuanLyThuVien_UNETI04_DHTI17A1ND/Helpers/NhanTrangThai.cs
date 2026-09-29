// Họ và tên: Nguyễn Văn Hùng
// Mã sinh viên: [MSSV Văn Hùng]
// Nội dung thực hiện: Module 3 - Chuyển mã trạng thái phiếu mượn sang chữ hiển thị

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Helpers
{
    public static class NhanTrangThai
    {
        // 0: Chờ xác nhận, 1: Đang mượn, 2: Đã trả, 3: Đã hủy (theo Entity PhieuMuon)
        public static string TenPhieuMuon(int trangThai) => trangThai switch
        {
            0 => "Chờ xác nhận",
            1 => "Đang mượn",
            2 => "Đã trả",
            3 => "Đã hủy",
            _ => "Không xác định"
        };

        public static string MauPhieuMuon(int trangThai) => trangThai switch
        {
            0 => "bg-warning text-dark",
            1 => "bg-primary",
            2 => "bg-success",
            3 => "bg-secondary",
            _ => "bg-dark"
        };
    }
}
