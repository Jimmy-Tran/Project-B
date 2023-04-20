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
            layout = new char[2, 3] {
                {' ', '_', ' '},
                {'╚', ' ', '╝'},
            };
        }
        else if (numSeats == 4)
        {
            layout = new char[3, 3] {
                {' ', ' ', ' '},
                {'╔', '_', '╗'},
                {'╚', ' ', '╝'}
            };
        }
        else if (numSeats == 6)
        {
            layout = new char[4, 3] {
                {' ', ' ', ' '},
                {'╔', '_', '╗'},
                {'╠', '_', '╣'},
                {'╚', ' ', '╝'}
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