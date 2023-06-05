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
      int selectedClass = MenuLogic.MultipleChoice(true, "", 1, new string[] { }, $"Email: {email}", $"Wachtwoord: {new string('*', password.Length)}", "", "Login", "Annuleren");
      if (selectedClass == 0)
      {
        Console.WriteLine("Graag uw email invullen.");
        email = Console.ReadLine();
      }
      else if (selectedClass == 1)
      {
        Console.WriteLine("Graag uw wachtwoord invullen.");
        password = Console.ReadLine();
      }
      else if (selectedClass == 4)
      {
        Menu.Start();
      }
      else if (selectedClass == 3)
      {
        accountNull = false;
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
          else if (level == 1) // aka het is een medewerker
          {
            Menu.Worker(id, naam);
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