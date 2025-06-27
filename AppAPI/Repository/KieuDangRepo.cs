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
    public class KieuDangRepo : IKieuDangRepo
    {
        private readonly AppDbcontext _context;

        public KieuDangRepo(AppDbcontext context)
        {
            _context = context;
        }

        public bool Create(KieuDang kieuDang)
        {
            try
            {
                _context.kieuDangs.Add(kieuDang);
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
                var kieuDang = _context.kieuDangs.Find(id);
                if (kieuDang != null)
                {
                    _context.kieuDangs.Remove(kieuDang);
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

        public KieuDang GetKieuDangById(Guid id)
        {
            var kieuDang = _context.kieuDangs.Find(id);
            return kieuDang;
        }

        // Phương thức GetKieuDang (Bất đồng bộ)
        public List<KieuDang> GetKieuDang(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return _context.kieuDangs.ToList();
            }
            else
            {
                return _context.kieuDangs
                    .Where(k => k.TenKieuDang.Contains(name)).ToList();
            }
        }

        public bool Update(KieuDang kieuDang)
        {
            var kieuDangUpdate = _context.kieuDangs.Find(kieuDang.IdKieuDang);
            if (kieuDangUpdate != null)
            {
                kieuDangUpdate.TenKieuDang = kieuDang.TenKieuDang;
                kieuDangUpdate.NgayCapNhat = kieuDang.NgayCapNhat;
                kieuDangUpdate.NgayTao = kieuDang.NgayTao;
                kieuDangUpdate.NguoiCapNhat = kieuDang.NguoiCapNhat;
                kieuDangUpdate.NguoiTao = kieuDang.NguoiTao;
                kieuDangUpdate.KichHoat = kieuDang.KichHoat == 1 ? 1 : 0;
                _context.kieuDangs.Update(kieuDangUpdate);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}