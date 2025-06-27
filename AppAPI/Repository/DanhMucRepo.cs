using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Repository
{
    public class DanhMucRepo : IDanhMucRepo
    {
        private readonly AppDbcontext _context;

        public DanhMucRepo(AppDbcontext context)
        {
            _context = context;
        }

        public bool Create(DanhMuc danhMuc)
        {
            try
            {
                _context.danhMuc.Add(danhMuc);
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
                var danhMuc = _context.danhMuc.Find(id);
                if (danhMuc != null)
                {
                    _context.danhMuc.Remove(danhMuc);
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

        public DanhMuc GetDanhMucById(Guid id)
        {
            var danhMuc = _context.danhMuc.Find(id);
            return danhMuc;
        }

        public List<DanhMuc> GetDanhMuc(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return _context.danhMuc.ToList();
            }
            else
            {
                return _context.danhMuc
                    .Where(d => d.TenDanhMuc.Contains(name)).ToList();
            }
        }

        public bool Update(DanhMuc danhMuc)
        {
            var danhMucUpdate = _context.danhMuc.Find(danhMuc.IdDanhMuc);
            if (danhMucUpdate != null)
            {
                danhMucUpdate.TenDanhMuc = danhMuc.TenDanhMuc;
                danhMucUpdate.NgayCapNhat = danhMuc.NgayCapNhat;
                danhMucUpdate.NgayTao = danhMuc.NgayTao;
                danhMucUpdate.NguoiCapNhat = danhMuc.NguoiCapNhat;
                danhMucUpdate.NguoiTao = danhMuc.NguoiTao;
                danhMucUpdate.KichHoat = danhMuc.KichHoat == 1 ? 1 : 0;
                _context.danhMuc.Update(danhMucUpdate);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}