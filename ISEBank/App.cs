using ISEBank.Menus;

namespace ISEBank
{
    internal class App
    {
        public static bool ShouldExit { get; private set; } = false;

        static void Main(string[] args)
        {
            bool loggedIn = false; // Replace this with the actual user validity flag
            
            
            var signupMenu = new ActionMenuEntry("signup", "Sign up", (e) =>
                {
                    Console.WriteLine("Handling Signup procedure.");
                    //TODO:: Lucy to add signup procedure call
                    loggedIn = true;
                    e.Parent.handling = false;
                },
                () => !loggedIn
            );
            var loginMenu = new ActionMenuEntry("login", "Login", (e) =>
                {
                    Console.WriteLine("Handling Login procedure.");
                    //TODO:: @Mitchell to add login procedure call
                    loggedIn = true;
                    e.Parent.handling = false;
                },
                () => !loggedIn
            );

            var userMenu = new SubMenuEntry("user", "User Menu");
            userMenu.AddEntry(signupMenu);
            userMenu.AddEntry(loginMenu);
            
            
            /* TEMP MENU
             * Used to verify that Logging in effects the "Conditional Display" lambda correctly
             */
            
            var sendMoneyMenu = new SubMenuEntry("sendMoney", "Send Money");
            var transactionMenu = new SubMenuEntry("transaction", "Transaction Menu", () => loggedIn);
            transactionMenu.AddEntry(sendMoneyMenu);
            
            var exitMenu = new SubMenuEntry("exit", "Exit", () => ShouldExit = true);
            
            var mainMenu = new SubMenuEntry("main", "Main Menu");
            mainMenu.AddEntry(userMenu);
            mainMenu.AddEntry(transactionMenu);
            
            mainMenu.AddEntry(exitMenu);
            
            mainMenu.Select();
        }
    }
}