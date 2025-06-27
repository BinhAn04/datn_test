using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using static AppAPI.Repository.DiaChiRepo;

namespace AppAPI.Repository
{
    public class DiaChiHdRepo : IDiaChiHdRepo
    {
        private readonly AppDbcontext _context;
        private IHttpClientFactory _httpClientFactory;
        public DiaChiHdRepo(AppDbcontext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<DiaChiHoaDon>> GetAll()
        {
            return await _context.diaChiHoaDons.ToListAsync();
        }
        public async Task<DiaChiHoaDon> GetByIdAsync(Guid idDiaChi)
        {
            var getDc = _context.diaChiHoaDons.Find(idDiaChi);
            return getDc;
        }
        public async Task<DiaChiHoaDon> AddAsync(DiaChiHoaDon diaChi)
        {
            _context.diaChiHoaDons.Add(diaChi);
            await _context.SaveChangesAsync();
            return diaChi;
        }

        public async Task<bool> DeleteAsync(Guid idDiaChi)
        {
            var diaChi = await _context.diaChiHoaDons.FindAsync(idDiaChi);
            if (diaChi == null) return false;
            _context.diaChiHoaDons.Remove(diaChi);
            return await _context.SaveChangesAsync() > 0;
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

        public async Task<List<Province>> GetProvincesAsync()
        {
            var client = _httpClientFactory.CreateClient("GHN");

            var response = await client.GetAsync("https://online-gateway.ghn.vn/shiip/public-api/master-data/province");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var ghnReponse = JsonConvert.DeserializeObject<GHNResponse<Province>>(result);
            return ghnReponse.Data.ToList();
        }

        public async Task<List<Ward>> GetWardsAsync(int districtId)
        {
            var client = _httpClientFactory.CreateClient("GHN");         
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

        public async Task<DiaChiHoaDon> UpdateAsync(DiaChiHoaDon diaChi)
        {
           var editDiaChi = await _context.diaChiHoaDons.FindAsync(diaChi.IdDiaChiHoaDon);
            if(diaChi == null)
            {
                return null;
            }
            editDiaChi.HoTen = diaChi.HoTen;
            editDiaChi.SoDienThoai = diaChi.SoDienThoai;
            editDiaChi.ProvinceId = diaChi.ProvinceId;
            editDiaChi.ProvinceName = diaChi.ProvinceName;
            editDiaChi.DistrictId = diaChi.DistrictId;
            editDiaChi.DistrictName = diaChi.DistrictName;
            editDiaChi.WardId = diaChi.WardId;
            editDiaChi.WardName = diaChi.WardName;
            editDiaChi.MoTa = diaChi.MoTa;
            editDiaChi.DiaChiMacDinh = diaChi.DiaChiMacDinh;

            await _context.SaveChangesAsync();
            return editDiaChi;
        }
    }
}
