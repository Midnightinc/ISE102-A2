//TODO - Add Names and Student IDs

using ISEBank.Flags;
using ISEBank.Menus;

namespace ISEBank
{
    internal class App
    {
        public static bool ShouldExit { get; private set; } = false;

        static void Main(string[] args)
        {
            LoginFlag loginStatus = new DefaultFlag();

            var mainMenu = new SubMenuEntry("Main", "Main Menu")
            {
                Entries =
                {
                    new ActionMenuEntry("signup", "Sign up", (e) =>
                        {
                            Console.WriteLine("Handling Signup procedure.");
                            //TODO:: Lucy to add signup procedure call
                            loginStatus = new SuccessFlag("[Insert Username Here]");
                            //e.Parent.handling = false;
                        },
                        () => !loginStatus.IsLoggedIn()
                    ),
                    new ActionMenuEntry("login", "Login", (e) =>
                    {
                        Console.Clear();

                        if(loginStatus is LockedFlag lf)
                        {
                            if(!lf.IsCleared())
                            {
                                Console.WriteLine("Still locked out!");
                                Console.WriteLine($"Time remaining: {lf.TimeRemaining()} minutes");
                                return;
                            }
                            else
                                loginStatus = new DefaultFlag();
                        }

                        loginStatus = LoginProcess();
                        //e.Parent.handling = false;
                    },
                    () => !loginStatus.IsLoggedIn()
                    ),
                    new SubMenuEntry("transaction", "Transaction Menu", () => loginStatus.IsLoggedIn())
                    {
                        Entries =
                        {
                            new SubMenuEntry("sendMoney", "Send Money")
                        }
                    },
                    new ActionMenuEntry("logout", "Log Out", (e) =>
                        {
                            loginStatus = new DefaultFlag();
                        },
                        () => loginStatus.IsLoggedIn()
                    ),
                    new ActionMenuEntry("exit", "Exit", (e) =>
                    {
                        ShouldExit = true;
                        Console.Clear();
                        Console.WriteLine("Goodbye!");
                    })
                }
            };
            
            mainMenu.ValidateMenuStack();
            mainMenu.Select();
        }

        private static LoginFlag LoginProcess()
        {
            //TODO - Potentially move the login attempt tracking to the default flag.
            int loginAttempts = 0;
            int maxAttempts = 3;

            while (!ShouldExit && loginAttempts < maxAttempts)
            {
                Console.Clear();
                Console.WriteLine("--- Login ---\n");
                string username = RequestUserInput("Username");
                string password = RequestUserInput("Password");

                if (username != String.Empty && password != String.Empty)
                {
                    bool result = Bank.Login(username, password);

                    if (result)
                    {
                        Console.WriteLine("Login Successful!");
                        return new SuccessFlag(username);
                    }
                    else
                    {
                        loginAttempts++;

                        if (loginAttempts < maxAttempts)
                        {
                            Console.Clear();
                            Console.WriteLine("Incorrect Details.");
                            Console.WriteLine("Press [Backspace] to return to Main Menu. Or any other key to try again...");
                            ConsoleKeyInfo pressedKey = Console.ReadKey();
                            if (pressedKey.Key == ConsoleKey.Backspace)
                            {
                                Console.Clear();
                                return new DefaultFlag();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"ERROR: Maximum Attempts reached.\nLocked out for {Bank.lockOutMinutes} minutes.");
                            Console.WriteLine("Returning to Main Menu...");
                            return new LockedFlag();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\n** Error: Fields were blank");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
            }

            return new DefaultFlag();
        }

        private static string RequestUserInput(string desiredInformation)
        {
            Console.WriteLine($"Please Enter {desiredInformation}:");
            string? userInput = Console.ReadLine();

            return userInput ?? String.Empty;
        }
    }
}