using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Validation
{
    public class CheckTenChatLieu : ValidationAttribute
    {
        private readonly AppDbcontext _context;

        public CheckTenChatLieu()
        {
            _context = new AppDbcontext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = value as string;
            if (string.IsNullOrEmpty(name))
                return ValidationResult.Success;

            var objectType = validationContext.ObjectType;
            var idProperty = objectType.GetProperty("IdChatLieu");
            if (idProperty == null)
            {
                // Handle the case where Id property is not found
                return ValidationResult.Success; // or return an error message if needed
            }

            var chatLieuId = (Guid)idProperty.GetValue(validationContext.ObjectInstance);
            var existing = _context.chatLieus
                .AsNoTracking() // Add this to avoid tracking issues
                .Any(cl => cl.TenChatLieu == name && cl.IdChatLieu != chatLieuId);

            if (existing)
            {
                return new ValidationResult("Tên chất liệu đã tồn tại", new[] { "TenChatLieu" });
            }
            return ValidationResult.Success;
        }
    }
}