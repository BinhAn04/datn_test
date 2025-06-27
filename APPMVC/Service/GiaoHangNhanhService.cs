using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GiaoHangNhanhService
{
    private readonly HttpClient _httpClient;

    public GiaoHangNhanhService(HttpClient httpClient, string apiToken, string shopId)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Token", apiToken);
        _httpClient.DefaultRequestHeaders.Add("ShopId", shopId);
    }

    public async Task<double> CalculateShippingFee(int fromDistrictId, string fromWardCode, int toDistrictId, string toWardCode, double height, double length, double totalWeight, double width, int serviceTypeId, string itemName, int itemQuantity)
    {
        // Kiểm tra đầu vào
        if (height <= 0 || length <= 0 || totalWeight <= 0 || width <= 0)
            throw new ArgumentException("Chiều cao, chiều dài, trọng lượng và chiều rộng phải lớn hơn 0.");

        var requestData = new
        {
            service_type_id = serviceTypeId,
            from_district_id = fromDistrictId,
            from_ward_code = fromWardCode,
            to_district_id = toDistrictId,
            to_ward_code = toWardCode,
            height = (int)height,
            length = (int)length,
            weight = (int)totalWeight, 
            width = (int)width,
            insurance_value = 0,
            coupon = (string)null,
            items = new[]
            {
                new
                {
                    name = itemName, 
                    quantity = itemQuantity,
                    height = (int)height,
                    weight = (int)(totalWeight / itemQuantity), 
                    length = (int)length,
                    width = (int)width
                }
            }
        };

        var json = JsonConvert.SerializeObject(requestData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await _httpClient.PostAsync("https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee", content);

            if (response.IsSuccessStatusCode)
            {
                var shipInfo = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(shipInfo);
                return Convert.ToDouble(jsonResponse["data"]["total"]);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Lỗi khi gửi yêu cầu: {response.ReasonPhrase}. Nội dung lỗi: {errorContent}");
                throw new Exception($"Không thể lấy thông tin: {response.ReasonPhrase}. Nội dung lỗi: {errorContent}");
            }
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"Lỗi khi gửi yêu cầu: {httpEx.Message}");
            throw new Exception($"Đã xảy ra lỗi khi gửi yêu cầu: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
            throw;
        }
    }
}