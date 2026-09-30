// TẠM - File test M4, xóa sau khi Đạt fix Module 1

using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Data;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var taiKhoan = _context.TaiKhoans
                .FirstOrDefault(t => t.TenDangNhap == vm.TenDangNhap
                                     && t.MatKhau == vm.MatKhau);

            if (taiKhoan == null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu.";
                return View(vm);
            }

            if (!taiKhoan.TrangThai)
            {
                ViewBag.Error = "Tài khoản đã bị khóa.";
                return View(vm);
            }

            var docGia = _context.DocGias
                .FirstOrDefault(d => d.MaTaiKhoan == taiKhoan.MaTaiKhoan);

            // Ghi Session theo đúng key nhóm quy định
            HttpContext.Session.SetInt32("MaTaiKhoan", taiKhoan.MaTaiKhoan);
            HttpContext.Session.SetString("VaiTro", taiKhoan.VaiTro);
            HttpContext.Session.SetString("HoTen", docGia?.HoTen ?? "Admin");
            HttpContext.Session.SetInt32("MaDocGia", docGia?.MaDocGia ?? 0);

            // Điều hướng theo vai trò
            if (taiKhoan.VaiTro == "Admin")
                return RedirectToAction("Index", "Dashboard");

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Đăng xuất thành công.";
            return RedirectToAction("Login");
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            // Check trùng tên đăng nhập
            if (_context.TaiKhoans.Any(t => t.TenDangNhap == vm.TenDangNhap))
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại.";
                return View(vm);
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Tạo tài khoản
                var taiKhoan = new TaiKhoan
                {
                    TenDangNhap = vm.TenDangNhap,
                    MatKhau = vm.MatKhau,
                    VaiTro = "DocGia",
                    TrangThai = true
                };
                _context.TaiKhoans.Add(taiKhoan);
                _context.SaveChanges();

                // Tạo độc giả liên kết
                var docGia = new DocGia
                {
                    MaTaiKhoan = taiKhoan.MaTaiKhoan,
                    HoTen = vm.HoTen,
                    Email = vm.Email,
                    SoDienThoai = vm.SoDienThoai,
                    DiaChi = vm.DiaChi,
                    NgayDangKy = DateTime.Now,
                    TrangThai = true
                };
                _context.DocGias.Add(docGia);
                _context.SaveChanges();

                transaction.Commit();

                TempData["Success"] = "Đăng ký thành công. Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }
            catch
            {
                transaction.Rollback();
                ViewBag.Error = "Có lỗi xảy ra. Vui lòng thử lại.";
                return View(vm);
            }
        }
    }
}