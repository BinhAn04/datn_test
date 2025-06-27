using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class SanPhamChiTietService : ISanPhamChiTietService
    {
        private readonly ISanPhamChiTietRepo _repository;

        public SanPhamChiTietService(ISanPhamChiTietRepo repository)
        {
            _repository = repository;
        }

        public bool Create(SanPhamChiTiet sanPhamChiTiet)
        {
            return _repository.Create(sanPhamChiTiet);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.Delete(id); 
        }

        public SanPhamChiTiet GetSanPhamChitietById(Guid id)
        {
            return _repository.GetSanPhamChitietById(id);
        }

        // Phương thức GetSanPhamChiTiet (Bất đồng bộ)
        public List<SanPhamChiTiet> GetSanPhamChiTiet()
        {
            return _repository.GetSanPhamChiTiet();
        }

        // Phương thức Update (Bất đồng bộ)
        public async Task<bool> Update(SanPhamChiTiet sanPhamChiTiet)
        {
            return await _repository.Update(sanPhamChiTiet); 
        }

        public async Task<List<SanPhamChiTiet>> GetSanPhamChiTietBySanPhamId(Guid sanPhamId)
        {
            return await _repository.GetSanPhamChiTietBySanPhamId(sanPhamId);
        }
        public async Task<SanPhamChiTiet> GetIdSanPhamChiTietByFilter(Guid idSanPham, Guid idKichCo, Guid idMauSac)
        {
            return await _repository.GetIdSanPhamChiTietByFilter(idSanPham, idKichCo, idMauSac);
        }

        public async Task<SanPhamDto> GetSanPhamByIdSanPhamChiTietAsync(Guid idSanPhamChiTiet)
        {
            return await _repository.GetSanPhamByIdSanPhamChiTietAsync(idSanPhamChiTiet);
        }

        public async Task<SanPhamChiTietDto> GetByIdHoaDonChiTietAsync(Guid id)
        {
            return await _repository.GetByIdHoaDonChiTietAsync(id);
        }

        public async Task<SanPhamChiTiet> GetByProductCodeAsync(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode))
            {
                throw new ArgumentException("Product code cannot be null or empty.", nameof(productCode));
            }

            return await _repository.GetByProductCodeAsync(productCode);
        }
    }
}
