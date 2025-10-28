using System;

namespace ISEBank
{
    internal class User
    {
        public string Username { get; private set; }
        public int Password { get; private set; }
        public string UserSalt { get; private set; }

        public User(string username, string password)
        {
            Username = username;
            UserSalt = Bank.GenerateSalt();
            Password = $"{password}{UserSalt}".GetHashCode();
        }
    }
}
