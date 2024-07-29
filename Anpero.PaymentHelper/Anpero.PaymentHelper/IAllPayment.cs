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
        string? GetRedirectUrl(PaymentConfig config, OrderModel data);
        Task<dynamic> GetCallBackData(string token);
    }
}
