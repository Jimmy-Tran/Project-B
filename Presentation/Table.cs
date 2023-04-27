using System;

class Table
{
    private int seats;
    private char[,] layout;
    public int ID { get; private set; }


    public Table(int numSeats, int id)
    {
        seats = numSeats;
        ID = id;
        if (numSeats == 2)
        {
            layout = new char[5, 5] {
                {' ', ' ', ' ',' ', ' '},
                {' ', '╔','═', '╗', ' '},
                {'∘', '║',' ', '║', '∘'},
                {' ', '╚', '═', '╝', ' '},
                {' ', ' ', ' ', ' ', ' '}
            };
        }
        else if (numSeats == 4)
        {
            layout = new char[6, 5] {
                {' ', ' ', ' ',' ', ' '},
                {' ', '╔','═', '╗', ' '},
                {'∘', '║',' ', '║', '∘'},
                {'∘', '║',' ', '║', '∘'},
                {' ', '╚', '═', '╝', ' '},
                {' ', ' ', ' ', ' ', ' '}
            };
        }
        else if (numSeats == 6)
        {
            layout = new char[7, 5] {
                {' ', ' ', ' ',' ', ' '},
                {' ', '╔','═', '╗', ' '},
                {'∘', '║',' ', '║', '∘'},
                {'∘', '║',' ', '║', '∘'},
                {'∘', '║',' ', '║', '∘'},
                {' ', '╚', '═', '╝', ' '},
                {' ', ' ', ' ', ' ', ' '}
            };
        }
        else
        {
            throw new ArgumentException("Invalid number of seats for table.");
        }
    }

    public void DisplayTable()
    {
        for (int i = 0; i < layout.GetLength(0); i++)
        {
            for (int j = 0; j < layout.GetLength(1); j++)
            {
                Console.Write(layout[i, j] + " ");
            }
            Console.WriteLine();
            
        }
    }
}