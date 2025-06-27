
using AppData.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppData
{
    public static class Dataseeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Định nghĩa ID cố định cho các chức vụ
            var chucVuQL = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var chucVuNV = Guid.Parse("22222222-2222-2222-2222-222222222222");
         

            // Seed dữ liệu chức vụ
            modelBuilder.Entity<ChucVu>().HasData(
                new ChucVu() { IdChucVu = chucVuQL, Code = "QL", TenChucVu = "Quản lý" },
                new ChucVu() { IdChucVu = chucVuNV, Code = "NV", TenChucVu = "Nhân viên" }
            );

            // Định nghĩa ID cố định cho các nhân viên
            var nhanVien1 = Guid.NewGuid();
            var nhanVien2 = Guid.NewGuid();
            var nhanVien3 = Guid.NewGuid();
            var nhanVien4 = Guid.NewGuid();

            // Seed dữ liệu nhân viên
            modelBuilder.Entity<NhanVien>().HasData(
                new NhanVien()
                {
                    IdNhanVien = nhanVien1,
                    TenNhanVien = "Phạm Đức Hiếu",
                    SoDienThoai = "0976819974",
                    Email = "hieupdph40428@fpt.edu.vn",
                    AnhNhanVien = "1.png",
                    MatKhau = "1",
                    AuthProvider = "Local",
                    DiaChi = "Hà Nội",
                    NgayCapNhat = new DateTime(2024, 9, 4),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1,
                    TrangThai = 1,
                    IdchucVu = chucVuQL // Liên kết với ID chức vụ Quản lý
                },
                new NhanVien()
                {
                    IdNhanVien = nhanVien2,
                    TenNhanVien = "Trần Ngọc Hà",
                    SoDienThoai = "0969293263",
                    Email = "tranha10112004@gmail.com",
                    AnhNhanVien = "1.png",
                    MatKhau = "1",
                    AuthProvider = "Local",
                    DiaChi = "Đà Nẵng",
                    NgayCapNhat = new DateTime(2024, 9, 4),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1,
                    TrangThai = 1,
                    IdchucVu = chucVuQL // Liên kết với ID chức vụ Nhân viên
                },
                new NhanVien()
                {
                    IdNhanVien = nhanVien3,
                    TenNhanVien = "Kim Hoàng Long",
                    SoDienThoai = "0377804800",
                    Email = "longkhph35837@fpt.edu.vn",
                    AnhNhanVien = "1.png",
                    MatKhau = "1",
                    AuthProvider = "Local",
                    DiaChi = "Đà Nẵng",
                    NgayCapNhat = new DateTime(2024, 9, 4),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1,
                    TrangThai = 1,
                    IdchucVu = chucVuNV // Liên kết với ID chức vụ Kế toán
                },
                new NhanVien()
                {
                    IdNhanVien = nhanVien4,
                    TenNhanVien = "Đào Thành Nam",
                    SoDienThoai = "0855896668",
                    Email = "namdtph39830@fpt.edu.vn",
                    AnhNhanVien = "1.png",
                    MatKhau = "1",
                    AuthProvider = "Local",
                    DiaChi = "Đà Nẵng",
                    NgayCapNhat = new DateTime(2024, 9, 4),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1,
                    TrangThai = 1,
                    IdchucVu = chucVuNV // Liên kết với ID chức vụ Thủ kho
                }
            );

            // Định nghĩa các ID cố định mới (các GUID khác)
            var chatLieuCotton = Guid.NewGuid();
            var chatLieuDaThat = Guid.NewGuid();
            var chatLieuPolyester = Guid.NewGuid();
            var chatLieuVaiDet = Guid.NewGuid();
            var chatLieuDaTuNhien = Guid.NewGuid();
            var chatLieuDaLon = Guid.NewGuid();

            // Seed dữ liệu chất liệu với các GUID mới
            modelBuilder.Entity<ChatLieu>().HasData(
                new ChatLieu
                {
                    IdChatLieu = chatLieuCotton, // ID cố định cho "Vải Cotton"
                    TenChatLieu = "Vải Cotton",
                    NgayCapNhat = new DateTime(2024, 9, 4),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1
                },
                new ChatLieu
                {
                    IdChatLieu = chatLieuDaThat, // ID cố định cho "Da thật"
                    TenChatLieu = "Da thật",
                    NgayCapNhat = new DateTime(2024, 9, 4),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1
                },
                new ChatLieu
                {
                    IdChatLieu = chatLieuPolyester, // ID cố định cho "Vải Polyester"
                    TenChatLieu = "Vải Polyester",
                    NgayCapNhat = new DateTime(2024, 9, 4),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1,
                },
                new ChatLieu
                {
                    IdChatLieu = chatLieuVaiDet, // ID cố định cho ""
                    TenChatLieu = "Vải Dệt",
                    NgayCapNhat = new DateTime(2024, 9, 4),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1,
                },
                new ChatLieu
                {
                    IdChatLieu = chatLieuDaTuNhien, // ID cố định cho "Vải Polyester"
                    TenChatLieu = "Da Tự Nhiên",
                    NgayCapNhat = new DateTime(2024, 9, 4),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1,
                },
                new ChatLieu
                {
                    IdChatLieu = chatLieuDaLon, // ID cố định cho "Vải Polyester"
                    TenChatLieu = "Da Lộn",
                    NgayCapNhat = DateTime.Now,
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1,
                }
            );


            // Định nghĩa các ID cố định ngẫu nhiên cho DanhMuc
            var danhMucGiayTheThao = Guid.NewGuid();
            var danhMucGiayDa = Guid.NewGuid();
            var danhMucLowTop = Guid.NewGuid();
            var danhMucMidTop = Guid.NewGuid();
            var danhMucHighTop = Guid.NewGuid();

            // Seed dữ liệu DanhMuc với GUID ngẫu nhiên cố định
            modelBuilder.Entity<DanhMuc>().HasData(
                new DanhMuc
                {
                    IdDanhMuc = danhMucGiayTheThao, // ID cố định ngẫu nhiên cho "Giày Thể Thao"
                    TenDanhMuc = "Giày Thể Thao",
                    NgayTao = new DateTime(2024, 9, 4),
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new DanhMuc
                {
                    IdDanhMuc = danhMucGiayDa, // ID cố định ngẫu nhiên cho "Giày Da"
                    TenDanhMuc = "Giày Da",
                    NgayTao = new DateTime(2024, 9, 4),
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new DanhMuc
                {
                    IdDanhMuc = danhMucLowTop, // ID cố định ngẫu nhiên cho "Low-top"
                    TenDanhMuc = "Giày Cổ Thấp",
                    NgayTao = new DateTime(2024, 9, 4),
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new DanhMuc
                {
                    IdDanhMuc = danhMucMidTop, // ID cố định ngẫu nhiên cho "Mid-top"
                    TenDanhMuc = "Giày Cổ Trung",
                    NgayTao = new DateTime(2024, 9, 4),
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new DanhMuc
                {
                    IdDanhMuc = danhMucHighTop, // ID cố định ngẫu nhiên cho "High-top"
                    TenDanhMuc = "Giày Cổ Cao",
                    NgayTao = new DateTime(2024, 9, 4),
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                }
            );

            // Định nghĩa các ID cố định ngẫu nhiên cho DeGiay
            var deCaoSu = Guid.NewGuid();
            var deNhua = Guid.NewGuid();
            var deVai = Guid.NewGuid();
            var deEva = Guid.NewGuid();
            var deCaoSuLuuHoa = Guid.NewGuid();
            var dePVC = Guid.NewGuid();
            var deBPU = Guid.NewGuid();
            var deChunky = Guid.NewGuid();
            var deChisty = Guid.NewGuid();
            var deGiayLuoi = Guid.NewGuid();

            // Seed dữ liệu cho bảng DeGiay
            modelBuilder.Entity<DeGiay>().HasData(
                new DeGiay
                {
                    IdDeGiay = deCaoSu,
                    TenDeGiay = "Đế cao su",
                    NgayCapNhat = new DateTime(2024, 9, 4),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1
                },
                new DeGiay
                {
                    IdDeGiay = deNhua,
                    TenDeGiay = "Đế nhựa",
                    NgayCapNhat = new DateTime(2024, 9, 4),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1
                },
                new DeGiay
                {
                    IdDeGiay = deVai,
                    TenDeGiay = "Đế vải",
                    NgayCapNhat = new DateTime(2023, 10, 22),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1
                },
                new DeGiay
                {
                    IdDeGiay = deEva,
                    TenDeGiay = "Đế Eva",
                    NgayCapNhat = new DateTime(2023, 10, 22),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1
                },
                new DeGiay
                {
                    IdDeGiay = deCaoSuLuuHoa,
                    TenDeGiay = "Đế Cao Su Lưu Hóa",
                    NgayCapNhat = new DateTime(2023, 10, 22),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1
                },
                new DeGiay
                {
                    IdDeGiay = dePVC,
                    TenDeGiay = "Đế PVC",
                    NgayCapNhat = new DateTime(2023, 10, 22),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1
                },
                new DeGiay
                {
                    IdDeGiay = deBPU,
                    TenDeGiay = "Đế BPU",
                    NgayCapNhat = new DateTime(2023, 10, 22),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1
                },
                new DeGiay
                {
                    IdDeGiay = deChunky,
                    TenDeGiay = "Đế Chunky",
                    NgayCapNhat = new DateTime(2023, 10, 22),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1
                },
                new DeGiay
                {
                    IdDeGiay = deChisty,
                    TenDeGiay = "Đế Chisty",
                    NgayCapNhat = new DateTime(2023, 10, 22),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1
                },
                new DeGiay
                {
                    IdDeGiay = deGiayLuoi,
                    TenDeGiay = "Đế Giày Lười",
                    NgayCapNhat = new DateTime(2023, 10, 22),
                    NgayTao = new DateTime(2024, 9, 4),
                    NguoiCapNhat = "Admin",
                    NguoiTao = "Admin",
                    KichHoat = 1
                }
            );
            // định dnagj cố định id khahcs hàng
            var khachhang1 = Guid.NewGuid();
            var khachhang2 = Guid.NewGuid();
            var khachhang3 = Guid.NewGuid();
            var khachhang4 = Guid.NewGuid();
            var khachhang5 = Guid.NewGuid();
            var khachhang6 = Guid.NewGuid();
            var khachhang7 = Guid.NewGuid();
            var khachhang8 = Guid.NewGuid();
            var khachhang9 = Guid.NewGuid();
            var khachhang10 = Guid.NewGuid();
            var khachhang11 = Guid.NewGuid();
            var khachhang12 = Guid.NewGuid();
            var khachhang13 = Guid.NewGuid();
            var khachhang14 = Guid.NewGuid();
            var khachhang15 = Guid.NewGuid();
            var khachhang16 = Guid.NewGuid();
            var khachhang17 = Guid.NewGuid();
            modelBuilder.Entity<KhachHang>().HasData(
            // thêm dữ liệu khách hàng
            new KhachHang
            {
                IdKhachHang = khachhang1,
                HoTen = "Nguyễn Văn Vinh",
                SoDienThoai = "0912345678",
                Email = "nguyenvanvinh@gmail.com",
                AuthProvider = "Local",
                MatKhau = "1", // Lưu ý: Mật khẩu thực tế nên được mã hóa
                NgayTao = new DateTime(2024, 9, 4),
                NgayCapNhat = new DateTime(2024, 9, 4),
                NguoiTao = "Admin",
                NguoiCapNhat = "Admin",
                KichHoat = 1
            },
            new KhachHang
            {
                IdKhachHang = khachhang2,
                HoTen = "Trần Thị Bé",
                SoDienThoai = "0987654321",
                Email = "tranthibe@gmail.com",
                AuthProvider = "Google",
                MatKhau = "1", // Mật khẩu mã hóa
                NgayTao = new DateTime(2024, 9, 4),
                NgayCapNhat = new DateTime(2024, 9, 4),
                NguoiTao = "Admin",
                NguoiCapNhat = "Admin",
                KichHoat = 1
            },
            new KhachHang
            {
                IdKhachHang = khachhang3,
                HoTen = "Phạm Văn Cảnh",
                SoDienThoai = "0971123456",
                Email = "phamvancanh@gmail.com",
                AuthProvider = "Facebook",
                MatKhau = "1",
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now,
                NguoiTao = "Admin",
                NguoiCapNhat = "Admin",
                KichHoat = 0 // Không kích hoạt
            },
            new KhachHang
            {
                IdKhachHang = khachhang4,
                HoTen = "Lê Thị Dậu",
                SoDienThoai = "0356789123",
                Email = "lethidau@gmail.com",
                AuthProvider = "Local",
                MatKhau = "1", // Lưu ý: Mã hóa mật khẩu
                NgayTao = DateTime.Now.AddDays(-10),
                NgayCapNhat = DateTime.Now,
                NguoiTao = "System",
                NguoiCapNhat = "Admin",
                KichHoat = 1
            },
            new KhachHang
            {
                IdKhachHang = khachhang5,
                HoTen = "Đặng Văn Em",
                SoDienThoai = "0398765432",
                Email = "dangvanem@gmail.com",
                AuthProvider = "Google",
                MatKhau = "1",
                NgayTao = DateTime.Now.AddMonths(-2),
                NgayCapNhat = DateTime.Now.AddDays(-1),
                NguoiTao = "Admin",
                NguoiCapNhat = "Admin",
                KichHoat = 1
            },
            new KhachHang
            {
                IdKhachHang = khachhang6,
                HoTen = "Ngô Văn Phát",
                SoDienThoai = "0376543210",
                Email = "ngovanf@gmail.com",
                AuthProvider = "Facebook",
                MatKhau = "1",
                NgayTao = DateTime.Now.AddYears(-1),
                NgayCapNhat = DateTime.Now.AddMonths(-6),
                NguoiTao = "Admin",
                NguoiCapNhat = "Admin",
                KichHoat = 0
            },
            new KhachHang
            {
                IdKhachHang = khachhang7,
                HoTen = "Hoàng Anh Gia Lai",
                SoDienThoai = "0965432109",
                Email = "hoanganhgialai@gmail.com",
                AuthProvider = "Local",
                MatKhau = "1",
                NgayTao = DateTime.Now.AddMonths(-3),
                NgayCapNhat = DateTime.Now.AddMonths(-1),
                NguoiTao = "System",
                NguoiCapNhat = "System",
                KichHoat = 1
            },
            new KhachHang
            {
                IdKhachHang = khachhang8,
                HoTen = "Phan Thị Hà",
                SoDienThoai = "0845678901",
                Email = "phanthiha@gmail.com",
                AuthProvider = "Google",
                MatKhau = "1",
                NgayTao = DateTime.Now.AddMonths(-5),
                NgayCapNhat = DateTime.Now.AddMonths(-4),
                NguoiTao = "Admin",
                NguoiCapNhat = "Admin",
                KichHoat = 1
            },
            new KhachHang
            {
                IdKhachHang = khachhang9,
                HoTen = "Vũ Văn Ich",
                SoDienThoai = "0856781234",
                Email = "vuvanich@gmail.com",
                AuthProvider = "Facebook",
                MatKhau = "Fb@12345",
                NgayTao = DateTime.Now.AddDays(-20),
                NgayCapNhat = DateTime.Now.AddDays(-5),
                NguoiTao = "Admin",
                NguoiCapNhat = "Admin",
                KichHoat = 0
            },
            new KhachHang
            {
                IdKhachHang = khachhang10,
                HoTen = "Nguyễn Thị Khánh",
                SoDienThoai = "0834567890",
                Email = "nguyenthikhanh@gmail.com",
                AuthProvider = "Google",
                MatKhau = "1",
                NgayTao = DateTime.Now.AddDays(-15),
                NgayCapNhat = DateTime.Now.AddDays(-10),
                NguoiTao = "System",
                NguoiCapNhat = "Admin",
                KichHoat = 1
            },
            new KhachHang
            {
                IdKhachHang = khachhang11,
                HoTen = "Trần Văn Lý",
                SoDienThoai = "0901234567",
                Email = "tranvanly@yahoo.com",
                AuthProvider = "Facebook",
                MatKhau = "1",
                NgayTao = DateTime.Now.AddMonths(-8),
                NgayCapNhat = DateTime.Now.AddMonths(-2),
                NguoiTao = "Admin",
                NguoiCapNhat = "Admin",
                KichHoat = 1
            },
            new KhachHang
            {
                IdKhachHang = khachhang12,
                HoTen = "Lê Thị Mai",
                SoDienThoai = "0387654321",
                Email = "lethimai@gmail.com",
                AuthProvider = "Local",
                MatKhau = "1",
                NgayTao = DateTime.Now.AddYears(-1),
                NgayCapNhat = DateTime.Now.AddMonths(-6),
                NguoiTao = "System",
                NguoiCapNhat = "Admin",
                KichHoat = 1
            },
            new KhachHang
            {
                IdKhachHang = khachhang13,
                HoTen = "Phạm Văn Nam",
                SoDienThoai = "0919876543",
                Email = "phamvannam@outlook.com",
                AuthProvider = "Facebook",
                MatKhau = "1",
                NgayTao = DateTime.Now.AddMonths(-3),
                NgayCapNhat = DateTime.Now.AddDays(-30),
                NguoiTao = "Admin",
                NguoiCapNhat = "Admin",
                KichHoat = 0
            },
            new KhachHang
            {
                IdKhachHang = khachhang14,
                HoTen = "Đỗ Minh Hải",
                SoDienThoai = "0867891234",
                Email = "dminhai@gmail.com",
                AuthProvider = "Google",
                MatKhau = "1",
                NgayTao = DateTime.Now.AddDays(-100),
                NgayCapNhat = DateTime.Now.AddDays(-50),
                NguoiTao = "System",
                NguoiCapNhat = "System",
                KichHoat = 1
            },
            new KhachHang
            {
                IdKhachHang = khachhang15,
                HoTen = "Nguyễn Hoàng Phong",
                SoDienThoai = "0923456789",
                Email = "nguyenhoangp@gmail.com",
                AuthProvider = "Local",
                MatKhau = "1",
                NgayTao = DateTime.Now.AddMonths(-2),
                NgayCapNhat = DateTime.Now.AddDays(-20),
                NguoiTao = "Admin",
                NguoiCapNhat = "Admin",
                KichHoat = 1
            },
            new KhachHang
            {
                IdKhachHang = khachhang16,
                HoTen = "Trịnh Thị Quyên",
                SoDienThoai = "0898765432",
                Email = "trinhthiquyen@gmail.com",
                AuthProvider = "Google",
                MatKhau = "1",
                NgayTao = DateTime.Now.AddYears(-2),
                NgayCapNhat = DateTime.Now.AddMonths(-12),
                NguoiTao = "System",
                NguoiCapNhat = "System",
                KichHoat = 1
            },
            new KhachHang
            {
                IdKhachHang = khachhang17,
                HoTen = "Lý Văn Rô",
                SoDienThoai = "0945678901",
                Email = "lyvanro@gmail.com",
                AuthProvider = "Facebook",
                MatKhau = "1",
                NgayTao = DateTime.Now.AddMonths(-5),
                NgayCapNhat = DateTime.Now.AddDays(-10),
                NguoiTao = "Admin",
                NguoiCapNhat = "Admin",
                KichHoat = 0
            }
        );
            var diachi1 = Guid.NewGuid();
            var diachi2 = Guid.NewGuid();
            var diachi3 = Guid.NewGuid();
            var diachi4 = Guid.NewGuid();
            var diachi5 = Guid.NewGuid();
            var diachi6 = Guid.NewGuid();
            var diachi7 = Guid.NewGuid();
            var diachi8 = Guid.NewGuid();
            var diachi9 = Guid.NewGuid();
            var diachi10 = Guid.NewGuid();
            var diachi11 = Guid.NewGuid();
            var diachi12 = Guid.NewGuid();
            var diachi13 = Guid.NewGuid();
            var diachi14 = Guid.NewGuid();
            var diachi15 = Guid.NewGuid();
            var diachi16 = Guid.NewGuid();
            modelBuilder.Entity<DiaChi>().HasData(
            // thêm dữ liệu địa chỉ
            new DiaChi
            {

                IdDiaChi = diachi1,
                HoTen = "Nguyễn Văn Vinh",
                SoDienThoai = "0912345678",
                MoTa = "Số 10, Ngõ 15, Đường Láng",
                ProvinceName = "Hà Nội",
                ProvinceId = 1,
                DistrictName = "Quận Đống Đa",
                DistrictId = 101,
                WardName = "Phường Láng Thượng",
                WardId = "00101",
                DiaChiMacDinh = true,
                IdKhachHang = khachhang1,
            },
            new DiaChi
            {
                IdDiaChi = diachi2,
                HoTen = "Trần Thị Bé",
                SoDienThoai = "0987654321",
                MoTa = "Tòa nhà Landmark 81, Bình Thạnh",
                ProvinceName = "Hồ Chí Minh",
                ProvinceId = 2,
                DistrictName = "Quận Bình Thạnh",
                DistrictId = 201,
                WardName = "Phường 22",
                WardId = "00201",
                DiaChiMacDinh = true,
                IdKhachHang = khachhang2
            },
            new DiaChi
            {
                IdDiaChi = diachi3,
                HoTen = "Phạm Văn Cảnh",
                SoDienThoai = "0971123456",
                MoTa = "Số 5, Đường Trần Phú",
                ProvinceName = "Đà Nẵng",
                ProvinceId = 3,
                DistrictName = "Quận Hải Châu",
                DistrictId = 301,
                WardName = "Phường Thạch Thang",
                WardId = "00301",
                DiaChiMacDinh = false,
                IdKhachHang = khachhang3
            },
            new DiaChi
            {
                IdDiaChi = diachi4,
                HoTen = "Lê Thị Dậu",
                SoDienThoai = "0356789123",
                MoTa = "Khu phố 1, Phường Vĩnh Phú",
                ProvinceName = "Bình Dương",
                ProvinceId = 4,
                DistrictName = "Thị xã Thuận An",
                DistrictId = 401,
                WardName = "Phường Vĩnh Phú",
                WardId = "00401",
                DiaChiMacDinh = true,
                IdKhachHang = khachhang4
            },
            new DiaChi
            {
                IdDiaChi = diachi5,
                HoTen = "Đặng Văn Em",
                SoDienThoai = "0398765432",
                MoTa = "Ấp An Hòa, Xã An Bình",
                ProvinceName = "Đồng Nai",
                ProvinceId = 5,
                DistrictName = "Huyện Long Thành",
                DistrictId = 501,
                WardName = "Xã An Bình",
                WardId = "00501",
                DiaChiMacDinh = false,
                IdKhachHang = khachhang5
            },
            new DiaChi
            {
                IdDiaChi = diachi6,
                HoTen = "Ngô Văn Phát",
                SoDienThoai = "0376543210",
                MoTa = "Số 20, Đường Nguyễn Huệ",
                ProvinceName = "Thừa Thiên Huế",
                ProvinceId = 6,
                DistrictName = "Thành phố Huế",
                DistrictId = 601,
                WardName = "Phường Vĩnh Ninh",
                WardId = "00601",
                DiaChiMacDinh = true,
                IdKhachHang = khachhang6
            },
            new DiaChi
            {
                IdDiaChi = diachi7,
                HoTen = "Hoàng Anh Gia Lai",
                SoDienThoai = "0965432109",
                MoTa = "Khu dân cư Bình Minh",
                ProvinceName = "Cần Thơ",
                ProvinceId = 7,
                DistrictName = "Quận Ninh Kiều",
                DistrictId = 701,
                WardName = "Phường Tân An",
                WardId = "00701",
                DiaChiMacDinh = false,
                IdKhachHang = khachhang7
            },
            new DiaChi
            {
                IdDiaChi = diachi8,
                HoTen = "Phan Thị Hà",
                SoDienThoai = "0845678901",
                MoTa = "Số 15, Đường Hoàng Diệu",
                ProvinceName = "Khánh Hòa",
                ProvinceId = 8,
                DistrictName = "Thành phố Nha Trang",
                DistrictId = 801,
                WardName = "Phường Phước Long",
                WardId = "00801",
                DiaChiMacDinh = true,
                IdKhachHang = khachhang8
            },
            new DiaChi
            {
                IdDiaChi = diachi9,
                HoTen = "Vũ Văn Ich",
                SoDienThoai = "0856781234",
                MoTa = "Số 30, Đường Lê Lợi",
                ProvinceName = "Quảng Ninh",
                ProvinceId = 9,
                DistrictName = "Thành phố Hạ Long",
                DistrictId = 901,
                WardName = "Phường Bãi Cháy",
                WardId = "00901",
                DiaChiMacDinh = false,
                IdKhachHang = khachhang9
            },
            new DiaChi
            {
                IdDiaChi = diachi10,
                HoTen = "Hoàng Anh J",
                SoDienThoai = "0976543210",
                MoTa = "Thôn Hồng Thái, Xã Đông Hòa",
                ProvinceName = "Phú Yên",
                ProvinceId = 10,
                DistrictName = "Huyện Tây Hòa",
                DistrictId = 1001,
                WardName = "Xã Đông Hòa",
                WardId = "01001",
                DiaChiMacDinh = true,
                IdKhachHang = khachhang10
            }
        );
            var kichco35 = Guid.NewGuid();
            var kichco36 = Guid.NewGuid();
            var kichco37 = Guid.NewGuid();
            var kichco38 = Guid.NewGuid();
            var kichco39 = Guid.NewGuid();
            var kichco40 = Guid.NewGuid();
            var kichco41 = Guid.NewGuid();
            var kichco42 = Guid.NewGuid();
            var kichco43 = Guid.NewGuid();
            var kichco44 = Guid.NewGuid();
            var kichco45 = Guid.NewGuid();
            modelBuilder.Entity<KichCo>().HasData(
            // thêm kích cỡ
            new KichCo
            {
                IdKichCo = kichco35,
                TenKichCo = "35",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new KichCo
            {
                IdKichCo = kichco36,
                TenKichCo = "36",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new KichCo
            {
                IdKichCo = kichco37,
                TenKichCo = "37",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new KichCo
            {
                IdKichCo = kichco38,
                TenKichCo = "38",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new KichCo
            {
                IdKichCo = kichco39,
                TenKichCo = "39",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new KichCo
            {
                IdKichCo = kichco40,
                TenKichCo = "40",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new KichCo
            {
                IdKichCo = kichco41,
                TenKichCo = "41",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new KichCo
            {
                IdKichCo = kichco42,
                TenKichCo = "42",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new KichCo
            {
                IdKichCo = kichco43,
                TenKichCo = "43",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new KichCo
            {
                IdKichCo = kichco44,
                TenKichCo = "44",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new KichCo
            {
                IdKichCo = kichco45,
                TenKichCo = "45",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            }
        );
            var kieudangTT = Guid.NewGuid();
            var kieudangCD = Guid.NewGuid();
            var kieudangHD = Guid.NewGuid();
            modelBuilder.Entity<KieuDang>().HasData(
            //thêm kiểu dáng
            new KieuDang
            {
                IdKieuDang = kieudangTT,
                TenKieuDang = "Thể Thao",
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now,
                NguoiTao = "Admin",
                NguoiCapNhat = "Admin",
                KichHoat = 1
            },
            new KieuDang
            {
                IdKieuDang = kieudangCD,
                TenKieuDang = "Cổ Điển",
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now,
                NguoiTao = "Admin",
                NguoiCapNhat = "Admin",
                KichHoat = 1
            },
             new KieuDang
             {
                 IdKieuDang = kieudangHD,
                 TenKieuDang = "Hiện Đại",
                 NgayTao = DateTime.Now,
                 NgayCapNhat = DateTime.Now,
                 NguoiTao = "Admin",
                 NguoiCapNhat = "Admin",
                 KichHoat = 1
             }
        );
            var redId = Guid.NewGuid();
            var greenId = Guid.NewGuid();
            var blueId = Guid.NewGuid();
            var blackId = Guid.NewGuid();
            var whiteId = Guid.NewGuid();
            var greyId = Guid.NewGuid();
            var navyBlueId = Guid.NewGuid();
            var brownId = Guid.NewGuid();
            var pinkId = Guid.NewGuid();
            var orangeId = Guid.NewGuid();
            var metallicsId = Guid.NewGuid();
            var fluorescentsId = Guid.NewGuid();
            modelBuilder.Entity<MauSac>().HasData(
            new MauSac
            {
                IdMauSac = redId,
                TenMauSac = "Đỏ",
                NgayCapNhat = new DateTime(2024, 9, 4),
                NgayTao = new DateTime(2024, 9, 4),
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new MauSac
            {
                IdMauSac = greenId,
                TenMauSac = "Xanh lá",
                NgayCapNhat = new DateTime(2024, 9, 4),
                NgayTao = new DateTime(2024, 9, 4),
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new MauSac
            {
                IdMauSac = blueId,
                TenMauSac = "Xanh dương",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new MauSac
            {
                IdMauSac = blackId,
                TenMauSac = "Đen",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new MauSac
            {
                IdMauSac = whiteId,
                TenMauSac = "Trắng",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new MauSac
            {
                IdMauSac = greyId,
                TenMauSac = "Xám",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new MauSac
            {
                IdMauSac = navyBlueId,
                TenMauSac = "Xanh Navy",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new MauSac
            {
                IdMauSac = brownId,
                TenMauSac = "Nâu",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new MauSac
            {
                IdMauSac = pinkId,
                TenMauSac = "Hồng",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new MauSac
            {
                IdMauSac = orangeId,
                TenMauSac = "Cam",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new MauSac
            {
                IdMauSac = metallicsId,
                TenMauSac = "Kim loại",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            },
            new MauSac
            {
                IdMauSac = fluorescentsId,
                TenMauSac = "Phát quang",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "Admin",
                NguoiTao = "Admin",
                KichHoat = 1
            }
        );
            // Định nghĩa Guid cho từng thương hiệu
            var nikeId = Guid.NewGuid();
            var adidasId = Guid.NewGuid();
            var pumaId = Guid.NewGuid();
            var reebokId = Guid.NewGuid();
            var converseId = Guid.NewGuid();
            var vansId = Guid.NewGuid();
            var newBalanceId = Guid.NewGuid();
            var filaId = Guid.NewGuid();
            var mlbId = Guid.NewGuid();
            var gucciId = Guid.NewGuid();
            var louisVuittonId = Guid.NewGuid();
            var balenciagaId = Guid.NewGuid();
            var diorId = Guid.NewGuid();
            var shondoId = Guid.NewGuid();
            var bitisId = Guid.NewGuid();
            var ananasId = Guid.NewGuid();

            // Thêm dữ liệu vào bảng ThuongHieu
            modelBuilder.Entity<ThuongHieu>().HasData(
                new ThuongHieu
                {
                    IdThuongHieu = nikeId,
                    TenThuongHieu = "Nike",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = adidasId,
                    TenThuongHieu = "Adidas",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = pumaId,
                    TenThuongHieu = "Puma",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = reebokId,
                    TenThuongHieu = "Reebok",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = converseId,
                    TenThuongHieu = "Converse",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = vansId,
                    TenThuongHieu = "Vans",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = newBalanceId,
                    TenThuongHieu = "New Balance",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = filaId,
                    TenThuongHieu = "Fila",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = mlbId,
                    TenThuongHieu = "MLB",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = gucciId,
                    TenThuongHieu = "Gucci",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = louisVuittonId,
                    TenThuongHieu = "Louis Vuitton",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = balenciagaId,
                    TenThuongHieu = "Balenciaga",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = diorId,
                    TenThuongHieu = "Dior",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = shondoId,
                    TenThuongHieu = "Shondo",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = bitisId,
                    TenThuongHieu = "Biti’s",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                },
                new ThuongHieu
                {
                    IdThuongHieu = ananasId,
                    TenThuongHieu = "Ananas",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                }
            );
            var GiayTTId = Guid.NewGuid();
            var GiayDaId = Guid.NewGuid();
            var sneakerId1 = Guid.NewGuid();
            var sneakerId2 = Guid.NewGuid();
            var sneakerId3 = Guid.NewGuid();
            var sneakerId4 = Guid.NewGuid();
            var sneakerId5 = Guid.NewGuid();
            var sneakerId6 = Guid.NewGuid();
            var sneakerId7 = Guid.NewGuid();
            var sneakerId8 = Guid.NewGuid();
            var sneakerId9 = Guid.NewGuid();
            var sneakerId10 = Guid.NewGuid();
            //    modelBuilder.Entity<SanPham>().HasData(
            //    // thêm sản phẩm 
            //    // Tạo danh sách sản phẩm mẫu
            //    new SanPham
            //    {
            //        IdSanPham = GiayTTId,
            //        TenSanPham = "Nike Air Force 1",
            //        NgayCapNhat = new DateTime(2024, 9, 4),
            //        NgayTao = new DateTime(2024, 9, 4),
            //        NguoiCapNhat = "Admin",
            //        NguoiTao = "Admin",
            //        KichHoat = 1,
            //        MoTa = "Nike Air Force 1 là dòng giày thể thao biểu tượng của Nike, ra mắt năm 1982. Với thiết kế cổ điển, chất liệu da cao cấp, đế tích hợp công nghệ Air êm ái, và kiểu dáng đa dạng (low, mid, high), đôi giày này phù hợp cho cả thể thao lẫn thời trang đường phố. Là biểu tượng của phong cách và văn hóa sneaker toàn cầu.",
            //        //Sale = 10.0,
            //        IdChatLieu = chatLieuDaTuNhien,
            //        IdKieuDang = kieudangTT,
            //        IdThuongHieu = nikeId,
            //        IdDanhMuc = danhMucGiayTheThao,
            //        IdDeGiay = deCaoSu,

            //    },
            //    new SanPham
            //    {
            //        IdSanPham = GiayDaId,
            //        TenSanPham = "Adidas Ultra Boost",
            //        NgayCapNhat = new DateTime(2024, 9, 4),
            //        NgayTao = new DateTime(2024, 9, 4),
            //        NguoiCapNhat = "Admin",
            //        NguoiTao = "Admin",
            //        KichHoat = 1,
            //        MoTa = "Adidas Ultra Boost là dòng giày chạy bộ cao cấp, nổi bật với công nghệ đệm Boost mang lại độ êm ái và hoàn trả năng lượng tối ưu. Thiết kế hiện đại, phần thân Primeknit co giãn ôm sát chân, kết hợp đế ngoài Continental giúp tăng độ bám và bền bỉ. Phù hợp cho cả chạy bộ lẫn thời trang hàng ngày..",
            //        //Sale = 5.0,
            //        IdChatLieu = chatLieuPolyester,
            //        IdKieuDang = kieudangCD,
            //        IdThuongHieu = adidasId,
            //        IdDanhMuc = danhMucGiayTheThao,
            //        IdDeGiay = deEva,
            //    },
            //    new SanPham
            //    {
            //        IdSanPham = sneakerId1,
            //        TenSanPham = "Nike Jordan 1 (JD1)",
            //        NgayCapNhat = new DateTime(2024, 9, 4),
            //        NgayTao = new DateTime(2024, 9, 4),
            //        NguoiCapNhat = "Admin",
            //        NguoiTao = "Admin",
            //        KichHoat = 1,
            //        MoTa = "Nike Jordan 1 (JD1) là đôi giày huyền thoại ra mắt năm 1985, gắn liền với Michael Jordan. Thiết kế biểu tượng với chất liệu da cao cấp, cổ thấp/mid/cao và đế ngoài chống trượt. Phù hợp cho cả chơi bóng rổ và thời trang đường phố, JD1 là biểu tượng của văn hóa sneaker toàn cầu.",
            //        //Sale = 15.0,
            //        IdChatLieu = chatLieuPolyester, // Vải Canvas
            //        IdKieuDang = kieudangHD,
            //        IdThuongHieu = nikeId,
            //        IdDanhMuc = danhMucLowTop,
            //        IdDeGiay = deCaoSu,
            //    },
            //    new SanPham
            //    {
            //        IdSanPham = sneakerId2,
            //        TenSanPham = "Adidas Yeezy 350",
            //        NgayCapNhat = new DateTime(2024, 9, 4),
            //        NgayTao = new DateTime(2024, 9, 4),
            //        NguoiCapNhat = "Admin",
            //        NguoiTao = "Admin",
            //        KichHoat = 1,
            //        MoTa = "Adidas Yeezy 350 là dòng giày thời trang nổi bật, hợp tác giữa Adidas và Kanye West. Thiết kế tối giản với thân Primeknit co giãn, đế Boost êm ái, và phong cách hiện đại. Yeezy 350 được yêu thích nhờ sự thoải mái và tính biểu tượng trong làng thời trang đường phố.\r\n\r\n\r\n\r\n\r\n\r\n\r\n",
            //        //Sale = 20.0,
            //        IdChatLieu = chatLieuDaThat, // Da thật
            //        IdKieuDang = kieudangHD,
            //        IdThuongHieu = adidasId,
            //        IdDanhMuc = danhMucHighTop,
            //        IdDeGiay = deEva,
            //    },
            //     new SanPham
            //     {
            //         IdSanPham = sneakerId3,
            //         TenSanPham = "Adidas Samba",
            //         NgayTao = new DateTime(2024, 9, 4),
            //         NguoiCapNhat = "Admin",
            //         NguoiTao = "Admin",
            //         KichHoat = 1,
            //         MoTa = "Adidas Samba là dòng giày cổ điển ra mắt năm 1950, ban đầu thiết kế cho bóng đá trong nhà. Với chất liệu da bền bỉ, mũi giày da lộn và đế cao su chống trượt, Samba là biểu tượng vượt thời gian, phù hợp cho cả thể thao và thời trang hàng ngày.",
            //         //Sale = 25.0,
            //         IdChatLieu = chatLieuCotton, // Vải lưới thoáng khí
            //         IdKieuDang = kieudangHD,
            //         IdThuongHieu = adidasId,
            //         IdDanhMuc = danhMucHighTop,
            //         IdDeGiay = deBPU,
            //     },
            //     new SanPham
            //     {
            //         IdSanPham = sneakerId4,
            //         TenSanPham = "Adidas Forum 84 Low",
            //         NgayCapNhat = new DateTime(2024, 9, 4),
            //         NgayTao = new DateTime(2024, 9, 4),
            //         NguoiCapNhat = "Admin",
            //         NguoiTao = "Admin",
            //         KichHoat = 1,
            //         MoTa = "Adidas Forum 84 Low là dòng giày retro lấy cảm hứng từ bóng rổ thập niên 80. Thiết kế thấp cổ, chất liệu da cao cấp, dây đeo đặc trưng, và đế cao su bền bỉ, mang lại phong cách cổ điển pha chút hiện đại. Phù hợp cho cả thể thao và thời trang đường phố.",
            //         //Sale = 10.0,
            //         IdChatLieu = chatLieuPolyester, // Vải Canvas
            //         IdKieuDang = kieudangCD,
            //         IdThuongHieu = adidasId,
            //         IdDanhMuc = danhMucMidTop,
            //         IdDeGiay = deCaoSu,
            //     },
            //     new SanPham
            //     {
            //         IdSanPham = sneakerId5,
            //         TenSanPham = "Converse Chuck Taylor All Star",
            //         NgayCapNhat = new DateTime(2024, 9, 4),
            //         NgayTao = new DateTime(2024, 9, 4),
            //         NguoiCapNhat = "Admin",
            //         NguoiTao = "Admin",
            //         KichHoat = 1,
            //         MoTa = "Converse Chuck Taylor All Star là đôi giày huyền thoại ra mắt năm 1917, nổi bật với thiết kế vải canvas bền nhẹ, đế cao su chống trượt và logo tròn đặc trưng. Phù hợp với mọi phong cách, từ thường ngày đến thời trang đường phố, là biểu tượng vượt thời gian.\r\n\r\n\r\n\r\n\r\n\r\n\r\n",
            //         //Sale = 18.0,
            //         IdChatLieu = chatLieuPolyester, // Vải Canvas
            //         IdKieuDang = kieudangHD,
            //         IdThuongHieu = converseId,
            //         IdDanhMuc = danhMucLowTop,
            //         IdDeGiay = deCaoSu,
            //     },
            //     new SanPham
            //     {
            //         IdSanPham = sneakerId6,
            //         TenSanPham = "Giày Thể Thao Biti's Hunter",
            //         NgayCapNhat = new DateTime(2024, 9, 4),
            //         NgayTao = new DateTime(2024, 9, 4),
            //         NguoiCapNhat = "Admin",
            //         NguoiTao = "Admin",
            //         KichHoat = 1,
            //         MoTa = "Biti's Hunter là dòng giày thể thao hiện đại của Biti's, nổi bật với thiết kế năng động, chất liệu nhẹ, thoáng khí, và đế EVA êm ái, hỗ trợ vận động linh hoạt. Phù hợp cho cả tập luyện lẫn thời trang hàng ngày, mang đậm phong cách Việt trẻ trung.",
            //         //Sale = 12.0,
            //         IdChatLieu = chatLieuPolyester, // Vải lưới thoáng khí
            //         IdKieuDang = kieudangHD,
            //         IdThuongHieu = bitisId,
            //         IdDanhMuc = danhMucLowTop,
            //         IdDeGiay = deChisty,
            //     },
            //     new SanPham
            //     {
            //         IdSanPham = sneakerId7,
            //         TenSanPham = "Louis Vuitton Trainers",
            //         NgayCapNhat = new DateTime(2024, 9, 4),
            //         NgayTao = new DateTime(2024, 9, 4),
            //         NguoiCapNhat = "Admin",
            //         NguoiTao = "Admin",
            //         KichHoat = 1,
            //         MoTa = "Louis Vuitton Trainers là dòng giày thể thao cao cấp của Louis Vuitton, với thiết kế sang trọng, phối hợp giữa chất liệu da, vải và cao su. Đặc trưng với logo LV nổi bật, đôi giày này mang đến sự kết hợp hoàn hảo giữa thời trang và sự thoải mái, phù hợp cho những ai yêu thích phong cách sang trọng và thể thao.",
            //         //Sale = 15.0,
            //         IdChatLieu = chatLieuDaThat, // Da thật
            //         IdKieuDang = kieudangHD,
            //         IdThuongHieu = louisVuittonId,
            //         IdDanhMuc = danhMucHighTop,
            //         IdDeGiay = deCaoSu,
            //     },
            //     new SanPham
            //     {
            //         IdSanPham = sneakerId8,
            //         TenSanPham = "Nike Infinity 4 Fp",
            //         NgayCapNhat = DateTime.Now,
            //         NgayTao = new DateTime(2024, 9, 4),
            //         NguoiCapNhat = "Admin",
            //         NguoiTao = "Admin",
            //         KichHoat = 1,
            //         MoTa = "Nike Infinity 4 FP là giày chạy bộ cao cấp, thiết kế với công nghệ đệm ZoomX và Flyknit giúp tối ưu hóa sự thoải mái và hỗ trợ khi chạy. Phần đế giữa mềm mại, nhẹ và bền bỉ, mang lại trải nghiệm chạy mượt mà. Phù hợp cho người chạy dài và vận động viên.",
            //         //Sale = 20.0,
            //         IdChatLieu = chatLieuPolyester, // Vải Canvas
            //         IdKieuDang = kieudangHD,
            //         IdThuongHieu = nikeId,
            //         IdDanhMuc = danhMucGiayTheThao,
            //         IdDeGiay = deCaoSu,
            //     },
            //     new SanPham
            //     {
            //         IdSanPham = sneakerId9,
            //         TenSanPham = "PUMA Unisex Caven",
            //         NgayCapNhat = DateTime.Now,
            //         NgayTao = new DateTime(2024, 9, 4),
            //         NguoiCapNhat = "Admin",
            //         NguoiTao = "Admin",
            //         KichHoat = 1,
            //         MoTa = "PUMA Unisex Caven là giày thể thao phong cách unisex với thiết kế hiện đại và thoải mái. Chất liệu da cao cấp kết hợp với đế cao su bền bỉ, tạo sự hỗ trợ và độ bám tốt. Phù hợp cho cả nam và nữ, dễ dàng phối hợp với nhiều trang phục.",
            //         //Sale = 18.0,
            //         IdChatLieu = chatLieuPolyester, // Vải lưới
            //         IdKieuDang = kieudangTT,
            //         IdThuongHieu = pumaId,
            //         IdDanhMuc = danhMucGiayTheThao,
            //         IdDeGiay = deCaoSuLuuHoa,
            //     },
            //     new SanPham
            //     {
            //         IdSanPham = sneakerId10,
            //         TenSanPham = "Converse Lugged 2.0 Platform Denim",
            //         NgayCapNhat = new DateTime(2024, 9, 4),
            //         NgayTao = new DateTime(2024, 9, 4),
            //         NguoiCapNhat = "Admin",
            //         NguoiTao = "Admin",
            //         KichHoat = 1,
            //         MoTa = "Converse Lugged 2.0 Platform Denim là giày sneaker với thiết kế platform cao, kết hợp giữa vải denim bền và đế cao su dày, tạo phong cách thời trang mạnh mẽ. Đặc trưng với logo Converse và đế chống trượt, thích hợp cho cả streetwear và tạo điểm nhấn trong các bộ trang phục năng động.",
            //         //Sale = 25.0,
            //         IdChatLieu = chatLieuVaiDet, // Vải Canvas
            //         IdKieuDang = kieudangHD,
            //         IdThuongHieu = converseId,
            //         IdDanhMuc = danhMucHighTop,
            //         IdDeGiay = deCaoSu,
            //     }

            //);




           
        

        }
    }
}