using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Validation
{
    public class CheckTenThuongHieu : ValidationAttribute
    {
        private readonly AppDbcontext _context;

        public CheckTenThuongHieu()
        {
            _context = new AppDbcontext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = value as string;
            if (string.IsNullOrEmpty(name))
                return ValidationResult.Success;

            var objectType = validationContext.ObjectType;
            var idProperty = objectType.GetProperty("IdThuongHieu");
            if (idProperty == null)
            {
                // Handle the case where Id property is not found
                return ValidationResult.Success; // or return an error message if needed
            }

            var thuongHieuId = (Guid)idProperty.GetValue(validationContext.ObjectInstance);
            var existing = _context.thuongHieus
                .AsNoTracking() // Add this to avoid tracking issues
                .Any(th => th.TenThuongHieu == name && th.IdThuongHieu != thuongHieuId);

            if (existing)
            {
                return new ValidationResult("Tên thương hiệu đã tồn tại", new[] { "TenThuongHieu" });
            }
            return ValidationResult.Success;
        }
    }
}