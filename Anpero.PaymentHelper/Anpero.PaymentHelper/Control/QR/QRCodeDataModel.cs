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
        public string payloadFormatIndicator { get; set; } = "000201";
        //11 = QR static – applied when the QR code allows multiple transactions.
        //12 = Dynamic QR – applies when a QR code is allowed for a single transaction.
        public string pointOfInitiationMethod { get; set; } = "010212";
        public string consumerAccountInformation { get; set; } = "";
        public string guid { get; set; } = "0010A000000727";
        public string serviceCode = "0208QRIBFTTA";//napas
        public string currency { get; set; } = "";
        public string transactionAmount { get; set; } = "";
        public string countryCode { get; set; } = "5802VN";
        public string additionalDataFieldTemplate { get; set; } = "";
        public string crc { get; set; } = "";

    }
}
