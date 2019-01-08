using System;

namespace IJERTS.Common
{
    public static class CommonHelper
    {

        public const string SaltPassword = "a#$QJMJADL@JE";
        public const string EncryptKey = "a8d3eb40-8184-43a9-a95f-869a2a33d864";

        public static string GenerateDynamicPassword()
        {
            try
            {
                int PasswordLength = 8;
                string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ@#$!*";
                Random randNum = new Random();
                char[] chars = new char[PasswordLength];
                int allowedCharCount = _allowedChars.Length;
                for (int i = 0; i < PasswordLength; i++)
                {
                    chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
                }
                return new string(chars);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
