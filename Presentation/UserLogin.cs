static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            //Console.WriteLine("Welcome back " + acc.FullName);
            //Console.WriteLine("Your email number is " + acc.EmailAddress);
            int id = acc.Id;
            string naam = acc.FullName;
            int level = acc.Level;


            //Write some code to go back to the menu
            // ingelogd versie
            if (level == 0) // wanneer het een gewone gast is
            {
                Menu.Continue(id, naam);
            }
            else if (level == 1) // aka het is een admin
            {
                Menu.Admin(id, naam);
            }


        }
        else
        {
            Console.WriteLine($"No account found with that email({email}) and password({password})");
            Start();
        }
    }
}