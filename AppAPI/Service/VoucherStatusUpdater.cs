//using AppAPI.Hubs;
//using AppData;
//using Microsoft.AspNetCore.SignalR;

//namespace AppAPI.Service
//{
//    public class VoucherStatusUpdater : IHostedService, IDisposable
//    {
//        private Timer _timer;
//        private readonly IServiceScopeFactory _scopeFactory;
//        private readonly IHubContext<VoucherHub> _hubContext;

//        public VoucherStatusUpdater(IServiceScopeFactory scopeFactory, IHubContext<VoucherHub> hubContext)
//        {
//            _scopeFactory = scopeFactory;
//            _hubContext = hubContext;
//        }

//        public Task StartAsync(CancellationToken cancellationToken)
//        {
//            // Khởi động timer để kiểm tra trạng thái voucher mỗi phút
//            _timer = new Timer(UpdateVoucherStatus, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
//            return Task.CompletedTask;
//        }

//        private async void UpdateVoucherStatus(object state)
//        {
//            using (var scope = _scopeFactory.CreateScope())
//            {
//                var context = scope.ServiceProvider.GetRequiredService<AppDbcontext>();

//                var now = DateTime.Now;

//                // Lấy danh sách voucher cần cập nhật trạng thái
//                var vouchersToUpdate = context.vouchers
//                    .Where(v => v.TrangThai == 1 && v.NgayBatDau <= now && v.NgayKetThuc >= now) // 1 = Chờ kích hoạt
//                    .ToList();

//                foreach (var voucher in vouchersToUpdate)
//                {
//                    voucher.TrangThai = 2; // 2 = Đang kích hoạt
//                    // Gửi thông báo đến tất cả client
//                    await _hubContext.Clients.All.SendAsync("VoucherStatusUpdated", voucher.VoucherId);
//                }

//                // Lưu thay đổi vào cơ sở dữ liệu
//                context.SaveChanges();
//            }
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            // Dừng timer khi ứng dụng dừng
//            _timer?.Change(Timeout.Infinite, 0);
//            return Task.CompletedTask;
//        }

//        public void Dispose()
//        {
//            // Giải phóng tài nguyên
//            _timer?.Dispose();
//        }
//    }
//}
