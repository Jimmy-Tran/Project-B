class Reservatie
{

    public string name;
    public string email;
    public void Reserveren()
    {
        Console.WriteLine("ʜᴇᴇғᴛ ᴜ ᴇᴇɴ ᴀᴄᴄᴏᴜɴᴛ");
        Console.WriteLine("[1] Ja [ Work in progress ]");
        Console.WriteLine("[2] Nee");
        string input = Console.ReadLine();

        if (input == "2") {

            Console.WriteLine("Wat is uw naam?");
            string? nameInput = Console.ReadLine();
            name = nameInput;

            Console.WriteLine($"{name}, ᴡᴀᴛ ɪs ᴜᴡ ᴇᴍᴀɪʟ?");
            bool validEmail = true;
            while(validEmail is true) {
                string ?emailInput = Console.ReadLine();
                email = emailInput;
                if(emailInput.Contains("@")) {
                    validEmail = false;
                } else {
                    Console.WriteLine("Graag een email met een @ invoeren.");
                }
            }
            Console.WriteLine("email: " + email);
        }



    }
}
