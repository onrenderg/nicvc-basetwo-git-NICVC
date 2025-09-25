using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NICVC.Model
{
    public class AESCryptography
    {
        public static string key = "NIC&VC@24@Sep";
        public static string EncryptAES(string plainText)
        {
            try
            {
#if DEBUG
                {
                    var plainBytes = Encoding.UTF8.GetBytes(plainText);
                    return Convert.ToBase64String(Encrypt(plainBytes, getRijndaelManaged(key)));
                }
#else
                {
               
                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(Encrypt(plainBytes, getRijndaelManaged(key)));
                }
#endif

            }
            catch (Exception)
            {
#if DEBUG
                {
                    return plainText;
                }
#else
                {
return  "";
                }
#endif
            }

        }

        public static string DecryptAES(string encryptedText)
        {
            try
            {

                var encryptedBytes = Convert.FromBase64String(encryptedText);
                return Encoding.UTF8.GetString(Decrypt(encryptedBytes, getRijndaelManaged(key)));
            }
            catch (Exception)
            {
#if DEBUG
                {
                    return encryptedText;
                }
#else
                {
return  "";
                }
#endif
            }

        }

        private static RijndaelManaged getRijndaelManaged(String secretKey)
        {
            var keyBytes = new byte[16];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = keyBytes
            };
        }

        private static byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        private static byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }
    }
}
