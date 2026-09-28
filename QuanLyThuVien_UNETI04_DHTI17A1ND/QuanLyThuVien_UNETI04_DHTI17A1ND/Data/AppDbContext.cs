// Họ và tên: [Nguyễn Mạnh Hùng]
// Mã sinh viên: 23203100075
// Nội dung thực hiện: Cấu hình DbContext, DbSet và quan hệ giữa các Entity

using Microsoft.EntityFrameworkCore;
using QuanLyThuVien_UNETI04_DHTI17A1ND.Models.Entities;

namespace QuanLyThuVien_UNETI04_DHTI17A1ND.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // 6 DbSet tương ứng 6 bảng
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<TheLoai> TheLoais { get; set; }
        public DbSet<Sach> Sachs { get; set; }
        public DbSet<DocGia> DocGias { get; set; }
        public DbSet<PhieuMuon> PhieuMuons { get; set; }
        public DbSet<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique: Tên đăng nhập không được trùng
            modelBuilder.Entity<TaiKhoan>()
                .HasIndex(t => t.TenDangNhap)
                .IsUnique();

            // Unique: Tên thể loại không được trùng
            modelBuilder.Entity<TheLoai>()
                .HasIndex(t => t.TenTheLoai)
                .IsUnique();

            // 1-1: TaiKhoan - DocGia
            modelBuilder.Entity<DocGia>()
                .HasOne(d => d.TaiKhoan)
                .WithOne(t => t.DocGia)
                .HasForeignKey<DocGia>(d => d.MaTaiKhoan)
                .OnDelete(DeleteBehavior.Cascade);

            // 1-n: TheLoai - Sach (không cho xóa thể loại nếu có sách)
            modelBuilder.Entity<Sach>()
                .HasOne(s => s.TheLoai)
                .WithMany(t => t.Sachs)
                .HasForeignKey(s => s.MaTheLoai)
                .OnDelete(DeleteBehavior.Restrict);

            // 1-n: DocGia - PhieuMuon
            modelBuilder.Entity<PhieuMuon>()
                .HasOne(p => p.DocGia)
                .WithMany(d => d.PhieuMuons)
                .HasForeignKey(p => p.MaDocGia)
                .OnDelete(DeleteBehavior.Restrict);

            // 1-n: PhieuMuon - ChiTietPhieuMuon
            modelBuilder.Entity<ChiTietPhieuMuon>()
                .HasOne(c => c.PhieuMuon)
                .WithMany(p => p.ChiTietPhieuMuons)
                .HasForeignKey(c => c.MaPhieuMuon)
                .OnDelete(DeleteBehavior.Cascade);

            // 1-n: Sach - ChiTietPhieuMuon
            modelBuilder.Entity<ChiTietPhieuMuon>()
                .HasOne(c => c.Sach)
                .WithMany(s => s.ChiTietPhieuMuons)
                .HasForeignKey(c => c.MaSach)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}