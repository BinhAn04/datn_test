using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Validation
{
    public class CheckTenMauSac : ValidationAttribute
    {
        private readonly AppDbcontext _context;

        public CheckTenMauSac()
        {
            _context = new AppDbcontext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = value as string;
            if (string.IsNullOrEmpty(name))
                return ValidationResult.Success;

            // Lấy DbContext từ dependency injection
            var _context = validationContext.GetService(typeof(AppDbcontext)) as AppDbcontext;

            if (_context == null)
            {
                return ValidationResult.Success;
            }

            try
            {
                var objectType = validationContext.ObjectType;
                var idProperty = objectType.GetProperty("IdMauSac");

                if (idProperty == null)
                {
                    return ValidationResult.Success;
                }

                var mauSacId = (Guid)idProperty.GetValue(validationContext.ObjectInstance);

                // Sử dụng phương thức FirstOrDefault thay vì Any để tránh việc mở nhiều DataReader
                var existing = _context.mauSacs
                    .AsNoTracking()
                    .FirstOrDefault(ms =>
                        ms.TenMauSac.ToLower() == name.ToLower() &&
                        ms.IdMauSac != mauSacId);

                if (existing != null)
                {
                    return new ValidationResult("Tên màu sắc đã tồn tại", new[] { "TenMauSac" });
                }

                return ValidationResult.Success;
            }
            catch (Exception)
            {
                // Nếu có lỗi, trả về Success để không block việc validate
                return ValidationResult.Success;
            }
        }
    }
}