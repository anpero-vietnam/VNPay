
using System;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace Anpero.PaymentHelper.Control.QR
{

    public class VietQR
    {
        private readonly string payloadFormatIndicator = "000201";
        private readonly string pointOfInitiationMethod = "010212";
        private string consumerAccountInformation = "";
        private readonly string guid = "0010A000000727";
        private readonly string serviceCode = "0208QRIBFTTA";
        private readonly string transactionCurrency = "5303704";
        private string transactionAmount = "100000";
        private readonly string countryCode = "5802VN";
        private string additionalDataFieldTemplate = "test";
        private string crc = "";

        private string ConvertLength(string str)
        {
            int num = int.Parse(str);
            return num < 10 ? $"0{num}" : num.ToString();
        }

        public void SetTransactionAmount(string money)
        {
            string length = ConvertLength(money.Length.ToString());
            transactionAmount = $"54{length}{money}";
         
        }

        public void SetBeneficiaryOrganization(string acquierID, string consumerID)
        {
            string acquierLength = ConvertLength(acquierID.Length.ToString());
            string acquier = $"00{acquierLength}{acquierID}";
            string consumerLength = ConvertLength(consumerID.Length.ToString());
            string consumer = $"01{consumerLength}{consumerID}";
            string beneficiaryOrganizationLength = ConvertLength($"{acquier}{consumer}".Length.ToString());

            string consumerAccountInformationLength = ConvertLength((int.Parse(beneficiaryOrganizationLength) + 30).ToString());
            // 3853 0010A000000727012300069704230109mynamebvh0208QRIBFTTA
            // 0010A000000727 --14 +2 
            // 0208QRIBFTTA

            consumerAccountInformation = $"38{consumerAccountInformationLength}{guid}01{beneficiaryOrganizationLength}{acquier}{consumer}{serviceCode}";
            
        }

        public void SetAdditionalDataFieldTemplate(string content)
        {
            string contentLength = ConvertLength(content.Length.ToString());
            string additionalDataFieldTemplateLength = ConvertLength((content.Length + 4).ToString());
            additionalDataFieldTemplate = $"62{additionalDataFieldTemplateLength}08{contentLength}{content}";
           
        }

        
        public string Build()
        {
            SetAdditionalDataFieldTemplate("test");
            SetBeneficiaryOrganization("970422", "00570406011832");
            
            
            SetTransactionAmount("1000000");

            string contentQR = $"{payloadFormatIndicator}{pointOfInitiationMethod}{consumerAccountInformation}{transactionCurrency}{transactionAmount}{countryCode}{additionalDataFieldTemplate}6304";

            ushort crc = CRC16.ComputeChecksum(contentQR);
            string crcHex = crc.ToString("X4");
            return contentQR + crcHex;
        }
    }
    public class QRCodeGenerator
    {
        public static string CreateQRCode(string bankAccount, string bankId, string message, int amount, CurrencyCode currency = CurrencyCode.VND, string service = "0208QRIBFTTA", string countryCode = "VN", string guid = "A000000727")
        {
            // Create the initial QR code content
            StringBuilder qrCodeContent = new StringBuilder();
            qrCodeContent.Append("000201"); // Payload Format Indicator
            qrCodeContent.Append("010211"); // Point of Initiation Method

            // Merchant Account Information
            qrCodeContent.Append("26"); // Merchant Account Information Template
            qrCodeContent.Append("00"); // Bank Info ID (Assume default for simplicity)
            qrCodeContent.Append($"{guid.Length:D2}{guid}"); // GUID with length
            qrCodeContent.Append($"010{service.Length:D2}{service}"); // Service Length
            qrCodeContent.Append($"02{bankAccount.Length:D2}{bankAccount}");

            qrCodeContent.Append($"5802{countryCode}"); // Country Code
            qrCodeContent.Append($"5303{(int)currency:000}"); // Currency

            string amountStr = amount.ToString();
            qrCodeContent.Append($"54{amountStr.Length:00}{amountStr}"); // Amount with length

            qrCodeContent.Append($"62{message.Length:D2}{message}"); // Additional Data Field Template
            qrCodeContent.Append($"29{bankId.Length:D2}{bankId}"); // Bank ID

            // Calculate CRC
            string qrCodeWithoutCRC = qrCodeContent.ToString() + "6304";
            ushort crc = CRC16.ComputeChecksum(qrCodeWithoutCRC);
            string crcHex = crc.ToString("X4");

            return qrCodeWithoutCRC + crcHex;
        }
        

    }
    public class CRC16
    {
        private static readonly ushort[] crcTable = new ushort[256];

        static CRC16()
        {
            const ushort polynomial = 0x1021;
            for (ushort i = 0; i < crcTable.Length; ++i)
            {
                ushort crc = 0;
                ushort c = (ushort)(i << 8);
                for (byte j = 0; j < 8; ++j)
                {
                    if (((crc ^ c) & 0x8000) != 0)
                    {
                        crc = (ushort)((crc << 1) ^ polynomial);
                    }
                    else
                    {
                        crc <<= 1;
                    }
                    c <<= 1;
                }
                crcTable[i] = crc;
            }
        }

        public static ushort ComputeChecksum(string input)
        {
            ushort crc = 0xFFFF;
            foreach (byte b in System.Text.Encoding.ASCII.GetBytes(input))
            {
                byte tableIndex = (byte)(((crc >> 8) ^ b) & 0xFF);
                crc = (ushort)((crc << 8) ^ crcTable[tableIndex]);
            }
            return crc;
        }
    }
}

