using AppData.Model;
using AppData.Validation;
using System.ComponentModel.DataAnnotations;

namespace APPMVC.IService
{
    public interface IVoucherService
    {
        Task<List<Voucher>> GetVouchersAsync();
        Task<Voucher> GetVoucherByIdAsync(Guid id);
        Task<bool> CreateAsync(VoucherDto voucher, List<Guid> selectedKhachHangIds); 
        Task<bool> UpdateAsync(Voucher voucher);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddLichSuSuDungVoucherAsync(LichSuSuDungVoucher lichSuSuDungVoucher);
        Task<List<KhachHang>> GetKhachHangDaNhanVoucherAsync(Guid voucherId);

        Task<List<Voucher>> GetAvailableVouchersForCustomerAsync(Guid khachHangId);
        Task<bool> CheckMaVoucher(string maVoucher);
        Task<bool> UpdateVoucherStatusAsync(Voucher voucher, int status);
    }
}
