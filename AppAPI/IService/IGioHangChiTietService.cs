using AppData.Model;

namespace AppAPI.IService
{
	public interface IGioHangChiTietService
	{
		Task<List<GioHangChiTiet>> GetAllAsync();
		Task<GioHangChiTiet> GetByIdAsync(Guid id);
		Task AddAsync(GioHangChiTiet gioHangChiTiet);
		Task UpdateAsync(GioHangChiTiet gioHangChiTiet);
		Task DeleteAsync(Guid id);

        Task<List<GioHangChiTiet>> GetByGioHangIdAsync(Guid gioHangId);

        Task RemoveItemsFromCartAsync(Guid cartId, List<Guid> productDetailIds);

        Task<double> GetTotalQuantityBySanPhamChiTietIdAsync(Guid sanPhamChiTietId, Guid cartId);

        Task<GioHangChiTiet> GetByProductIdAndCartIdAsync(Guid sanPhamChiTietId, Guid cartId);

        Task<List<GioHangChiTiet>> GetByIdsAsync(List<Guid> ids);
    }
}
