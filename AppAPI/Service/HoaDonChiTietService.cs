using AppAPI.IRepository;
using AppAPI.IService;
using AppAPI.Repository;
using AppData.Model;

namespace AppAPI.Service
{
    public class HoaDonChiTietService : IHoaDonChiTietService
    {
        private readonly IHoaDonChiTietRepo _hoaDonChiTietRepository;

        public HoaDonChiTietService(IHoaDonChiTietRepo hoaDonChiTietRepository)
        {
            _hoaDonChiTietRepository = hoaDonChiTietRepository;
        }

        public async Task<List<HoaDonChiTiet>> GetAllAsync()
        {
            return await _hoaDonChiTietRepository.GetAllAsync();
        }

        public async Task<HoaDonChiTiet> GetByIdAsync(Guid id)
        {
            return await _hoaDonChiTietRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(List<HoaDonChiTiet> hoaDonChiTietList)
        {
            await _hoaDonChiTietRepository.AddAsync(hoaDonChiTietList);
        }

        public async Task UpdateAsync(List<HoaDonChiTiet> hoaDonChiTietList)
        {
            await _hoaDonChiTietRepository.UpdateAsync(hoaDonChiTietList);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _hoaDonChiTietRepository.DeleteAsync(id);
        }

        public async Task<List<HoaDonChiTiet>> GetByIdHoaDonAsync(Guid idHoaDon)
        {
            return await _hoaDonChiTietRepository.GetByIdHoaDonAsync(idHoaDon);
        }
        public async Task<double> GetTotalQuantityBySanPhamChiTietIdAsync(Guid sanPhamChiTietId, Guid hDCTId)
        {
            var totalQuantity = await _hoaDonChiTietRepository.GetTotalQuantityBySanPhamChiTietIdAsync(sanPhamChiTietId, hDCTId);
            return totalQuantity;
        }

        public async Task<HoaDonChiTiet> GetByIdAndProduct(Guid idHoaDon, Guid idSanPhamChiTiet)
        {
            return await _hoaDonChiTietRepository.GetByIdAndProduct(idHoaDon, idSanPhamChiTiet);
        }
    }
}
