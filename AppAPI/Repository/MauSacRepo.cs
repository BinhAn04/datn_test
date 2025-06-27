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
    public class MauSacRepo : IMauSacRepo
    {
        private readonly AppDbcontext _context;

        public MauSacRepo(AppDbcontext context)
        {
            _context = context;
        }

        public bool Create(MauSac mauSac)
        {
            try
            {
                _context.mauSacs.Add(mauSac);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var mauSac = _context.mauSacs.Find(id);
                if (mauSac != null)
                {
                    _context.mauSacs.Remove(mauSac);
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

        public MauSac GetMauSacById(Guid id)
        {
            var mauSac = _context.mauSacs.Find(id);
            return mauSac;
        }

        public List<MauSac> GetMauSac(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return _context.mauSacs.ToList();
            }
            else
            {
                return _context.mauSacs
                    .Where(m => m.TenMauSac.Contains(name)).ToList();
            }
        }

        public bool Update(MauSac mauSac)
        {
            var mauSacUpdate = _context.mauSacs.Find(mauSac.IdMauSac);
            if (mauSacUpdate != null)
            {
                mauSacUpdate.TenMauSac = mauSac.TenMauSac;
                mauSacUpdate.KichHoat = mauSac.KichHoat;
                mauSacUpdate.NgayCapNhat = DateTime.Now;
                mauSacUpdate.NguoiCapNhat = mauSac.NguoiCapNhat;

                _context.mauSacs.Update(mauSacUpdate);
                _context.SaveChanges(); // Đảm bảo gọi SaveChanges để lưu thay đổi
                return true;
            }
            return false;
        }
        public async Task<List<MauSac>> GetMauSacBySanPhamId(Guid sanPhamId)
        {
            return await _context.sanPhamChiTietMausacs
                .Where(spctms => spctms.SanPhamChiTiet.IdSanPham == sanPhamId)
                .Select(spctms => spctms.MauSac)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<MauSac>> GetMauSacByIdsAsync(List<Guid> mauSacIds)
        {
            return await _context.mauSacs
            .Where(ms => mauSacIds.Contains(ms.IdMauSac))
            .ToListAsync();
        }
    }
}