using APPMVC.IService;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class PromotionStatusUpdaterService : IHostedService, IDisposable
{
    private readonly IPromotionService _promotionService;
    private readonly ISanPhamChiTietService _sanPhamChiTietService;
    private readonly IPromotionSanPhamChiTietService _promotionSanPhamChiTietService;
    private readonly ILogger<PromotionStatusUpdaterService> _logger;
    private Timer _timer;

    public PromotionStatusUpdaterService(IPromotionService promotionService,
                                         ISanPhamChiTietService sanPhamChiTietService,
                                         ILogger<PromotionStatusUpdaterService> logger,
                                         IPromotionSanPhamChiTietService promotionSanPhamChiTietService)
    {
        _promotionService = promotionService;
        _sanPhamChiTietService = sanPhamChiTietService;
        _logger = logger;
        _promotionSanPhamChiTietService = promotionSanPhamChiTietService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(UpdatePromotionStatus, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        return Task.CompletedTask;
    }

    private async void UpdatePromotionStatus(object state)
    {
        try
        {
            var promotions = await _promotionService.GetPromotionsAsync();
            var currentDateTime = DateTime.Now;

            foreach (var promotion in promotions)
            {
                if (promotion.NgayKetThuc < currentDateTime && promotion.TrangThai == 1)
                {
                    // Cập nhật trạng thái khuyến mãi thành không hoạt động
                    promotion.TrangThai = 0;
                    await _promotionService.UpdateAsync(promotion);
                    _logger.LogInformation($"Updated promotion status to inactive for Id: {promotion.IdPromotion}");

                    // Lấy danh sách sản phẩm chi tiết liên quan đến khuyến mãi
                    var promotionSanPhamChiTiets = await _promotionSanPhamChiTietService.GetPromotionSanPhamChiTietsByPromotionIdAsync(promotion.IdPromotion);
                    foreach (var promotionSanPhamChiTiet in promotionSanPhamChiTiets)
                    {
                        // Lấy sản phẩm chi tiết dựa trên Id
                        var sanPhamChiTiet = await _sanPhamChiTietService.GetSanPhamChiTietById(promotionSanPhamChiTiet.IdSanPhamChiTiet);
                        if (sanPhamChiTiet != null)
                        {
                            sanPhamChiTiet.GiaGiam = 0;
                            await _sanPhamChiTietService.Update(sanPhamChiTiet);
                            _logger.LogInformation($"Updated discount price to 0 for product detail Id: {sanPhamChiTiet.IdSanPhamChiTiet}");
                        }
                    }
                }
                else if (promotion.TrangThai == 2 && promotion.NgayBatDau <= currentDateTime)
                {
                    // Cập nhật trạng thái khuyến mãi thành hoạt động
                    promotion.TrangThai = 1;
                    await _promotionService.UpdateAsync(promotion);
                    _logger.LogInformation($"Updated promotion status to active for Id: {promotion.IdPromotion}");

                    // Cập nhật giá giảm cho các sản phẩm chi tiết liên quan
                    var promotionSanPhamChiTiets = await _promotionSanPhamChiTietService.GetPromotionSanPhamChiTietsByPromotionIdAsync(promotion.IdPromotion);
                    foreach (var promotionSanPhamChiTiet in promotionSanPhamChiTiets)
                    {
                        var sanPhamChiTiet = await _sanPhamChiTietService.GetSanPhamChiTietById(promotionSanPhamChiTiet.IdSanPhamChiTiet);
                        if (sanPhamChiTiet != null)
                        {
                            // Cập nhật giá giảm dựa trên phần trăm giảm giá của khuyến mãi
                            double originalPrice = sanPhamChiTiet.Gia;
                            double discountPercentage = promotion.PhanTramGiam;
                            sanPhamChiTiet.GiaGiam = originalPrice * (1 - (discountPercentage / 100.0));
                            await _sanPhamChiTietService.Update(sanPhamChiTiet);
                            _logger.LogInformation($"Updated discount price for product detail Id: {sanPhamChiTiet.IdSanPhamChiTiet} to {sanPhamChiTiet.GiaGiam}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating promotion statuses");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}