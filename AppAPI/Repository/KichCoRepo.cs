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
    public class KichCoRepo : IKichCoRepo
    {
        private readonly AppDbcontext _context;

        public KichCoRepo(AppDbcontext context)
        {
            _context = context;
        }

        public bool Create(KichCo kichCo)
        {
            try
            {
                _context.kichCos.Add(kichCo);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var kichCo = _context.kichCos.Find(id);
                if (kichCo != null)
                {
                    _context.kichCos.Remove(kichCo);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public KichCo GetKichCoById(Guid id)
        {
            var kichCo = _context.kichCos.Find(id);
            return kichCo;
        }

        public List<KichCo> GetKichCo(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return _context.kichCos.ToList();
            }
            else
            {
                return _context.kichCos
                    .Where(k => k.TenKichCo.Contains(name)).ToList();
            }
        }

        public bool Update(KichCo kichCo)
        {
            var kichCoUpdate = _context.kichCos.Find(kichCo.IdKichCo);
            if (kichCoUpdate != null)
            {
                kichCoUpdate.TenKichCo = kichCo.TenKichCo;
                kichCoUpdate.NgayCapNhat = kichCo.NgayCapNhat;
                kichCoUpdate.NgayTao = kichCo.NgayTao;
                kichCoUpdate.NguoiCapNhat = kichCo.NguoiCapNhat;
                kichCoUpdate.NguoiTao = kichCo.NguoiTao;
                kichCoUpdate.KichHoat = kichCo.KichHoat == 1 ? 1 : 0;

                _context.kichCos.Update(kichCoUpdate);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<List<KichCo>> GetKichCoBySanPhamId(Guid sanPhamId)
        {
            return await _context.sanPhamChiTietKichCos
                .Where(spctkc => spctkc.SanPhamChiTiet.IdSanPham == sanPhamId)
                .Select(spctkc => spctkc.KichCo)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<KichCo>> GetKichCoByIdsAsync(List<Guid> kichCoIds)
        {
            return await _context.kichCos
            .Where(kc => kichCoIds.Contains(kc.IdKichCo))
            .ToListAsync();
        }
    }
}