using AppData.Model;

namespace AppAPI.IRepository
{
    public interface IChucVuRepo
    {
        Task<List<ChucVu>> GetAllChucVu();
        Task<ChucVu> GetIdChucVu(Guid id);
        Task<ChucVu> CreateCV(ChucVu cv);
        Task<ChucVu> UpdateCV(ChucVu cv);
        Task DeleteCV(Guid id);
        Task<ChucVu> GetChucVuId(Guid? idChucVu);
    }
}
