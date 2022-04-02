using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commento.Helpers
{
    public static class Conversion
    {
        public static byte[] ConvertHexToByte(string inputToDecode)
        {
            byte[] decodeByte = Enumerable.Range(0, inputToDecode.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(inputToDecode.Substring(x, 2), 16))
                     .ToArray();
            return decodeByte;

        }

        public static string ConvertStringToHex(String input)
        {
            byte[] bytes = Encoding.Default.GetBytes(input);
            StringBuilder sbBytes = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }

        public static string ConvertByteToHex(byte[] input)
        {
            StringBuilder sbBytes = new StringBuilder(input.Length * 2);
            foreach (byte b in input)
            {
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }
    }
}
