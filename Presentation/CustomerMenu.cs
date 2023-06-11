namespace Project_B.Logic;
public class CustomerMenu
{
    static public void Start(string username, int id) // ingelogd geef parameter's mee om aan te geven dat de persoon is ingelogd
    {

        int selectedClass = MenuLogic.MultipleChoice(true, "", 1, new string[] { }, "Menu bekijken", "Reserveren", "Mijn reservering", "Restaurant informatie", "Mijn gegevens", "Log uit");
        if (selectedClass == 0)
        {
            // voor stellen om een foto te laten up poppen van een menu kaart, anders vraag wat precies geshowed moet worden
            MenuPresentation.Menu();
            Console.ReadKey();
            Start(username, id);
        }
        else if (selectedClass == 1)
        {
            // ga naar reserveren waar je een paar optie's weer krijgt
            // Reservatie res = new Reservatie();
            ReservationConsole res = new ReservationConsole();
            res.Reserveren(id, username);
        }
        else if (selectedClass == 2)
        {
            // start de locatie class en show detail
            MyReservation.ShowReservationInfo(username, id, MyReservation.GetReservationCode(username, id));
            Start(username, id);
        }
        else if (selectedClass == 3)
        {
            // start de locatie class en show detail
            Location? location = Location.CreateLocation();
            if (location != null)
            {
                LocationPresentation.ShowLocation(location);
            }
            Start(username, id);
        }
        else if (selectedClass == 4)
        {
            AccountData.Start(id);
        }

        else if (selectedClass == 5)
        {
            // roep welkom aan sinds je word uitgelogd
            Welkom.welkom();
            // je word gestuurd naar start aka je bent uitgelogd'
            Menu.Start();
        }

    }
}