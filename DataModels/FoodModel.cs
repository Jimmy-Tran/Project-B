// data models voor de voedsel
public class MenuItem // losse item class
{
    public int ID { get; set; }
    public string? Category { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}
public class Foodmenu
{
    // maak nu een lijst voor iedere gang en drinken
    public List<MenuItem> Starters { get; set; }
    public List<MenuItem> Soups { get; set; }
    public List<MenuItem> Mains { get; set; }
    public List<MenuItem> Desserts { get; set; }
    public List<MenuItem> Drinks { get; set; }
    public List<MenuItem> Wijn { get; set; }
    public List<MenuItem> Gangen { get; set; }
    public Foodmenu()
    {
        Starters = new List<MenuItem>();
        Soups = new List<MenuItem>();
        Mains = new List<MenuItem>();
        Desserts = new List<MenuItem>();
        Drinks = new List<MenuItem>();
        Wijn = new List<MenuItem>();
        Gangen = new List<MenuItem>();
    }

}