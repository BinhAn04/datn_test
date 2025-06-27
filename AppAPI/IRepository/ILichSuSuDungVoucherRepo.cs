using AppData.Model;

namespace AppAPI.IRepository
{
    public interface ILichSuSuDungVoucherRepo
    {
        Task<List<LichSuSuDungVoucher>> GetAllAsync();
        Task<LichSuSuDungVoucher> GetByIdAsync(Guid id);
        Task AddAsync(LichSuSuDungVoucher lichSuSuDungVoucher);
        Task UpdateAsync(LichSuSuDungVoucher lichSuSuDungVoucher);
        Task DeleteAsync(Guid id);
    }
}