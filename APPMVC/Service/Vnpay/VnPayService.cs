using AppData.Model.Vnpay;
using APPMVC.Libraries;

namespace APPMVC.Service.Vnpay
{
    public class VnPayService : IVnPayService
    {
        private IConfiguration _configuration;
        public VnPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            //var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            //var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            //var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString());

            pay.AddRequestData("vnp_CreateDate", model.CreatDate.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo","Thanh Toán Đơn Hàng" + model.OrderId);
            pay.AddRequestData("vnp_OrderType","Order Type" + model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", _configuration["Vnpay:ReturnUrl"]);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }


        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            foreach (var(key, value) in collections)
            {
                if(!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    pay.AddResponseData(key, value.ToString());
                }
                    
            }
            var vnp_orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collections.FirstOrDefault(p =>  p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = pay.GetResponseData("vnp_OrderInfo");
            bool checkSignature = pay.ValidateSignature(vnp_SecureHash, _configuration["Vnpay:HashSecret"]);
            if(!checkSignature)
            {
                return new PaymentResponseModel
                {
                    Success = false,
                };
            }

            return new PaymentResponseModel
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderId = vnp_orderId.ToString(),
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash.ToString(),
                VnPayResponseCode = vnp_ResponseCode.ToString(),
            };
        }

    }
}
