//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AppData.Validation
//{
//    public class CheckTenDayGiay : ValidationAttribute
//    {
//        private readonly AppDbcontext _context;

//        public CheckTenDayGiay()
//        {
//            _context = new AppDbcontext();
//        }

//        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//        {
//            var name = value as string;
//            if (string.IsNullOrEmpty(name))
//                return ValidationResult.Success;

//            var objectType = validationContext.ObjectType;
//            var idProperty = objectType.GetProperty("IdDayGiay");
//            if (idProperty == null)
//            {
//                // Handle the case where Id property is not found
//                return ValidationResult.Success; // or return an error message if needed
//            }

//            var dayGiayId = (Guid)idProperty.GetValue(validationContext.ObjectInstance);
//            var existing = _context.dayGiay
//                .AsNoTracking() // Add this to avoid tracking issues
//                .Any(dg => dg.TenDayGiay == name && dg.IdDayGiay != dayGiayId);

//            if (existing)
//            {
//                return new ValidationResult("Tên giày đã tồn tại", new[] { "TenDayGiay" });
//            }
//            return ValidationResult.Success;
//        }
//    }
//}

