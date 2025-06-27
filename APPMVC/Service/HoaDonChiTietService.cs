using AppData.Model;
using APPMVC.IService;
using System.Net;

namespace APPMVC.Service
{
    public class HoaDonChiTietService : IHoaDonChiTietService
    {
        private readonly HttpClient _httpClient;

        public HoaDonChiTietService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7198") 
            };
        }

        public async Task AddAsync(List<HoaDonChiTiet> hoaDonChiTietList)
        {
            if (hoaDonChiTietList == null || !hoaDonChiTietList.Any())
            {
                throw new ArgumentException("The list of HoaDonChiTiet cannot be null or empty.", nameof(hoaDonChiTietList));
            }

            try
            {
                // Send a POST request with the list of HoaDonChiTiet
                var response = await _httpClient.PostAsJsonAsync("api/HoaDonChiTiet/them", hoaDonChiTietList);

                // Check if the response indicates success
                if (!response.IsSuccessStatusCode)
                {
                    // Log the response content for debugging
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");

                    // Optionally, you can throw an exception with a more descriptive message
                    throw new HttpRequestException($"Failed to add HoaDonChiTiet: {response.StatusCode} - {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Request error: {ex.Message}");
                throw; // Rethrow the exception to be handled by the caller
            }
            catch (Exception ex)
            {
                // Handle any other exceptions that may occur
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw; // Rethrow the exception to maintain the stack trace
            }
        }

        // Xóa chi tiết hóa đơn theo ID
        public async Task DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/HoaDonChiTiet/xoa?id={id}");
            response.EnsureSuccessStatusCode();
        }

        // Lấy tất cả chi tiết hóa đơn
        public async Task<List<HoaDonChiTiet>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/HoaDonChiTiet/getall");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<HoaDonChiTiet>>();
        }

        // Lấy chi tiết hóa đơn theo ID
        public async Task<HoaDonChiTiet> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/HoaDonChiTiet/getbyid?id={id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<HoaDonChiTiet>();
        }

        public async Task<List<HoaDonChiTiet>> GetByIdHoaDonAsync(Guid idHoaDon)
        {
            var response = await _httpClient.GetAsync($"api/HoaDonChiTiet/getbyidhd?idhoadon={idHoaDon}");
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    // Xử lý trường hợp không tìm thấy
                    return new List<HoaDonChiTiet>(); // Trả về danh sách rỗng
                }
                response.EnsureSuccessStatusCode(); // Ném ngoại lệ cho các lỗi khác
            }
            return await response.Content.ReadFromJsonAsync<List<HoaDonChiTiet>>();
        }

        // Cập nhật chi tiết hóa đơn
        public async Task UpdateAsync(List<HoaDonChiTiet> hoaDonChiTietList)
        {
            var response = await _httpClient.PutAsJsonAsync("api/HoaDonChiTiet/sua", hoaDonChiTietList);
            response.EnsureSuccessStatusCode();
        }

        public async Task<double> GetTotalQuantityBySanPhamChiTietIdAsync(Guid sanPhamChiTietId, Guid hDCTId)
        {
            var response = await _httpClient.GetAsync($"api/HoaDonChiTiet/gettotalquantity?sanPhamChiTietId={sanPhamChiTietId}&cartId={hDCTId}");

            // Ensure the response is successful
            response.EnsureSuccessStatusCode();

            // Read the total quantity from the response
            var totalQuantity = await response.Content.ReadFromJsonAsync<double>();
            return totalQuantity;
        }

        public async Task<HoaDonChiTiet> GetByIdAndProduct(Guid idHoaDon, Guid idSanPhamChiTiet)
        {
            // Validate the input IDs
            if (idHoaDon == Guid.Empty || idSanPhamChiTiet == Guid.Empty)
            {
                throw new ArgumentException("Invalid order ID or product detail ID.");
            }

            var requestUrl = $"api/HoaDonChiTiet/getbyidandproduct?idhoadon={idHoaDon}&idsanphamchitiet={idSanPhamChiTiet}";

            var response = await _httpClient.GetAsync(requestUrl);

            // Check if the response is successful
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null; // Return null if not found
                }

                // Throw an exception for other status codes
                throw new Exception($"Error retrieving order detail: {response.ReasonPhrase}. Content: {content}");
            }

            // Read and return the HoaDonChiTiet from the response
            return await response.Content.ReadFromJsonAsync<HoaDonChiTiet>()
                   ?? throw new Exception("Failed to deserialize response to HoaDonChiTiet.");
        }
    }
}
