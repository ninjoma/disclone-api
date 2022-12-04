using System;  
using System.IO;  
using System.Security.Cryptography;  
using System.Text;  
  
namespace disclone_api.utils
{
    public class DCrypt 
    {  
        private static readonly string EncryptionKey = Program.Settings["EncryptionKey"];

        public static string Encrypt(string plainText)  
        {  
            byte[] iv = new byte[16];  
            byte[] array;  
            using (Aes aes = Aes.Create())  
            {
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);  
                aes.IV = iv;  
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);  
                using (MemoryStream memoryStream = new MemoryStream())  
                {  
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))  
                    {  
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))  
                        {  
                            streamWriter.Write(plainText);  
                        }
                        array = memoryStream.ToArray();  
                    }  
                }  
            }  
            return Convert.ToBase64String(array);  
        }  

        public static string Decrypt( string cipherText)  
        {  
            byte[] iv = new byte[16];  
            byte[] buffer = Convert.FromBase64String(cipherText);  
            using (Aes aes = Aes.Create())  
            {  
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);  
                aes.IV = iv;  
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);  
                using (MemoryStream memoryStream = new MemoryStream(buffer))  
                {  
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))  
                    {  
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))  
                        {  
                            return streamReader.ReadToEnd();  
                        }  
                    }  
                }  
            }  
        }  
    }  
}  