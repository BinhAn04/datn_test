using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class LichSuSuDungVoucherService : ILichSuSuDungVoucherService
    {
        private readonly ILichSuSuDungVoucherRepo _lichSuSuDungVoucherRepository;

        public LichSuSuDungVoucherService(ILichSuSuDungVoucherRepo lichSuSuDungVoucherRepository)
        {
            _lichSuSuDungVoucherRepository = lichSuSuDungVoucherRepository;
        }

        public async Task<List<LichSuSuDungVoucher>> GetAllAsync()
        {
            return await _lichSuSuDungVoucherRepository.GetAllAsync();
        }

        public async Task<LichSuSuDungVoucher> GetByIdAsync(Guid id)
        {
            return await _lichSuSuDungVoucherRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(LichSuSuDungVoucher lichSuSuDungVoucher)
        {
            await _lichSuSuDungVoucherRepository.AddAsync(lichSuSuDungVoucher);
        }

        public async Task UpdateAsync(LichSuSuDungVoucher lichSuSuDungVoucher)
        {
            await _lichSuSuDungVoucherRepository.UpdateAsync(lichSuSuDungVoucher);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _lichSuSuDungVoucherRepository.DeleteAsync(id);
        }
    }
}