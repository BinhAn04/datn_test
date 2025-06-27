using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    public partial class duan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chatLieus",
                columns: table => new
                {
                    IdChatLieu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenChatLieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KichHoat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatLieus", x => x.IdChatLieu);
                });

            migrationBuilder.CreateTable(
                name: "chuVu",
                columns: table => new
                {
                    IdChucVu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chuVu", x => x.IdChucVu);
                });

            migrationBuilder.CreateTable(
                name: "danhMuc",
                columns: table => new
                {
                    IdDanhMuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KichHoat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_danhMuc", x => x.IdDanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "deGiay",
                columns: table => new
                {
                    IdDeGiay = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenDeGiay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KichHoat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deGiay", x => x.IdDeGiay);
                });

            migrationBuilder.CreateTable(
                name: "diaChiHoaDons",
                columns: table => new
                {
                    IdDiaChiHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    WardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChiMacDinh = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diaChiHoaDons", x => x.IdDiaChiHoaDon);
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_districts", x => x.DistrictId);
                });

            migrationBuilder.CreateTable(
                name: "khachHangs",
                columns: table => new
                {
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AuthProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KichHoat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khachHangs", x => x.IdKhachHang);
                });

            migrationBuilder.CreateTable(
                name: "kichCos",
                columns: table => new
                {
                    IdKichCo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenKichCo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KichHoat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kichCos", x => x.IdKichCo);
                });

            migrationBuilder.CreateTable(
                name: "kieuDangs",
                columns: table => new
                {
                    IdKieuDang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenKieuDang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KichHoat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kieuDangs", x => x.IdKieuDang);
                });

            migrationBuilder.CreateTable(
                name: "mauSacs",
                columns: table => new
                {
                    IdMauSac = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenMauSac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KichHoat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mauSacs", x => x.IdMauSac);
                });

            migrationBuilder.CreateTable(
                name: "promotions",
                columns: table => new
                {
                    IdPromotion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenPromotion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhanTramGiam = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotions", x => x.IdPromotion);
                });

            migrationBuilder.CreateTable(
                name: "provinces",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provinces", x => x.ProvinceId);
                });

            migrationBuilder.CreateTable(
                name: "thuongHieus",
                columns: table => new
                {
                    IdThuongHieu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenThuongHieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KichHoat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thuongHieus", x => x.IdThuongHieu);
                });

            migrationBuilder.CreateTable(
                name: "vouchers",
                columns: table => new
                {
                    VoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaVoucher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTaVoucher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiGiamGia = table.Column<int>(type: "int", nullable: false),
                    GiaTriGiam = table.Column<int>(type: "int", nullable: false),
                    GiaTriDonHangToiThieu = table.Column<int>(type: "int", nullable: true),
                    SoTienToiDa = table.Column<int>(type: "int", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiUpdate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vouchers", x => x.VoucherId);
                });

            migrationBuilder.CreateTable(
                name: "wards",
                columns: table => new
                {
                    WardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wards", x => x.WardId);
                });

            migrationBuilder.CreateTable(
                name: "nhanViens",
                columns: table => new
                {
                    IdNhanVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KichHoat = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    IdchucVu = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nhanViens", x => x.IdNhanVien);
                    table.ForeignKey(
                        name: "FK_nhanViens_chuVu_IdchucVu",
                        column: x => x.IdchucVu,
                        principalTable: "chuVu",
                        principalColumn: "IdChucVu");
                });

            migrationBuilder.CreateTable(
                name: "diaChi",
                columns: table => new
                {
                    IdDiaChi = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    WardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChiMacDinh = table.Column<bool>(type: "bit", nullable: false),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diaChi", x => x.IdDiaChi);
                    table.ForeignKey(
                        name: "FK_diaChi_khachHangs_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "khachHangs",
                        principalColumn: "IdKhachHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gioHang",
                columns: table => new
                {
                    IdGioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gioHang", x => x.IdGioHang);
                    table.ForeignKey(
                        name: "FK_gioHang_khachHangs_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "khachHangs",
                        principalColumn: "IdKhachHang");
                });

            migrationBuilder.CreateTable(
                name: "sanPhams",
                columns: table => new
                {
                    IdSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KichHoat = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sale = table.Column<double>(type: "float", nullable: false),
                    IdChatLieu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdKieuDang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdThuongHieu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdDanhMuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdDeGiay = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanPhams", x => x.IdSanPham);
                    table.ForeignKey(
                        name: "FK_sanPhams_chatLieus_IdChatLieu",
                        column: x => x.IdChatLieu,
                        principalTable: "chatLieus",
                        principalColumn: "IdChatLieu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sanPhams_danhMuc_IdDanhMuc",
                        column: x => x.IdDanhMuc,
                        principalTable: "danhMuc",
                        principalColumn: "IdDanhMuc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sanPhams_deGiay_IdDeGiay",
                        column: x => x.IdDeGiay,
                        principalTable: "deGiay",
                        principalColumn: "IdDeGiay",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sanPhams_kieuDangs_IdKieuDang",
                        column: x => x.IdKieuDang,
                        principalTable: "kieuDangs",
                        principalColumn: "IdKieuDang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sanPhams_thuongHieus_IdThuongHieu",
                        column: x => x.IdThuongHieu,
                        principalTable: "thuongHieus",
                        principalColumn: "IdThuongHieu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hoaDons",
                columns: table => new
                {
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaDon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoaiNguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiGiaoHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiHoaDon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TienShip = table.Column<double>(type: "float", nullable: true),
                    TienGiam = table.Column<double>(type: "float", nullable: true),
                    TongTienDonHang = table.Column<double>(type: "float", nullable: false),
                    TongTienHoaDon = table.Column<double>(type: "float", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KichHoat = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdNhanVien = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdDiaChiHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoaDons", x => x.IdHoaDon);
                    table.ForeignKey(
                        name: "FK_hoaDons_diaChiHoaDons_IdDiaChiHoaDon",
                        column: x => x.IdDiaChiHoaDon,
                        principalTable: "diaChiHoaDons",
                        principalColumn: "IdDiaChiHoaDon");
                    table.ForeignKey(
                        name: "FK_hoaDons_khachHangs_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "khachHangs",
                        principalColumn: "IdKhachHang");
                    table.ForeignKey(
                        name: "FK_hoaDons_nhanViens_IdNhanVien",
                        column: x => x.IdNhanVien,
                        principalTable: "nhanViens",
                        principalColumn: "IdNhanVien");
                });

            migrationBuilder.CreateTable(
                name: "sanPhamChiTiets",
                columns: table => new
                {
                    IdSanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    MaSp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuong = table.Column<double>(type: "float", nullable: false),
                    GiaGiam = table.Column<double>(type: "float", nullable: true),
                    CoHienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XuatXu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KichHoat = table.Column<int>(type: "int", nullable: false),
                    IdSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanPhamChiTiets", x => x.IdSanPhamChiTiet);
                    table.ForeignKey(
                        name: "FK_sanPhamChiTiets_sanPhams_IdSanPham",
                        column: x => x.IdSanPham,
                        principalTable: "sanPhams",
                        principalColumn: "IdSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lichSuHoaDons",
                columns: table => new
                {
                    IdLichSuHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThaoTac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiThaoTac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNhanVien = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lichSuHoaDons", x => x.IdLichSuHoaDon);
                    table.ForeignKey(
                        name: "FK_lichSuHoaDons_hoaDons_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "hoaDons",
                        principalColumn: "IdHoaDon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lichSuHoaDons_nhanViens_IdNhanVien",
                        column: x => x.IdNhanVien,
                        principalTable: "nhanViens",
                        principalColumn: "IdNhanVien",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LichSuSuDungVouchers",
                columns: table => new
                {
                    IdVoucher = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdLichSuVoucher = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NgaySuDungVoucher = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThaiVoucher = table.Column<int>(type: "int", nullable: false),
                    HoaDonIdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuSuDungVouchers", x => new { x.IdVoucher, x.IdKhachHang });
                    table.ForeignKey(
                        name: "FK_LichSuSuDungVouchers_hoaDons_HoaDonIdHoaDon",
                        column: x => x.HoaDonIdHoaDon,
                        principalTable: "hoaDons",
                        principalColumn: "IdHoaDon");
                    table.ForeignKey(
                        name: "FK_LichSuSuDungVouchers_khachHangs_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "khachHangs",
                        principalColumn: "IdKhachHang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LichSuSuDungVouchers_vouchers_IdVoucher",
                        column: x => x.IdVoucher,
                        principalTable: "vouchers",
                        principalColumn: "VoucherId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lichSuThanhToans",
                columns: table => new
                {
                    IdLichSuThanhToan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoTien = table.Column<double>(type: "float", nullable: false),
                    TienThua = table.Column<double>(type: "float", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaiGiaoDich = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pttt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiThaoTac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNhanVien = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lichSuThanhToans", x => x.IdLichSuThanhToan);
                    table.ForeignKey(
                        name: "FK_lichSuThanhToans_hoaDons_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "hoaDons",
                        principalColumn: "IdHoaDon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lichSuThanhToans_nhanViens_IdNhanVien",
                        column: x => x.IdNhanVien,
                        principalTable: "nhanViens",
                        principalColumn: "IdNhanVien",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gioHangChiTiets",
                columns: table => new
                {
                    IdGioHangChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false),
                    SoLuong = table.Column<double>(type: "float", nullable: false),
                    TongTien = table.Column<double>(type: "float", nullable: false),
                    KichHoat = table.Column<int>(type: "int", nullable: false),
                    IdGioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gioHangChiTiets", x => x.IdGioHangChiTiet);
                    table.ForeignKey(
                        name: "FK_gioHangChiTiets_gioHang_IdGioHang",
                        column: x => x.IdGioHang,
                        principalTable: "gioHang",
                        principalColumn: "IdGioHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gioHangChiTiets_sanPhamChiTiets_IdSanPhamChiTiet",
                        column: x => x.IdSanPhamChiTiet,
                        principalTable: "sanPhamChiTiets",
                        principalColumn: "IdSanPhamChiTiet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hinhAnh",
                columns: table => new
                {
                    IdHinhAnh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHinhAnh = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LoaiFileHinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    IdSanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hinhAnh", x => x.IdHinhAnh);
                    table.ForeignKey(
                        name: "FK_hinhAnh_sanPhamChiTiets_IdSanPhamChiTiet",
                        column: x => x.IdSanPhamChiTiet,
                        principalTable: "sanPhamChiTiets",
                        principalColumn: "IdSanPhamChiTiet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hoaDonChiTiets",
                columns: table => new
                {
                    IdHoaDonChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false),
                    TienGiam = table.Column<double>(type: "float", nullable: false),
                    SoLuong = table.Column<double>(type: "float", nullable: false),
                    TongTien = table.Column<double>(type: "float", nullable: false),
                    KichHoat = table.Column<double>(type: "float", nullable: false),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoaDonChiTiets", x => x.IdHoaDonChiTiet);
                    table.ForeignKey(
                        name: "FK_hoaDonChiTiets_hoaDons_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "hoaDons",
                        principalColumn: "IdHoaDon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hoaDonChiTiets_sanPhamChiTiets_IdSanPhamChiTiet",
                        column: x => x.IdSanPhamChiTiet,
                        principalTable: "sanPhamChiTiets",
                        principalColumn: "IdSanPhamChiTiet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "promotionSanPhamChiTiets",
                columns: table => new
                {
                    IdPromotion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotionSanPhamChiTiets", x => new { x.IdPromotion, x.IdSanPhamChiTiet });
                    table.ForeignKey(
                        name: "FK_promotionSanPhamChiTiets_promotions_IdPromotion",
                        column: x => x.IdPromotion,
                        principalTable: "promotions",
                        principalColumn: "IdPromotion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_promotionSanPhamChiTiets_sanPhamChiTiets_IdSanPhamChiTiet",
                        column: x => x.IdSanPhamChiTiet,
                        principalTable: "sanPhamChiTiets",
                        principalColumn: "IdSanPhamChiTiet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sanPhamChiTietKichCos",
                columns: table => new
                {
                    IdSanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdKichCo = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanPhamChiTietKichCos", x => new { x.IdSanPhamChiTiet, x.IdKichCo });
                    table.ForeignKey(
                        name: "FK_sanPhamChiTietKichCos_kichCos_IdKichCo",
                        column: x => x.IdKichCo,
                        principalTable: "kichCos",
                        principalColumn: "IdKichCo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sanPhamChiTietKichCos_sanPhamChiTiets_IdSanPhamChiTiet",
                        column: x => x.IdSanPhamChiTiet,
                        principalTable: "sanPhamChiTiets",
                        principalColumn: "IdSanPhamChiTiet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sanPhamChiTietMausacs",
                columns: table => new
                {
                    IdSanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMauSac = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanPhamChiTietMausacs", x => new { x.IdSanPhamChiTiet, x.IdMauSac });
                    table.ForeignKey(
                        name: "FK_sanPhamChiTietMausacs_mauSacs_IdMauSac",
                        column: x => x.IdMauSac,
                        principalTable: "mauSacs",
                        principalColumn: "IdMauSac",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sanPhamChiTietMausacs_sanPhamChiTiets_IdSanPhamChiTiet",
                        column: x => x.IdSanPhamChiTiet,
                        principalTable: "sanPhamChiTiets",
                        principalColumn: "IdSanPhamChiTiet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "chatLieus",
                columns: new[] { "IdChatLieu", "KichHoat", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenChatLieu" },
                values: new object[,]
                {
                    { new Guid("25633aae-993c-48eb-a59a-6d264dc044a4"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(416), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Da Lộn" },
                    { new Guid("275d3617-06b8-4cfc-8333-bc5568b62ec2"), 1, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Da thật" },
                    { new Guid("28630cff-113f-4a47-a032-13b5029eb1d7"), 1, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Vải Dệt" },
                    { new Guid("662254f3-4a08-4bc6-a733-2a75b4cd8bcc"), 1, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Vải Cotton" },
                    { new Guid("8c298557-dd0f-497a-902a-4e09825d54ff"), 1, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Vải Polyester" },
                    { new Guid("db442b2e-a77e-4dd2-8a89-9cb94441e5b8"), 1, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Da Tự Nhiên" }
                });

            migrationBuilder.InsertData(
                table: "chuVu",
                columns: new[] { "IdChucVu", "Code", "TenChucVu" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "QL", "Quản lý" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "NV", "Nhân viên" }
                });

            migrationBuilder.InsertData(
                table: "danhMuc",
                columns: new[] { "IdDanhMuc", "KichHoat", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenDanhMuc" },
                values: new object[,]
                {
                    { new Guid("3d186db5-6fe9-4e3b-8ee9-8be3ce7a80e1"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(455), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Giày Cổ Thấp" },
                    { new Guid("4558f74f-e441-4054-a5ed-1cedb8139c3f"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(457), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Giày Cổ Cao" },
                    { new Guid("4cc63561-ccfd-41c4-a0bc-7ecd13ef4a97"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(452), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Giày Thể Thao" },
                    { new Guid("8ea52b5c-8818-4db9-978c-82940ba29240"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(453), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Giày Da" },
                    { new Guid("fab0f9ab-408a-4830-a8a8-88fa2ee6d4d8"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(456), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Giày Cổ Trung" }
                });

            migrationBuilder.InsertData(
                table: "deGiay",
                columns: new[] { "IdDeGiay", "KichHoat", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenDeGiay" },
                values: new object[,]
                {
                    { new Guid("0bb7b0f3-b832-4bf0-a0e6-01145638c786"), 1, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Đế cao su" },
                    { new Guid("22c84cca-bf3e-4fed-a277-d80efd6b0ae8"), 1, new DateTime(2023, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Đế Eva" },
                    { new Guid("4520670f-5b10-4f9f-9ac3-b15bb4a95bb6"), 1, new DateTime(2023, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Đế PVC" },
                    { new Guid("5a149b40-112a-417b-8b9f-e43d0952de47"), 1, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Đế nhựa" },
                    { new Guid("63936b61-a9c7-4f03-9398-43b1fe21894d"), 1, new DateTime(2023, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Đế BPU" },
                    { new Guid("80f8acf3-46ce-4d2a-82d2-0947779eb411"), 1, new DateTime(2023, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Đế Chunky" },
                    { new Guid("c8ce982a-6496-42f6-b821-026d20a3d55d"), 1, new DateTime(2023, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Đế Giày Lười" },
                    { new Guid("e1c5ff65-db28-493c-8bd4-a4bfe2d5dbaf"), 1, new DateTime(2023, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Đế Chisty" },
                    { new Guid("e9f38249-9faa-48af-a886-889803e10dc1"), 1, new DateTime(2023, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Đế vải" },
                    { new Guid("fd1dd9b8-cd51-4da2-8eac-d0c27c7350d8"), 1, new DateTime(2023, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Đế Cao Su Lưu Hóa" }
                });

            migrationBuilder.InsertData(
                table: "khachHangs",
                columns: new[] { "IdKhachHang", "AuthProvider", "Email", "HoTen", "KichHoat", "MatKhau", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "SoDienThoai" },
                values: new object[,]
                {
                    { new Guid("16dd1e93-b0b0-4171-b12c-f157709941b2"), "Google", "trinhthiquyen@gmail.com", "Trịnh Thị Quyên", 1, "1", new DateTime(2024, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(638), new DateTime(2023, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(637), "System", "System", "0898765432" },
                    { new Guid("1a3a55bc-1e88-4876-b03e-9ee53721babc"), "Local", "hoanganhgialai@gmail.com", "Hoàng Anh Gia Lai", 1, "1", new DateTime(2024, 12, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(620), new DateTime(2024, 10, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(620), "System", "System", "0965432109" },
                    { new Guid("247b46b3-aa84-4ed2-8f90-b81bcf6ab425"), "Google", "tranthibe@gmail.com", "Trần Thị Bé", 1, "1", new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "0987654321" },
                    { new Guid("270660ad-2a20-4252-bbd5-ca4b4d4a95be"), "Local", "lethimai@gmail.com", "Lê Thị Mai", 1, "1", new DateTime(2024, 7, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(629), new DateTime(2024, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(628), "Admin", "System", "0387654321" },
                    { new Guid("482b039b-3050-4413-864d-5cdb7af88f1f"), "Google", "phanthiha@gmail.com", "Phan Thị Hà", 1, "1", new DateTime(2024, 9, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(622), new DateTime(2024, 8, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(622), "Admin", "Admin", "0845678901" },
                    { new Guid("4a7d1d7f-9ed0-4447-9266-9c27076a7e91"), "Facebook", "tranvanly@yahoo.com", "Trần Văn Lý", 1, "1", new DateTime(2024, 11, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(627), new DateTime(2024, 5, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(627), "Admin", "Admin", "0901234567" },
                    { new Guid("5256cd9c-bce0-4c2b-9a75-aebd368f2189"), "Google", "nguyenthikhanh@gmail.com", "Nguyễn Thị Khánh", 1, "1", new DateTime(2025, 1, 6, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(625), new DateTime(2025, 1, 1, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(625), "Admin", "System", "0834567890" },
                    { new Guid("5284b47d-5b10-4e17-b726-9ed8b5b4c7f9"), "Facebook", "ngovanf@gmail.com", "Ngô Văn Phát", 0, "1", new DateTime(2024, 7, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(619), new DateTime(2024, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(618), "Admin", "Admin", "0376543210" },
                    { new Guid("5ce4315f-466b-485d-bfdc-6d6bfbae0171"), "Local", "nguyenvanvinh@gmail.com", "Nguyễn Văn Vinh", 1, "1", new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "0912345678" },
                    { new Guid("63861704-fdbe-4569-bdd4-2052ee5d132f"), "Facebook", "vuvanich@gmail.com", "Vũ Văn Ich", 0, "Fb@12345", new DateTime(2025, 1, 11, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(624), new DateTime(2024, 12, 27, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(623), "Admin", "Admin", "0856781234" },
                    { new Guid("a331d27c-ff7a-4a02-86eb-071669c8d63b"), "Facebook", "lyvanro@gmail.com", "Lý Văn Rô", 0, "1", new DateTime(2025, 1, 6, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(640), new DateTime(2024, 8, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(639), "Admin", "Admin", "0945678901" },
                    { new Guid("a8ebd3ac-7543-4614-badc-7e12e14eded6"), "Local", "nguyenhoangp@gmail.com", "Nguyễn Hoàng Phong", 1, "1", new DateTime(2024, 12, 27, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(636), new DateTime(2024, 11, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(635), "Admin", "Admin", "0923456789" },
                    { new Guid("b70e218a-de2a-466a-bd9d-f22524087b4a"), "Facebook", "phamvancanh@gmail.com", "Phạm Văn Cảnh", 0, "1", new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(608), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(607), "Admin", "Admin", "0971123456" },
                    { new Guid("bed813e9-5f71-47a7-a2fa-c78efe51ffc5"), "Google", "dangvanem@gmail.com", "Đặng Văn Em", 1, "1", new DateTime(2025, 1, 15, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(616), new DateTime(2024, 11, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(615), "Admin", "Admin", "0398765432" },
                    { new Guid("ce8f7d4c-2e01-44dd-b583-c653edcc674a"), "Facebook", "phamvannam@outlook.com", "Phạm Văn Nam", 0, "1", new DateTime(2024, 12, 17, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(631), new DateTime(2024, 10, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(631), "Admin", "Admin", "0919876543" },
                    { new Guid("eb1230c5-6835-40b2-aa99-1886c53f56ba"), "Local", "lethidau@gmail.com", "Lê Thị Dậu", 1, "1", new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(613), new DateTime(2025, 1, 6, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(609), "Admin", "System", "0356789123" },
                    { new Guid("f1a6d111-6a95-4d05-b7e7-8646f3c75edf"), "Google", "dminhai@gmail.com", "Đỗ Minh Hải", 1, "1", new DateTime(2024, 11, 27, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(634), new DateTime(2024, 10, 8, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(634), "System", "System", "0867891234" }
                });

            migrationBuilder.InsertData(
                table: "kichCos",
                columns: new[] { "IdKichCo", "KichHoat", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenKichCo" },
                values: new object[,]
                {
                    { new Guid("10c74277-b317-4a8c-a6ed-398ff44bc4ba"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(715), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(716), "Admin", "Admin", "36" },
                    { new Guid("12421dc5-149a-4db2-aa18-672ffbf2cc6b"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(725), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(725), "Admin", "Admin", "42" }
                });

            migrationBuilder.InsertData(
                table: "kichCos",
                columns: new[] { "IdKichCo", "KichHoat", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenKichCo" },
                values: new object[,]
                {
                    { new Guid("239ebe61-231f-48f5-8a89-de5ae59e18a5"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(720), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(720), "Admin", "Admin", "39" },
                    { new Guid("2cad44f5-a20a-42b4-9dfb-77dfbb77f14d"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(727), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(727), "Admin", "Admin", "43" },
                    { new Guid("37c2cf3b-dc3f-48a3-b572-9013dd235bbd"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(717), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(717), "Admin", "Admin", "37" },
                    { new Guid("5cdc586d-26b9-4d75-8099-1d0c1a2598dd"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(722), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(722), "Admin", "Admin", "40" },
                    { new Guid("75e7e08d-276b-49f5-8b02-723fab032baa"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(729), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(730), "Admin", "Admin", "45" },
                    { new Guid("9d0d16d5-a169-4a0c-b609-cdc4e3619629"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(728), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(728), "Admin", "Admin", "44" },
                    { new Guid("bc7bf6a4-27f1-4bd1-960a-690abb24401c"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(719), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(719), "Admin", "Admin", "38" },
                    { new Guid("c670251c-c7b7-4d37-b3c3-7572ce8bf4be"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(712), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(713), "Admin", "Admin", "35" },
                    { new Guid("cde63b05-35a2-4f1e-9d31-4f32864fd0cf"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(724), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(724), "Admin", "Admin", "41" }
                });

            migrationBuilder.InsertData(
                table: "kieuDangs",
                columns: new[] { "IdKieuDang", "KichHoat", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenKieuDang" },
                values: new object[,]
                {
                    { new Guid("983fea1c-b836-47d5-81af-c11ebe83e867"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(756), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(756), "Admin", "Admin", "Hiện Đại" },
                    { new Guid("a5d4ecf9-4a03-4505-b159-b47f8b741c9c"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(752), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(752), "Admin", "Admin", "Thể Thao" },
                    { new Guid("bd9f5459-0f06-4e69-aa1e-f2780225a5d7"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(754), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(754), "Admin", "Admin", "Cổ Điển" }
                });

            migrationBuilder.InsertData(
                table: "mauSacs",
                columns: new[] { "IdMauSac", "KichHoat", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenMauSac" },
                values: new object[,]
                {
                    { new Guid("17f49e26-4e91-41a4-b1de-56489cab6b4e"), 1, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Đỏ" },
                    { new Guid("215ecfd1-1ccd-4d83-ae5a-69a9eafd6ccd"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(800), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(800), "Admin", "Admin", "Cam" },
                    { new Guid("5445f861-1fb3-43be-9e2f-2863f318574c"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(798), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(799), "Admin", "Admin", "Hồng" },
                    { new Guid("5e16b052-f773-40e1-b613-364f34f6fcf4"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(797), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(797), "Admin", "Admin", "Nâu" },
                    { new Guid("77de94db-7d42-42c7-8047-956e5b8a9ac9"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(802), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(802), "Admin", "Admin", "Kim loại" },
                    { new Guid("83f91963-297c-4b65-9946-e64f39ba99e8"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(794), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(794), "Admin", "Admin", "Xám" },
                    { new Guid("a15c8839-4c47-4b59-a983-1bb9b0788be8"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(804), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(804), "Admin", "Admin", "Phát quang" },
                    { new Guid("ad4875ec-d18f-4d76-a3ef-60f4b434f31d"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(795), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(795), "Admin", "Admin", "Xanh Navy" },
                    { new Guid("d0a43e12-3ffd-4a41-99d9-5403fdf02f47"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(791), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(792), "Admin", "Admin", "Trắng" },
                    { new Guid("db3f3409-a046-4aff-a764-e6c44d2b0498"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(789), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(789), "Admin", "Admin", "Đen" },
                    { new Guid("e92ef8e2-0978-48ec-8bcb-8c40f7d84cbe"), 1, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "Xanh lá" },
                    { new Guid("f10ce0c7-b9ce-4605-aa05-6440ab7f0fa1"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(786), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(786), "Admin", "Admin", "Xanh dương" }
                });

            migrationBuilder.InsertData(
                table: "thuongHieus",
                columns: new[] { "IdThuongHieu", "KichHoat", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenThuongHieu" },
                values: new object[,]
                {
                    { new Guid("12a4d1ba-7884-4014-830a-d5ebb702112b"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(850), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(850), "Admin", "Admin", "Dior" },
                    { new Guid("18ebfcd0-5607-4867-9ab3-f806adeb2d96"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(849), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(848), "Admin", "Admin", "Balenciaga" },
                    { new Guid("2e2dbbbc-d4e9-4788-bff6-12a67f1593b5"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(845), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(844), "Admin", "Admin", "Fila" },
                    { new Guid("3eed1b9b-5468-46b9-a6e7-932e586fb63c"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(841), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(841), "Admin", "Admin", "Converse" },
                    { new Guid("5c6e73af-241e-421e-9595-c6a252d78660"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(847), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(846), "Admin", "Admin", "Gucci" },
                    { new Guid("64ec2704-c9eb-478d-97c9-d430cef84e67"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(851), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(851), "Admin", "Admin", "Shondo" },
                    { new Guid("6af01f07-be11-4dd1-ae29-0dceb4a979cd"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(848), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(847), "Admin", "Admin", "Louis Vuitton" },
                    { new Guid("6e3e3daf-fd1d-4b5a-9fc1-7e809d514e52"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(843), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(843), "Admin", "Admin", "New Balance" },
                    { new Guid("77bfa189-aada-4f15-af87-8767e9e244bf"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(839), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(839), "Admin", "Admin", "Puma" },
                    { new Guid("88556209-5387-46b6-a20d-6271ee3869b2"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(846), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(845), "Admin", "Admin", "MLB" },
                    { new Guid("991f93c3-f046-4585-b985-fd89412adfb7"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(842), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(842), "Admin", "Admin", "Vans" },
                    { new Guid("c2b70604-8bc9-4957-9058-7646ffb58c8c"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(836), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(835), "Admin", "Admin", "Nike" },
                    { new Guid("e5f53644-a45c-4378-9ed0-216d530a8dd2"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(840), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(840), "Admin", "Admin", "Reebok" },
                    { new Guid("e843f1b4-d769-4a10-8cc9-47615b7a7c4c"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(838), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(837), "Admin", "Admin", "Adidas" },
                    { new Guid("eb088778-5127-4fa1-8ee3-fb287e480678"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(853), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(853), "Admin", "Admin", "Ananas" },
                    { new Guid("fb9b6fa4-81de-41fa-97f4-b69c54e229b4"), 1, new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(852), new DateTime(2025, 1, 16, 9, 32, 38, 213, DateTimeKind.Local).AddTicks(852), "Admin", "Admin", "Biti’s" }
                });

            migrationBuilder.InsertData(
                table: "diaChi",
                columns: new[] { "IdDiaChi", "DiaChiMacDinh", "DistrictId", "DistrictName", "HoTen", "IdKhachHang", "MoTa", "ProvinceId", "ProvinceName", "SoDienThoai", "WardId", "WardName" },
                values: new object[,]
                {
                    { new Guid("244e3bb8-c6a0-4663-b1ba-0122b660d6b4"), true, 201, "Quận Bình Thạnh", "Trần Thị Bé", new Guid("247b46b3-aa84-4ed2-8f90-b81bcf6ab425"), "Tòa nhà Landmark 81, Bình Thạnh", 2, "Hồ Chí Minh", "0987654321", "00201", "Phường 22" },
                    { new Guid("4a4eabd8-030a-476f-a0be-ce218d865245"), true, 1001, "Huyện Tây Hòa", "Hoàng Anh J", new Guid("5256cd9c-bce0-4c2b-9a75-aebd368f2189"), "Thôn Hồng Thái, Xã Đông Hòa", 10, "Phú Yên", "0976543210", "01001", "Xã Đông Hòa" },
                    { new Guid("64ba9133-cbc6-42ed-a64e-293d2d952623"), false, 301, "Quận Hải Châu", "Phạm Văn Cảnh", new Guid("b70e218a-de2a-466a-bd9d-f22524087b4a"), "Số 5, Đường Trần Phú", 3, "Đà Nẵng", "0971123456", "00301", "Phường Thạch Thang" },
                    { new Guid("90f9ed50-b60f-4928-9c3e-fc4a8b45b531"), false, 701, "Quận Ninh Kiều", "Hoàng Anh Gia Lai", new Guid("1a3a55bc-1e88-4876-b03e-9ee53721babc"), "Khu dân cư Bình Minh", 7, "Cần Thơ", "0965432109", "00701", "Phường Tân An" },
                    { new Guid("9df6323d-caeb-40a2-ba9b-98ea2be3f7ef"), true, 601, "Thành phố Huế", "Ngô Văn Phát", new Guid("5284b47d-5b10-4e17-b726-9ed8b5b4c7f9"), "Số 20, Đường Nguyễn Huệ", 6, "Thừa Thiên Huế", "0376543210", "00601", "Phường Vĩnh Ninh" },
                    { new Guid("b312037b-aac1-4888-8ad7-aaec8f63ac10"), false, 501, "Huyện Long Thành", "Đặng Văn Em", new Guid("bed813e9-5f71-47a7-a2fa-c78efe51ffc5"), "Ấp An Hòa, Xã An Bình", 5, "Đồng Nai", "0398765432", "00501", "Xã An Bình" },
                    { new Guid("b623ca6d-65dc-4d37-a135-daced64a22c2"), true, 401, "Thị xã Thuận An", "Lê Thị Dậu", new Guid("eb1230c5-6835-40b2-aa99-1886c53f56ba"), "Khu phố 1, Phường Vĩnh Phú", 4, "Bình Dương", "0356789123", "00401", "Phường Vĩnh Phú" },
                    { new Guid("b8cbde83-b6a2-4f8c-bef5-d578c4a9f3f0"), false, 901, "Thành phố Hạ Long", "Vũ Văn Ich", new Guid("63861704-fdbe-4569-bdd4-2052ee5d132f"), "Số 30, Đường Lê Lợi", 9, "Quảng Ninh", "0856781234", "00901", "Phường Bãi Cháy" },
                    { new Guid("ce2815c9-5606-44e0-889c-818183ecb80b"), true, 801, "Thành phố Nha Trang", "Phan Thị Hà", new Guid("482b039b-3050-4413-864d-5cdb7af88f1f"), "Số 15, Đường Hoàng Diệu", 8, "Khánh Hòa", "0845678901", "00801", "Phường Phước Long" },
                    { new Guid("d7999d0d-50d4-4719-8d75-8908793e6a18"), true, 101, "Quận Đống Đa", "Nguyễn Văn Vinh", new Guid("5ce4315f-466b-485d-bfdc-6d6bfbae0171"), "Số 10, Ngõ 15, Đường Láng", 1, "Hà Nội", "0912345678", "00101", "Phường Láng Thượng" }
                });

            migrationBuilder.InsertData(
                table: "nhanViens",
                columns: new[] { "IdNhanVien", "AnhNhanVien", "AuthProvider", "DiaChi", "Email", "IdchucVu", "KichHoat", "MatKhau", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "SoDienThoai", "TenNhanVien", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555555"), "1.png", "Local", "Hà Nội", "hieupdph40428@fpt.edu.vn", new Guid("11111111-1111-1111-1111-111111111111"), 1, "1", new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "0976819974", "Phạm Đức Hiếu", 1 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "1.png", "Local", "Đà Nẵng", "tranha10112004@gmail.com", new Guid("11111111-1111-1111-1111-111111111111"), 1, "1", new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "0969293263", "Trần Ngọc Hà", 1 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "1.png", "Local", "Đà Nẵng", "longkhph35837@fpt.edu.vn", new Guid("22222222-2222-2222-2222-222222222222"), 1, "1", new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "0377804800", "Kim Hoàng Long", 1 },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "1.png", "Local", "Đà Nẵng", "namdtph39830@fpt.edu.vn", new Guid("22222222-2222-2222-2222-222222222222"), 1, "1", new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "0855896668", "Đào Thành Nam", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_diaChi_IdKhachHang",
                table: "diaChi",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_gioHang_IdKhachHang",
                table: "gioHang",
                column: "IdKhachHang",
                unique: true,
                filter: "[IdKhachHang] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_gioHangChiTiets_IdGioHang",
                table: "gioHangChiTiets",
                column: "IdGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_gioHangChiTiets_IdSanPhamChiTiet",
                table: "gioHangChiTiets",
                column: "IdSanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_hinhAnh_IdSanPhamChiTiet",
                table: "hinhAnh",
                column: "IdSanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonChiTiets_IdHoaDon",
                table: "hoaDonChiTiets",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonChiTiets_IdSanPhamChiTiet",
                table: "hoaDonChiTiets",
                column: "IdSanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_IdDiaChiHoaDon",
                table: "hoaDons",
                column: "IdDiaChiHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_IdKhachHang",
                table: "hoaDons",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_IdNhanVien",
                table: "hoaDons",
                column: "IdNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_lichSuHoaDons_IdHoaDon",
                table: "lichSuHoaDons",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_lichSuHoaDons_IdNhanVien",
                table: "lichSuHoaDons",
                column: "IdNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuSuDungVoucher_IdVoucher_IdKhachHang_IdHoaDon",
                table: "LichSuSuDungVouchers",
                columns: new[] { "IdVoucher", "IdKhachHang", "IdHoaDon" },
                unique: true,
                filter: "[IdHoaDon] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuSuDungVouchers_HoaDonIdHoaDon",
                table: "LichSuSuDungVouchers",
                column: "HoaDonIdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuSuDungVouchers_IdKhachHang",
                table: "LichSuSuDungVouchers",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_lichSuThanhToans_IdHoaDon",
                table: "lichSuThanhToans",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_lichSuThanhToans_IdNhanVien",
                table: "lichSuThanhToans",
                column: "IdNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_nhanViens_IdchucVu",
                table: "nhanViens",
                column: "IdchucVu");

            migrationBuilder.CreateIndex(
                name: "IX_promotionSanPhamChiTiets_IdSanPhamChiTiet",
                table: "promotionSanPhamChiTiets",
                column: "IdSanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhamChiTietKichCos_IdKichCo",
                table: "sanPhamChiTietKichCos",
                column: "IdKichCo");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhamChiTietMausacs_IdMauSac",
                table: "sanPhamChiTietMausacs",
                column: "IdMauSac");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhamChiTiets_IdSanPham",
                table: "sanPhamChiTiets",
                column: "IdSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhams_IdChatLieu",
                table: "sanPhams",
                column: "IdChatLieu");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhams_IdDanhMuc",
                table: "sanPhams",
                column: "IdDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhams_IdDeGiay",
                table: "sanPhams",
                column: "IdDeGiay");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhams_IdKieuDang",
                table: "sanPhams",
                column: "IdKieuDang");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhams_IdThuongHieu",
                table: "sanPhams",
                column: "IdThuongHieu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "diaChi");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "gioHangChiTiets");

            migrationBuilder.DropTable(
                name: "hinhAnh");

            migrationBuilder.DropTable(
                name: "hoaDonChiTiets");

            migrationBuilder.DropTable(
                name: "lichSuHoaDons");

            migrationBuilder.DropTable(
                name: "LichSuSuDungVouchers");

            migrationBuilder.DropTable(
                name: "lichSuThanhToans");

            migrationBuilder.DropTable(
                name: "promotionSanPhamChiTiets");

            migrationBuilder.DropTable(
                name: "provinces");

            migrationBuilder.DropTable(
                name: "sanPhamChiTietKichCos");

            migrationBuilder.DropTable(
                name: "sanPhamChiTietMausacs");

            migrationBuilder.DropTable(
                name: "wards");

            migrationBuilder.DropTable(
                name: "gioHang");

            migrationBuilder.DropTable(
                name: "vouchers");

            migrationBuilder.DropTable(
                name: "hoaDons");

            migrationBuilder.DropTable(
                name: "promotions");

            migrationBuilder.DropTable(
                name: "kichCos");

            migrationBuilder.DropTable(
                name: "mauSacs");

            migrationBuilder.DropTable(
                name: "sanPhamChiTiets");

            migrationBuilder.DropTable(
                name: "diaChiHoaDons");

            migrationBuilder.DropTable(
                name: "khachHangs");

            migrationBuilder.DropTable(
                name: "nhanViens");

            migrationBuilder.DropTable(
                name: "sanPhams");

            migrationBuilder.DropTable(
                name: "chuVu");

            migrationBuilder.DropTable(
                name: "chatLieus");

            migrationBuilder.DropTable(
                name: "danhMuc");

            migrationBuilder.DropTable(
                name: "deGiay");

            migrationBuilder.DropTable(
                name: "kieuDangs");

            migrationBuilder.DropTable(
                name: "thuongHieus");
        }
    }
}
