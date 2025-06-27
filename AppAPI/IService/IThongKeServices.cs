using AppData.ViewModel;

namespace AppAPI.IService
{
    public interface IThongKeServices
    {
        Task<List<ThongKeNgay>> GetStatisticsByDate(DateTime date);
        Task<List<ThongKeNgay>> GetStatisticsByWeek(DateTime date);
        Task<List<ThongKeNgay>> GetStatisticsByMonth(int year, int month);
        Task<List<ThongKeThang>> GetStatisticsByYear(int year);
        Task<ThongKeTongQuan> GetTotalOrdersAndRevenue();
        Task<List<ThongKeKhoangThoiGian>> GetStatisticsByTimeRange(DateTime startDate, DateTime endDate);
        Task<List<TopSellingProductViewModel>> GetTopSellingProductsAsync(DateTime? startDate, DateTime? endDate);
        Task<List<ThongKeDoanhThu>> GetRevenueStatisticsAsync(DateTime startDate, DateTime endDate);
    }
}
