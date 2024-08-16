
using Anpero.PaymentHelper.Model;
using System;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace Anpero.PaymentHelper.Control.QR
{
    public class QRCodeGenerator
    {
        private static string ConvertLength(string str)
        {
            int num = int.Parse(str);
            return num < 10 ? $"0{num}" : num.ToString();
        }

        private static string GetMerchantAccountInformation(string bankId, string bankAccount, string guid,string serviceCode)
        {
            string acquierLength = ConvertLength(bankId.Length.ToString());
            string acquier = $"00{acquierLength}{bankId}";
            string consumerLength = ConvertLength(bankAccount.Length.ToString());
            string consumer = $"01{consumerLength}{bankAccount}";
            string beneficiaryOrganizationLength = ConvertLength($"{acquier}{consumer}".Length.ToString());
            int length = ($"{guid}01{beneficiaryOrganizationLength}{serviceCode}").Length;
            string consumerAccountInformationLength = ConvertLength((int.Parse(beneficiaryOrganizationLength) + length).ToString());
            
            var rs = $"38{consumerAccountInformationLength}{guid}01{beneficiaryOrganizationLength}{acquier}{consumer}{serviceCode}";
            return  $"38{consumerAccountInformationLength}{guid}01{beneficiaryOrganizationLength}{acquier}{consumer}{serviceCode}";

        }
        private static string GetTransactionAmount(string money)
        {
            string length = ConvertLength(money.Length.ToString());
            return $"54{length}{money}";// Amount with length

        }
        private static string GetAdditionalDataFieldTemplate(string content)
        {
            if (!string.IsNullOrEmpty(content)) {
                if (content.Length > 99) {
                    content = content.Substring(0, 98);
                }
                string contentLength = ConvertLength(content.Length.ToString());
                string additionalDataFieldTemplateLength = ConvertLength((content.Length + 4).ToString());
                return $"62{additionalDataFieldTemplateLength}08{contentLength}{content}";
            }
            else
            {
                return string.Empty;
            }
            
        }
        public static CheckOutResultModel CreateQRCode(string bankAccount, string bankId, string message, string amount, CurrencyCode currency = CurrencyCode.VND, string service = "0208QRIBFTTA", string countryCode = "VN", string guid = "0010A000000727")
        {
            // Create the initial QR code content
            StringBuilder qrCodeContent = new StringBuilder();
            qrCodeContent.Append("000201"); // Payload Format Indicator
            //11 = QR static – applied when the QR code allows multiple transactions.
            //12 = Dynamic QR – applies when a QR code is allowed for a single transaction.
            qrCodeContent.Append("010212"); // Point of Initiation Method

            // Merchant Account Information
            qrCodeContent.Append(GetMerchantAccountInformation(bankId, bankAccount, guid, service));
            qrCodeContent.Append($"5303{(int)currency:000}"); // Currency
            qrCodeContent.Append(GetTransactionAmount(amount));  // Amount with length
            qrCodeContent.Append($"5802{countryCode}"); // Country Code
            qrCodeContent.Append(GetAdditionalDataFieldTemplate(message));

            // Calculate CRC
            string qrCodeWithoutCRC = qrCodeContent.ToString() + "6304";
            ushort crc = CRC16.ComputeChecksum(qrCodeWithoutCRC);
            string crcHex = crc.ToString("X4");
            var qrData = qrCodeWithoutCRC + crcHex;
            bool isValid = CRC16.ValidateQRCode(qrData);
            return new CheckOutResultModel {
                checkoutUrl = qrData,
                code = isValid ? "0" : "-1",
                message = isValid? "success":"failed, invalid qr code"
                
            };
        }        

    }
    internal class CRC16
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
        public static bool ValidateQRCode(string qrData)
        {         
            if (qrData.Length < 4) return false;
            string data = qrData.Substring(0, qrData.Length - 4);
            string crcString = qrData.Substring(qrData.Length - 4);
            ushort expectedCrc = Convert.ToUInt16(crcString, 16);
            ushort computedCrc = ComputeChecksum(data);
            return expectedCrc == computedCrc;
        }
    }
}

