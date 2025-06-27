using AppData.Model;
using APPMVC.IService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;
using System.Linq;
using System.Net;

namespace APPMVC.Service
{
    public class HinhAnhService : IHinhAnhService
    {
        private readonly HttpClient _httpClient;

        public HinhAnhService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7198");
        }

        public async Task<bool> UploadAsync(HinhAnh hinhAnh)
        {
            try
            {
                if (hinhAnh == null)
                    throw new ArgumentNullException(nameof(hinhAnh));

                Console.WriteLine($"Preparing to upload HinhAnh: Id={hinhAnh.IdHinhAnh}, Type={hinhAnh.LoaiFileHinhAnh}, Size={hinhAnh.DataHinhAnh?.Length ?? 0} bytes");

                // Create multipart form content
                var content = new MultipartFormDataContent();

                // Add the image data
                var imageContent = new ByteArrayContent(hinhAnh.DataHinhAnh);
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(hinhAnh.LoaiFileHinhAnh);

                // Add other properties as JSON
                var metadata = new
                {
                    hinhAnh.IdHinhAnh,
                    hinhAnh.LoaiFileHinhAnh,
                    hinhAnh.TrangThai, // Include TrangThai
                    hinhAnh.IdSanPhamChiTiet // Include IdSanPhamChiTiet
                };
                var metadataContent = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(metadata),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                // Get file extension from content type
                var extension = "." + hinhAnh.LoaiFileHinhAnh.Split('/').Last();

                content.Add(imageContent, "file", "image" + extension);
                content.Add(metadataContent, "metadata");

                var response = await _httpClient.PostAsync("api/HinhAnh/UploadHinhAnh", content);

                Console.WriteLine($"Upload response status: {response.StatusCode}");

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response content: {responseContent}");

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error when uploading HinhAnh: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when uploading HinhAnh: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/HinhAnh/DeleteHinhAnh?id={id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error when deleting HinhAnh: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when deleting HinhAnh: {ex.Message}");
                return false;
            }
        }

        public async Task<List<HinhAnh>> GetHinhAnhsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<HinhAnh>>("api/HinhAnh/GetAllHinhAnh") ?? new List<HinhAnh>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error when getting all HinhAnh: {ex.Message}");
                return new List<HinhAnh>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when getting all HinhAnh: {ex.Message}");
                return new List<HinhAnh>();
            }
        }

        public async Task<HinhAnh> GetHinhAnhByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/HinhAnh/GetHinhAnhById?id={id}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsByteArrayAsync();
                return new HinhAnh
                {
                    IdHinhAnh = id,
                    DataHinhAnh = content,
                    LoaiFileHinhAnh = response.Content.Headers.ContentType?.ToString()
                };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error when getting HinhAnh by ID: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when getting HinhAnh by ID: {ex.Message}");
                return null;
            }
        }

        public async Task<List<HinhAnh>> GetHinhAnhsBySanPhamChiTietId(Guid sanPhamChiTietId)
        {
            if (sanPhamChiTietId == Guid.Empty)
            {
                // Log hoặc xử lý việc không có giá trị IdSanPhamChiTiet
                Console.WriteLine("Invalid IdSanPhamChiTiet: Guid.Empty");
                return new List<HinhAnh>(); // Hoặc xử lý phù hợp hơn
            }

            try
            {
                var response = await _httpClient.GetAsync($"api/HinhAnh/GetHinhAnhsBySanPhamChiTietId?sanPhamChiTietId={sanPhamChiTietId}");

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<HinhAnh>>() ?? new List<HinhAnh>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error when getting HinhAnhs by SanPhamChiTietId: {ex.Message}");
                return new List<HinhAnh>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when getting HinhAnhs by SanPhamChiTietId: {ex.Message}");
                return new List<HinhAnh>();
            }
        }
        public async Task<HinhAnh> GetRepresentativeImageByMauSac(Guid sanPhamChiTietId, Guid mauSacId)
        {
            // Get images for the specific SanPhamChiTiet
            var hinhAnhs = await GetHinhAnhsBySanPhamChiTietId(sanPhamChiTietId);

            // Return the first image (you might want to implement a more sophisticated selection method)
            return hinhAnhs.FirstOrDefault();
        }
    }
}