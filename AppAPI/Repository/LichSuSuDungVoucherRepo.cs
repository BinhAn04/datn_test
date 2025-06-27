using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAPI.Repository
{
    public class LichSuSuDungVoucherRepo : ILichSuSuDungVoucherRepo
    {
        private readonly AppDbcontext _context;

        public LichSuSuDungVoucherRepo(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<List<LichSuSuDungVoucher>> GetAllAsync()
        {
            return await _context.LichSuSuDungVouchers.ToListAsync();
        }

        public async Task<LichSuSuDungVoucher> GetByIdAsync(Guid id)
        {
            return await _context.LichSuSuDungVouchers.FindAsync(id);
        }

        public async Task AddAsync(LichSuSuDungVoucher lichSuSuDungVoucher)
        {
            await _context.LichSuSuDungVouchers.AddAsync(lichSuSuDungVoucher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LichSuSuDungVoucher lichSuSuDungVoucher)
        {
            _context.LichSuSuDungVouchers.Update(lichSuSuDungVoucher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var lichSuSuDungVoucher = await GetByIdAsync(id);
            if (lichSuSuDungVoucher != null)
            {
                _context.LichSuSuDungVouchers.Remove(lichSuSuDungVoucher);
                await _context.SaveChangesAsync();
            }
        }
    }
}