using AppAPI.Hubs;
using AppAPI.IRepository;
using AppAPI.IService;
using AppAPI.ModelRestPassword;
using AppAPI.Repository;
using AppAPI.Service;
using AppData;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Redis settings
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("RedisSettings:ConnectionString");
});

// Configure SQL Server database context
builder.Services.AddDbContext<AppDbcontext>(options =>
    
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient("GHN", client =>
{
	client.BaseAddress = new Uri("https://online-gateway.ghn.vn/shiip/public-api/");
	client.DefaultRequestHeaders.Add("Token", "1b11f9f8-257d-11ef-99a7-3ed37c49343e");
});
// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Register services and repositories
builder.Services.AddTransient<IKhachHangService, KhachHangService>();
builder.Services.AddTransient<IKhachHangRepo, KhachHangRepo>();
builder.Services.AddTransient<IChucVuService, ChucVuService>();
builder.Services.AddTransient<IChucVuRepo, ChucVuRepo>();
builder.Services.AddTransient<IDeGiayService, DeGiayService>();
builder.Services.AddTransient<IDeGiayRepo, DeGiayRepo>();
builder.Services.AddTransient<IChatLieuService, ChatLieuService>();
builder.Services.AddTransient<IChatLieuRepo, ChatLieuRepo>();
builder.Services.AddTransient<IKieuDangService, KieuDangService>();
builder.Services.AddTransient<IKieuDangRepo, KieuDangRepo>();
builder.Services.AddTransient<IDanhMucService, DanhMucService>();
builder.Services.AddTransient<IDanhMucRepo, DanhMucRepo>();
builder.Services.AddTransient<IThuongHieuRepo, ThuongHieuRepo>();
builder.Services.AddTransient<IThuongHieuService, ThuongHieuService>();
builder.Services.AddTransient<ISanPhamChiTietService, SanPhamChiTietService>();
builder.Services.AddTransient<ISanPhamChiTietRepo, SanPhamChiTietRepo>();
builder.Services.AddTransient<ISanPhamService, SanPhamService>();
builder.Services.AddTransient<ISanPhamRepo, SanPhamRepo>();
builder.Services.AddTransient<INhanVienService, NhanVienService>();
builder.Services.AddTransient<INhanVienRepo, NhanVienRepo>();
builder.Services.AddTransient<IDiaChiService, DiaChiService>();
builder.Services.AddTransient<IDiaChiRepo, DiaChiRepo>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IEmailRepo, EmailRepo>();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IKichCoService, KichCoService>();
builder.Services.AddTransient<IKichCoRepo, KichCoRepo>();
builder.Services.AddTransient<IMauSacService, MauSacService>();
builder.Services.AddTransient<IMauSacRepo, MauSacRepo>();
builder.Services.AddTransient<IHinhAnhService, HinhAnhService>();
builder.Services.AddTransient<IHinhAnhRepo, HinhAnhRepo>();
builder.Services.AddTransient<IPromotionRepo, PromotionRepo>();
builder.Services.AddTransient<IPromotionService, PromotionService>();
builder.Services.AddTransient<IVoucherRepo, VoucherRepo>();
builder.Services.AddTransient<IVoucherService, VoucherService>();
builder.Services.AddTransient<ISanPhamChiTietMauSacRepo, SanPhamChiTietMauSacRepo>();
builder.Services.AddTransient<ISanPhamChiTietMauSacService, SanPhamChiTietMauSacService>();
builder.Services.AddTransient<ISanPhamChiTietKichCoRepo, SanPhamChiTietKichCoRepo>();
builder.Services.AddTransient<ISanPhamChiTietKichCoService, SanPhamChiTietKichCoService>();
builder.Services.AddTransient<IGioHangChiTietRepo, GioHangChiTietRepo>();
builder.Services.AddTransient<IGioHangChiTietService, GioHangChiTietService>();
builder.Services.AddTransient<IHoaDonRepo, HoaDonRepo>();
builder.Services.AddTransient<IHoaDonService, HoaDonService>();
builder.Services.AddTransient<IHoaDonChiTietRepo, HoaDonChiTietRepo>();
builder.Services.AddTransient<IHoaDonChiTietService, HoaDonChiTietService>();
builder.Services.AddTransient<ILichSuHoaDonRepo, LichSuHoaDonRepo>();
builder.Services.AddTransient<ILichSuHoaDonService, LichSuHoaDonService>();
builder.Services.AddTransient<ICardRepo, CardRepo>();
builder.Services.AddTransient<ICardService, CardService>();
builder.Services.AddTransient<ILichSuThanhToanRepo, LichSuThanhToanRepo>();
builder.Services.AddTransient<ILichSuThanhToanService, LichSuThanhToanService>();
builder.Services.AddTransient<ILichSuSuDungVoucherRepo, LichSuSuDungVoucherRepo>();
builder.Services.AddTransient<ILichSuSuDungVoucherService, LichSuSuDungVoucherService>();
builder.Services.AddTransient<IPromotionSanPhamChiTietRepo, PromotionSanPhamChiTietRepo>();
builder.Services.AddTransient<IPromotionSanPhamChiTietService, PromotionSanPhamChiTietService>();

builder.Services.AddTransient<IThongKeRepo, ThongKeRepo>();
builder.Services.AddTransient<IThongKeServices, ThongKeServices>();
//builder.Services.AddHostedService<VoucherStatusUpdater>();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<VoucherHub>("/voucherHub");
});

app.Run();