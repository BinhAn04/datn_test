using AppData.Model;

namespace APPMVC.IService
{
    public interface IChucVuService
    {
        Task<List<ChucVu>> GetAllChucVu();
        Task<ChucVu> GetIdChucVu(Guid id);

        Task CreateCV(ChucVu cv);
        Task UpdateCV(ChucVu cv);
        Task DeleteCV(Guid id);
        Task<ChucVu> GetChucVuId(Guid? idChucVu);
    }
}
