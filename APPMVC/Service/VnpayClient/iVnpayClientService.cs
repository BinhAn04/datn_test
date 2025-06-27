using AppData.Model.Vnpay;

namespace APPMVC.Service.VnpayClient
{
    public interface iVnpayClientService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
