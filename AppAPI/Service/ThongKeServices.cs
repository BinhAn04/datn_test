using AppAPI.IRepository;
using AppAPI.IService;
using AppData.ViewModel;

namespace AppAPI.Service
{
    public class ThongKeServices : IThongKeServices
    {
        private readonly IThongKeRepo _repository;
        public ThongKeServices(IThongKeRepo thongKeRepo)
        {
            _repository = thongKeRepo;
        }
        public async Task<List<ThongKeNgay>> GetStatisticsByDate(DateTime date)
        {
            return await _repository.GetStatisticsByDate(date);
        }
        public async Task<List<ThongKeNgay>> GetStatisticsByWeek(DateTime date)
        {
            return await _repository.GetStatisticsByWeek(date);
        }
        public async Task<List<ThongKeNgay>> GetStatisticsByMonth(int year, int month)
        {
            return await _repository.GetStatisticsByMonth(year, month);
        }
        public async Task<List<ThongKeThang>> GetStatisticsByYear(int year)
        {
            return await _repository.GetStatisticsByYear(year);
        }
        public async Task<ThongKeTongQuan> GetTotalOrdersAndRevenue()
        {
            return await _repository.GetTotalOrdersAndRevenue();
        }
        public async Task<List<ThongKeKhoangThoiGian>> GetStatisticsByTimeRange(DateTime startDate, DateTime endDate)
        {
            return await _repository.GetStatisticsByTimeRange(startDate, endDate);
        }
        public async Task<List<TopSellingProductViewModel>> GetTopSellingProductsAsync(DateTime? startDate, DateTime? endDate)
        {
            return await _repository.GetTopSellingProductsAsync(startDate, endDate);
        }
        public async Task<List<ThongKeDoanhThu>> GetRevenueStatisticsAsync(DateTime startDate, DateTime endDate)
        {
            return await _repository.GetRevenueStatisticsAsync(startDate,endDate);
        }
    }
}
