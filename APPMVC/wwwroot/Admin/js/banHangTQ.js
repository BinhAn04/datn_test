$(document).on('click', '#btnPayVNPay', function () {
     var idHoaDon = '@Model.HoaDon.IdHoaDon';
     console.log("ID Hóa Đơn 111:", idHoaDon);
     $.ajax({
         url: '/Admin/BanHangTQ/ThanhToanVNPay',
         type: 'POST',
         data: { idHoaDon: idHoaDon },
         success: function (response) {
             if (response.paymentUrl) {
                 window.location.href = response.paymentUrl;
             } else {
                 alert('Không thể tạo giao dịch VNPay.');
             }
         },
         error: function () {
             alert('Có lỗi xảy ra khi tạo giao dịch VNPay.');
         }
     });
});
