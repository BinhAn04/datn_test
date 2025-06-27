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
    public class DeGiayRepo : IDeGiayRepo
    {
        private readonly AppDbcontext _context;

        public DeGiayRepo(AppDbcontext context)
        {
            _context = context;
        }

        public bool Create(DeGiay deGiay)
        {
            try
            {
                _context.deGiay.Add(deGiay);
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
                var deGiay = _context.deGiay.Find(id);
                if (deGiay != null)
                {
                    _context.deGiay.Remove(deGiay);
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

        public DeGiay GetDeGiayById(Guid id)
        {
            var deGiay = _context.deGiay.Find(id);
            return deGiay;
        }

        public List<DeGiay> GetDeGiay(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return _context.deGiay.ToList();
            }
            else
            {
                return _context.deGiay
                    .Where(d => d.TenDeGiay.Contains(name)).ToList();
            }
        }

        public bool Update(DeGiay deGiay)
        {
            var deGiayUpdate = _context.deGiay.Find(deGiay.IdDeGiay);
            if (deGiayUpdate != null)
            {
                deGiayUpdate.TenDeGiay = deGiay.TenDeGiay;
                deGiayUpdate.NgayCapNhat = deGiay.NgayCapNhat;
                deGiayUpdate.NgayTao = deGiay.NgayTao;
                deGiayUpdate.NguoiCapNhat = deGiay.NguoiCapNhat;
                deGiayUpdate.NguoiTao = deGiay.NguoiTao;
                deGiayUpdate.KichHoat = deGiay.KichHoat == 1 ? 1 : 0;
                _context.deGiay.Update(deGiayUpdate);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}