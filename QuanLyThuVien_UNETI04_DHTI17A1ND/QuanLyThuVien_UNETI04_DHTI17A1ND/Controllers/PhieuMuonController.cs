// Họ và tên: Nguyễn Mạnh Hùng
// Mã sinh viên: 23203100075
// Nội dung thực hiện: Module 4 - Quản lý phiếu mượn, xác nhận mượn/trả, Dashboard, Thống kê

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Data;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Controllers
{
    public class PhieuMuonController : Controller
    {
        private readonly AppDbContext _context;

        public PhieuMuonController(AppDbContext context)
        {
            _context = context;
        }

        // Kiểm tra quyền Admin (dùng Session key do Module 1 set)
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("VaiTro") == "Admin";
        }

        // GET: /PhieuMuon
        public async Task<IActionResult> Index(string? tenDocGia, int? trangThai,
                                               DateTime? tuNgay, DateTime? denNgay)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var query = _context.PhieuMuons
                .Include(p => p.DocGia)
                .Include(p => p.ChiTietPhieuMuons)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(tenDocGia))
            {
                query = query.Where(p => p.DocGia != null && p.DocGia.HoTen.Contains(tenDocGia));
            }

            if (trangThai.HasValue)
            {
                query = query.Where(p => p.TrangThai == trangThai.Value);
            }

            if (tuNgay.HasValue)
            {
                query = query.Where(p => p.NgayMuon >= tuNgay.Value);
            }
            if (denNgay.HasValue)
            {
                query = query.Where(p => p.NgayMuon <= denNgay.Value);
            }

            var danhSach = await query
                .OrderByDescending(p => p.NgayMuon)
                .ToListAsync();

            ViewBag.TenDocGia = tenDocGia;
            ViewBag.TrangThai = trangThai;
            ViewBag.TuNgay = tuNgay?.ToString("yyyy-MM-dd");
            ViewBag.DenNgay = denNgay?.ToString("yyyy-MM-dd");

            return View(danhSach);
        }

        // GET: /PhieuMuon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (id == null) return NotFound();

            var phieuMuon = await _context.PhieuMuons
                .Include(p => p.DocGia)
                .Include(p => p.ChiTietPhieuMuons)
                    .ThenInclude(ct => ct.Sach)
                .FirstOrDefaultAsync(p => p.MaPhieuMuon == id);

            if (phieuMuon == null) return NotFound();

            return View(phieuMuon);
        }

        // POST: /PhieuMuon/XacNhanMuon/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XacNhanMuon(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var phieuMuon = await _context.PhieuMuons
                .Include(p => p.ChiTietPhieuMuons)
                    .ThenInclude(ct => ct.Sach)
                .FirstOrDefaultAsync(p => p.MaPhieuMuon == id);

            if (phieuMuon == null)
            {
                TempData["Error"] = "Không tìm thấy phiếu mượn.";
                return RedirectToAction(nameof(Index));
            }

            if (phieuMuon.TrangThai != 0)
            {
                TempData["Error"] = "Chỉ xác nhận được phiếu đang ở trạng thái Chờ xác nhận.";
                return RedirectToAction(nameof(Details), new { id });
            }

            // Kiểm tra tồn kho và trạng thái sách
            foreach (var ct in phieuMuon.ChiTietPhieuMuons ?? new List<ChiTietPhieuMuon>())
            {
                if (ct.Sach == null) continue;

                if (ct.Sach.TrangThai != 0)
                {
                    TempData["Error"] = $"Sách \"{ct.Sach.TenSach}\" đã ngừng phục vụ.";
                    return RedirectToAction(nameof(Details), new { id });
                }

                if (ct.Sach.SoLuongCon < ct.SoLuongMuon)
                {
                    TempData["Error"] = $"Sách \"{ct.Sach.TenSach}\" chỉ còn {ct.Sach.SoLuongCon} cuốn, không đủ để xác nhận.";
                    return RedirectToAction(nameof(Details), new { id });
                }
            }

            // Trừ tồn kho
            foreach (var ct in phieuMuon.ChiTietPhieuMuons ?? new List<ChiTietPhieuMuon>())
            {
                if (ct.Sach == null) continue;
                ct.Sach.SoLuongCon -= ct.SoLuongMuon;
            }

            phieuMuon.TrangThai = 1; // Đang mượn
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã xác nhận mượn sách.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: /PhieuMuon/HuyPhieu/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HuyPhieu(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var phieuMuon = await _context.PhieuMuons.FindAsync(id);
            if (phieuMuon == null)
            {
                TempData["Error"] = "Không tìm thấy phiếu mượn.";
                return RedirectToAction(nameof(Index));
            }

            if (phieuMuon.TrangThai != 0)
            {
                TempData["Error"] = "Chỉ hủy được phiếu đang ở trạng thái Chờ xác nhận.";
                return RedirectToAction(nameof(Details), new { id });
            }

            phieuMuon.TrangThai = 3; // Đã hủy
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã hủy phiếu mượn.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /PhieuMuon/TraSach/5
        public async Task<IActionResult> TraSach(int? id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (id == null) return NotFound();

            var phieuMuon = await _context.PhieuMuons
                .Include(p => p.DocGia)
                .Include(p => p.ChiTietPhieuMuons)
                    .ThenInclude(ct => ct.Sach)
                .FirstOrDefaultAsync(p => p.MaPhieuMuon == id);

            if (phieuMuon == null) return NotFound();

            if (phieuMuon.TrangThai != 1)
            {
                TempData["Error"] = "Chỉ trả được phiếu đang ở trạng thái Đang mượn.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var vm = new TraSachViewModel
            {
                MaPhieuMuon = phieuMuon.MaPhieuMuon,
                TenDocGia = phieuMuon.DocGia?.HoTen ?? "",
                NgayMuon = phieuMuon.NgayMuon,
                HanTra = phieuMuon.HanTra,
                DanhSachSach = (phieuMuon.ChiTietPhieuMuons ?? new List<ChiTietPhieuMuon>())
                    .Select(ct => new ChiTietTraViewModel
                    {
                        MaChiTiet = ct.MaChiTiet,
                        TenSach = ct.Sach?.TenSach ?? "",
                        TacGia = ct.Sach?.TacGia ?? "",
                        SoLuongMuon = ct.SoLuongMuon,
                        NgayTra = DateTime.Now,
                        TinhTrangTra = "Tốt"
                    }).ToList()
            };

            return View(vm);
        }

        // POST: /PhieuMuon/TraSach/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TraSach(int id, TraSachViewModel vm)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var phieuMuon = await _context.PhieuMuons
                .Include(p => p.ChiTietPhieuMuons)
                    .ThenInclude(ct => ct.Sach)
                .FirstOrDefaultAsync(p => p.MaPhieuMuon == id);

            if (phieuMuon == null) return NotFound();

            if (phieuMuon.TrangThai != 1)
            {
                TempData["Error"] = "Chỉ trả được phiếu đang ở trạng thái Đang mượn.";
                return RedirectToAction(nameof(Details), new { id });
            }

            if (!ModelState.IsValid)
            {
                vm.MaPhieuMuon = phieuMuon.MaPhieuMuon;
                vm.TenDocGia = phieuMuon.DocGia?.HoTen ?? "";
                vm.NgayMuon = phieuMuon.NgayMuon;
                vm.HanTra = phieuMuon.HanTra;
                return View(vm);
            }

            foreach (var ctInput in vm.DanhSachSach)
            {
                var ct = phieuMuon.ChiTietPhieuMuons?
                    .FirstOrDefault(x => x.MaChiTiet == ctInput.MaChiTiet);
                if (ct == null) continue;

                ct.NgayTra = ctInput.NgayTra;
                ct.TinhTrangTra = ctInput.TinhTrangTra;

                if (ctInput.TinhTrangTra != "Mất" && ct.Sach != null)
                {
                    ct.Sach.SoLuongCon += ct.SoLuongMuon;
                    if (ct.Sach.SoLuongCon > ct.Sach.SoLuong)
                    {
                        ct.Sach.SoLuongCon = ct.Sach.SoLuong;
                    }
                }
            }

            phieuMuon.TrangThai = 2; // Đã trả
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã ghi nhận trả sách.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /PhieuMuon/QuaHan
        public async Task<IActionResult> QuaHan()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var today = DateTime.Now.Date;
            var quaHan = await _context.PhieuMuons
                .Include(p => p.DocGia)
                .Include(p => p.ChiTietPhieuMuons)
                    .ThenInclude(ct => ct.Sach)
                .Where(p => p.TrangThai == 1 && p.HanTra < today)
                .OrderBy(p => p.HanTra)
                .ToListAsync();

            return View(quaHan);
        }
    }
}