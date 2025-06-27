using AppData.Model;
using APPMVC.IService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace APPMVC.Service
{
    public class LichSuThanhToanService : ILichSuThanhToanService
    {
        private readonly HttpClient _httpClient;

        public LichSuThanhToanService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7198") 
            };
        }

        // Thêm lịch sử thanh toán
        public async Task AddAsync(LichSuThanhToan lichSuThanhToan)
        {
            var response = await _httpClient.PostAsJsonAsync("api/LichSuThanhToan/them", lichSuThanhToan);
            response.EnsureSuccessStatusCode();
        }

        // Xóa lịch sử thanh toán theo ID
        public async Task DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/LichSuThanhToan/xoa?id={id}");
            response.EnsureSuccessStatusCode();
        }

        // Lấy tất cả lịch sử thanh toán
        public async Task<List<LichSuThanhToan>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/LichSuThanhToan/getall");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<LichSuThanhToan>>();
        }

        // Lấy lịch sử thanh toán theo ID
        public async Task<LichSuThanhToan> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/LichSuThanhToan/getbyid?id={id}");
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {

                    return new LichSuThanhToan(); 
                }
                response.EnsureSuccessStatusCode(); // Ném ngoại lệ cho các lỗi khác
            }
            return await response.Content.ReadFromJsonAsync<LichSuThanhToan>();
        }

        // Lấy lịch sử thanh toán theo ID hóa đơn
        public async Task<List<LichSuThanhToan>> GetByIdHoaDonAsync(Guid idHoaDon)
        {
            var response = await _httpClient.GetAsync($"api/LichSuThanhToan/getbyidhoadon?idHoaDon={idHoaDon}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<LichSuThanhToan>();
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<LichSuThanhToan>>();
        }

        // Cập nhật lịch sử thanh toán
        public async Task UpdateAsync(LichSuThanhToan lichSuThanhToan)
        {
            var response = await _httpClient.PutAsJsonAsync("api/LichSuThanhToan/sua", lichSuThanhToan);
            response.EnsureSuccessStatusCode();
        }
    }
}