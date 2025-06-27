using AppData.Model;
using APPMVC.IService;
using System.Text.Json;

namespace APPMVC.Service
{
    public class VoucherService : IVoucherService
    {
        private readonly HttpClient _httpClient;
        public VoucherService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7198");
        }

        public async Task<bool> CreateAsync(VoucherDto voucher, List<Guid> selectedKhachHangIds)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Voucher/CreateVoucher?selectedKhachHangIds=" + string.Join("&selectedKhachHangIds=", selectedKhachHangIds), voucher);
                var content = await response.Content.ReadAsStringAsync();

                // Log response
                Console.WriteLine($"Create Response: Status: {response.StatusCode}, Content: {content}");

                if (!response.IsSuccessStatusCode)
                {
                    // Log details when unsuccessful
                    Console.WriteLine($"Failed to create voucher. Status: {response.StatusCode}, Content: {content}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Voucher/DeleteVoucher?id={id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex) 
            {
                Console.WriteLine($"HTTP request error when deleting voucher: {ex.Message}");
                return false;
            }catch(Exception ex)
            {
                Console.WriteLine($"Error when deleting voucher: {ex.Message}");
                return false;
            }
        }

        public async Task<Voucher> GetVoucherByIdAsync(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty", nameof(id));
            }

            try
            {
                var response = await _httpClient.GetAsync($"api/Voucher/GetVoucherById?id={id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Voucher>(); 
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error when getting voucher by ID: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when getting voucher by ID: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Voucher>> GetVouchersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Voucher>>("api/Voucher/GetAllVouchers") ?? new List<Voucher>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error when getting all vouchers: {ex.Message}");
                return new List<Voucher>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when getting all vouchers: {ex.Message}");
                return new List<Voucher>();
            }
        }

        public async Task<bool> UpdateAsync(Voucher voucher)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Voucher/UpdateVoucher/{voucher.VoucherId}", voucher);

                // Add debug logging
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Update Response: Status: {response.StatusCode}, Content: {content}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Update failed with status code: {response.StatusCode}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in VoucherService.UpdateAsync: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> UpdateVoucherStatusAsync(Voucher voucher, int status)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Voucher/UpdateVoucherStatusAsync/{voucher.VoucherId}?status={status}", voucher);

                // Add debug logging
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Update Response: Status: {response.StatusCode}, Content: {content}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Update failed with status code: {response.StatusCode}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in VoucherService.UpdateAsync: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> AddLichSuSuDungVoucherAsync(LichSuSuDungVoucher lichSuSuDungVoucher)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Voucher/AddLichSuSuDungVoucher", lichSuSuDungVoucher);

                // Log response
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Create Response: Status: {response.StatusCode}, Content: {content}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAsync: {ex.Message}");
                return false;
            }
        }
        public async Task<List<KhachHang>> GetKhachHangDaNhanVoucherAsync(Guid voucherId)
        {
            var response = await _httpClient.GetAsync($"/api/Voucher/GetKHDaNhanVoucher?voucherId={voucherId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<List<KhachHang>>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });        
        }

        public async Task<List<Voucher>> GetAvailableVouchersForCustomerAsync(Guid khachHangId)
        {
            try
            {
                Console.WriteLine($"Requesting available vouchers for customer ID: {khachHangId}");

                var response = await _httpClient.GetAsync($"api/Voucher/available/{khachHangId}");

                Console.WriteLine($"Response Status: {response.StatusCode}");

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {content}");

                response.EnsureSuccessStatusCode();

                var vouchers = await response.Content.ReadFromJsonAsync<List<Voucher>>() ?? new List<Voucher>();

                Console.WriteLine($"Parsed Vouchers Count: {vouchers.Count}");

                return vouchers;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return new List<Voucher>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return new List<Voucher>();
            }
        }
        public async Task<bool> CheckMaVoucher(string maVoucher)
        {
            if (string.IsNullOrEmpty(maVoucher))
            {
                throw new ArgumentException("Mã Voucher không được để trống");

            }
            var response = await _httpClient.GetAsync($"/api/Voucher/checkMaVoucher?maVoucher={Uri.EscapeDataString(maVoucher)}");
            return response.IsSuccessStatusCode && bool.Parse(await response.Content.ReadAsStringAsync());
        }
    }
}
