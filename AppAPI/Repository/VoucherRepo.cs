using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AppAPI.Repository
{
    public class VoucherRepo : IVoucherRepo
    {
        private readonly AppDbcontext _context;
        private readonly ILogger<VoucherRepo> _logger;
        public VoucherRepo(AppDbcontext context, ILogger<VoucherRepo> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<List<Voucher>> GetVouchersAsync()
        {
            try
            {
                return await _context.vouchers.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in VoucherRepo.GetVouchersAsync: {ex.Message}");
                return new List<Voucher>();
            }
        }

        public async Task<Voucher> GetVoucherByIdAsync(Guid id)
        {
            return await _context.vouchers.FindAsync(id);
        }

        public async Task<bool> CreateAsync(VoucherDto voucherDto, List<Guid> selectedKhachHangIds)
        {
            try
            {
                _logger.LogInformation("Starting to add voucher to database.");

                if (voucherDto == null)
                {
                    _logger.LogWarning("VoucherDto is null.");
                    throw new ArgumentNullException(nameof(voucherDto));
                }

                // Validate voucher properties using Data Annotations
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(voucherDto);
                if (!Validator.TryValidateObject(voucherDto, validationContext, validationResults, true))
                {
                    foreach (var validationResult in validationResults)
                    {
                        _logger.LogWarning(validationResult.ErrorMessage);
                    }
                    throw new ValidationException("Validation failed for the voucher.");
                }

                var voucher = new Voucher
                {
                    VoucherId = Guid.NewGuid(), 
                    MaVoucher = voucherDto.MaVoucher,
                    MoTaVoucher = voucherDto.MoTaVoucher,
                    LoaiGiamGia = voucherDto.LoaiGiamGia,
                    GiaTriGiam = voucherDto.GiaTriGiam,
                    GiaTriDonHangToiThieu = voucherDto.GiaTriDonHangToiThieu,
                    SoTienToiDa = voucherDto.SoTienToiDa,
                    NgayBatDau = voucherDto.NgayBatDau,
                    NgayKetThuc = voucherDto.NgayKetThuc,
                    TrangThai = voucherDto.TrangThai,
                    NgayTao = DateTime.UtcNow,
                    NguoiTao = voucherDto.NguoiTao,
                    NgayUpdate = null,
                    NguoiUpdate = null,
                    
                };

                // Save the voucher to the database
                _context.vouchers.Add(voucher);
                var saveResult = await _context.SaveChangesAsync();

                if (saveResult > 0)
                {
                    _logger.LogInformation("Voucher saved successfully.");

                    // Handle customer IDs for voucher usage history
                    if (selectedKhachHangIds != null && selectedKhachHangIds.Any())
                    {
                        await AddVoucherUsageHistoriesAsync(voucher.VoucherId, selectedKhachHangIds);
                    }
                    else
                    {
                        _logger.LogWarning("No customer IDs selected for voucher usage history.");
                    }

                    return true;
                }

                _logger.LogWarning("Failed to save voucher to database.");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateAsync method in VoucherRepo.");
                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException, "Inner exception details.");
                }
                throw; // Rethrow to handle in upper layers
            }
        }

        private async Task AddVoucherUsageHistoriesAsync(Guid voucherId, List<Guid> selectedKhachHangIds)
        {
            foreach (var khachHangId in selectedKhachHangIds)
            {
                var lichSuSuDung = new LichSuSuDungVoucher
                {
                    IdLichSuVoucher = Guid.NewGuid(),
                    IdKhachHang = khachHangId,
                    IdVoucher = voucherId,
                    TrangThaiVoucher = 1
                };

                _context.LichSuSuDungVouchers.Add(lichSuSuDung);
            }

            var historyResult = await _context.SaveChangesAsync();
            if (historyResult > 0)
            {
                _logger.LogInformation($"Successfully added voucher usage history for Voucher ID: {voucherId}");
            }
            else
            {
                _logger.LogWarning("Failed to save voucher usage history.");
            }
        }
        public async Task<bool> UpdateAsync(Voucher voucher)
        {
            try
            {
                var existingVoucher = await _context.vouchers.FindAsync(voucher.VoucherId);
                if (existingVoucher == null)
                {
                    return false;
                }

                // Update properties
                existingVoucher.MaVoucher = voucher.MaVoucher;
                existingVoucher.MoTaVoucher = voucher.MoTaVoucher;
                existingVoucher.LoaiGiamGia = voucher.LoaiGiamGia;
                existingVoucher.GiaTriGiam = voucher.GiaTriGiam;
                existingVoucher.GiaTriDonHangToiThieu = voucher.GiaTriDonHangToiThieu;
                existingVoucher.SoTienToiDa = voucher.SoTienToiDa;
                existingVoucher.NgayBatDau = voucher.NgayBatDau;
                existingVoucher.NgayKetThuc = voucher.NgayKetThuc;
                existingVoucher.TrangThai = voucher.TrangThai;
                existingVoucher.NgayUpdate = voucher.NgayUpdate;
                existingVoucher.NguoiUpdate = voucher.NguoiUpdate;

                _context.vouchers.Update(existingVoucher);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in VoucherRepo.UpdateAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var voucher = await _context.vouchers.FindAsync(id);
                if (voucher != null)
                {
                    var xoaLichSuVoucher = _context.LichSuSuDungVouchers.Where(ls => ls.IdVoucher == id);
                    _context.LichSuSuDungVouchers.RemoveRange(xoaLichSuVoucher);
                    _context.vouchers.Remove(voucher);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;   
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in VoucherRepo.DeleteAsync: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> AddLichSuSuDungVoucherAsync(LichSuSuDungVoucher lichSuSuDungVoucher)
        {
            try
            {
                _context.LichSuSuDungVouchers.Add(lichSuSuDungVoucher);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in VoucherRepo.AddLichSuSuDungVoucherAsync: {ex.Message}");
                return false;
            }
        }
        public async Task<List<KhachHang>> GetKhachHangDaNhanVoucherAsync(Guid voucherId)
        {
            return await _context.LichSuSuDungVouchers
                .Where(ls => ls.IdVoucher == voucherId)
                .Select(ls => ls.KhachHang)
                .ToListAsync();
        }

        public async Task<List<Voucher>> GetAvailableVouchersForCustomerAsync(Guid khachHangId)
        {
            try
            {
                var availableVouchers = await _context.vouchers
                    .Where(v =>
                        v.TrangThai == 2 && // Chỉ lấy voucher có TrangThai = 2 (Đang kích hoạt)
                        v.NgayBatDau <= DateTime.Now &&
                        v.NgayKetThuc >= DateTime.Now &&
                        _context.LichSuSuDungVouchers.Any(ls =>
                            ls.IdVoucher == v.VoucherId &&
                            ls.IdKhachHang == khachHangId &&
                            ls.TrangThaiVoucher == 1 // Chỉ lấy voucher chưa sử dụng
                        )
                    )
                    .ToListAsync();

                return availableVouchers;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting available vouchers: {ex.Message}");
                return new List<Voucher>();
            }
        }
        public async Task<bool> MaVoucher(string maVoucher)
        {
            return await _context.vouchers.AnyAsync(x => x.MaVoucher == maVoucher);
        }
        public async Task<bool> UpdateVoucherStatusAsync(Voucher voucher, int status)
        {
            try
            {
                var existingVoucher = await _context.vouchers.FindAsync(voucher.VoucherId);
                if (existingVoucher == null)
                {
                    return false;
                }
                existingVoucher.TrangThai = status;
                _context.vouchers.Update(existingVoucher);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating status for voucher {voucher}");
                return false;
            }
        }
    }
}
