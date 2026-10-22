IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [TaiKhoan] (
    [MaTaiKhoan] int NOT NULL IDENTITY,
    [TenDangNhap] nvarchar(50) NOT NULL,
    [MatKhau] nvarchar(255) NOT NULL,
    [VaiTro] nvarchar(20) NOT NULL,
    [TrangThai] bit NOT NULL,
    CONSTRAINT [PK_TaiKhoan] PRIMARY KEY ([MaTaiKhoan])
);

CREATE TABLE [TheLoai] (
    [MaTheLoai] int NOT NULL IDENTITY,
    [TenTheLoai] nvarchar(100) NOT NULL,
    [MoTa] nvarchar(500) NULL,
    [TrangThai] bit NOT NULL,
    CONSTRAINT [PK_TheLoai] PRIMARY KEY ([MaTheLoai])
);

CREATE TABLE [DocGia] (
    [MaDocGia] int NOT NULL IDENTITY,
    [MaTaiKhoan] int NOT NULL,
    [HoTen] nvarchar(100) NOT NULL,
    [NgaySinh] datetime2 NULL,
    [GioiTinh] nvarchar(10) NULL,
    [SoDienThoai] nvarchar(15) NULL,
    [Email] nvarchar(100) NULL,
    [DiaChi] nvarchar(255) NULL,
    [NgayDangKy] datetime2 NOT NULL,
    [TrangThai] bit NOT NULL,
    CONSTRAINT [PK_DocGia] PRIMARY KEY ([MaDocGia]),
    CONSTRAINT [FK_DocGia_TaiKhoan_MaTaiKhoan] FOREIGN KEY ([MaTaiKhoan]) REFERENCES [TaiKhoan] ([MaTaiKhoan]) ON DELETE CASCADE
);

CREATE TABLE [Sach] (
    [MaSach] int NOT NULL IDENTITY,
    [TenSach] nvarchar(200) NOT NULL,
    [MaTheLoai] int NOT NULL,
    [TacGia] nvarchar(150) NOT NULL,
    [NhaXuatBan] nvarchar(150) NULL,
    [NamXuatBan] int NULL,
    [SoLuong] int NOT NULL,
    [SoLuongCon] int NOT NULL,
    [GiaSach] decimal(18,2) NOT NULL,
    [MoTa] nvarchar(1000) NULL,
    [TrangThai] int NOT NULL,
    CONSTRAINT [PK_Sach] PRIMARY KEY ([MaSach]),
    CONSTRAINT [FK_Sach_TheLoai_MaTheLoai] FOREIGN KEY ([MaTheLoai]) REFERENCES [TheLoai] ([MaTheLoai]) ON DELETE NO ACTION
);

CREATE TABLE [PhieuMuon] (
    [MaPhieuMuon] int NOT NULL IDENTITY,
    [MaDocGia] int NOT NULL,
    [NgayMuon] datetime2 NOT NULL,
    [HanTra] datetime2 NOT NULL,
    [TrangThai] int NOT NULL,
    CONSTRAINT [PK_PhieuMuon] PRIMARY KEY ([MaPhieuMuon]),
    CONSTRAINT [FK_PhieuMuon_DocGia_MaDocGia] FOREIGN KEY ([MaDocGia]) REFERENCES [DocGia] ([MaDocGia]) ON DELETE NO ACTION
);

CREATE TABLE [ChiTietPhieuMuon] (
    [MaChiTiet] int NOT NULL IDENTITY,
    [MaPhieuMuon] int NOT NULL,
    [MaSach] int NOT NULL,
    [SoLuongMuon] int NOT NULL,
    [NgayTra] datetime2 NULL,
    [TinhTrangTra] nvarchar(255) NULL,
    CONSTRAINT [PK_ChiTietPhieuMuon] PRIMARY KEY ([MaChiTiet]),
    CONSTRAINT [FK_ChiTietPhieuMuon_PhieuMuon_MaPhieuMuon] FOREIGN KEY ([MaPhieuMuon]) REFERENCES [PhieuMuon] ([MaPhieuMuon]) ON DELETE CASCADE,
    CONSTRAINT [FK_ChiTietPhieuMuon_Sach_MaSach] FOREIGN KEY ([MaSach]) REFERENCES [Sach] ([MaSach]) ON DELETE NO ACTION
);

CREATE INDEX [IX_ChiTietPhieuMuon_MaPhieuMuon] ON [ChiTietPhieuMuon] ([MaPhieuMuon]);

CREATE INDEX [IX_ChiTietPhieuMuon_MaSach] ON [ChiTietPhieuMuon] ([MaSach]);

CREATE UNIQUE INDEX [IX_DocGia_MaTaiKhoan] ON [DocGia] ([MaTaiKhoan]);

CREATE INDEX [IX_PhieuMuon_MaDocGia] ON [PhieuMuon] ([MaDocGia]);

CREATE INDEX [IX_Sach_MaTheLoai] ON [Sach] ([MaTheLoai]);

CREATE UNIQUE INDEX [IX_TaiKhoan_TenDangNhap] ON [TaiKhoan] ([TenDangNhap]);

CREATE UNIQUE INDEX [IX_TheLoai_TenTheLoai] ON [TheLoai] ([TenTheLoai]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260928061024_InitialCreate', N'10.0.12');

COMMIT;
GO

