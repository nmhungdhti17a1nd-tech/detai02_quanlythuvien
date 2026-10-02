// Họ và tên: Nguyễn Văn Hùng
// Mã sinh viên: [MSSV Văn Hùng]
// Nội dung thực hiện: Module 3 - Đăng ký mượn sách và xem thông tin mượn của chính độc giả

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Data;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Helpers;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.ViewModels;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Controllers
{
    public class MuonSachController : Controller
    {
        private readonly AppDbContext _context;

        public MuonSachController(AppDbContext context)
        {
            _context = context;
        }

        private IActionResult ChuyenDenDangNhap()
        {
            TempData["Loi"] = "Vui lòng đăng nhập bằng tài khoản độc giả để mượn sách.";
            return RedirectToAction("Login", "Account");
        }

        private static DangKyMuonViewModel TaoViewModel(Sach sach, int soLuongMuon, int soNgayMuon) => new()
        {
            MaSach = sach.MaSach,
            TenSach = sach.TenSach,
            TacGia = sach.TacGia,
            TenTheLoai = sach.TheLoai?.TenTheLoai,
            SoLuongCon = sach.SoLuongCon,
            SoLuongMuon = soLuongMuon,
            SoNgayMuon = soNgayMuon
        };

        // GET: /MuonSach/DangKy?maSach=5
        public async Task<IActionResult> DangKy(int maSach)
        {
            if (!HttpContext.LaDocGia()) return ChuyenDenDangNhap();

            var sach = await _context.Sachs.AsNoTracking()
                .Include(s => s.TheLoai)
                .FirstOrDefaultAsync(s => s.MaSach == maSach);
            if (sach == null) return NotFound();

            return View(TaoViewModel(sach, 1, 14));
        }

        // POST: /MuonSach/DangKy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangKy(DangKyMuonViewModel model)
        {
            if (!HttpContext.LaDocGia()) return ChuyenDenDangNhap();

            var sach = await _context.Sachs
                .Include(s => s.TheLoai)
                .FirstOrDefaultAsync(s => s.MaSach == model.MaSach);
            if (sach == null) return NotFound(); // sách không tồn tại

            var maTaiKhoan = HttpContext.LayMaTaiKhoan()!.Value;
            var docGia = await _context.DocGias
                .Include(d => d.TaiKhoan)
                .FirstOrDefaultAsync(d => d.MaTaiKhoan == maTaiKhoan);

            // ----- Kiểm tra nghiệp vụ (đề mục 7.4) -----
            if (docGia == null)
            {
                ModelState.AddModelError("", "Không tìm thấy hồ sơ độc giả gắn với tài khoản của bạn.");
            }
            else if (!docGia.TrangThai || docGia.TaiKhoan?.TrangThai == false)
            {
                ModelState.AddModelError("", "Tài khoản độc giả đang bị khóa, không thể đăng ký mượn sách.");
            }

            if (sach.TrangThai != 0) // 0: Đang phục vụ
            {
                ModelState.AddModelError("", "Sách hiện không phục vụ mượn.");
            }

            if (model.SoLuongMuon > sach.SoLuongCon)
            {
                ModelState.AddModelError(nameof(model.SoLuongMuon),
                    $"Không đủ sách: hiện chỉ còn {sach.SoLuongCon} cuốn.");
            }

            if (docGia != null)
            {
                // Không tạo yêu cầu trùng: đã có phiếu Chờ xác nhận / Đang mượn chứa cùng sách này
                bool trung = await _context.ChiTietPhieuMuons.AnyAsync(ct =>
                    ct.MaSach == sach.MaSach
                    && ct.PhieuMuon!.MaDocGia == docGia.MaDocGia
                    && (ct.PhieuMuon!.TrangThai == 0 || ct.PhieuMuon!.TrangThai == 1));
                if (trung)
                {
                    ModelState.AddModelError("",
                        "Bạn đã có yêu cầu mượn hoặc đang mượn cuốn sách này, không thể đăng ký trùng.");
                }
            }

            if (!ModelState.IsValid)
            {
                // Không lưu dữ liệu không hợp lệ; nạp lại thông tin hiển thị từ CSDL
                model.TenSach = sach.TenSach;
                model.TacGia = sach.TacGia;
                model.TenTheLoai = sach.TheLoai?.TenTheLoai;
                model.SoLuongCon = sach.SoLuongCon;
                return View(model);
            }

            // ----- Lưu: 1 phiếu (Chờ xác nhận) + 1 chi tiết, trong cùng 1 lần SaveChanges -----
            var phieu = new PhieuMuon
            {
                MaDocGia = docGia!.MaDocGia,
                NgayMuon = DateTime.Now,
                HanTra = DateTime.Today.AddDays(model.SoNgayMuon),
                TrangThai = 0, // Chờ xác nhận
                ChiTietPhieuMuons = new List<ChiTietPhieuMuon>
                {
                    new ChiTietPhieuMuon
                    {
                        MaSach = sach.MaSach,
                        SoLuongMuon = model.SoLuongMuon
                        // NgayTra, TinhTrangTra: để trống, Module 4 cập nhật khi trả sách
                    }
                }
            };

            // Lưu ý: KHÔNG trừ SoLuongCon ở đây. Module 4 kiểm tra lại và trừ khi Admin xác nhận mượn.
            _context.PhieuMuons.Add(phieu);
            await _context.SaveChangesAsync();

            TempData["ThanhCong"] = "Đăng ký mượn sách thành công. Vui lòng chờ thư viện xác nhận.";
            return RedirectToAction(nameof(DangMuon));
        }

        // Lấy phiếu của CHÍNH độc giả đang đăng nhập theo các trạng thái yêu cầu (null nếu không có hồ sơ)
        private async Task<List<PhieuMuon>?> LayPhieuCuaToi(params int[] trangThais)
        {
            var maTaiKhoan = HttpContext.LayMaTaiKhoan()!.Value;
            var docGia = await _context.DocGias.AsNoTracking()
                .FirstOrDefaultAsync(d => d.MaTaiKhoan == maTaiKhoan);
            if (docGia == null) return null;

            return await _context.PhieuMuons.AsNoTracking()
                .Where(p => p.MaDocGia == docGia.MaDocGia && trangThais.Contains(p.TrangThai))
                .Include(p => p.ChiTietPhieuMuons).ThenInclude(ct => ct.Sach)
                .OrderByDescending(p => p.NgayMuon)
                .ToListAsync();
        }

        // GET: /MuonSach/DangMuon - yêu cầu chờ xác nhận và sách đang mượn
        public async Task<IActionResult> DangMuon()
        {
            if (!HttpContext.LaDocGia()) return ChuyenDenDangNhap();

            var phieus = await LayPhieuCuaToi(0, 1);
            if (phieus == null) return NotFound();

            return View(new DangMuonViewModel
            {
                ChoXacNhan = phieus.Where(p => p.TrangThai == 0).ToList(),
                DangMuon = phieus.Where(p => p.TrangThai == 1).ToList()
            });
        }

        // GET: /MuonSach/LichSu - phiếu đã trả hoặc đã hủy
        public async Task<IActionResult> LichSu()
        {
            if (!HttpContext.LaDocGia()) return ChuyenDenDangNhap();

            var phieus = await LayPhieuCuaToi(2, 3);
            if (phieus == null) return NotFound();

            return View(phieus);
        }
    }
}
