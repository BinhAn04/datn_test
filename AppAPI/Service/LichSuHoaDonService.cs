using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;

namespace AppAPI.Service
{
    public class LichSuHoaDonService : ILichSuHoaDonService
    {
        private readonly ILichSuHoaDonRepo _lichSuHoaDonRepository;

        public LichSuHoaDonService(ILichSuHoaDonRepo lichSuHoaDonRepository)
        {
            _lichSuHoaDonRepository = lichSuHoaDonRepository;
        }

        public async Task<List<LichSuHoaDon>> GetAllAsync()
        {
            return await _lichSuHoaDonRepository.GetAllAsync();
        }

        public async Task<LichSuHoaDon> GetByIdAsync(Guid id)
        {
            return await _lichSuHoaDonRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(LichSuHoaDon lichSuHoaDon)
        {
            await _lichSuHoaDonRepository.AddAsync(lichSuHoaDon);
        }

        public async Task UpdateAsync(LichSuHoaDon lichSuHoaDon)
        {
            await _lichSuHoaDonRepository.UpdateAsync(lichSuHoaDon);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _lichSuHoaDonRepository.DeleteAsync(id);
        }

        public async Task<List<LichSuHoaDon>> GetByIdHoaDonAsync(Guid idHoaDon)
        {
            return await _lichSuHoaDonRepository.GetByIdHoaDonAsync(idHoaDon);
        }
    }
}
