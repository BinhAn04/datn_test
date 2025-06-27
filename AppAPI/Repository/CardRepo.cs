using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Repository
{
    public class CardRepo : ICardRepo
    {
        private readonly AppDbcontext _context;

        public CardRepo(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<bool> Create(GioHang gioHang)
        {
            try
            {
                await _context.gioHang.AddAsync(gioHang);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error adding cart: {ex.Message}");
                return false;
            }
        }

        public async Task<Guid> GetCartIdByCustomerIdAsync(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                Console.WriteLine("Invalid customer ID provided.");
                return Guid.Empty; // Trả về Guid.Empty nếu customerId không hợp lệ
            }

            try
            {
                // Tìm giỏ hàng theo customerId
                var cart = await _context.gioHang
                    .AsNoTracking() // Không theo dõi thay đổi để cải thiện hiệu suất
                    .FirstOrDefaultAsync(c => c.IdKhachHang == customerId);

                if (cart == null)
                {
                    Console.WriteLine($"No cart found for customer ID: {customerId}");
                    return Guid.Empty; // Trả về Guid.Empty nếu không tìm thấy giỏ hàng
                }

                return cart.IdGioHang; // Trả về ID giỏ hàng
            }
            catch (Exception ex)
            {
                // Log lỗi
                Console.WriteLine($"Error fetching cart ID for customer {customerId}: {ex.Message}");
                return Guid.Empty; // Trả về Guid.Empty khi có lỗi
            }
        }
    }
}