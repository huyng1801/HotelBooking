using System.Security.Cryptography;
using System.Text;

namespace HotelBookingAPI.Services
{
    public class PasswordHashService
    {
        public static string CreatePasswordHash(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static bool VerifyPasswordHash(string password, string storedHash)
        {
            string hashedPassword = CreatePasswordHash(password);
            return storedHash.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
