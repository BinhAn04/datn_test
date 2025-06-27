using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class SanPhamService : ISanPhamService
    {
        private readonly ISanPhamRepo _repository;

        public SanPhamService(ISanPhamRepo repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsync(SanPham sanPham)
        {
            try
            {
                return await _repository.CreateAsync(sanPham);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                Console.WriteLine($"An error occurred while creating the product: {ex.Message}");
                return false; // Indicate failure
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
                // Log the exception
                Console.WriteLine($"An error occurred while deleting the product: {ex.Message}");
                return false; // Indicate failure
            }
        }

        public async Task<SanPham?> GetSanPhamByIdAsync(Guid id)
        {
            try
            {
                return await _repository.GetSanPhamByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving the product: {ex.Message}");
                return null; // Indicate failure
            }
        }


        public async Task<bool> UpdateAsync(SanPham sanPham)
        {
            try
            {
                return await _repository.UpdateAsync(sanPham);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while updating the product: {ex.Message}");
                return false; // Indicate failure
            }
        }

        public async Task<IEnumerable<SanPham>> GetSanPhamAsync(string? name)
        {
            try
            {
                return await _repository.GetSanPhamAsync(name);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while retrieving products: {ex.Message}");
                return new List<SanPham>(); // Return an empty list in case of failure
            }
        }

        public async Task<ThuongHieu?> GetThuongHieuBySanPhamIdAsync(Guid sanPhamId)
        {
            return await _repository.GetThuongHieuBySanPhamIdAsync(sanPhamId);
        }
        public async Task<IEnumerable<SanPham>> GetSanPhamClientAsync(string? name)
        {
            try
            {
                return await _repository.GetSanPhamClientAsync(name);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Lỗi GetSanPhamClient SanPhamService");
                return new List<SanPham>();
            }
        }
        public async Task<List<SanPham>> GetSanPhamByCategory(Guid idDanhMuc)
        {
            try
            {
                return await _repository.GetSanPhamByCategory(idDanhMuc);
            
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Lỗi GetSanPhamClient SanPhamService");
                return new List<SanPham>();
            }
}
    }
}