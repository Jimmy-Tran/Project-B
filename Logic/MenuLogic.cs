public class MenuLogic
{
    public static int MultipleChoice(bool canClear, bool canCancel, string symbol, int opl, params string[] options)
    {
        const int startX = 0;
        const int startY = 10;
        int optionsPerLine = opl;
        const int spacingPerLine = 20;

        int currentSelection = 0;

        ConsoleKey key;

        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        do
        {
            // if (canClear == true) Console.Clear();
            Console.Clear();
            Welkom.welkom();
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine, startY + i / optionsPerLine);

                if(i == currentSelection)
                    Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine($"{symbol} " + options[i]);

                Console.ResetColor();
            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                {
                    if (currentSelection % optionsPerLine > 0)
                        currentSelection--;
                    break;
                }
                case ConsoleKey.RightArrow:
                {
                    if (currentSelection % optionsPerLine < optionsPerLine - 1)
                        currentSelection++;
                    break;
                }
                case ConsoleKey.UpArrow:
                {
                    if (currentSelection >= optionsPerLine)
                        currentSelection -= optionsPerLine;
                    break;
                }
                case ConsoleKey.DownArrow:
                {
                    if (currentSelection + optionsPerLine < options.Length)
                        currentSelection += optionsPerLine;
                    break;
                }
                case ConsoleKey.Escape:
                {
                    if (canCancel)
                        return -1;
                    break;
                }
            }
        } while (key != ConsoleKey.Enter);

        Console.CursorVisible = true;

        return currentSelection;
    }
}