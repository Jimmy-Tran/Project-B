public class Prijs
{
    // Fields for the gangen menu prices
    public double gangenmenu_2 = 15.50;
    public double gangenmenu_3 = 20.50;
    public double gangenmenu_4 = 25.50;

    // Field for the discount percentage
    public double discount = 0.1;
    public double wijn_arrangement = 3.50;

    // Method to calculate the price
    public double prijs(int aantal)
    {
        // Prompt for the chosen gangen menu
        Console.WriteLine("Welke gangen menu gaat u nemen?");
        int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] { "Welke gangen menu gaat u nemen?" }, "2", "3", "4");
        int menu;
        switch (selectedClass)
        {
            case 0:
                menu = 2;
                break;
            case 1:
                menu = 3;
                break;
            case 2:
                menu = 4;
                break;
            default:
                menu = 0;
                break;
        }

        bool validInput = false;
        int onder_12 = 0;
        int wineCount = 0;
        while (!validInput)
        {
            Console.WriteLine($"Hoeveel van de {aantal} personen zijn 12 jaar of jonger?");
            onder_12 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Hoeveel van de {aantal} personen willen het wijnarrangement ({wijn_arrangement})?");
            wineCount = Convert.ToInt32(Console.ReadLine());

            if (onder_12 > aantal)
            {
                Console.WriteLine("Het aantal personen jonger dan 12 kan niet groter zijn dan het totale aantal personen.");
            }
            else if (wineCount > aantal)
            {
                Console.WriteLine("Het aantal personen dat wijn wil nemen kan niet groter zijn dan het totale aantal personen.");
            }
            else if (onder_12 + wineCount > aantal)
            {
                Console.WriteLine("Het totale aantal personen jonger dan 12 en het aantal personen dat wijn wil nemen kan niet groter zijn dan het totale aantal personen.");
            }
            else
            {
                validInput = true;
                // All numbers are logical, continue with the rest of your code
                // ...
            }
        }
        double menuPrijs = 0.0;

        // Calculate the price based on the chosen menu
        switch (menu)
        {
            case 2:
                menuPrijs = gangenmenu_2;
                break;
            case 3:
                menuPrijs = gangenmenu_3;
                break;
            case 4:
                menuPrijs = gangenmenu_4;
                break;
            default:
                Console.WriteLine("Ongeldige menukeuze.");
                return 0.0; // Return 0 if an invalid menu choice is entered
        }

        // Calculate the total price
        double totPrijs = menuPrijs * aantal;

        // Apply the discount for people under 12
        double korting = menuPrijs * onder_12 * discount;
        totPrijs -= korting;
        // zorg nu dat je de wijn erbij doet
        if (wineCount > 0)
        {
            // indien er dus wel mensen zijn met wijn
            double wijnprijs = wijn_arrangement * wineCount;
            totPrijs += wijnprijs;
        }
        return totPrijs;
    }
}
