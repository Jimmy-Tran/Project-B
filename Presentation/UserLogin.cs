namespace Project_B.Logic;

static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        string email = "";
        string password = "";
        Console.Clear();

        bool accountNull = true;
        while (accountNull is true)
        {
            int selectedClass = MenuLogic.MultipleChoice(true, "", 1, new string[] { }, $"Email: {email ?? "N/A"}", $"Wachtwoord: {new string('*', password?.Length ?? 0)}", "", "Login", "Annuleren");
            if (selectedClass == 0)
            {
                Console.WriteLine("Graag uw email invullen.");
                email = Console.ReadLine() ?? string.Empty;
            }
            else if (selectedClass == 1)
            {
                Console.WriteLine("Graag uw wachtwoord invullen.");
                password = Console.ReadLine() ?? string.Empty;
            }
            else if (selectedClass == 4)
            {
                Menu.Start();
            }
            else if (selectedClass == 3 && email != null && password != null)
            {
                accountNull = false;
                AccountModel? acc = accountsLogic.CheckLogin(email, password);
                if (acc != null)
                {
                    //Console.WriteLine("Welcome back " + acc.FullName);
                    //Console.WriteLine("Your email number is " + acc.EmailAddress);
                    int id = acc.Id;
                    string naam = acc.FullName;
                    int level = acc.Level;
                    string mail = acc.EmailAddress;
                    string ww = acc.Password;

                    AccountModel? persoon = null;
                    // op basis van level geef de user zn eigen model
                    if (level == 1) // Manager
                    {
                        persoon = new Manager(id, mail, ww, naam, level);
                        // Additional manager-specific logic here
                    }
                    else if (level == 2) // Employee
                    {
                        persoon = new Employee(id, mail, ww, naam, level);
                        // Additional employee-specific logic here
                    }
                    else if (level == 0) // Customer
                    {
                        persoon = new Customer(id, mail, ww, naam, level);
                        // Additional customer-specific logic here
                    }
                    if (persoon != null)
                    {
                        Menu.Continue(persoon);
                    }
                    else
                    {
                        Console.WriteLine($"Er ging iets fout! je word terug naar de menu gestuurd");
                        Menu.Start();
                    }
                }
                else
                {
                    Console.WriteLine("No account found with that email and password");
                    System.Threading.Thread.Sleep(1000);
                    Start();
                }
            }
        }
    }
}