using System;
using System.Text;

namespace ISEBank
{
    internal class Bank
    {
        readonly static Random random = new((int)DateTime.UtcNow.Ticks);
        static List<User> bankUsers = [new("JoeDoe", "Password123")];
        public const int lockOutMinutes = 15;

        public static bool Login(string username, string password)
        {
            if (UsernameCheck(username, out User? foundUser))
            {
                return PasswordCheck(password, foundUser);
            }
            else return false;
        }

        private static bool UsernameCheck(string username, out User? foundUser)
        {
            if (bankUsers?.Count > 0)
            {
                foundUser = bankUsers.Find(x => x.Username.ToLower().Equals(username.ToLower()));
                return (foundUser != null);
            }
            else
            {
                foundUser = null;
                return false;
            }
        }

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