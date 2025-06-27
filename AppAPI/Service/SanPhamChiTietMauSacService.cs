using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;

namespace AppAPI.Service
{
    public class SanPhamChiTietMauSacService : ISanPhamChiTietMauSacService
    {
        private readonly ISanPhamChiTietMauSacRepo _repository;

        public SanPhamChiTietMauSacService(ISanPhamChiTietMauSacRepo repository)
        {
            _repository = repository;
        }

        // Retrieve all Mau Sac items
        public List<SanPhamChiTietMauSac> GetSanPhamChiTietMauSac()
        {
            return _repository.GetSanPhamChiTietMauSac();
        }

        // Retrieve a specific Mau Sac by ID
        public List<Guid> GetSanPhamChiTietIdsByMauSacId(Guid mauSacId)
        {
            return _repository.GetSanPhamChiTietIdsByMauSacId(mauSacId);
        }

        // Create a new Mau Sac
        public bool Create(SanPhamChiTietMauSac mauSac)
        {
            return _repository.Create(mauSac);
        }

        // Update an existing Mau Sac
        public bool Update(SanPhamChiTietMauSac mauSac)
        {
            return _repository.Update(mauSac);
        }

        // Delete a Mau Sac by ID
        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public async Task<List<MauSac>> GetMauSacIdsBySanPhamChiTietId(Guid sanPhamChiTietId)
        {
            return await _repository.GetMauSacIdsBySanPhamChiTietId(sanPhamChiTietId);
        }
    }
}