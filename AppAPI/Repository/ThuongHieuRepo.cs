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
    public class ThuongHieuRepo : IThuongHieuRepo
    {
        private readonly AppDbcontext _context;

        public ThuongHieuRepo(AppDbcontext context)
        {
            _context = context;
        }

        public bool Create(ThuongHieu thuongHieu)
        {
            try
            {
                _context.thuongHieus.Add(thuongHieu);
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
                var thuongHieu = _context.thuongHieus.Find(id);
                if (thuongHieu != null)
                {
                    _context.thuongHieus.Remove(thuongHieu);
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

        public ThuongHieu GetThuongHieuById(Guid id)
        {
            var thuongHieu = _context.thuongHieus.Find(id);
            return thuongHieu;
        }

        // Phương thức GetThuongHieu (Bất đồng bộ)
        public List<ThuongHieu> GetThuongHieu(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return _context.thuongHieus.ToList();
            }
            else
            {
                return _context.thuongHieus
                    .Where(t => t.TenThuongHieu.Contains(name)).ToList();
            }
        }

        public bool Update(ThuongHieu thuongHieu)
        {
            var thuongHieuUpdate = _context.thuongHieus.Find(thuongHieu.IdThuongHieu);
            if (thuongHieuUpdate != null)
            {
                thuongHieuUpdate.TenThuongHieu = thuongHieu.TenThuongHieu;
                thuongHieuUpdate.NgayCapNhat = thuongHieu.NgayCapNhat;
                thuongHieuUpdate.NgayTao = thuongHieu.NgayTao;
                thuongHieuUpdate.NguoiCapNhat = thuongHieu.NguoiCapNhat;
                thuongHieuUpdate.NguoiTao = thuongHieu.NguoiTao;
                thuongHieuUpdate.KichHoat = thuongHieu.KichHoat == 1 ? 1 : 0;
                _context.thuongHieus.Update(thuongHieuUpdate);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}