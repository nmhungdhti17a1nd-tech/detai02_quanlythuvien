// Họ và tên: Nguyễn Văn Hùng
// Mã sinh viên: [MSSV Văn Hùng]
// Nội dung thực hiện: Module 3 - Quản lý độc giả (Admin) và cập nhật thông tin cá nhân (Độc giả)

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Data;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Helpers;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Controllers
{
    public class DocGiaController : Controller
    {
        private const int PageSize = 10;
        private readonly AppDbContext _context;

        public DocGiaController(AppDbContext context)
        {
            _context = context;
        }

        private IActionResult ChuyenDenDangNhap()
        {
            TempData["Loi"] = "Bạn cần đăng nhập với quyền phù hợp để sử dụng chức năng này.";
            return RedirectToAction("Login", "Account");
        }

        // ===================== PHẦN ADMIN =====================

        // GET: /DocGia?keyword=&trangThai=&page=
        public async Task<IActionResult> Index(string? keyword, bool? trangThai, int page = 1)
        {
            if (!HttpContext.LaAdmin()) return ChuyenDenDangNhap();

            var query = _context.DocGias.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                query = query.Where(d => d.HoTen.Contains(keyword)
                    || (d.Email != null && d.Email.Contains(keyword))
                    || (d.SoDienThoai != null && d.SoDienThoai.Contains(keyword)));
            }

            if (trangThai.HasValue)
                query = query.Where(d => d.TrangThai == trangThai.Value);

            int total = await query.CountAsync();
            int totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
            page = Math.Clamp(page, 1, totalPages);

            var danhSach = await query
                .OrderBy(d => d.HoTen).ThenBy(d => d.MaDocGia)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return View(new DocGiaViewModel
            {
                DanhSach = danhSach,
                Keyword = keyword,
                TrangThai = trangThai,
                Page = page,
                PageSize = PageSize,
                TotalItems = total
            });
        }

        // GET: /DocGia/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (!HttpContext.LaAdmin()) return ChuyenDenDangNhap();

            var docGia = await _context.DocGias.AsNoTracking()
                .Include(d => d.TaiKhoan)
                .Include(d => d.PhieuMuons!.OrderByDescending(p => p.NgayMuon).Take(5))
                .FirstOrDefaultAsync(d => d.MaDocGia == id);

            if (docGia == null) return NotFound();

            ViewBag.TongSoPhieu = await _context.PhieuMuons.CountAsync(p => p.MaDocGia == id);
            return View(docGia);
        }

        // POST: /DocGia/DoiTrangThai  (khóa / mở khóa độc giả)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoiTrangThai(int id, bool trangThai,
            string? keyword, bool? locTrangThai, int page = 1)
        {
            if (!HttpContext.LaAdmin()) return ChuyenDenDangNhap();

            var docGia = await _context.DocGias.FindAsync(id);
            if (docGia == null) return NotFound();

            docGia.TrangThai = trangThai;
            await _context.SaveChangesAsync();

            TempData["ThanhCong"] = trangThai
                ? $"Đã mở khóa độc giả \"{docGia.HoTen}\"."
                : $"Đã khóa độc giả \"{docGia.HoTen}\".";

            // Giữ nguyên điều kiện tìm kiếm/lọc/trang khi quay lại danh sách
            return RedirectToAction(nameof(Index), new { keyword, trangThai = locTrangThai, page });
        }

        // ===================== PHẦN ĐỘC GIẢ =====================

        private async Task<DocGia?> LayDocGiaHienTai()
        {
            var maTaiKhoan = HttpContext.LayMaTaiKhoan();
            if (maTaiKhoan == null) return null;
            return await _context.DocGias.FirstOrDefaultAsync(d => d.MaTaiKhoan == maTaiKhoan.Value);
        }

        // GET: /DocGia/HoSo
        public async Task<IActionResult> HoSo()
        {
            if (!HttpContext.LaDocGia()) return ChuyenDenDangNhap();

            var docGia = await LayDocGiaHienTai();
            if (docGia == null) return NotFound();

            ViewBag.NgayDangKy = docGia.NgayDangKy;
            return View(new HoSoDocGiaViewModel
            {
                HoTen = docGia.HoTen,
                NgaySinh = docGia.NgaySinh,
                GioiTinh = docGia.GioiTinh,
                SoDienThoai = docGia.SoDienThoai,
                Email = docGia.Email,
                DiaChi = docGia.DiaChi
            });
        }

        // POST: /DocGia/HoSo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HoSo(HoSoDocGiaViewModel model)
        {
            if (!HttpContext.LaDocGia()) return ChuyenDenDangNhap();

            var docGia = await LayDocGiaHienTai();
            if (docGia == null) return NotFound();

            // Kiểm tra ngày tháng hợp lệ (Data Annotation không so sánh được với ngày hiện tại)
            if (model.NgaySinh.HasValue
                && (model.NgaySinh.Value.Date > DateTime.Today || model.NgaySinh.Value.Year < 1900))
            {
                ModelState.AddModelError(nameof(model.NgaySinh), "Ngày sinh không hợp lệ.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.NgayDangKy = docGia.NgayDangKy;
                return View(model);
            }

            docGia.HoTen = model.HoTen.Trim();
            docGia.NgaySinh = model.NgaySinh;
            docGia.GioiTinh = model.GioiTinh;
            docGia.SoDienThoai = model.SoDienThoai?.Trim();
            docGia.Email = model.Email?.Trim();
            docGia.DiaChi = model.DiaChi?.Trim();
            await _context.SaveChangesAsync();

            // Cập nhật lại tên hiển thị trong Session (nếu menu đang dùng)
            HttpContext.Session.SetString(PhienDangNhap.KhoaHoTen, docGia.HoTen);

            TempData["ThanhCong"] = "Cập nhật thông tin cá nhân thành công.";
            return RedirectToAction(nameof(HoSo));
        }
    }
}
