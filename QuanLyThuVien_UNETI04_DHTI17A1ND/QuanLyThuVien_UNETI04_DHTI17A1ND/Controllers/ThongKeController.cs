// Họ và tên: Nguyễn Mạnh Hùng
// Mã sinh viên: 23203100075
// Nội dung thực hiện: Module 4 - Thống kê

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Data;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly AppDbContext _context;

        public ThongKeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("VaiTro") != "Admin")
                return RedirectToAction("Login", "Account");

            // 1. Số đầu sách theo thể loại
            ViewBag.ThongKeTheLoai = await _context.Sachs
                .Include(s => s.TheLoai)
                .GroupBy(s => s.TheLoai!.TenTheLoai)
                .Select(g => new ThongKeTheLoaiViewModel
                {
                    TenTheLoai = g.Key,
                    SoLuongSach = g.Count()
                })
                .OrderByDescending(x => x.SoLuongSach)
                .ToListAsync();

            // 2. Lượt mượn theo độc giả (top 10)
            ViewBag.ThongKeDocGia = await _context.PhieuMuons
                .Include(p => p.DocGia)
                .GroupBy(p => p.DocGia!.HoTen)
                .Select(g => new ThongKeDocGiaViewModel
                {
                    HoTen = g.Key,
                    SoLuotMuon = g.Count()
                })
                .OrderByDescending(x => x.SoLuotMuon)
                .Take(10)
                .ToListAsync();

            // 3. Sách được mượn nhiều nhất (top 10)
            ViewBag.SachHot = await _context.ChiTietPhieuMuons
                .Include(ct => ct.Sach)
                .GroupBy(ct => ct.Sach!.TenSach)
                .Select(g => new
                {
                    TenSach = g.Key,
                    TongLuotMuon = g.Sum(ct => ct.SoLuongMuon)
                })
                .OrderByDescending(x => x.TongLuotMuon)
                .Take(10)
                .ToListAsync();

            // 4. Phiếu mượn theo tháng (năm hiện tại)
            var namHienTai = DateTime.Now.Year;
            ViewBag.PhieuTheoThang = await _context.PhieuMuons
                .Where(p => p.NgayMuon.Year == namHienTai)
                .GroupBy(p => p.NgayMuon.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    SoPhieu = g.Count()
                })
                .OrderBy(x => x.Thang)
                .ToListAsync();

            // 5. Phiếu theo trạng thái
            ViewBag.PhieuTheoTrangThai = await _context.PhieuMuons
                .GroupBy(p => p.TrangThai)
                .Select(g => new
                {
                    TrangThai = g.Key,
                    SoLuong = g.Count()
                })
                .ToListAsync();

            return View();
        }
    }
}