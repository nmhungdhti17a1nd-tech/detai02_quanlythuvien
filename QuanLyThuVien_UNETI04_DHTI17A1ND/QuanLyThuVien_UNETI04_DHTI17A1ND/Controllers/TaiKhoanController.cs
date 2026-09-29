using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Data;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly AppDbContext _context;

        public TaiKhoanController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult DangNhap()
        {
            if (HttpContext.Session.GetString("VaiTro") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangNhap(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var taiKhoan = _context.TaiKhoans
                .FirstOrDefault(t => t.TenDangNhap == model.TenDangNhap
                                  && t.MatKhau == model.MatKhau);

            if (taiKhoan == null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu";
                return View(model);
            }

            if (!taiKhoan.TrangThai)
            {
                ViewBag.Error = "Tài khoản đã bị khóa. Vui lòng liên hệ Admin.";
                return View(model);
            }

            var docGia = _context.DocGias
                .FirstOrDefault(d => d.MaTaiKhoan == taiKhoan.MaTaiKhoan);

            HttpContext.Session.SetInt32("MaTaiKhoan", taiKhoan.MaTaiKhoan);
            HttpContext.Session.SetString("HoTen", docGia?.HoTen ?? "Admin");
            HttpContext.Session.SetString("VaiTro", taiKhoan.VaiTro);
            HttpContext.Session.SetInt32("MaDocGia", docGia?.MaDocGia ?? 0);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("DangNhap");
        }

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangKy(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var daTonTai = _context.TaiKhoans
                .Any(t => t.TenDangNhap == model.TenDangNhap);

            if (daTonTai)
            {
                ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại");
                return View(model);
            }

            var taiKhoanMoi = new TaiKhoan
            {
                TenDangNhap = model.TenDangNhap,
                MatKhau = model.MatKhau,
                VaiTro = "DocGia",
                TrangThai = true
            };
            _context.TaiKhoans.Add(taiKhoanMoi);
            _context.SaveChanges();

            var docGiaMoi = new DocGia
            {
                MaTaiKhoan = taiKhoanMoi.MaTaiKhoan,
                HoTen = model.HoTen,
                Email = model.Email,
                SoDienThoai = model.SoDienThoai,
                DiaChi = model.DiaChi,
                NgayDangKy = DateTime.Now,
                TrangThai = true
            };
            _context.DocGias.Add(docGiaMoi);
            _context.SaveChanges();

            TempData["Success"] = "Đăng ký thành công! Vui lòng đăng nhập.";
            return RedirectToAction("DangNhap");
        }
    }
}