using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IJERTS.Common
{
    public static class IJERTSEncryptioncs
    {
        static string hashPepper = "sQnzu7wkTrgkQ";

        /// <summary>
        /// Generates randrom salt.
        /// To be used only on first time registration.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GenerateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[8];
            rng.GetNonZeroBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Hashes the input using salt and returns hash as Base64 string.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetHash(string password, string salt)
        {
            SHA512 hashAlgorithm = SHA512.Create();
            Byte[] bytes = Encoding.UTF8.GetBytes(hashPepper + password + salt);
            return Convert.ToBase64String(hashAlgorithm.ComputeHash(bytes));
        }

        /// <summary>
        /// Hashes the password with salt and result will be compared with compareHash value.
        /// </summary>
        /// <param name="password">password to be verified</param>
        /// <param name="salt">salt used</param>
        /// <param name="compareHash">Hash, against which the generated hash is verified</param>
        /// <returns></returns>
        public static bool CompareHash(string password, string salt, string compareHash)
        {

            SHA512 hashAlgorithm = SHA512.Create();
            Byte[] bytes = Encoding.UTF8.GetBytes(hashPepper + password + salt);
            return Convert.ToBase64String(hashAlgorithm.ComputeHash(bytes)) == compareHash;
        }

        /// <summary>
        /// Gets H-MAC for the data.
        /// </summary>
        public static byte[] HmacSHA256(String data, byte[] key)
        {
            String algorithm = "HmacSHA256";
            KeyedHashAlgorithm kha = KeyedHashAlgorithm.Create(algorithm);
            kha.Key = key;

            return kha.ComputeHash(Encoding.UTF8.GetBytes(data));
        }

        public static int GenerateRandomTokenNumber()
        {
            Random r = new Random();
            return r.Next(100000, 999999);
        }

        /// <summary>
        /// Not used currently.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static string Encrypt(string input, string salt, string encryptKey)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] encryptKeyBytes = Encoding.UTF8.GetBytes(encryptKey);

            byte[] encryptedBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(encryptKeyBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputBytes, 0, inputBytes.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Not used currently.
        /// </summary>
        /// <param name="encryptedInput"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static string Decrypt(string encryptedInput, string salt, string encryptKey)
        {

            byte[] inputBytes = Convert.FromBase64String(encryptedInput.Replace(' ', '+'));
            byte[] decryptedBytes = null;
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] encryptKeyBytes = Encoding.UTF8.GetBytes(encryptKey);

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(encryptKeyBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputBytes, 0, inputBytes.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
