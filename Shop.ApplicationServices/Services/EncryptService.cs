using System.Security.Cryptography;
using System.Text;

namespace Shop.ApplicationServices.Services
{
    public class EncryptService
    {
        public static byte[] EncryptData(string data)
        {
            byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(data);
            byte[] encryptedBytes = ProtectedData.Protect(bytesToEncrypt, null, DataProtectionScope.CurrentUser);
            return encryptedBytes;
        }

        public static string DecryptData(byte[] encryptedBytes)
        {
            byte[] decryptedBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
            string decryptedData = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedData;
        }
    }
}
