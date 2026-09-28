// Họ và tên: [Nguyễn Mạnh Hùng]
// Mã sinh viên: 23203100075
// Nội dung thực hiện: Khởi tạo dữ liệu mẫu cho database

using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Nếu đã có dữ liệu thì bỏ qua
            if (context.TaiKhoans.Any()) return;

            // ============ 1. TÀI KHOẢN (2 Admin + 15 Độc giả) ============
            var taiKhoans = new TaiKhoan[]
            {
                new TaiKhoan { TenDangNhap = "admin", MatKhau = "123456", VaiTro = "Admin", TrangThai = true },
                new TaiKhoan { TenDangNhap = "admin2", MatKhau = "123456", VaiTro = "Admin", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia01", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia02", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia03", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia04", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia05", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia06", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia07", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia08", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia09", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia10", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia11", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia12", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia13", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia14", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
                new TaiKhoan { TenDangNhap = "docgia15", MatKhau = "123456", VaiTro = "DocGia", TrangThai = true },
            };
            context.TaiKhoans.AddRange(taiKhoans);
            context.SaveChanges();

            // ============ 2. THỂ LOẠI (5 thể loại) ============
            var theLoais = new TheLoai[]
            {
                new TheLoai { TenTheLoai = "Văn học", MoTa = "Tiểu thuyết, truyện ngắn, thơ", TrangThai = true },
                new TheLoai { TenTheLoai = "Khoa học", MoTa = "Sách khoa học tự nhiên", TrangThai = true },
                new TheLoai { TenTheLoai = "Kinh tế", MoTa = "Sách kinh tế, tài chính, quản trị", TrangThai = true },
                new TheLoai { TenTheLoai = "Công nghệ thông tin", MoTa = "Lập trình, mạng, AI", TrangThai = true },
                new TheLoai { TenTheLoai = "Thiếu nhi", MoTa = "Truyện tranh, sách thiếu nhi", TrangThai = true },
            };
            context.TheLoais.AddRange(theLoais);
            context.SaveChanges();

            // ============ 3. SÁCH (20 đầu sách) ============
            var sachs = new Sach[]
            {
                new Sach { TenSach = "Tắt đèn", MaTheLoai = theLoais[0].MaTheLoai, TacGia = "Ngô Tất Tố", NhaXuatBan = "NXB Văn học", NamXuatBan = 2015, SoLuong = 10, SoLuongCon = 10, GiaSach = 85000, MoTa = "Tác phẩm văn học hiện thực", TrangThai = 0 },
                new Sach { TenSach = "Số đỏ", MaTheLoai = theLoais[0].MaTheLoai, TacGia = "Vũ Trọng Phụng", NhaXuatBan = "NXB Văn học", NamXuatBan = 2016, SoLuong = 8, SoLuongCon = 8, GiaSach = 90000, MoTa = "Tiểu thuyết trào phúng", TrangThai = 0 },
                new Sach { TenSach = "Chí Phèo", MaTheLoai = theLoais[0].MaTheLoai, TacGia = "Nam Cao", NhaXuatBan = "NXB Giáo dục", NamXuatBan = 2014, SoLuong = 12, SoLuongCon = 12, GiaSach = 70000, MoTa = "Truyện ngắn hiện thực", TrangThai = 0 },
                new Sach { TenSach = "Vợ nhặt", MaTheLoai = theLoais[0].MaTheLoai, TacGia = "Kim Lân", NhaXuatBan = "NXB Văn học", NamXuatBan = 2018, SoLuong = 6, SoLuongCon = 6, GiaSach = 65000, MoTa = "Truyện ngắn nổi tiếng", TrangThai = 0 },
                new Sach { TenSach = "Lão Hạc", MaTheLoai = theLoais[0].MaTheLoai, TacGia = "Nam Cao", NhaXuatBan = "NXB Giáo dục", NamXuatBan = 2013, SoLuong = 9, SoLuongCon = 9, GiaSach = 60000, MoTa = "Truyện ngắn về người nông dân", TrangThai = 0 },
                new Sach { TenSach = "Vật lý đại cương", MaTheLoai = theLoais[1].MaTheLoai, TacGia = "Nguyễn Văn A", NhaXuatBan = "NXB Khoa học", NamXuatBan = 2020, SoLuong = 15, SoLuongCon = 15, GiaSach = 120000, MoTa = "Giáo trình vật lý", TrangThai = 0 },
                new Sach { TenSach = "Hóa học hữu cơ", MaTheLoai = theLoais[1].MaTheLoai, TacGia = "Trần Thị B", NhaXuatBan = "NXB Khoa học", NamXuatBan = 2019, SoLuong = 7, SoLuongCon = 7, GiaSach = 135000, MoTa = "Giáo trình hóa học", TrangThai = 0 },
                new Sach { TenSach = "Toán cao cấp", MaTheLoai = theLoais[1].MaTheLoai, TacGia = "Lê Văn C", NhaXuatBan = "NXB Giáo dục", NamXuatBan = 2021, SoLuong = 20, SoLuongCon = 20, GiaSach = 110000, MoTa = "Giáo trình toán đại học", TrangThai = 0 },
                new Sach { TenSach = "Sinh học phân tử", MaTheLoai = theLoais[1].MaTheLoai, TacGia = "Phạm Văn D", NhaXuatBan = "NXB Y học", NamXuatBan = 2022, SoLuong = 5, SoLuongCon = 5, GiaSach = 150000, MoTa = "Sách chuyên ngành sinh học", TrangThai = 0 },
                new Sach { TenSach = "Kinh tế vĩ mô", MaTheLoai = theLoais[2].MaTheLoai, TacGia = "Nguyễn Văn E", NhaXuatBan = "NXB Kinh tế", NamXuatBan = 2019, SoLuong = 10, SoLuongCon = 10, GiaSach = 95000, MoTa = "Giáo trình kinh tế vĩ mô", TrangThai = 0 },
                new Sach { TenSach = "Kinh tế vi mô", MaTheLoai = theLoais[2].MaTheLoai, TacGia = "Trần Văn F", NhaXuatBan = "NXB Kinh tế", NamXuatBan = 2020, SoLuong = 8, SoLuongCon = 8, GiaSach = 95000, MoTa = "Giáo trình kinh tế vi mô", TrangThai = 0 },
                new Sach { TenSach = "Quản trị học", MaTheLoai = theLoais[2].MaTheLoai, TacGia = "Lê Thị G", NhaXuatBan = "NXB Lao động", NamXuatBan = 2021, SoLuong = 6, SoLuongCon = 6, GiaSach = 100000, MoTa = "Sách quản trị kinh doanh", TrangThai = 0 },
                new Sach { TenSach = "Lập trình C# căn bản", MaTheLoai = theLoais[3].MaTheLoai, TacGia = "Nguyễn Văn H", NhaXuatBan = "NXB Bách Khoa", NamXuatBan = 2022, SoLuong = 12, SoLuongCon = 12, GiaSach = 180000, MoTa = "Giáo trình lập trình C#", TrangThai = 0 },
                new Sach { TenSach = "ASP.NET Core MVC", MaTheLoai = theLoais[3].MaTheLoai, TacGia = "Trần Văn I", NhaXuatBan = "NXB Bách Khoa", NamXuatBan = 2023, SoLuong = 10, SoLuongCon = 10, GiaSach = 200000, MoTa = "Lập trình web với ASP.NET Core", TrangThai = 0 },
                new Sach { TenSach = "Cơ sở dữ liệu", MaTheLoai = theLoais[3].MaTheLoai, TacGia = "Lê Văn K", NhaXuatBan = "NXB Giáo dục", NamXuatBan = 2021, SoLuong = 15, SoLuongCon = 15, GiaSach = 130000, MoTa = "Giáo trình cơ sở dữ liệu", TrangThai = 0 },
                new Sach { TenSach = "Trí tuệ nhân tạo", MaTheLoai = theLoais[3].MaTheLoai, TacGia = "Phạm Văn L", NhaXuatBan = "NXB Khoa học", NamXuatBan = 2023, SoLuong = 7, SoLuongCon = 7, GiaSach = 220000, MoTa = "Sách về AI và machine learning", TrangThai = 0 },
                new Sach { TenSach = "Dế Mèn phiêu lưu ký", MaTheLoai = theLoais[4].MaTheLoai, TacGia = "Tô Hoài", NhaXuatBan = "NXB Kim Đồng", NamXuatBan = 2018, SoLuong = 20, SoLuongCon = 20, GiaSach = 50000, MoTa = "Truyện thiếu nhi kinh điển", TrangThai = 0 },
                new Sach { TenSach = "Truyện cổ tích Việt Nam", MaTheLoai = theLoais[4].MaTheLoai, TacGia = "Nhiều tác giả", NhaXuatBan = "NXB Kim Đồng", NamXuatBan = 2019, SoLuong = 15, SoLuongCon = 15, GiaSach = 60000, MoTa = "Tuyển tập truyện cổ tích", TrangThai = 0 },
                new Sach { TenSach = "Harry Potter và Hòn đá phù thủy", MaTheLoai = theLoais[4].MaTheLoai, TacGia = "J.K. Rowling", NhaXuatBan = "NXB Trẻ", NamXuatBan = 2020, SoLuong = 10, SoLuongCon = 10, GiaSach = 150000, MoTa = "Tiểu thuyết fantasy nổi tiếng", TrangThai = 0 },
                new Sach { TenSach = "Sách tạm ngừng", MaTheLoai = theLoais[0].MaTheLoai, TacGia = "Tác giả X", NhaXuatBan = "NXB Test", NamXuatBan = 2024, SoLuong = 5, SoLuongCon = 5, GiaSach = 100000, MoTa = "Sách để test trạng thái tạm ngừng", TrangThai = 1 },
            };
            context.Sachs.AddRange(sachs);
            context.SaveChanges();

            // ============ 4. ĐỘC GIẢ (15 độc giả) ============
            var docGias = new DocGia[]
            {
                new DocGia { MaTaiKhoan = taiKhoans[2].MaTaiKhoan, HoTen = "Nguyễn Văn An", NgaySinh = new DateTime(2000, 1, 15), GioiTinh = "Nam", SoDienThoai = "0901234567", Email = "an@gmail.com", DiaChi = "Hà Nội", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[3].MaTaiKhoan, HoTen = "Trần Thị Bình", NgaySinh = new DateTime(2001, 5, 20), GioiTinh = "Nữ", SoDienThoai = "0901234568", Email = "binh@gmail.com", DiaChi = "Hải Phòng", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[4].MaTaiKhoan, HoTen = "Lê Văn Cường", NgaySinh = new DateTime(1999, 8, 10), GioiTinh = "Nam", SoDienThoai = "0901234569", Email = "cuong@gmail.com", DiaChi = "Nam Định", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[5].MaTaiKhoan, HoTen = "Phạm Thị Dung", NgaySinh = new DateTime(2002, 3, 25), GioiTinh = "Nữ", SoDienThoai = "0901234570", Email = "dung@gmail.com", DiaChi = "Thái Bình", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[6].MaTaiKhoan, HoTen = "Hoàng Văn Em", NgaySinh = new DateTime(2000, 7, 12), GioiTinh = "Nam", SoDienThoai = "0901234571", Email = "em@gmail.com", DiaChi = "Ninh Bình", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[7].MaTaiKhoan, HoTen = "Vũ Thị Phương", NgaySinh = new DateTime(2001, 11, 8), GioiTinh = "Nữ", SoDienThoai = "0901234572", Email = "phuong@gmail.com", DiaChi = "Hà Nam", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[8].MaTaiKhoan, HoTen = "Đặng Văn Giang", NgaySinh = new DateTime(1998, 2, 14), GioiTinh = "Nam", SoDienThoai = "0901234573", Email = "giang@gmail.com", DiaChi = "Thanh Hóa", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[9].MaTaiKhoan, HoTen = "Bùi Thị Hoa", NgaySinh = new DateTime(2002, 6, 30), GioiTinh = "Nữ", SoDienThoai = "0901234574", Email = "hoa@gmail.com", DiaChi = "Nghệ An", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[10].MaTaiKhoan, HoTen = "Ngô Văn Inh", NgaySinh = new DateTime(2000, 9, 5), GioiTinh = "Nam", SoDienThoai = "0901234575", Email = "inh@gmail.com", DiaChi = "Hà Tĩnh", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[11].MaTaiKhoan, HoTen = "Dương Thị Kim", NgaySinh = new DateTime(2001, 4, 18), GioiTinh = "Nữ", SoDienThoai = "0901234576", Email = "kim@gmail.com", DiaChi = "Quảng Bình", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[12].MaTaiKhoan, HoTen = "Lý Văn Long", NgaySinh = new DateTime(1999, 12, 22), GioiTinh = "Nam", SoDienThoai = "0901234577", Email = "long@gmail.com", DiaChi = "Huế", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[13].MaTaiKhoan, HoTen = "Trịnh Thị Mai", NgaySinh = new DateTime(2002, 8, 15), GioiTinh = "Nữ", SoDienThoai = "0901234578", Email = "mai@gmail.com", DiaChi = "Đà Nẵng", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[14].MaTaiKhoan, HoTen = "Phan Văn Nam", NgaySinh = new DateTime(2000, 10, 3), GioiTinh = "Nam", SoDienThoai = "0901234579", Email = "nam@gmail.com", DiaChi = "Quảng Nam", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[15].MaTaiKhoan, HoTen = "Võ Thị Oanh", NgaySinh = new DateTime(2001, 1, 27), GioiTinh = "Nữ", SoDienThoai = "0901234580", Email = "oanh@gmail.com", DiaChi = "Quảng Ngãi", NgayDangKy = DateTime.Now, TrangThai = true },
                new DocGia { MaTaiKhoan = taiKhoans[16].MaTaiKhoan, HoTen = "Đỗ Văn Phúc", NgaySinh = new DateTime(1998, 5, 9), GioiTinh = "Nam", SoDienThoai = "0901234581", Email = "phuc@gmail.com", DiaChi = "Bình Định", NgayDangKy = DateTime.Now, TrangThai = true },
            };
            context.DocGias.AddRange(docGias);
            context.SaveChanges();

            // ============ 5. PHIẾU MƯỢN (18 phiếu) ============
            var phieuMuons = new PhieuMuon[]
            {
                new PhieuMuon { MaDocGia = docGias[0].MaDocGia, NgayMuon = DateTime.Now.AddDays(-30), HanTra = DateTime.Now.AddDays(-16), TrangThai = 2 },
                new PhieuMuon { MaDocGia = docGias[1].MaDocGia, NgayMuon = DateTime.Now.AddDays(-25), HanTra = DateTime.Now.AddDays(-11), TrangThai = 2 },
                new PhieuMuon { MaDocGia = docGias[2].MaDocGia, NgayMuon = DateTime.Now.AddDays(-20), HanTra = DateTime.Now.AddDays(-6), TrangThai = 2 },
                new PhieuMuon { MaDocGia = docGias[3].MaDocGia, NgayMuon = DateTime.Now.AddDays(-15), HanTra = DateTime.Now.AddDays(-1), TrangThai = 1 },
                new PhieuMuon { MaDocGia = docGias[4].MaDocGia, NgayMuon = DateTime.Now.AddDays(-12), HanTra = DateTime.Now.AddDays(2), TrangThai = 1 },
                new PhieuMuon { MaDocGia = docGias[5].MaDocGia, NgayMuon = DateTime.Now.AddDays(-10), HanTra = DateTime.Now.AddDays(4), TrangThai = 1 },
                new PhieuMuon { MaDocGia = docGias[6].MaDocGia, NgayMuon = DateTime.Now.AddDays(-8), HanTra = DateTime.Now.AddDays(6), TrangThai = 1 },
                new PhieuMuon { MaDocGia = docGias[7].MaDocGia, NgayMuon = DateTime.Now.AddDays(-6), HanTra = DateTime.Now.AddDays(8), TrangThai = 1 },
                new PhieuMuon { MaDocGia = docGias[8].MaDocGia, NgayMuon = DateTime.Now.AddDays(-5), HanTra = DateTime.Now.AddDays(9), TrangThai = 0 },
                new PhieuMuon { MaDocGia = docGias[9].MaDocGia, NgayMuon = DateTime.Now.AddDays(-4), HanTra = DateTime.Now.AddDays(10), TrangThai = 0 },
                new PhieuMuon { MaDocGia = docGias[10].MaDocGia, NgayMuon = DateTime.Now.AddDays(-3), HanTra = DateTime.Now.AddDays(11), TrangThai = 0 },
                new PhieuMuon { MaDocGia = docGias[11].MaDocGia, NgayMuon = DateTime.Now.AddDays(-2), HanTra = DateTime.Now.AddDays(12), TrangThai = 0 },
                new PhieuMuon { MaDocGia = docGias[12].MaDocGia, NgayMuon = DateTime.Now.AddDays(-1), HanTra = DateTime.Now.AddDays(13), TrangThai = 0 },
                new PhieuMuon { MaDocGia = docGias[13].MaDocGia, NgayMuon = DateTime.Now.AddDays(-40), HanTra = DateTime.Now.AddDays(-26), TrangThai = 2 },
                new PhieuMuon { MaDocGia = docGias[14].MaDocGia, NgayMuon = DateTime.Now.AddDays(-35), HanTra = DateTime.Now.AddDays(-21), TrangThai = 2 },
                new PhieuMuon { MaDocGia = docGias[0].MaDocGia, NgayMuon = DateTime.Now.AddDays(-18), HanTra = DateTime.Now.AddDays(-4), TrangThai = 3 },
                new PhieuMuon { MaDocGia = docGias[1].MaDocGia, NgayMuon = DateTime.Now.AddDays(-45), HanTra = DateTime.Now.AddDays(-31), TrangThai = 2 },
                new PhieuMuon { MaDocGia = docGias[2].MaDocGia, NgayMuon = DateTime.Now.AddDays(-50), HanTra = DateTime.Now.AddDays(-36), TrangThai = 2 },
            };
            context.PhieuMuons.AddRange(phieuMuons);
            context.SaveChanges();

            // ============ 6. CHI TIẾT PHIẾU MƯỢN ============
            var chiTiets = new ChiTietPhieuMuon[]
            {
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[0].MaPhieuMuon, MaSach = sachs[0].MaSach, SoLuongMuon = 1, NgayTra = DateTime.Now.AddDays(-16), TinhTrangTra = "Tốt" },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[0].MaPhieuMuon, MaSach = sachs[1].MaSach, SoLuongMuon = 1, NgayTra = DateTime.Now.AddDays(-16), TinhTrangTra = "Tốt" },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[1].MaPhieuMuon, MaSach = sachs[2].MaSach, SoLuongMuon = 1, NgayTra = DateTime.Now.AddDays(-11), TinhTrangTra = "Tốt" },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[2].MaPhieuMuon, MaSach = sachs[3].MaSach, SoLuongMuon = 2, NgayTra = DateTime.Now.AddDays(-6), TinhTrangTra = "Tốt" },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[3].MaPhieuMuon, MaSach = sachs[5].MaSach, SoLuongMuon = 1 },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[3].MaPhieuMuon, MaSach = sachs[6].MaSach, SoLuongMuon = 1 },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[4].MaPhieuMuon, MaSach = sachs[7].MaSach, SoLuongMuon = 1 },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[5].MaPhieuMuon, MaSach = sachs[9].MaSach, SoLuongMuon = 2 },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[6].MaPhieuMuon, MaSach = sachs[12].MaSach, SoLuongMuon = 1 },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[7].MaPhieuMuon, MaSach = sachs[13].MaSach, SoLuongMuon = 1 },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[8].MaPhieuMuon, MaSach = sachs[14].MaSach, SoLuongMuon = 1 },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[9].MaPhieuMuon, MaSach = sachs[16].MaSach, SoLuongMuon = 2 },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[10].MaPhieuMuon, MaSach = sachs[17].MaSach, SoLuongMuon = 1 },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[11].MaPhieuMuon, MaSach = sachs[18].MaSach, SoLuongMuon = 1 },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[12].MaPhieuMuon, MaSach = sachs[0].MaSach, SoLuongMuon = 1 },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[12].MaPhieuMuon, MaSach = sachs[2].MaSach, SoLuongMuon = 1 },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[13].MaPhieuMuon, MaSach = sachs[4].MaSach, SoLuongMuon = 1, NgayTra = DateTime.Now.AddDays(-26), TinhTrangTra = "Tốt" },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[14].MaPhieuMuon, MaSach = sachs[8].MaSach, SoLuongMuon = 1, NgayTra = DateTime.Now.AddDays(-21), TinhTrangTra = "Tốt" },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[16].MaPhieuMuon, MaSach = sachs[10].MaSach, SoLuongMuon = 1, NgayTra = DateTime.Now.AddDays(-28), TinhTrangTra = "Tốt" },
                new ChiTietPhieuMuon { MaPhieuMuon = phieuMuons[17].MaPhieuMuon, MaSach = sachs[11].MaSach, SoLuongMuon = 1, NgayTra = DateTime.Now.AddDays(-33), TinhTrangTra = "Tốt" },
            };
            context.ChiTietPhieuMuons.AddRange(chiTiets);
            context.SaveChanges();
        }
    }
}