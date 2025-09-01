using static System.Console;
namespace big_game
{
    public class PotionsMenu
    {
        private List<object> Options;
        int SelectedPotionIndex;
        string Prompt;
        public PotionsMenu(Player player)
        {
            Prompt = "What option do you want to pick?";
            string ReturnBack = "Go back";

            Options = new List<object>(player.Inventory);
            Options.Add(ReturnBack);
                SelectedPotionIndex = 0;
        }
        private void DisplayPotionsMenu()
        {
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            Console.WriteLine(Prompt);
            for (int i = 0; i < Options.Count; i++)
            {
                
                string OptionsPrefix;
                if (i == SelectedPotionIndex)
                {   
                   
                        
                        OptionsPrefix = "***";
                        ForegroundColor = ConsoleColor.Black;
                        BackgroundColor = ConsoleColor.White;

                    
                }
                else
                {
                    if (Options[i] is string ReturnBack)
                    {
                        OptionsPrefix = "   ";
                        ForegroundColor = ConsoleColor.Red;
                        BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        OptionsPrefix = "   ";
                        ForegroundColor = ConsoleColor.White;
                        BackgroundColor = ConsoleColor.Black;
                    }
          

                }
                if (Options[i] is Potion potion)
                {
                    switch (potion.Effect?.ToLower())
                    {
                        case "shield":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case "regen":
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case "buff":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;


                            break;
                        default:
                            break;
                    }



                    string potionbufftext = $"Potion: +{potion.Health} HP, Effect: {potion.Effect ?? "None"}";
                    WriteLine($"{OptionsPrefix}<<{potionbufftext}>>{OptionsPrefix}");
                }
                else if (Options[i] is string str)
                {
                    Console.WriteLine($"{OptionsPrefix} {str} {OptionsPrefix}");
                }
                Console.ResetColor();

            }

        }
        public int PotionsMenuRun()
        {
            ConsoleKey MenuKeyPressed;

            do
            {
                Clear();
                DisplayPotionsMenu();
                ConsoleKeyInfo keyInfo = ReadKey(true);
                MenuKeyPressed = keyInfo.Key;


                if (MenuKeyPressed == ConsoleKey.UpArrow)
                {
                    SelectedPotionIndex--;
                    if (SelectedPotionIndex < 0) SelectedPotionIndex = Options.Count - 1;
                }
                else if (MenuKeyPressed == ConsoleKey.DownArrow)
                {
                    SelectedPotionIndex++;
                    if (SelectedPotionIndex >= Options.Count) SelectedPotionIndex = 0;
                }

            } while (MenuKeyPressed != ConsoleKey.Enter);
            if (Options[SelectedPotionIndex] is string str && str == "Go back")
                return -1;


            return SelectedPotionIndex;
        }

    }
    public class GameMenu
    {
        string[] Options;
        int SelectedIndex;
        string Prompt;
        public GameMenu(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }
        private void DisplayMenu()
        {
            Clear();
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string Optionsprefix;
                if (i == SelectedIndex)
                {
                    Optionsprefix = "***";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {

                    Optionsprefix = "   ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;

                }




                string currentOption = Options[i];
                WriteLine($"{Optionsprefix}<<{currentOption}>>{Optionsprefix}    ");
            }
            ResetColor();
        }
        public int menuRun()
        {
            ConsoleKey MenuKeyPressed;
            do
            {
                Clear();
                DisplayMenu();
                ConsoleKeyInfo keyInfo = ReadKey(true);
                MenuKeyPressed = keyInfo.Key;
                if (MenuKeyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }



                }
                if (MenuKeyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;

                    }

                }


            } while (MenuKeyPressed != ConsoleKey.Enter);
            return SelectedIndex;
        }
    }
    public class FightMenu
    {
        private List<Attack> Options;
        int SelectedIndex;
        string Prompt;
        public FightMenu(string prompt, List<Attack> attacks)
        {
            Prompt = prompt;
            Options = attacks;
            SelectedIndex = 0;
        }
        private void DisplayMenu()
        {
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            Console.WriteLine(Prompt);

            Console.WriteLine(@"                           
                            ,-.                               
       ___,---.__          /'|`\          __,---,___          
    ,-'    \`    `-.____,-'  |  `-.____,-'    //    `-.       
  ,'        |           ~'\     /`~           |        `.      
 /      ___//              `. ,'          ,  , \___      \    
|    ,-'   `-.__   _         |        ,    __,-'   `-.    |    
|   /          /\_  `   .    |    ,      _/\          \   |   
\  |           \ \`-.___ \   |   / ___,-'/ /           |  /  
 \  \           | `._   `\\  |  //'   _,' |           /  /      
  `-.\         /'  _ `---'' , . ``---' _  `\         /,-'     
     ``       /     \    ,='/ \`=.    /     \       ''          
             |__   /|\_,--.,-.--,--._/|\   __|                  
             /  `./  \\`\ |  |  | /,//' \,'  \                  
            /   /     ||--+--|--+-/-|     \   \                 
           |   |     /'\_\_\ | /_/_/`\     |   |                
            \   \__, \_     `~'     _/ .__/   /            
             `-._,-'   `-._______,-'   `-._,-'                                                                                                                                                                                                                    
                                                                                                   ");
            Console.WriteLine(@"");
            Console.ResetColor();
            for (int i = 0; i < Options.Count; i++)
            {
                var attack = Options[i];
                bool isSelected = i == SelectedIndex;

                // Default colors this making is easier to read and change also smaller :) 
                ConsoleColor fg = ConsoleColor.White;
                ConsoleColor bg = ConsoleColor.Black;

                // Elemental colors
                switch (attack.Element.ToLower())
                {
                    case "grass": fg = ConsoleColor.DarkGreen; bg = isSelected ? ConsoleColor.White : ConsoleColor.Black; break;
                    case "fire": fg = ConsoleColor.Red; bg = isSelected ? ConsoleColor.White : ConsoleColor.Black; break;
                    case "water": fg = ConsoleColor.Blue; bg = isSelected ? ConsoleColor.White : ConsoleColor.Black; break;
                    case "none": // Weapon-specific attacks
                        if (attack.Name.Contains("Venom Slash")) fg = ConsoleColor.DarkMagenta;
                        if (attack.Name.Contains("Piercing Slash")) fg = ConsoleColor.Cyan;
                        if (attack.Name.Contains("Crushing Blow")) fg = ConsoleColor.DarkYellow;
                        if (attack.Name.Contains("Dangerous Slash")) fg = ConsoleColor.DarkYellow;
                        break;
                }

                if (isSelected)
                {
                    // Highlight hovered option
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = fg;
                }
                else
                {
                    Console.BackgroundColor = bg;
                    Console.ForegroundColor = fg;
                }

                Console.WriteLine($"<<{attack.Name} ({attack.Element})>>");
            }

            Console.ResetColor();
        }
        private int RunAttacksMenu()
        {
            ConsoleKey key;
            do
            {
                DisplayMenu();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;

                if (key == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex < 0) SelectedIndex = Options.Count - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex >= Options.Count) SelectedIndex = 0;
                }

            } while (key != ConsoleKey.Enter);

            return SelectedIndex; // returns chosen attack index

        }
        private int RunPotionsMenu(Player player)
        {
            // grab the players potion inventory
            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("You have no potions!");
                Thread.Sleep(1000);
                return -1;
            }

            PotionsMenu potionsMenu = new PotionsMenu(player);
            int chosenPotionIndex = potionsMenu.PotionsMenuRun();
            if (chosenPotionIndex == -1)
                return -1;
            
            Potion chosenPotion = player.Inventory[chosenPotionIndex];
            bool Potionused = chosenPotion.UsePotion(player);
            if (Potionused)
            {
                player.Inventory.RemoveAt(chosenPotionIndex);
            }

            // remove potion once used
            

            return chosenPotionIndex; // -1 means no attack was used in beginfightmenu
        }


        public int FightmenuRun(Player player)
        {
            string[] fightOptions = { "Attack", "Items", "Escape" };
            int currentIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine("Choose an option:");
                for (int i = 0; i < fightOptions.Length; i++)
                {
                    bool isSelected = (i == currentIndex);
                    Console.BackgroundColor = isSelected ? ConsoleColor.White : ConsoleColor.Black;
                    Console.ForegroundColor = isSelected ? ConsoleColor.Black : ConsoleColor.White;
                    Console.WriteLine(fightOptions[i]);
                    Console.ResetColor();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;

                if (key == ConsoleKey.UpArrow)
                {
                    currentIndex--;
                    if (currentIndex < 0) currentIndex = fightOptions.Length - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    currentIndex++;
                    if (currentIndex >= fightOptions.Length) currentIndex = 0;
                }

            } while (key != ConsoleKey.Enter);

            // Handle chosen action
            switch (currentIndex)
            {
                case 0: // ATTACK
                    return RunAttacksMenu();
                case 1: // ITEMS

                    RunPotionsMenu(player);
                    return -1; // stay in fight loop
                case 2: // ESCAPE
                    Console.WriteLine($"{player.name} fled the battle!");
                    Thread.Sleep(1000);
                    return -2; // special code for escape
                default:
                    return -1;
            }




        }
    }
}

