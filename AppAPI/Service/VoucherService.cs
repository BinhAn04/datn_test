using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;

namespace AppAPI.Service
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepo _repository;
        private readonly ILogger<VoucherService> _logger;
        public VoucherService(IVoucherRepo repository, ILogger<VoucherService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<List<Voucher>> GetVouchersAsync()
        {
            try
            {
                return await _repository.GetVouchersAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ServiceVoucher.GetVouchersAsync: {ex.Message}");
                return new List<Voucher>();
            }
        }
        public async Task<Voucher> GetVoucherByIdAsync(Guid id)
        {
            try
            {
                return await _repository.GetVoucherByIdAsync(id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in ServiceVoucher.GetVoucherByIdAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateAsync(VoucherDto voucher, List<Guid> selectedKhachHangIds)
        {
            try
            {
                _logger.LogInformation("Starting to create voucher in service.");

                if (voucher == null)
                {
                    _logger.LogWarning("Voucher is null.");
                    throw new ArgumentNullException(nameof(voucher));
                }

                // Additional validation
                if (string.IsNullOrEmpty(voucher.MaVoucher))
                {
                    _logger.LogWarning("MaVoucher is required.");
                    throw new ArgumentException("MaVoucher is required");
                }

                if (voucher.NgayBatDau >= voucher.NgayKetThuc)
                {
                    _logger.LogWarning("NgayBatDau must be before NgayKetThuc.");
                    throw new ArgumentException("NgayBatDau must be before NgayKetThuc");
                }

                var result = await _repository.CreateAsync(voucher, selectedKhachHangIds);
                if (result)
                {
                    _logger.LogInformation($"Successfully created voucher with ID: {voucher.VoucherId}");
                }
                else
                {
                    _logger.LogWarning("Failed to create voucher in repository.");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateAsync method.");
                throw;
            }
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in ServiceVoucher.DeleteAsync: {ex.Message}");
                return false;
            }
        }
        
        public async Task<bool> UpdateAsync(Voucher voucher)
        {
            try
            {
                return await _repository.UpdateAsync(voucher);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in ServiceVoucher.UpdateAsync: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> AddLichSuSuDungVoucherAsync(LichSuSuDungVoucher lichSuSuDungVoucher)
        {
            return await _repository.AddLichSuSuDungVoucherAsync(lichSuSuDungVoucher);
        }
        public async Task<List<KhachHang>> GetKhachHangDaNhanVoucherAsync(Guid voucherId)
        {
            try
            {
                return await _repository.GetKhachHangDaNhanVoucherAsync(voucherId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ServiceVoucher.GetKhachHangDaNhanVoucher: {ex.Message}");
                return new List<KhachHang>();
            }
        }

        public async Task<List<Voucher>> GetAvailableVouchersForCustomerAsync(Guid khachHangId)
        {
            try
            {
                return await _repository.GetAvailableVouchersForCustomerAsync(khachHangId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAvailableVouchersForCustomerAsync: {ex.Message}");
                return new List<Voucher>();
            }
        }
        public async Task<bool> MaVoucher(string maVoucher)
        {
            return await _repository.MaVoucher(maVoucher);
        }
        public async Task<bool> UpdateVoucherStatusAsync(Voucher voucher, int status)
        {
            try
            {
                return await _repository.UpdateVoucherStatusAsync(voucher, status);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in ServiceVoucher.UpdateAsync: {ex.Message}");
                return false;
            }
        }
    }
}
