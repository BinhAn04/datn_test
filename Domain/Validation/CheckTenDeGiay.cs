using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Validation
{
    public class CheckTenDeGiay : ValidationAttribute
    {
        private readonly AppDbcontext _context;

        public CheckTenDeGiay()
        {
            _context = new AppDbcontext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = value as string;
            if (string.IsNullOrEmpty(name))
                return ValidationResult.Success;

            var objectType = validationContext.ObjectType;
            var idProperty = objectType.GetProperty("IdDeGiay");
            if (idProperty == null)
            {
                // Handle the case where Id property is not found
                return ValidationResult.Success; // or return an error message if needed
            }

            var deGiayId = (Guid)idProperty.GetValue(validationContext.ObjectInstance);
            var existing = _context.deGiay
                .AsNoTracking() // Add this to avoid tracking issues
                .Any(dg => dg.TenDeGiay == name && dg.IdDeGiay != deGiayId);

            if (existing)
            {
                return new ValidationResult("Tên đế giày đã tồn tại", new[] { "TenDeGiay" });
            }
            return ValidationResult.Success;
        }
    }
}