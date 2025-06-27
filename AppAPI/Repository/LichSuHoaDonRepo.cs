using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Repository
{
    public class LichSuHoaDonRepo : ILichSuHoaDonRepo
    {
        private readonly AppDbcontext _context;

        public LichSuHoaDonRepo(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<List<LichSuHoaDon>> GetAllAsync()
        {
            return await _context.lichSuHoaDons.ToListAsync();
        }

        public async Task<LichSuHoaDon> GetByIdAsync(Guid id)
        {
            return await _context.lichSuHoaDons.FindAsync(id);
        }

        public async Task AddAsync(LichSuHoaDon lichSuHoaDon)
        {
            await _context.lichSuHoaDons.AddAsync(lichSuHoaDon);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LichSuHoaDon lichSuHoaDon)
        {
            _context.lichSuHoaDons.Update(lichSuHoaDon);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var lichSuHoaDon = await GetByIdAsync(id);
            if (lichSuHoaDon != null)
            {
                _context.lichSuHoaDons.Remove(lichSuHoaDon);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<LichSuHoaDon>> GetByIdHoaDonAsync(Guid idHoaDon)
        {
            return await _context.lichSuHoaDons
               .Where(l => l.IdHoaDon == idHoaDon)
               .OrderBy(ls => ls.NgayTao)
               .ToListAsync();
        }
    }
}
