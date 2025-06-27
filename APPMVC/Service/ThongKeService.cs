using AppData.ViewModel;
using APPMVC.IService;
using Newtonsoft.Json;

namespace APPMVC.Service
{
    public class ThongKeService : IThongKeService
    {
        private readonly HttpClient _httpClient;
        public ThongKeService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7198");
        }
        public async Task<List<ThongKeNgay>> GetStatisticsByDate(DateTime date)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/ThongKe/ThongKeTheoNgay?date={date:yyyy-MM-dd}");

                // Kiểm tra trạng thái phản hồi
                response.EnsureSuccessStatusCode();

                // Đọc và trả về danh sách thống kê
                var statistics = await response.Content.ReadFromJsonAsync<List<ThongKeNgay>>();

                return statistics ?? new List<ThongKeNgay>(); // Trả về danh sách rỗng nếu không có dữ liệu
            }
            catch (HttpRequestException ex)
            {
                // Xử lý lỗi HTTP
                Console.WriteLine($"Error fetching statistics: {ex.Message}");
                return new List<ThongKeNgay>(); // Trả về danh sách rỗng trong trường hợp lỗi
            }
        }
        public async Task<List<ThongKeNgay>> GetStatisticsByWeek(DateTime date)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/ThongKe/ThongKeTheoTuan?date={date:yyyy-MM-dd}");
                response.EnsureSuccessStatusCode();
                var statistics = await response.Content.ReadFromJsonAsync<List<ThongKeNgay>>();
                return statistics ?? new List<ThongKeNgay>(); // Trả về danh sách rỗng nếu không có dữ liệu
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching statistics by week: {ex.Message}");
                return new List<ThongKeNgay>(); // Trả về danh sách rỗng trong trường hợp lỗi
            }
        }

        public async Task<List<ThongKeNgay>> GetStatisticsByMonth(int year, int month)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/ThongKe/ThongKeTheoThang?year={year}&month={month}");
                response.EnsureSuccessStatusCode();
                var statistics = await response.Content.ReadFromJsonAsync<List<ThongKeNgay>>();
                return statistics ?? new List<ThongKeNgay>(); // Trả về danh sách rỗng nếu không có dữ liệu
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching statistics by month: {ex.Message}");
                return new List<ThongKeNgay>(); // Trả về danh sách rỗng trong trường hợp lỗi
            }
        }

        public async Task<List<ThongKeThang>> GetStatisticsByYear(int year)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/ThongKe/ThongKeTheoNam?year={year}");
                response.EnsureSuccessStatusCode();
                var statistics = await response.Content.ReadFromJsonAsync<List<ThongKeThang>>();
                return statistics ?? new List<ThongKeThang>(); // Trả về danh sách rỗng nếu không có dữ liệu
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching statistics by year: {ex.Message}");
                return new List<ThongKeThang>(); // Trả về danh sách rỗng trong trường hợp lỗi
            }
        }

        public async Task<ThongKeTongQuan> GetTotalOrdersAndRevenue()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/ThongKe/TongQuan");
                response.EnsureSuccessStatusCode();
                var total = await response.Content.ReadFromJsonAsync<ThongKeTongQuan>();
                return total ?? new ThongKeTongQuan(); // Trả về giá trị mặc định nếu không tìm thấy
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching total orders and revenue: {ex.Message}");
                return new ThongKeTongQuan(); // Trả về giá trị mặc định trong trường hợp lỗi
            }
        }

        public async Task<List<ThongKeKhoangThoiGian>> GetStatisticsByTimeRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/ThongKe/ThongKeTheoKhoangThoiGian?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                response.EnsureSuccessStatusCode();
                var statistics = await response.Content.ReadFromJsonAsync<List<ThongKeKhoangThoiGian>>();
                return statistics ?? new List<ThongKeKhoangThoiGian>(); // Trả về danh sách rỗng nếu không có dữ liệu
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching statistics by time range: {ex.Message}");
                return new List<ThongKeKhoangThoiGian>(); // Trả về danh sách rỗng trong trường hợp lỗi
            }
        }

        public async Task<List<TopSellingProductViewModel>> GetTopSellingProductsAsync(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/ThongKe/TopSellingProducts?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                response.EnsureSuccessStatusCode();
                var products = await response.Content.ReadFromJsonAsync<List<TopSellingProductViewModel>>();
                return products ?? new List<TopSellingProductViewModel>(); // Trả về danh sách rỗng nếu không có dữ liệu
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching top selling products: {ex.Message}");
                return new List<TopSellingProductViewModel>(); // Trả về danh sách rỗng trong trường hợp lỗi
            }
        }
        public async Task<List<ThongKeDoanhThu>> GetRevenueStatisticsAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/ThongKe/ThongKeDoanThu?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                response.EnsureSuccessStatusCode();
                var products = await response.Content.ReadFromJsonAsync<List<ThongKeDoanhThu>>();
                return products ?? new List<ThongKeDoanhThu>(); // Trả về danh sách rỗng nếu không có dữ liệu
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Bùm: {ex.Message}");
                return new List<ThongKeDoanhThu>(); // Trả về danh sách rỗng trong trường hợp lỗi
            }
        }
    }
}
