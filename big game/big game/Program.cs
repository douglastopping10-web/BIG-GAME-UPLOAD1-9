using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
namespace big_game
{
    internal class Program
    {
        // ELEMENT TO INTEGER:
        // 1 = NORMAL // weak to sword Attacks
        // 2 = FIRE // weak to water
        // 3 = WATER // weak to grass
        // 4 = GRASS // weak to fire
        // SWORD TYPES:
        // 1 longSword
        // 2 Claymore
        // 3 Katana
        // 4 Dagger
        // CHEST RARITIES:
        // 1 common
        // 2 rare
        // 3 epic
        // 4 Legendary
        static Random rng = new Random();
        static Monster CreateMonster(int MinMonsterDMG, int MaxMonsterDMG, int MinMonsterElement, int MaxMonsterElement, int MinMonsterHP, int MaxMonsterHP)
        {



            // list of available elements index 0:3
            string[] MonsterElementsArray = { "none", "fire", "water", "grass" };


            // Assures that the elements are within the range
            MinMonsterElement = Math.Max(1, MinMonsterElement);
            MaxMonsterElement = Math.Min(MonsterElementsArray.Length, MaxMonsterElement);


            // assigns variables based on the random numbers
            int MonsterElementIndex = rng.Next(MinMonsterElement - 1, MaxMonsterElement);
            string MonsterElementChosen = MonsterElementsArray[MonsterElementIndex];
            int MonsterHPRandomizer = rng.Next(MinMonsterHP, MaxMonsterHP + 1);
            int MonsterDMGRandomizer = rng.Next(MinMonsterDMG, MaxMonsterDMG + 1);





            Monster newMonster = new Monster(MonsterDMGRandomizer, MonsterHPRandomizer, MonsterElementChosen);
            return newMonster;
        }
        static Sword OpenChest(Chest newChest, Player player)
        {
            Console.WriteLine("Opening Chest");
            Thread.Sleep(500);
            Sword newSword = null;
            Potion newPotion = null;
            
            if (newChest.rarity == "common")
            {
                newSword = CreateSword(1, 4, 1, 3);
                CreatePotion(player);
                
            }
            else if (newChest.rarity == "rare")
            {
                newSword = CreateSword(1, 4, 3, 5);
                CreatePotion(player);
            }
            else if (newChest.rarity == "epic")
            {
                newSword = CreateSword(1, 4, 5, 8);
                CreatePotion(player);
                
            }
            else if (newChest.rarity == "legendary")
            {
                newSword = CreateSword(2, 4, 9, 10);
                CreatePotion(player);
                
            }
            if (newSword != null)
            {
                return newSword;
            }
            return null;
        }
        static Potion CreatePotion(Player player)
        {
            string[] potionbuffarray = { "shield", "regen", "buff" };
            string potionbuffchosen = null;
            if (rng.NextDouble() <= 0.50)
            {
                int potionbuffchooserrandomizerinator3000 = rng.Next(0, 3);
                 potionbuffchosen = potionbuffarray[potionbuffchooserrandomizerinator3000];
            }
            string[] possiblepotionHP = { "10", "25", "35", "50" };
            int potionHPChooser = rng.Next(0, 4);
            string chosenpotionHPStringed = possiblepotionHP[potionHPChooser];
            int chosenpotionHP = 0;
            switch (chosenpotionHPStringed)
            {
                case "10":
                    chosenpotionHP = 10;
                    break;
                case "25":
                    chosenpotionHP = 25;
                    break;
                case "35":
                    chosenpotionHP = 35;
                    break;
                case "50":
                    chosenpotionHP = 50;
                    break;
            }
            Potion potioncreated = new Potion(chosenpotionHP, potionbuffchosen);
            player.Inventory.Add(potioncreated);
            return potioncreated;
        }
        static Chest CreateChest(int minRarity, int maxRarity)
        {
            Random chestRandom = new Random();
            string chestRarityRandomizerStringified = null;
            int chestRarity = chestRandom.Next(minRarity, maxRarity);
            if (chestRarity == 1)
            {
                chestRarityRandomizerStringified = "common";
            }
            else if (chestRarity == 2)
            {
                chestRarityRandomizerStringified = "rare";
            }
            else if (chestRarity == 3)
            {
                chestRarityRandomizerStringified = "epic";
            }
            else if (chestRarity == 4)
            {
                chestRarityRandomizerStringified = "legendary";
            }
            Chest chest = new Chest(chestRarityRandomizerStringified);
            return chest;
        }
        static List<Potion> playerPotions = new List<Potion>();
        static Sword CreateSword(int minElement, int maxElement, int mindmg, int maxdmg)
        {
            string swordTypeRandomizerStringified = null;
            string ElementRandomizerStringified = null;
            Random CREATESWORDRANDOM = new Random();
            int swordTypeRandomizer = CREATESWORDRANDOM.Next(1, 5);
            int ElementRandomizer = CREATESWORDRANDOM.Next(minElement, maxElement + 1);
            int DMGRandomizer = CREATESWORDRANDOM.Next(mindmg, maxdmg + 1);
            if (ElementRandomizer == 1)
            {
                ElementRandomizerStringified = "None";
            }
            else if (ElementRandomizer == 2)
            {
                ElementRandomizerStringified = "fire";
            }
            else if (ElementRandomizer == 3)
            {
                ElementRandomizerStringified = "water";
            }
            else if (ElementRandomizer == 4)
            {
                ElementRandomizerStringified = "grass";
            }
            if (swordTypeRandomizer == 1)
            {
                swordTypeRandomizerStringified = "longsword";
            }
            else if (swordTypeRandomizer == 2)
            {
                swordTypeRandomizerStringified = "claymore";
            }
            else if (swordTypeRandomizer == 3)
            {
                swordTypeRandomizerStringified = "katana";
            }
            else if (swordTypeRandomizer == 4)
            {
                swordTypeRandomizerStringified = "dagger";
            }
            Sword sword = new Sword(DMGRandomizer, ElementRandomizerStringified, swordTypeRandomizerStringified);

            return sword;
        }
        static int GetValidInt(string RetryParsePrompt, int tryParseMin, int tryParseMax)
        {
            while (true)
            {
                int tryparseresult = 0;
                Console.Write(RetryParsePrompt);
                string TryParseInput = Console.ReadLine();
                if (int.TryParse(TryParseInput, out tryparseresult) && tryparseresult >= tryParseMin && tryparseresult <= tryParseMax)
                {
                    return tryparseresult;
                }

            }



        }
        public static  double ElementalModifier(string attackElement, string monsterElement)
        {
            if (attackElement == "grass" && monsterElement == "water") return 2;
            if (attackElement == "fire" && monsterElement == "grass") return 2;
            if (attackElement == "water" && monsterElement == "fire") return 2;
            if (attackElement == "none" && monsterElement == "none") return 1.5;

            if (attackElement == "grass" && monsterElement == "fire") return 0.5;
            if (attackElement == "fire" && monsterElement == "water") return 0.5;
            if (attackElement == "water" && monsterElement == "grass") return 0.5;
            if (attackElement == "fire" && monsterElement == "none") return 0.8;
            if (attackElement == "water" && monsterElement == "none") return 0.8;
            if (attackElement == "grass" && monsterElement == "none") return 0.8;

            return 1.0;
        }
        // KILL COUNTER
        static int monstersKilled = 0;
        
            static void BeginMonsterFight(Player player)
            {
            Thread.Sleep(500);
            Console.Clear();
               Monster monster = CreateMonster(10, 35, 1, 4, 75, 200);
 
                Console.WriteLine(monster.MonsterElement == "none"
                 ? "You are fighting a monster weak to non-elemental attacks!"
                : $"You are fighting a monster imbued with {monster.MonsterElement}");
            Thread.Sleep(1000);
            // Fight loop
                while (monster.MonsterHP > 0 && player.IsAlive)
                {
                    // creates menu
                    FightMenu fightMenu = new FightMenu(@$"{player.name}'s HP: {player.Health}
Monster HP: {monster.MonsterHP}
Choose your attack to fight a {monster.MonsterElement} Monster:", player.Weapon.AvailableAttacks);
                   int selectedIndex = fightMenu.FightmenuRun(player);
                if (selectedIndex == -2)
                {
                    Console.WriteLine("You escaped safely!");
                    return;
                }

                // if player used item, just skip monster’s turn this loop
                if (selectedIndex == -1)
                {
                    continue;
                }
                Attack selectedAttack = player.Weapon.AvailableAttacks[selectedIndex];

                   // Apply elemental modifier

                // Apply damage

                     player.AttackMonster(monster, selectedAttack);

                     // Monster dies
                     if (monster.MonsterHP <= 0)
                     {
                        monstersKilled++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nYou defeated the monster! Total kills: {monstersKilled}");
                    Thread.Sleep(1000);
                        Console.ResetColor();
                        break;
                     }

                      // Monster attacks player
                     int monsterDamage = monster.GetAdjustedDamage();
                     if (player.IsShielded)
                     {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"{player.name} was shielded and took no damage");
                        Thread.Sleep(750);
                       Console.ResetColor();
                     }
                     else
                     {

                       if (player.IsReflecting)
                       {
                        int ReflectionDamage = (int)monsterDamage / 2; // hopefully works
                        player.ReflectDamage(monster, ReflectionDamage);
                       
                       }
                       else
                       {
                        player.TakeDamage(monsterDamage);
                       }
                  
                     }

                    // End of turn status effects
                    monster.ProcessEndOfTurnEffects();
                player.ProccessPlayerEffects();
            }
        }

            static void Main(string[] args)
            {
        

                string[] mainMenuOptions = { "Play", "Exit" };
                string mainMenuPrompt = @"

    ███████╗██╗   ██╗██╗██╗          ██████╗  █████╗ ███╗   ███╗███████╗
    ██╔════╝██║   ██║██║██║         ██╔════╝ ██╔══██╗████╗ ████║██╔════╝
    █████╗  ██║   ██║██║██║         ██║  ███╗███████║██╔████╔██║█████╗   
    ██╔══╝  ╚██╗ ██╔╝██║██║         ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  
    ███████╗ ╚████╔╝ ██║███████╗    ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗
    ╚══════╝  ╚═══╝  ╚═╝╚══════╝     ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝
                                                                        
      ██████╗ ███████╗    ██████╗  ██████╗  ██████╗ ███╗   ███╗              
     ██╔═══██╗██╔════╝    ██╔══██╗██╔═══██╗██╔═══██╗████╗ ████║              
     ██║   ██║█████╗      ██║  ██║██║   ██║██║   ██║██╔████╔██║              
     ██║   ██║██╔══╝      ██║  ██║██║   ██║██║   ██║██║╚██╔╝██║              
     ╚██████╔╝██║         ██████╔╝╚██████╔╝╚██████╔╝██║ ╚═╝ ██║              
      ╚═════╝ ╚═╝         ╚═════╝  ╚═════╝  ╚═════╝ ╚═╝     ╚═╝             
                                  
             Navigate Through the menu using the Arrow keys!";

                GameMenu MainMenu = new GameMenu(mainMenuPrompt, mainMenuOptions);
                int SelectedIndex = MainMenu.menuRun();



            switch (SelectedIndex)
            {
                case 0:
                    Thread.Sleep(350);
                    Console.Clear();



                    Console.Write("Give your hero a name:");
                    string playerName = Console.ReadLine();

                    Chest newChest = CreateChest(1, 5);
                    Console.WriteLine("Whats that over there?");
                    Thread.Sleep(750);
                    Console.WriteLine($"You found a {newChest.rarity} Chest!");
                    Console.WriteLine(@"                                                                                                                                                                                                                                                                                                                                                                                 
                           ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓                   
                           ▓▓░░▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓░░██                  
                           ▓▓░░▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓░░██                      
                           ▓▓░░▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓░░██                     
                           ▓▓░░▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓░░██                   
                           ▓▓░░▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓░░██                  
                           ▓▓░░▓▓▒▒▒▒▒▒▒▒▒▒▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▓▓░░██                  
                           ▓▓░░▓▓▓▓▓▓▓▓▓▓▓▓░░░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░██                     
                           ▓▓░░▒▒▒▒▒▒▒▒░░▓▓░░░░▓▓▒▒░░▒▒░░░░▒▒░░██                     
                         ▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒               
                         ▒▒▓▓▒▒▓▓▓▓▓▓▓▓▓▓▓▓▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒▓▓▒▒          
                         ▒▒▓▓▒▒▓▓▓▓▓▓▓▓▓▓▓▓▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒▓▓▒▒           
                         ▒▒▓▓░░▓▓▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▒▒▒▒▒▒▒▒▒▒▓▓░░▓▓▒▒            
                         ▒▒▓▓░░▓▓▒▒▒▒▒▒▒▒▒▒▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▓▓░░▓▓▒▒            
                         ▒▒▓▓░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░▓▓▒▒           
                         ▒▒▓▓░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▓▓▒▒           
                         ▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒                                                                                                                                                                                                                                                                                                                                                                                                                                           
     ");
                    Console.ResetColor();
                    Console.WriteLine("Press Enter to open Chest!");
                    Console.ReadLine();
                    Thread.Sleep(1500);
                    Sword playerSword = null;
                    Player Name = new Player(playerName, playerSword);

                    Console.WriteLine(@"                                                                                                       
                                                                                 ▓▓▓▓                    
                                                                 ▓▓▒▒▒▒▒▒▒▒▒▒▒▒▓▓▒▒▒▒▓▓▒▒▒▒▒▒▒▒▒▒▒▒▓▓    
                                                                 ▓▓▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒▓▓    
                                                                 ▓▓▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒▓▓    
                                                                 ▓▓▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒▓▓    
                                                                 ▓▓▓▓▓▓▓▓▓▓░░░░░░░░▓▓▓▓░░░░░░▓▓▓▓▓▓▓▓    
                                                                 ▓▓▓▓▓▓▓▓▓▓░░░░░░░░▓▓▒▒░░░░░░▓▓▓▓▓▓▓▓    
                                                                 ▓▓░░▓▓▓▓░░░░░░░░░░░░░░░░░░░░▓▓▓▓░░▓▓    
                                                                 ▓▓░░▓▓░░░░▒▒░░░░░░░░▒▒▒▒░░░░░░▓▓░░▓▓    
                                                                 ▓▓░░▓▓░░▒▒▒▒▒▒░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓░░▓▓    
                                                                 ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓    
                                                                 ▓▓░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▓▓    
                                                               ▒▒▓▓░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░▓▓▒▒  
                                                               ▒▒▓▓░░▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓░░▓▓▒▒
                                                               ▒▒▓▓░░▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓░░▓▓▒▒
                                                               ▒▒▓▓░░▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓░░▓▓▒▒
                                                               ▒▒▓▓░░▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓░░▓▓▒▒
                                                               ▒▒▓▓░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░▓▓▒▒
                                                               ▒▒▓▓░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▓▓▒▒
   ");
                        Name.Weapon = OpenChest(newChest, Name);
                      
                    
                        Name.IsAlive = true;
                        Name.Weapon.printsword(playerSword);
                        Console.WriteLine("Press Enter To Continue");
                        Console.ReadLine();
                        while (Name.IsAlive)
                        {

                            BeginMonsterFight(Name);




                        }




                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Bye!");
                        Thread.Sleep(500);
                        Environment.Exit(0);  
                        break;

                }



            }
        }
    }



