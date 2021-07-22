using Expressway.Utility.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Expressway.Utility.Encriptors
{
    public class Encriptor
    {
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "pemgail9uzpgzl88";
        // This constant is used to determine the keysize of the encryption algorithm
        private const int keysize = 256;
        private static string passPhrase = "SNB";

        //Encrypt
        public static string EncryptFromLong(long number, EncriptObjectType encriptObjectType)
        {
            try
            {
                string plainText = number.ToString();

                passPhrase = GetPassPhrase(encriptObjectType);

                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();

                string hexString = EncriptHelper.ConvertFromStringToHex(Convert.ToBase64String(cipherTextBytes), Encoding.ASCII);
                return hexString;
            }
            catch (Exception ex)
            {
                return null;
            }            
        }

        //Decrypt
        public static long DecryptToLong(string cipherText, EncriptObjectType encriptObjectType)
        {
            try
            {
                passPhrase = GetPassPhrase(encriptObjectType);

                string stringString = EncriptHelper.ConvertFromHexToString(cipherText, Encoding.ASCII);

                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] cipherTextBytes = Convert.FromBase64String(stringString);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                string stringResult = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                long ctrl = 0;
                if (long.TryParse(stringResult, out ctrl))
                {
                    return ctrl;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private static string GetPassPhrase(EncriptObjectType encriptObjectType)
        {
            switch (encriptObjectType)
            {
                case EncriptObjectType.User:
                    return "USA";
                case EncriptObjectType.Ride:
                    return "PLL";
                case EncriptObjectType.Seat:
                    return "STT";
                case EncriptObjectType.Vehicle:
                    return "OPN";
                case EncriptObjectType.DriverRatingByPassenger:
                    return "DRR";
                case EncriptObjectType.PassengerRatingByDriver:
                    return "PAR";
                default:
                    return "NIL";
            }
        }
    }
}
