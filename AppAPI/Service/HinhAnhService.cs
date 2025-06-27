using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class HinhAnhService : IHinhAnhService
    {
        private readonly IHinhAnhRepo _repository;

        public HinhAnhService(IHinhAnhRepo repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> UploadAsync(HinhAnh hinhAnh)
        {
            try
            {
                return await _repository.UploadAsync(hinhAnh);
            }
            catch (Exception ex)
            {
                // Consider using a proper logging mechanism instead of Console.WriteLine
                Console.WriteLine($"Error in ServiceHinhAnh.UploadAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ServiceHinhAnh.DeleteAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<List<HinhAnh>> GetHinhAnhsAsync()
        {
            try
            {
                return await _repository.GetHinhAnhsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ServiceHinhAnh.GetHinhAnhsAsync: {ex.Message}");
                return new List<HinhAnh>();
            }
        }

        public async Task<HinhAnh> GetHinhAnhByIdAsync(Guid id)
        {
            try
            {
                return await _repository.GetHinhAnhByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ServiceHinhAnh.GetHinhAnhByIdAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<List<HinhAnh>> GetHinhAnhsBySanPhamChiTietId(Guid sanPhamChiTietId)
        {
            return await _repository.GetHinhAnhsBySanPhamChiTietId(sanPhamChiTietId);
        }
    }
}