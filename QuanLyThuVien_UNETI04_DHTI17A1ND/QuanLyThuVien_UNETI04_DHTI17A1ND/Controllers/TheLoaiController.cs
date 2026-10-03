// Họ và tên: Chu Công Đạt
// Mã sinh viên: [MSSV của bạn]
// Nội dung thực hiện: Module 1 - Quản lý thể loại sách

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Data;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Controllers
{
    public class TheLoaiController : Controller
    {
        private readonly AppDbContext _context;

        public TheLoaiController(AppDbContext context)
        {
            _context = context;
        }

        // Hàm kiểm tra quyền Admin
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("VaiTro") == "Admin";
        }

        // GET: /TheLoai
        public IActionResult Index()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var danhSach = _context.TheLoais.ToList();
            return View(danhSach);
        }

        // GET: /TheLoai/Details/5
        public IActionResult Details(int? id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (id == null) return NotFound();

            var theLoai = _context.TheLoais
                .FirstOrDefault(t => t.MaTheLoai == id);

            if (theLoai == null) return NotFound();

            return View(theLoai);
        }

        // GET: /TheLoai/Create
        public IActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View();
        }

        // POST: /TheLoai/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TheLoai theLoai)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var daTonTai = _context.TheLoais
                .Any(t => t.TenTheLoai == theLoai.TenTheLoai);

            if (daTonTai)
            {
                ModelState.AddModelError("TenTheLoai", "Tên thể loại đã tồn tại");
                return View(theLoai);
            }

            if (ModelState.IsValid)
            {
                _context.TheLoais.Add(theLoai);
                _context.SaveChanges();
                TempData["Success"] = "Thêm thể loại thành công!";
                return RedirectToAction(nameof(Index));
            }

            return View(theLoai);
        }

        // GET: /TheLoai/Edit/5
        public IActionResult Edit(int? id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (id == null) return NotFound();

            var theLoai = _context.TheLoais.Find(id);
            if (theLoai == null) return NotFound();

            return View(theLoai);
        }

        // POST: /TheLoai/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TheLoai theLoai)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (id != theLoai.MaTheLoai) return NotFound();

            var daTonTai = _context.TheLoais
                .Any(t => t.TenTheLoai == theLoai.TenTheLoai
                       && t.MaTheLoai != theLoai.MaTheLoai);

            if (daTonTai)
            {
                ModelState.AddModelError("TenTheLoai", "Tên thể loại đã tồn tại");
                return View(theLoai);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theLoai);
                    _context.SaveChanges();
                    TempData["Success"] = "Cập nhật thể loại thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.TheLoais.Any(t => t.MaTheLoai == id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(theLoai);
        }

        // GET: /TheLoai/Delete/5
        public IActionResult Delete(int? id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (id == null) return NotFound();

            var theLoai = _context.TheLoais
                .FirstOrDefault(t => t.MaTheLoai == id);

            if (theLoai == null) return NotFound();

            return View(theLoai);
        }

        // POST: /TheLoai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var theLoai = _context.TheLoais.Find(id);
            if (theLoai == null) return NotFound();

            // Kiểm tra còn sách thuộc thể loại này không
            var coSach = _context.Sachs.Any(s => s.MaTheLoai == id);
            if (coSach)
            {
                TempData["Error"] = "Không thể xóa! Còn sách thuộc thể loại này.";
                return RedirectToAction(nameof(Index));
            }

            _context.TheLoais.Remove(theLoai);
            _context.SaveChanges();
            TempData["Success"] = "Xóa thể loại thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}