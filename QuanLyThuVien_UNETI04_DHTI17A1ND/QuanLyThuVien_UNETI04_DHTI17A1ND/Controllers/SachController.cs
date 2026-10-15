// Họ và tên: Vũ Duy Anh
// Mã sinh viên: 23203100073
// Nội dung thực hiện: Quản lý sách, tìm kiếm, lọc, sắp xếp và phân trang

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Data;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities;
using QuanLyThuVien_UNETI04_DHTI17A1ND.ViewModels;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Controllers
{
    public class SachController : Controller
    {
        private readonly AppDbContext _context;
        private const int PAGE_SIZE = 10;

        public SachController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Sach/Index (Tìm kiếm + Lọc + Sắp xếp + Phân trang)
        public async Task<IActionResult> Index(
            string? tuKhoa,
            int? maTheLoai,
            string? nhaXuatBan,
            int? trangThai,
            int? namTu,
            int? namDen,
            string? sapXep,
            int trang = 1)
        {
            var vm = new SachSearchViewModel
            {
                TuKhoa = tuKhoa,
                MaTheLoai = maTheLoai,
                NhaXuatBan = nhaXuatBan,
                TrangThai = trangThai,
                NamXuatBanTu = namTu,
                NamXuatBanDen = namDen,
                SapXep = sapXep,
                TrangHienTai = trang < 1 ? 1 : trang,
                KichThuocTrang = PAGE_SIZE
            };

            // 1. Truy vấn
            var query = _context.Sachs.Include(s => s.TheLoai).AsQueryable();

            // 2. Tìm kiếm
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                tuKhoa = tuKhoa.Trim();
                query = query.Where(s => s.TenSach.Contains(tuKhoa) || s.TacGia.Contains(tuKhoa));
            }

            // 3. Lọc
            if (maTheLoai.HasValue && maTheLoai > 0)
                query = query.Where(s => s.MaTheLoai == maTheLoai);

            if (!string.IsNullOrWhiteSpace(nhaXuatBan))
                query = query.Where(s => s.NhaXuatBan != null && s.NhaXuatBan.Contains(nhaXuatBan));

            if (trangThai.HasValue)
                query = query.Where(s => s.TrangThai == trangThai);

            if (namTu.HasValue)
                query = query.Where(s => s.NamXuatBan >= namTu);

            if (namDen.HasValue)
                query = query.Where(s => s.NamXuatBan <= namDen);

            // 4. Sắp xếp
            query = sapXep switch
            {
                "ten_az" => query.OrderBy(s => s.TenSach),
                "ten_za" => query.OrderByDescending(s => s.TenSach),
                "nam_tang" => query.OrderBy(s => s.NamXuatBan),
                "nam_giam" => query.OrderByDescending(s => s.NamXuatBan),
                "gia_tang" => query.OrderBy(s => s.GiaSach),
                "gia_giam" => query.OrderByDescending(s => s.GiaSach),
                _ => query.OrderBy(s => s.MaSach)
            };

            // 5. Phân trang
            vm.TongSoBanGhi = await query.CountAsync();
            vm.TongSoTrang = (int)Math.Ceiling(vm.TongSoBanGhi / (double)PAGE_SIZE);

            vm.DanhSachSach = await query
                .Skip((vm.TrangHienTai - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToListAsync();

            // Dropdown
            vm.DanhSachTheLoai = await _context.TheLoais
                .Select(t => new SelectListItem { Value = t.MaTheLoai.ToString(), Text = t.TenTheLoai })
                .ToListAsync();

            vm.DanhSachNhaXuatBan = await _context.Sachs
                .Where(s => s.NhaXuatBan != null)
                .Select(s => new SelectListItem { Value = s.NhaXuatBan!, Text = s.NhaXuatBan! })
                .Distinct()
                .ToListAsync();

            return View(vm);
        }

        // GET: Sach/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var sach = await _context.Sachs.Include(s => s.TheLoai)
                .FirstOrDefaultAsync(m => m.MaSach == id);

            if (sach == null) return NotFound();

            return View(sach);
        }

        // GET: Sach/Create
        public async Task<IActionResult> Create()
        {
            await LoadViewBag();
            return View();
        }

        // POST: Sach/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("TenSach,MaTheLoai,TacGia,NhaXuatBan,NamXuatBan,SoLuong,SoLuongCon,GiaSach,MoTa,TrangThai")] Sach sach)
        {
            if (sach.SoLuongCon > sach.SoLuong)
                ModelState.AddModelError("SoLuongCon", "Số lượng còn không được vượt quá số lượng");

            if (ModelState.IsValid)
            {
                _context.Add(sach);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm sách thành công!";
                return RedirectToAction(nameof(Index));
            }

            await LoadViewBag(sach.MaTheLoai);
            return View(sach);
        }

        // GET: Sach/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sach = await _context.Sachs.FindAsync(id);
            if (sach == null) return NotFound();

            await LoadViewBag(sach.MaTheLoai);
            return View(sach);
        }

        // POST: Sach/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("MaSach,TenSach,MaTheLoai,TacGia,NhaXuatBan,NamXuatBan,SoLuong,SoLuongCon,GiaSach,MoTa,TrangThai")] Sach sach)
        {
            if (id != sach.MaSach) return NotFound();

            if (sach.SoLuongCon > sach.SoLuong)
                ModelState.AddModelError("SoLuongCon", "Số lượng còn không được vượt quá số lượng");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sach);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật sách thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SachExists(sach.MaSach)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            await LoadViewBag(sach.MaTheLoai);
            return View(sach);
        }

        // GET: Sach/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var sach = await _context.Sachs.Include(s => s.TheLoai)
                .FirstOrDefaultAsync(m => m.MaSach == id);

            if (sach == null) return NotFound();

            return View(sach);
        }

        // POST: Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sach = await _context.Sachs.FindAsync(id);
            if (sach != null)
            {
                _context.Sachs.Remove(sach);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xóa sách thành công!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(int id) => _context.Sachs.Any(e => e.MaSach == id);

        private async Task LoadViewBag(int? selected = null)
        {
            ViewBag.DanhSachTheLoai = new SelectList(
                await _context.TheLoais.ToListAsync(),
                "MaTheLoai", "TenTheLoai", selected);

            ViewBag.DanhSachTrangThai = new List<SelectListItem>
            {
                new() { Value = "0", Text = "Đang phục vụ" },
                new() { Value = "1", Text = "Tạm ngừng" },
                new() { Value = "2", Text = "Ngừng phục vụ" }
            };
        }
    }
}