using AppData.Model;

namespace APPMVC.IService
{
    public interface ILichSuSuDungVoucherService
    {
        Task<List<LichSuSuDungVoucher>> GetAllAsync();
        Task<LichSuSuDungVoucher> GetByIdAsync(Guid id);
        Task AddAsync(LichSuSuDungVoucher lichSuSuDungVoucher);
        Task UpdateAsync(LichSuSuDungVoucher lichSuSuDungVoucher);
        Task DeleteAsync(Guid id);
    }
}
