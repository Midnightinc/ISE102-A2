using System;
using System.Text;

namespace ISEBank
{
    internal class Bank
    {
        readonly static Random random = new((int)DateTime.UtcNow.Ticks);

        private static bool PasswordCheck(string passwordEntered, User user)
        {
            int passwordAttempt = String.Concat(passwordEntered, user.UserSalt).GetHashCode();

            return (passwordAttempt == user.Password);
        }
        
        /// <summary>
        /// Generates randomised string, to be added to password before <c>GetHashCode()</c> is used.
        /// </summary>
        public static string GenerateSalt(int saltLength = 5)
        {
            int minUniCode = 48, maxUnicode = 122;
            saltLength = Math.Abs(saltLength); //Ensures it's a positive number
            StringBuilder sb = new();

            for(int i = 0; i < saltLength; i++)
            {
                int randNumber = random.Next(minUniCode, maxUnicode + 1);
                sb.Append((char)randNumber);
            }

            return sb.ToString();
        }
    }
}