using AppData.Model;
using APPMVC.IService;
using Newtonsoft.Json;

namespace APPMVC.Service
{
    public class HoaDonService : IHoaDonService
    {
        private readonly HttpClient _httpClient;

        public HoaDonService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7198") 
            };
        }

        // Thêm hóa đơn
        public async Task AddAsync(HoaDon hoaDon)
        {
            // Log the HoaDon object
            Console.WriteLine(JsonConvert.SerializeObject(hoaDon));

            var response = await _httpClient.PostAsJsonAsync("api/HoaDon/them", hoaDon);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {content}"); // Log the error details
                response.EnsureSuccessStatusCode(); // This will throw the HttpRequestException
            }
        }

        // Xóa hóa đơn theo ID
        public async Task DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/HoaDon/xoa?id={id}");
            response.EnsureSuccessStatusCode();
        }

        // Lấy tất cả hóa đơn
        public async Task<List<HoaDon>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/HoaDon/getall");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<HoaDon>>();
        }

        // Lấy hóa đơn theo ID
        public async Task<HoaDon> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/HoaDon/getbyid?id={id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<HoaDon>();
        }

        public async Task<HoaDon> GetByMaDonAsync(string maDon)
        {
            var response = await _httpClient.GetAsync($"api/HoaDon/getbymadan?maDon={maDon}");

            if (response.IsSuccessStatusCode)
            {
                var hoaDon = await response.Content.ReadFromJsonAsync<HoaDon>();
                if (hoaDon == null)
                {
                    Console.WriteLine("Không tìm thấy hóa đơn nào với mã đơn này.");
                }
                return hoaDon;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error retrieving HoaDon by MaDon: {content}");
                return null;
            }
        }

        public async Task<HoaDon> GetByOrderNumberAsync(string orderNumber)
        {
            var response = await _httpClient.GetAsync($"api/HoaDon/getbyordernumber?orderNumber={orderNumber}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<HoaDon>();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error retrieving HoaDon by order number: {content}");

                return null; 
            }
        }

        public async Task<List<HoaDon>> GetHoaDonsByCustomerIdAsync(Guid customerId)
        {
            var response = await _httpClient.GetAsync($"api/HoaDon/getbycustomerid?customerId={customerId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<HoaDon>>() ?? new List<HoaDon>();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error retrieving HoaDons by customer ID: {content}");
                return new List<HoaDon>(); 
            }
        }

        public async Task UpdateAsync(HoaDon hoaDon)
        {
            // Log the HoaDon object being updated
            Console.WriteLine(JsonConvert.SerializeObject(hoaDon));

            // Send the PUT request to update the invoice
            var response = await _httpClient.PutAsJsonAsync("api/HoaDon/sua", hoaDon);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Log the error details for debugging
                Console.WriteLine($"Error updating HoaDon: Status Code: {response.StatusCode}, Response: {content}");

                // Throw an exception with detailed information
                throw new Exception($"Failed to update HoaDon. Status code: {response.StatusCode}, Response: {content}");
            }
        }
    }
}
