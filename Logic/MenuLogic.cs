public class MenuLogic
{
    public static int MultipleChoice(bool canCancel, string symbol, int opl, string[] notChoosableOptions, params string[] options)
    {
        const int startX = 0;
        int startY = (notChoosableOptions.Count() > 0 ? notChoosableOptions.Count() + 1 : 10);
        int optionsPerLine = opl;
        const int spacingPerLine = 20;

        int currentSelection = 0;

        ConsoleKey key;

        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            foreach (string i in notChoosableOptions) {
                Console.WriteLine(i);
            }
            

            for (int i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine, startY + i / optionsPerLine);

                if (i == currentSelection) {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"> " + options[i] + "\n");
                } else {
                    Console.WriteLine($"{symbol} " + options[i] + "\n");
                }
                    
                
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