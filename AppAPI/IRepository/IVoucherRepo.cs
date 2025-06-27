using AppData.Model;
using AppData.Validation;
using System.ComponentModel.DataAnnotations;

namespace AppAPI.IRepository
{
    public interface IVoucherRepo
    {
        Task<List<Voucher>> GetVouchersAsync();
        Task<Voucher> GetVoucherByIdAsync(Guid id);
        Task<bool> CreateAsync(VoucherDto voucher, List<Guid> selectedKhachHangIds);
        Task<bool> UpdateAsync(Voucher voucher);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddLichSuSuDungVoucherAsync(LichSuSuDungVoucher lichSuSuDungVoucher);
        Task<List<KhachHang>> GetKhachHangDaNhanVoucherAsync(Guid voucherId);
        Task<bool> MaVoucher(string maVoucher);
        Task<List<Voucher>> GetAvailableVouchersForCustomerAsync(Guid khachHangId);
        Task<bool> UpdateVoucherStatusAsync(Voucher voucher, int status);
    }
  
}
