class Restaurant
{
    public void DisplayTables(List<Table> tables)
    {
        foreach (Table table in tables)
        {
            Console.WriteLine($"Table nummer: {table.ID}");
            table.DisplayTable();
            Console.WriteLine();
        }
    }
    public void InitializeTables()
    {
        List<Table> tables = new List<Table>()
        {
            new Table(2, 1),
            new Table(2, 2),
            new Table(2, 3),
            new Table(2, 4),
            new Table(2, 5),
            new Table(2, 6),
            new Table(2, 7),
            new Table(2, 8),
            new Table(4, 9),
            new Table(4, 10),
            new Table(4, 11),
            new Table(4, 12),
            new Table(4, 13),
            new Table(6, 14),
            new Table(6, 15),
        };

        Restaurant restaurant = new Restaurant();
        restaurant.DisplayTables(tables);

        /*nu alles uitgelezen is 
            maak een menu om terug te gaan om te reserveren voor een tafel
        */
    }

}