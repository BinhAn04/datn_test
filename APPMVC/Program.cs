
using AppAPI.Service;

using AppData;
using APPMVC.IService;
using APPMVC.Service;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using APPMVC.Service.Vnpay;
using APPMVC.Service.VnpayClient;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1099511627776;
});


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian tồn tại của session
});

builder.Services.AddMemoryCache();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<IConfiguration>(builder.Configuration);

builder.Services.AddHttpClient();

//builder.Services.AddTransient<IDayGiayService, DayGiayService>();
builder.Services.AddTransient<IDanhMucService, DanhMucService>();
builder.Services.AddTransient<IThuongHieuService, ThuongHieuService>();
builder.Services.AddTransient<IChatLieuService, ChatLieuService>();
builder.Services.AddTransient<IKieuDangService, KieuDangService>();
builder.Services.AddTransient<IHinhAnhService, HinhAnhService>();
builder.Services.AddTransient<IPromotionService, PromotionService>();
builder.Services.AddTransient<IDeGiayService, DeGiayService>();
builder.Services.AddTransient<IKichCoService, KichCoService>();
builder.Services.AddTransient<IMauSacService, MauSacService>();
builder.Services.AddTransient<ISanPhamService, SanPhamService>();
builder.Services.AddTransient<ISanPhamChiTietService, SanPhamChiTietService>();
builder.Services.AddTransient<INhanVienService, NhanVienService>();
builder.Services.AddTransient<IKhachHangService, KhachHangService>();
builder.Services.AddTransient<IChucVuService, ChucVuService>();
builder.Services.AddTransient<IGioHangChiTietService, GioHangChiTietService>();
builder.Services.AddTransient<IDiaChiService, DiaChiService>();
builder.Services.AddTransient<IHoaDonService, HoaDonService>();
builder.Services.AddTransient<IHoaDonChiTietService, HoaDonChiTietService>();
builder.Services.AddDbContext<AppDbcontext>();
builder.Services.AddTransient<ISanPhamChiTietMauSacService, SanPhamChiTietMauSacService>();
builder.Services.AddTransient<ISanPhamChiTietKichCoService, SanPhamChiTietKichCoService>();
builder.Services.AddTransient<IVoucherService, VoucherService>();
builder.Services.AddTransient<ILichSuHoaDonService, LichSuHoaDonService>();
builder.Services.AddTransient<ICardService, CardService>();
builder.Services.AddTransient<IThongKeService, ThongKeService>();
//Đăng ký VnPay admin
builder.Services.AddSingleton<IVnPayService, VnPayService>();
//VnPay Client
builder.Services.AddSingleton<iVnpayClientService, VnpayServiceClient>();
//builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddTransient<ILichSuThanhToanService, LichSuThanhToanService>();
builder.Services.AddTransient<ILichSuSuDungVoucherService, LichSuSuDungVoucherService>();
builder.Services.AddTransient<IPromotionSanPhamChiTietService, PromotionSanPhamChiTietService>();
builder.Services.AddHttpClient<GiaoHangNhanhService>(client =>
{
    client.DefaultRequestHeaders.Add("Content-Type", "application/json");
});

builder.Services.AddTransient<GiaoHangNhanhService>(provider =>
{
    var httpClient = provider.GetRequiredService<HttpClient>();
    return new GiaoHangNhanhService(httpClient, "bcf656fe-256b-11ef-9e93-f2508e67c133", "5120262");
});
builder.Services.AddHostedService<PromotionStatusUpdaterService>();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();


app.UseAuthorization();

app.UseStaticFiles();

app.UseCors("AllowAll");
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{area=Client}/{controller=HomeClient}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=NhanVien}/{action=Login}/{id?}");

    endpoints.MapControllerRoute(
        name: "Admin",
        pattern: "{area:exists}/{controller=NhanVien}/{action=Login}/{id?}",
        defaults: new { area = "Admin", controller = "NhanVien", action = "Login" });
});

app.Run();
