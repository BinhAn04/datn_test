using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.IService
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