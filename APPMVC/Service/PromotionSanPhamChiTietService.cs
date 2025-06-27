using AppData.Model;
using APPMVC.IService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class PromotionSanPhamChiTietService : IPromotionSanPhamChiTietService
    {
        private readonly HttpClient _httpClient;

        public PromotionSanPhamChiTietService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7198")
            };
        }

        public async Task<List<PromotionSanPhamChiTiet>> GetPromotionSanPhamChiTietAsync(Guid idSanPhamChiTiet)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PromotionSanPhamChiTiet>>("api/PromotionSanPhamChiTiet");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching promotions: {ex.Message}");
                return new List<PromotionSanPhamChiTiet>(); // Return an empty list or handle accordingly
            }
        }

        public async Task<PromotionSanPhamChiTiet> GetPromotionByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<PromotionSanPhamChiTiet>($"api/PromotionSanPhamChiTiet/getbyid?id={id}");
        }

        public async Task<bool> CreateAsync(PromotionSanPhamChiTiet promotion)
        {
            var response = await _httpClient.PostAsJsonAsync("api/PromotionSanPhamChiTiet/them", promotion);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(PromotionSanPhamChiTiet promotion)
        {
            var response = await _httpClient.PutAsJsonAsync("api/PromotionSanPhamChiTiet/Sua", promotion);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/PromotionSanPhamChiTiet/Xoa?id={id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Guid?> GetPromotionsBySanPhamChiTietIdAsync(Guid sanPhamChiTietId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Guid?>($"api/PromotionSanPhamChiTiet/promotionsBySanPhamChiTietId?sanPhamChiTietId={sanPhamChiTietId}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching promotions by SanPhamChiTietId: {ex.Message}");
                return null; // Return null or handle accordingly
            }
        }

        public async Task<List<PromotionSanPhamChiTiet>> GetPromotionSanPhamChiTietsByPromotionIdAsync(Guid promotionId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PromotionSanPhamChiTiet>>($"api/PromotionSanPhamChiTiet/promotionsById?promotionId={promotionId}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching SanPhamChiTiets by PromotionId: {ex.Message}");
                return new List<PromotionSanPhamChiTiet>(); // Return an empty list or handle accordingly
            }
        }
    }
}