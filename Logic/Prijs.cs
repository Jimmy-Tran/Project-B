public class Prijs
{
    // Fields for the gangen menu prices
    public double gangenmenu_2 = 15.50;
    public double gangenmenu_3 = 20.50;
    public double gangenmenu_4 = 25.50;

    // Field for the discount percentage
    public double discount = 0.9;

    // Method to calculate the price
    public double prijs(int aantal)
    {
        // Prompt for the chosen gangen menu
        Console.WriteLine("Welke gangen menu gaat u nemen?");
        int menu = Convert.ToInt32(Console.ReadLine());

        // Prompt for the number of people under 12
        Console.WriteLine($"Hoeveel van de {aantal} personen zijn 12 jaar of jonger?");
        int onder_12 = Convert.ToInt32(Console.ReadLine());

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

        return totPrijs;
    }
}
