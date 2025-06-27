//using AppAPI.IRepository;
//using AppData;
//using AppData.Model;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AppAPI.Repository
//{
//    public class DayGiayRepo : IDayGiayRepo
//    {
//        private readonly AppDbcontext _context;

//        public DayGiayRepo(AppDbcontext context)
//        {
//            _context = context;
//        }

//        public bool Create(DayGiay dayGiay)
//        {
//            try
//            {
//                _context.dayGiay.Add(dayGiay);

//                _context.SaveChanges();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }

//        public bool Delete(Guid id)
//        {
//            try
//            {
//                var dayGiay = _context.dayGiay.Find(id);
//                if (dayGiay != null)
//                {
//                    _context.dayGiay.Remove(dayGiay);
//                    _context.SaveChanges();
//                    return true;
//                }
//                return false;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        public DayGiay GetDayGiayById(Guid id)
//        {
//            var dayGiay = _context.dayGiay.Find(id);
//            return dayGiay;
//        }

//        // Phương thức GetDayGiay (Bất đồng bộ)
//        public  List<DayGiay> GetDayGiay(string? name)
//        {
//            if (string.IsNullOrEmpty(name))
//            {
//                return  _context.dayGiay.ToList();
//            }
//            else
//            {
//                return _context.dayGiay
//                    .Where(d => d.TenDayGiay.Contains(name)).ToList();
//            }
//        }

//        public bool Update(DayGiay dayGiay)
//        {
//            var dayGiayUpdate = _context.dayGiay.Find(dayGiay.IdDayGiay);
//            if (dayGiayUpdate != null)
//            {
//                dayGiayUpdate.TenDayGiay = dayGiay.TenDayGiay;
//                dayGiayUpdate.NgayCapNhat = dayGiay.NgayCapNhat;
//                dayGiayUpdate.NgayTao = dayGiay.NgayTao;
//                dayGiayUpdate.NguoiCapNhat = dayGiay.NguoiCapNhat;
//                dayGiayUpdate.NguoiTao = dayGiay.NguoiTao;
//                dayGiayUpdate.KichHoat = dayGiay.KichHoat == 1 ? 1 : 0;
//                _context.dayGiay.Update(dayGiayUpdate);
//                _context.SaveChanges();
//                return true;
//            }
//            return false;
//        }
//    }
//}