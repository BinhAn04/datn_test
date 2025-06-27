using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model.Vnpay
{
    public class PaymentInformationModel
    {
        public Guid OrderId { get; set; }
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public DateTime CreatDate { get; set; }
        public Guid idHoadon { get; set; }

    }
}
