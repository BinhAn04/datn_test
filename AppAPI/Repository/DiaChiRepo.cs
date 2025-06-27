using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace AppAPI.Repository
{
	public class DiaChiRepo : IDiaChiRepo
	{
		private readonly AppDbcontext _context;
		private readonly IHttpClientFactory _httpClientFactory;
		public DiaChiRepo(AppDbcontext context, IHttpClientFactory httpClientFactory)
		{
			_context = context;
			_httpClientFactory = httpClientFactory;
		}
		public async Task<List<DiaChi>> GetAll()
		{
			return await _context.diaChi.ToListAsync();
		}
        public async Task<List<DiaChi>> GetByIdKh(Guid idKhachHang)
        {
            if (idKhachHang == Guid.Empty)
            {
                throw new ArgumentException("ID khách hàng không hợp lệ.", nameof(idKhachHang));
            }

            try
            {
                return await _context.diaChi
                    .Where(x => x.IdKhachHang == idKhachHang)
                    .AsNoTracking() 
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log lỗi hoặc xử lý theo cách bạn muốn
                throw new Exception("Có lỗi xảy ra khi lấy địa chỉ.", ex);
            }
        }
        public async Task<DiaChi> GetByIdAsync(Guid idDiaChi)
		{
			var Getdc = _context.diaChi.Find(idDiaChi);
			return Getdc;
		}
		public async Task<DiaChi> AddAsync(DiaChi diaChi)
		{
			if (diaChi.DiaChiMacDinh)
			{
                var existingDefaultAddresses = await _context.diaChi
            .Where(d => d.IdKhachHang == diaChi.IdKhachHang && d.DiaChiMacDinh)
            .ToListAsync();

                foreach (var address in existingDefaultAddresses)
                {
                    address.DiaChiMacDinh = false;
                }
            }
			_context.diaChi.Add(diaChi);
			await _context.SaveChangesAsync();
			return diaChi;
		}
		public async Task<DiaChi> UpdateAsync(DiaChi diaChi)
		{
			var existingDiaChi = await _context.diaChi.FindAsync(diaChi.IdDiaChi);
			if (existingDiaChi == null)
			{
				return null; // Không tìm thấy địa chỉ
			}

			// Cập nhật các trường dữ liệu trong đối tượng hiện tại
			existingDiaChi.HoTen = diaChi.HoTen;
			existingDiaChi.SoDienThoai = diaChi.SoDienThoai;
			existingDiaChi.ProvinceId = diaChi.ProvinceId;
			existingDiaChi.ProvinceName = diaChi.ProvinceName;
			existingDiaChi.DistrictId = diaChi.DistrictId;
			existingDiaChi.DistrictName = diaChi.DistrictName;
			existingDiaChi.WardId = diaChi.WardId;
			existingDiaChi.WardName = diaChi.WardName;
			existingDiaChi.MoTa = diaChi.MoTa;
			existingDiaChi.DiaChiMacDinh = diaChi.DiaChiMacDinh;

			// Lưu thay đổi vào cơ sở dữ liệu
			await _context.SaveChangesAsync();
			return existingDiaChi;
		}

		public async Task<bool> DeleteAsync(Guid idDiaChi)
		{
			var diaChi = await _context.diaChi.FindAsync(idDiaChi);
			if (diaChi == null) return false;
			_context.diaChi.Remove(diaChi);
			return await _context.SaveChangesAsync() > 0;
		}
		public async Task<List<Province>> GetProvincesAsync()
		{
			var client = _httpClientFactory.CreateClient("GHN");

			var response = await client.GetAsync("https://online-gateway.ghn.vn/shiip/public-api/master-data/province");
			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadAsStringAsync();
			var ghnReponse = JsonConvert.DeserializeObject<GHNResponse<Province>>(result);
			return ghnReponse.Data.ToList();
		}
		public async Task<List<District>> GetDistrictsAsync(int provinceId)
		{
			var client = _httpClientFactory.CreateClient("GHN");

			var content = new StringContent(JsonConvert.SerializeObject(new { province_id = provinceId }), Encoding.UTF8, "application/json");
			var response = await client.PostAsync("https://online-gateway.ghn.vn/shiip/public-api/master-data/district", content);
			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadAsStringAsync();
			var ghnResponse = JsonConvert.DeserializeObject<GHNResponse<District>>(result);

			return ghnResponse.Data.ToList();
		}
		public async Task<List<Ward>> GetWardsAsync(int districtId)
		{
			var client = _httpClientFactory.CreateClient("GHN");

			// Thêm token nếu GHN yêu cầu
			//client.DefaultRequestHeaders.Add("Token", "1b11f9f8-257d-11ef-99a7-3ed37c49343e"); // Thay YOUR_API_KEY bằng API key thật nếu cần

			var content = new StringContent(JsonConvert.SerializeObject(new { district_id = districtId }), Encoding.UTF8, "application/json");
			var response = await client.PostAsync("https://online-gateway.ghn.vn/shiip/public-api/master-data/ward", content);
			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadAsStringAsync();
			Console.WriteLine("Raw Response: " + result);  // Log dữ liệu thô để kiểm tra

			var ghnResponse = JsonConvert.DeserializeObject<GHNResponse<Ward>>(result);

			if (ghnResponse == null || ghnResponse.Data == null || !ghnResponse.Data.Any())
			{
				Console.WriteLine("No wards found for districtId: " + districtId);
				return new List<Ward>();
			}

			return ghnResponse.Data.ToList();
		}
		public class GHNResponse<T>
		{
			[JsonProperty("code")]
			public string Code { get; set; }

			[JsonProperty("message")]
			public string Message { get; set; }

			[JsonProperty("data")]
			public IEnumerable<T> Data { get; set; }
		}

		//Check số lượng thêm cho khách hàng và địa chỉ mặc định
		public async Task<int> GetAddressCountByCustomerId(Guid customerId)
		{
			return await _context.diaChi.CountAsync(d => d.IdKhachHang == customerId);
		}
		public async Task<bool> HasDefaultAddressAsync(Guid customerId)
		{
			var result = await _context.diaChi.Where(x => x.IdKhachHang == customerId && x.DiaChiMacDinh).ToListAsync();
			Console.WriteLine($"API: {result.Count} địa chỉ mặc định tìm thấy.");
			return result.Any();
		}

        public async Task<DiaChi> GetDefaultAddressByCustomerIdAsync(Guid customerId)
        {
            var diaChi = await _context.diaChi
            .Where(d => d.IdKhachHang == customerId && d.DiaChiMacDinh)
            .FirstOrDefaultAsync();

            return diaChi;
        }
    }
}
