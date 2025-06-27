using AppData.Model;
using APPMVC.IService;

namespace APPMVC.Service
{
    public class PromotionService : IPromotionService
    {
        private readonly HttpClient _httpClient;

        public PromotionService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7198");
        }

        public async Task<bool> CreateAsync(Promotion promotion)
        {
            try
            {
                if (promotion == null)
                    throw new ArgumentNullException(nameof(promotion));

                var response = await _httpClient.PostAsJsonAsync("api/Promotion/CreatePromotion", promotion);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Create failed. Status: {response.StatusCode}, Content: {await response.Content.ReadAsStringAsync()}");
                    return false;
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error when creating promotion: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when creating promotion: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Promotion/DeletePromotion?id={id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error when deleting promotion: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when deleting promotion: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Promotion>> GetPromotionsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Promotion>>("api/Promotion/GetAllPromotions") ?? new List<Promotion>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error when getting all promotions: {ex.Message}");
                return new List<Promotion>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when getting all promotions: {ex.Message}");
                return new List<Promotion>();
            }
        }

        public async Task<Promotion> GetPromotionByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty", nameof(id));
            }

            try
            {
                var response = await _httpClient.GetAsync($"api/Promotion/GetPromotionById?id={id}");
                response.EnsureSuccessStatusCode();

                // Nếu không có nội dung, trả về một đối tượng khuyến mãi rỗng
                var promotion = await response.Content.ReadFromJsonAsync<Promotion>();
                return promotion ?? new Promotion(); // Trả về một đối tượng khuyến mãi rỗng nếu promotion là null
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error when getting promotion by ID: {ex.Message}");
                return new Promotion(); // Trả về một đối tượng khuyến mãi rỗng trong trường hợp lỗi
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when getting promotion by ID: {ex.Message}");
                return new Promotion(); // Trả về một đối tượng khuyến mãi rỗng trong trường hợp lỗi khác
            }
        }

        public async Task<bool> UpdateAsync(Promotion promotion)
        {
            try
            {
                if (promotion == null)
                    throw new ArgumentNullException(nameof(promotion));

                var response = await _httpClient.PutAsJsonAsync($"api/Promotion/UpdatePromotion/{promotion.IdPromotion}", promotion);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Update failed. Status: {response.StatusCode}, Content: {await response.Content.ReadAsStringAsync()}");
                    return false;
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error when updating promotion: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when updating promotion: {ex.Message}");
                return false;
            }
        }
    }
}
