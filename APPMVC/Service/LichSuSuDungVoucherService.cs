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
    public class LichSuSuDungVoucherService : ILichSuSuDungVoucherService
    {
        private readonly HttpClient _httpClient;

        public LichSuSuDungVoucherService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7198")
            };
        }

        // Thêm lịch sử sử dụng voucher
        public async Task AddAsync(LichSuSuDungVoucher lichSuSuDungVoucher)
        {
            var response = await _httpClient.PostAsJsonAsync("api/LichSuSuDungVoucher/them", lichSuSuDungVoucher);
            response.EnsureSuccessStatusCode();
        }

        // Xóa lịch sử sử dụng voucher theo ID
        public async Task DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/LichSuSuDungVoucher/xoa?id={id}");
            response.EnsureSuccessStatusCode();
        }

        // Lấy tất cả lịch sử sử dụng voucher
        public async Task<List<LichSuSuDungVoucher>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/LichSuSuDungVoucher/getall");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<LichSuSuDungVoucher>>();
        }

        // Lấy lịch sử sử dụng voucher theo ID
        public async Task<LichSuSuDungVoucher> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/LichSuSuDungVoucher/getbyid?id={id}");
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new LichSuSuDungVoucher();
                }
                response.EnsureSuccessStatusCode(); // Ném ngoại lệ cho các lỗi khác
            }
            return await response.Content.ReadFromJsonAsync<LichSuSuDungVoucher>();
        }

        // Cập nhật lịch sử sử dụng voucher
        public async Task UpdateAsync(LichSuSuDungVoucher lichSuSuDungVoucher)
        {
            var response = await _httpClient.PutAsJsonAsync("api/LichSuSuDungVoucher/sua", lichSuSuDungVoucher);
            response.EnsureSuccessStatusCode();
        }
    }
}