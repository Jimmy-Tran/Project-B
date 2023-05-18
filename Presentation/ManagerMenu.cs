namespace Project_B.Logic;
public class ManagerMenu
{

    static public void Admin_menu(string username, int id)
    {
        int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] {}, "Menu", "Reserveringen ", "Medewerkers", "Log uit");

        switch (selectedClass) {
            case 0:
                selectedClass = MenuLogic.MultipleChoice(true, "", 1, new string[] {}, "Menu aanpassen", "Menu bekijken", "Terug");

                switch (selectedClass) {
                    case 0:
                        MenuAanpassen.EditMenu(username, id);
                        break;
                    case 1:
                        MenuPresentation.Menu(username, id);
                        Admin_menu(username, id);
                        break;
                    case 2:
                        Admin_menu(username, id);
                        break;
                }
                break;
            
            case 1:
                selectedClass = MenuLogic.MultipleChoice(true, "", 1, new string[] {}, "Reservering bekijken", "Reservering maken", "Reservering aanpassen", "Reservering verwijderen", "Terug");

                switch (selectedClass) {
                    case 0:
                        Reservation.DisplayReservation();
                        Admin_menu(username, id);
                        break;
                    case 1:
                        Reservation.MakeReservation();
                        Admin_menu(username, id);
                        break;
                    case 2:
                        Console.WriteLine("Nog niet beschikbaar");
                        // Reservation.ChangeReservation();
                        Admin_menu(username, id);
                        break;
                    case 3:
                        Reservation.DeleteReservationWithID();
                        Admin_menu(username, id);
                        break;
                    case 4:
                        Admin_menu(username, id);
                        break;
                }
                break;
            
            case 2:
                selectedClass = MenuLogic.MultipleChoice(true, "", 1, new string[] {}, "Medewerkers toevoegen", "Terug");

                switch (selectedClass) {
                    case 0:
                        Mederwerker.Toevoeg_Mederwerker_Menu(username, id);
                        break;
                    case 1:
                        Admin_menu(username, id);
                        break;
                }
                break;
        }
    }
}