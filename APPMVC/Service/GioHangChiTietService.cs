using AppData.Model;
using APPMVC.IService;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace APPMVC.Service
{
	public class GioHangChiTietService : IGioHangChiTietService
	{
		private readonly HttpClient _httpClient;

		public GioHangChiTietService()
		{
			_httpClient = new HttpClient
			{
				BaseAddress = new Uri("https://localhost:7198")
			};
		}

		// Thêm giỏ hàng chi tiết
		public async Task AddAsync(GioHangChiTiet gioHangChiTiet)
		{
			var response = await _httpClient.PostAsJsonAsync("api/GioHangChiTiet/them", gioHangChiTiet);
			response.EnsureSuccessStatusCode();
		}

        // Xóa giỏ hàng chi tiết theo ID
        public async Task DeleteAsync(Guid id)
		{
			var response = await _httpClient.DeleteAsync($"api/GioHangChiTiet/xoa?id={id}");
			response.EnsureSuccessStatusCode();
		}

		// Lấy tất cả giỏ hàng chi tiết
		public async Task<List<GioHangChiTiet>> GetAllAsync()
		{
			var response = await _httpClient.GetAsync("api/GioHangChiTiet/getall");
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<List<GioHangChiTiet>>();
		}

        public async Task<List<GioHangChiTiet>> GetByGioHangIdAsync(Guid gioHangId)
        {
            // Kiểm tra xem gioHangId có hợp lệ không
            if (gioHangId == Guid.Empty)
            {
                throw new ArgumentException("Invalid cart ID.", nameof(gioHangId));
            }

            var response = await _httpClient.GetAsync($"api/GioHangChiTiet/getbygiohangid?gioHangId={gioHangId}");

            // Kiểm tra phản hồi
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new List<GioHangChiTiet>();
                }

                throw new Exception($"Error retrieving cart details: {response.ReasonPhrase}. Content: {content}");
            }

            try
            {
                // Đọc nội dung JSON
                var jsonContent = await response.Content.ReadAsStringAsync();

                // Deserialize JSON thành danh sách GioHangChiTiet
                var gioHangChiTiets = JsonConvert.DeserializeObject<List<GioHangChiTiet>>(jsonContent);

                return gioHangChiTiets ?? new List<GioHangChiTiet>();
            }
            catch (Newtonsoft.Json.JsonException jsonEx) // Chỉ định rõ namespace
            {
                throw new Exception("Error parsing JSON response.", jsonEx);
            }
        }

        // Lấy giỏ hàng chi tiết theo ID
        public async Task<GioHangChiTiet> GetByIdAsync(Guid id)
		{
			var response = await _httpClient.GetAsync($"api/GioHangChiTiet/getbyid?id={id}");
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<GioHangChiTiet>();
		}

        public async Task<List<GioHangChiTiet>> GetByIdsAsync(List<Guid> ids)
        {
            // Build the query string with each ID as a separate parameter
            var queryParameters = string.Join("&", ids.Select(id => $"ids={Uri.EscapeDataString(id.ToString())}"));
            var requestUrl = $"api/GioHangChiTiet/getbyids?{queryParameters}";

            // Log the request URL for debugging
            Console.WriteLine("Request URL: " + requestUrl);

            try
            {
                var response = await _httpClient.GetAsync(requestUrl);

                // Check for success status
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<GioHangChiTiet>>();
                    return result ?? new List<GioHangChiTiet>(); // Return an empty list if result is null
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, Response: {responseBody}");
                    return new List<GioHangChiTiet>(); // Return an empty list on failure
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return new List<GioHangChiTiet>(); // Return an empty list on error
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return new List<GioHangChiTiet>(); // Return an empty list on unexpected error
            }
        }

        public async Task<GioHangChiTiet> GetByProductIdAndCartIdAsync(Guid sanPhamChiTietId, Guid cartId)
        {
            if (sanPhamChiTietId == Guid.Empty || cartId == Guid.Empty)
            {
                throw new ArgumentException("Invalid product ID or cart ID.");
            }

            var requestUrl = $"api/GioHangChiTiet/getbyproductidandcartid?sanPhamChiTietId={sanPhamChiTietId}&cartId={cartId}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null; 
                }

                throw new Exception($"Error retrieving cart item: {response.ReasonPhrase}. Content: {content}");
            }

            return await response.Content.ReadFromJsonAsync<GioHangChiTiet>()
                   ?? throw new Exception("Failed to deserialize response to GioHangChiTiet.");
        }

        public async Task<double> GetTotalQuantityBySanPhamChiTietIdAsync(Guid sanPhamChiTietId, Guid cartId)
        {
            var response = await _httpClient.GetAsync($"api/GioHangChiTiet/gettotalquantity?sanPhamChiTietId={sanPhamChiTietId}&cartId={cartId}");

            // Ensure the response is successful
            response.EnsureSuccessStatusCode();

            // Read the total quantity from the response
            var totalQuantity = await response.Content.ReadFromJsonAsync<double>();
            return totalQuantity;
        }

        public async Task RemoveItemsFromCartAsync(Guid cartId, List<Guid> productDetailIds)
        {
            // Kiểm tra xem danh sách productDetailIds có hợp lệ không
            if (productDetailIds == null || !productDetailIds.Any())
            {
                return; // Không làm gì nếu không có ID nào
            }

            // Tạo đối tượng yêu cầu
            var request = new RemoveItemsRequest
            {
                CartId = cartId,
                ProductDetailIds = productDetailIds
            };

            try
            {
                // Gửi yêu cầu POST tới API với body là request
                var response = await _httpClient.PostAsJsonAsync("api/GioHangChiTiet/removeitems", request);

                // Đảm bảo phản hồi thành công
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                throw; // Ném lại ngoại lệ để xử lý ở nơi khác nếu cần
            }
        }

        // Cập nhật giỏ hàng chi tiết
        public async Task UpdateAsync(GioHangChiTiet gioHangChiTiet)
		{
			var response = await _httpClient.PutAsJsonAsync("api/GioHangChiTiet/sua", gioHangChiTiet);
			response.EnsureSuccessStatusCode();
		}
	}
}

public class GioHangChiTiet
{
    public Guid IdGioHangChiTiet { get; set; }
    public double DonGia { get; set; }
    public double SoLuong { get; set; }
    public double TongTien { get; set; }
    public int KichHoat { get; set; }
    public Guid IdGioHang { get; set; }
    public Guid IdSanPhamChiTiet { get; set; }
}
public class RemoveItemsRequest
{
    public Guid CartId { get; set; }
    public List<Guid> ProductDetailIds { get; set; }

}