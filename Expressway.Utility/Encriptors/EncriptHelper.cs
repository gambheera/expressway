using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Utility.Encriptors
{
    public class EncriptHelper
    {
        public static string ConvertFromStringToHex(String input, System.Text.Encoding encoding)
        {
            try
            {
                Byte[] stringBytes = encoding.GetBytes(input);
                StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
                foreach (byte b in stringBytes)
                {
                    sbBytes.AppendFormat("{0:X2}", b);
                }
                return sbBytes.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string ConvertFromHexToString(String hexInput, System.Text.Encoding encoding)
        {
            try
            {
                int numberChars = hexInput.Length;
                byte[] bytes = new byte[numberChars / 2];
                for (int i = 0; i < numberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16);
                }
                return encoding.GetString(bytes);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
