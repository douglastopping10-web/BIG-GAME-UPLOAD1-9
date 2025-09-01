using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace big_game
{
     
    public class Potion
    {
        public int Health { get; set; }
        public string Effect { get; set; }
        public List<Potion> Potions  = new List<Potion>(); 
       
        public Potion(int PotionHP, string PotionEffect)
        {
            Health = PotionHP;
            Effect = PotionEffect;
            
        }
        public bool UsePotion(Player player)
        {
          
         if (player.Health == player.MaxHP)
         {
                Console.WriteLine("You already are at your max health?!?!");
                Console.WriteLine("Dont try waste your potions! they cost alot");
                Thread.Sleep(500);
                return false;
         }
           player.Health += Health;
            if (player.Health > player.MaxHP)         
               player.Health = player.MaxHP;
            if (!string.IsNullOrEmpty(Effect))
            {
                switch (Effect.ToLower())
                {
                    case "shield":
                        player.IsShielded = true;
                        break;

                    case "regen":
                        player.IsRegenerating = true;
                        player.regenTurns = 3;
                        break;
                    case "buff":
                        player.IsDmgBuffed = true;
                       player.dmgBuffTurns = 3;
                        break;



                }
                
            }
          if (!string.IsNullOrEmpty(Effect))
            {
                Console.WriteLine($"{player.name} used a potion! your new health is {player.Health}");
                switch (Effect.ToLower())
                {
                    case "shield":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("You got a shield from the potion!!!");
                        Thread.Sleep(1000);
                        break;
                    case "regen":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("You Begun Regenerating");
                        Thread.Sleep(1000);
                        break;
                    case "buff":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You gained a damage buff!");
                        Thread.Sleep(1000);
                        break;
                    default:
                        break;
                }


            }
            return true;
        } 
    }
}
