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
    public class LichSuThanhToanRepo : ILichSuThanhToanRepo
    {
        private readonly AppDbcontext _context;

        public LichSuThanhToanRepo(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<List<LichSuThanhToan>> GetAllAsync()
        {
            return await _context.lichSuThanhToans.ToListAsync();
        }

        public async Task<LichSuThanhToan> GetByIdAsync(Guid id)
        {
            return await _context.lichSuThanhToans.FindAsync(id);
        }

        public async Task AddAsync(LichSuThanhToan lichSuThanhToan)
        {
            await _context.lichSuThanhToans.AddAsync(lichSuThanhToan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LichSuThanhToan lichSuThanhToan)
        {
            _context.lichSuThanhToans.Update(lichSuThanhToan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var lichSuThanhToan = await GetByIdAsync(id);
            if (lichSuThanhToan != null)
            {
                _context.lichSuThanhToans.Remove(lichSuThanhToan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<LichSuThanhToan>> GetByIdHoaDonAsync(Guid idHoaDon)
        {
            try
            {
                var result = await _context.lichSuThanhToans
                    .Where(l => l.IdHoaDon == idHoaDon)
                    .ToListAsync();

                return result ?? new List<LichSuThanhToan>();
            }
            catch (Exception ex)
            {
                return new List<LichSuThanhToan>();
            }
        }
    }
}