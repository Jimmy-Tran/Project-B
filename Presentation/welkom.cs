static class Welkom
{

    // zeg welkom
    static public void welkom()
    {
        Console.WriteLine("");
    }

    static public void login()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
 ___       ________  ________  ___  ________      
|\  \     |\   __  \|\   ____\|\  \|\   ___  \    
\ \  \    \ \  \|\  \ \  \___|\ \  \ \  \\ \  \   
 \ \  \    \ \  \\\  \ \  \  __\ \  \ \  \\ \  \  
  \ \  \____\ \  \\\  \ \  \|\  \ \  \ \  \\ \  \ 
   \ \_______\ \_______\ \_______\ \__\ \__\\ \__\
    \|_______|\|_______|\|_______|\|__|\|__| \|__|
        ");
    }

}