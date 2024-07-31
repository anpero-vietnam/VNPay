using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Anpero.PaymentHelper.Control.QR
{
    public class QRCodeDataModel
    {
        public string payloadFormatIndicator = "000201";
        //    //11 = QR static – applied when the QR code allows multiple transactions.
        //    //12 = Dynamic QR – applies when a QR code is allowed for a single transaction.
        public string pointOfInitiationMethod = "010212";
        public string transactionCurrency { get; set; } = "";
        public string transactionAmount { get; set; } = "";
        public string countryCode = "5802VN";
        public string additionalDataFieldTemplate { get; set; } = "";
        public string consumerAccountInformation { get; set; } = "";
        public string serviceCode = "0208QRIBFTTA";
        public string guid = "0010A000000727";
    }
}
