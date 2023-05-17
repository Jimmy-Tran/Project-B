public class Prijs
{
    public List<double> Tot_prijs;
    public double discount_senior = 0.1;
    public double gangenmenu_2 = 15.50;
    public double gangenmenu_3 = 20.50;
    public double gangenmenu_4 = 25.50;
    public double discount = 0.1;

    public Prijs()
    {
        Tot_prijs = new List<double>(); // Initialize Tot_prijs list
    }
    public List<double> Prijs_berekenen(List<Person> gegevens)
    {
        foreach (Person persoon in gegevens)
        {
            if (persoon.Age <= 2) // aka is een baby gratis eten
            {
                // prijs word niks dus doe je niks
                Tot_prijs.Add(0);
            }
            else if (persoon.Age >= 60 || persoon.Age <= 12) // aka het is een senior of kind
            {
                switch (persoon.Gangen_menu)
                {
                    case 2:
                        Tot_prijs.Add(Math.Round(gangenmenu_2 * (1 - discount), 2));
                        break;
                    case 3:
                        Tot_prijs.Add(Math.Round(gangenmenu_3 * (1 - discount), 2));
                        break;
                    case 4:
                        Tot_prijs.Add(Math.Round(gangenmenu_4 * (1 - discount), 2));
                        break;
                    default:
                        // Handle unexpected value
                        break;
                }
            }
            else
            {
                switch (persoon.Gangen_menu)
                {
                    case 2:
                        Tot_prijs.Add(Math.Round(gangenmenu_2, 2));
                        break;
                    case 3:
                        Tot_prijs.Add(Math.Round(gangenmenu_3, 2));
                        break;
                    case 4:
                        Tot_prijs.Add(Math.Round(gangenmenu_4, 2));
                        break;
                    default:
                        // Handle unexpected value
                        break;
                }
            }
        }
        return Tot_prijs;
    }
}