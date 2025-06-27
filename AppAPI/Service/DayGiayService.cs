//using AppAPI.IRepository;
//using AppAPI.IService;
//using AppData.Model;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace AppAPI.Service
//{
//    public class DayGiayService : IDayGiayService
//    {
//        private readonly IDayGiayRepo _repository;

//        public DayGiayService(IDayGiayRepo repository)
//        {
//            _repository = repository;
//        }

//        public bool Create(DayGiay dayGiay)
//        {
//            return _repository.Create(dayGiay);
//        }

//        public bool Delete(Guid id)
//        {
//            return _repository.Delete(id);
//        }

//        public DayGiay GetDayGiayById(Guid id)
//        {
//            return _repository.GetDayGiayById(id);
//        }

//        // Phương thức GetDayGiay (Bất đồng bộ)
//        public  List<DayGiay> GetDayGiay(string? name)
//        {
//            return  _repository.GetDayGiay(name);
//        }

//        // Phương thức Update (Bất đồng bộ)
//        public bool Update(DayGiay dayGiay)
//        {
//            return _repository.Update(dayGiay);
//        }

//    }
//}