using AppData.Model;
using APPMVC.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace APPMVC.Service
{
    public class NhanVienService : INhanVienService
    {
        HttpClient _client;
        public NhanVienService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7198");
        }
        public async Task CreateNV(NhanVien nv)
        {
            var response = await _client.PostAsJsonAsync("/api/NhanVien/CreateNhanVien", nv);
            response.EnsureSuccessStatusCode(); // Ném ngoại lệ nếu không thành công
        }

        public async Task DeleteNV(Guid id)
        {
            await _client.DeleteAsync($"api/NhanVien/Xoa?id={id}");
        }

        public async Task<List<NhanVien>> GetAllNhaVien()
        {
            var repo = await _client.GetFromJsonAsync<List<NhanVien>>("/api/NhanVien/GetAllNhanVien");
            return repo;
        }

        public Task<NhanVien> GetIdNhanVien(Guid id)
        {
			var nv = _client.GetFromJsonAsync<NhanVien>($"api/NhanVien/GetIDNhanVien?id={id}");
			return nv;
		}

        public async Task UpdateNV(NhanVien nv)
        {
			await _client.PutAsJsonAsync("api/NhanVien/Sua", nv);
		}
        public async Task UpdateThongTin(NhanVien nv)
        {
            await _client.PutAsJsonAsync("api/NhanVien/SuaChoNhanVien", nv);
        }
        public async Task Delete(Guid id)
		{
			await _client.DeleteAsync($"api/NhanVien/Xoa?id={id}");
		}
		public async Task<NhanVien?> LoginKH(string email, string password)
        {
            var response = await _client.PostAsJsonAsync("/api/NhanVien/Login", new { Email = email, MatKhau = password });
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                // Deserializing chuỗi JSON thành đối tượng KhachHang
                return JsonConvert.DeserializeObject<NhanVien>(responseContent);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
        }
        public async Task<bool> DoiMK(Guid id, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                return false;
            }
            var response = await _client.PostAsync($"api/NhanVien/ChangePassword?id={id}&newPassword={newPassword}&confirmPassword={confirmPassword}", new StringContent(JsonConvert.SerializeObject(new { newPassword }), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RestPassword(string email, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                return false;
            }
            var response = await _client.PostAsync($"api/NhanVien/RestPassword?email={email}&newPassword={newPassword}&confirmPassword={confirmPassword}", new StringContent(JsonConvert.SerializeObject(new { newPassword }), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendVerificationCode(string email)
        {
            var content = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/SendCode/SendVerificationCode", content);
            return response.IsSuccessStatusCode;
        }
        public async Task<string> GetVerificationCodeFromRedisAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Email is empty in GetVerificationCodeFromRedisAsync.");
                return null;
            }
            var encodedEmail = Uri.EscapeDataString(email); // Mã hóa email
            var response = await _client.GetAsync($"/api/SendCode/GetVerificationCode?email={encodedEmail}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error retrieving verification code: {response.StatusCode}, Details: {errorContent}");
            return null;
        }
        public async Task<bool> CheckSDT(string soDienThoai)
        {
            if (string.IsNullOrEmpty(soDienThoai))
            {
                throw new ArgumentException("Số điện thoại không được để trống");

            }
            var response = await _client.GetAsync($"/api/NhanVien/CheckSDT?soDienThoai={soDienThoai}");
            return response.IsSuccessStatusCode && bool.Parse(await response.Content.ReadAsStringAsync());
        }
        public async Task<bool> CheckMail(string mail)
        {
            if (string.IsNullOrEmpty(mail))
            {
                throw new ArgumentException("Gmail không được để trống");

            }
            var response = await _client.GetAsync($"/api/NhanVien/CheckMail?mail={Uri.EscapeDataString(mail)}");
            return response.IsSuccessStatusCode && bool.Parse(await response.Content.ReadAsStringAsync());
        }
        public async Task<List<NhanVien>> SearchNhanVien(string? name)
        {
            var repo = await _client.GetFromJsonAsync<List<NhanVien>>($"/api/NhanVien/SearchNhanVien?name={name}");
            return repo;
        }
    }
}
