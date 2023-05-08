namespace Project_B.Logic;
public class CustomerMenu
{
    static public void Start(string username, int id) // ingelogd geef parameter's mee om aan te geven dat de persoon is ingelogd
    {

        int selectedClass = MenuLogic.MultipleChoice(true, "", 1, "Menu bekijken", "Reserveren", "Locatie bekijken", "Log uit");
        if (selectedClass == 0)
        {
            // voor stellen om een foto te laten up poppen van een menu kaart, anders vraag wat precies geshowed moet worden
            MenuPresentation.Menu();
            Start(username, id);
        }
        else if (selectedClass == 1)
        {
            // ga naar reserveren waar je een paar optie's weer krijgt
            // Reservatie res = new Reservatie();
            // res.Res_menu(username, id);
        }
        else if (selectedClass == 2)
        {
            // start de locatie class en show detail
            Location location = Location.CreateLocation();
            LocationPresentation.ShowLocation(location);
            Start(username, id);
        }
        else if (selectedClass == 3)
        {
            // roep welkom aan sinds je word uitgelogd
            Welkom.welkom();
            // je word gestuurd naar start aka je bent uitgelogd'
            Menu.Start();
        }

    }
}