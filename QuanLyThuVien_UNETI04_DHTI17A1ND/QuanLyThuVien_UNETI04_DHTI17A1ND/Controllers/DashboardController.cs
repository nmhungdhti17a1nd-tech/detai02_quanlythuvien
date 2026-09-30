// Họ và tên: Nguyễn Mạnh Hùng
// Mã sinh viên: 23203100075
// Nội dung thực hiện: Module 4 - Dashboard

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Data;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("VaiTro") != "Admin")
                return RedirectToAction("Login", "Account");

            var today = DateTime.Now.Date;

            var vm = new DashboardViewModel
            {
                TongDauSach = await _context.Sachs.CountAsync(),
                TongTheLoai = await _context.TheLoais.CountAsync(),
                TongDocGia = await _context.DocGias.CountAsync(),
                TongPhieuMuon = await _context.PhieuMuons.CountAsync(),
                PhieuChoXacNhan = await _context.PhieuMuons.CountAsync(p => p.TrangThai == 0),
                PhieuDangMuon = await _context.PhieuMuons.CountAsync(p => p.TrangThai == 1),
                PhieuQuaHan = await _context.PhieuMuons.CountAsync(p => p.TrangThai == 1 && p.HanTra < today),
                TongSachCoTheMuon = await _context.Sachs.SumAsync(s => (int?)s.SoLuongCon) ?? 0
            };

            return View(vm);
        }
    }
}