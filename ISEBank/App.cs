using ISEBank.Menus;

namespace ISEBank
{
    internal class App
    {
        public static bool ShouldExit { get; private set; } = false;

        static void Main(string[] args)
        {
            bool loggedIn = false; // Replace this with the actual user validity flag


            var mainMenu = new SubMenuEntry("Main", "Main Menu")
            {
                Entries =
                {
                    new ActionMenuEntry("signup", "Sign up", (e) =>
                        {
                            Console.WriteLine("Handling Signup procedure.");
                            //TODO:: Lucy to add signup procedure call
                            loggedIn = true;
                            //e.Parent.handling = false;
                        },
                        () => !loggedIn
                    ),
                    new ActionMenuEntry("login", "Login", (e) =>
                    {
                        Console.WriteLine("Handling Login procedure.");
                        //TODO:: @Mitchell to add login procedure call
                        loggedIn = true;
                        //e.Parent.handling = false;
                    },
                    () => !loggedIn
                    ),
                    new SubMenuEntry("transaction", "Transaction Menu", () => loggedIn)
                    {
                        Entries =
                        {
                            new SubMenuEntry("sendMoney", "Send Money")
                        }
                    },
                    new ActionMenuEntry("logout", "Log Out", (e) =>
                        {
                            loggedIn = false;
                        },
                        () => loggedIn
                    ),
                    new ActionMenuEntry("exit", "Exit", (e) =>
                    {
                        ShouldExit = true;
                        Console.Clear();
                        Console.WriteLine("Goodbye!");
                    })
                }
            };
            
            mainMenu.Select();
        }
    }
}