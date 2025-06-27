//using AppData.Model;
//using APPMVC.IService;


//namespace APPMVC.Service
//{
//    public class DayGiayService : IDayGiayService
//	{
//		HttpClient _httpClient;

//		public DayGiayService()
//		{
//			_httpClient = new HttpClient();
//			_httpClient.BaseAddress = new Uri("https://localhost:7198");
//		}
//        public async Task Create(DayGiay daygiay)
//        {
//            await _httpClient.PostAsJsonAsync("api/DayGiay/them", daygiay);
//        }

//        public async Task Delete(Guid id)
//		{
//			await _httpClient.DeleteAsync($"api/DayGiay/Xoa?id={id}");
//		}

//		public Task<List<DayGiay>> GetDayGiay(string? name)
//		{
//			var repo = _httpClient.GetFromJsonAsync<List<DayGiay>>($"api/DayGiay/getall?name={name}");
//			return repo;
//		}

//		public Task<DayGiay> GetDayGiayById(Guid id)
//		{
//			var dayGiay = _httpClient.GetFromJsonAsync<DayGiay>($"api/DayGiay/getbyid?id={id}");
//			return dayGiay;
//		}
//        public async Task Update(DayGiay dayGiay)
//        {
//            await _httpClient.PutAsJsonAsync("api/DayGiay/Sua", dayGiay);
//        }
//    }
//}
