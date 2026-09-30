// Họ và tên: Nguyễn Mạnh Hùng
// Mã sinh viên: 23203100075
// Nội dung thực hiện: Controller trang chủ - hiển thị sách nổi bật từ database

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Data;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels;
using System.Diagnostics;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /
        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel
            {
                // 6 sách nổi bật: đang phục vụ (TrangThai = 0) và còn sách
                SachNoiBat = await _context.Sachs
                    .Include(s => s.TheLoai)
                    .Where(s => s.TrangThai == 0 && s.SoLuongCon > 0)
                    .OrderByDescending(s => s.MaSach)
                    .Take(6)
                    .ToListAsync(),

                // Thông tin thư viện
                TongDauSach = await _context.Sachs.CountAsync(),
                TongDocGia = await _context.DocGias.CountAsync(),
                TongTheLoai = await _context.TheLoais.CountAsync()
            };

            return View(vm);
        }

        // GET: /Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: /Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}