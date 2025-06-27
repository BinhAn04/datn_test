using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class LichSuThanhToanService : ILichSuThanhToanService
    {
        private readonly ILichSuThanhToanRepo _lichSuThanhToanRepository;

        public LichSuThanhToanService(ILichSuThanhToanRepo lichSuThanhToanRepository)
        {
            _lichSuThanhToanRepository = lichSuThanhToanRepository;
        }

        public async Task<List<LichSuThanhToan>> GetAllAsync()
        {
            return await _lichSuThanhToanRepository.GetAllAsync();
        }

        public async Task<LichSuThanhToan> GetByIdAsync(Guid id)
        {
            return await _lichSuThanhToanRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(LichSuThanhToan lichSuThanhToan)
        {
            await _lichSuThanhToanRepository.AddAsync(lichSuThanhToan);
        }

        public async Task UpdateAsync(LichSuThanhToan lichSuThanhToan)
        {
            await _lichSuThanhToanRepository.UpdateAsync(lichSuThanhToan);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _lichSuThanhToanRepository.DeleteAsync(id);
        }

        public async Task<List<LichSuThanhToan>> GetByIdHoaDonAsync(Guid idHoaDon)
        {
            return await _lichSuThanhToanRepository.GetByIdHoaDonAsync(idHoaDon);
        }
    }
}