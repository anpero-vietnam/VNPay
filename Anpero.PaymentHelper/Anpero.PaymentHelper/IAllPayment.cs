using Anpero.PaymentHelper.Model;
using Anpero.PaymentHelper.Model.Alepay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anpero.PaymentHelper
{
    public interface IAllPayment
    {
        CheckOutResultModel? GetCheckoutUrl(OrderModel data);
        TransactionDetailModel ProcessCallBackData(string token);
    }
}
