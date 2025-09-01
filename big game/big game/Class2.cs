using big_game;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace big_game
{
    public class Attack
    {
        public string Name { get; }
        public string Element { get; }
        public int MinDamage { get; }
        public int MaxDamage { get; }
        public string Effect { get; }
        private static Random rng = new Random();
        public Attack(string name, string element, int minDamage, int maxDamage, string effect = "")
        {
            Name = name;
            Element = element;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            Effect = effect;
        }
        public int GetDamage()
        {
            return rng.Next(MinDamage, MaxDamage + 1);
        }
        public override string ToString()
        {

            return $"{Name} ({Element}) - {MinDamage}-{MaxDamage} dmg";
        }
    }

    public class AttacksList
    {
        public List<Attack> GrassAttacks { get; }
        public List<Attack> FireAttacks { get; }
        public List<Attack> WaterAttacks { get; }
        public List<Attack> WeaponAttacks { get; }


        private static Random rng = new Random();
        public AttacksList()
        {
            GrassAttacks = new List<Attack>
           {
            new Attack("Vine Barrage", "Grass", 18, 23 ,"Traps enemy"), // DOT
            new Attack("Natures Wrath", "Grass", 24, 50), // BIG DAMAGE
            new Attack("Verdant Surge", "Grass", 20, 32, "LifeSteal") // LIFESTEAL
           };
            FireAttacks = new List<Attack>
           {
               new Attack("Solar Curse", "Fire", 20, 34, "Curses enemy"), // DOT
               new Attack("Blazing Maelstrom", "Fire", 24, 50), // big damage
               new Attack("Inferno Veil", "Fire", 0, 0, "Reflects Damage")  // 50% of damage is reflected for 3 turns
           };
            WaterAttacks = new List<Attack>
            {
              
            new Attack("Ocean Blessing", "Water", 0, 0, "Heal 50%"), // Heal  50%
            new Attack("Torrent Punch", "Water", 24, 50), // Big damage
            new Attack("Whirlpool Strike", "Water", 21, 28, "Traps enemy") // DOT
            };
            WeaponAttacks = new List<Attack>
            {
              new Attack("Piercing Venom", "None", 20, 34, "Poisons enemy (Damage over time)"),
              new Attack("Bleeding Slash", "None", 20, 43, "Inflicts bleeding (Enemy Takes More Damage)"),
              new Attack("Crushing Blow", "None", 25, 41, "Weakens enemy (Reduces Enemy Strength) "),
               new Attack("Dangerous Slash", "None", 27, 56, "") // no special effect
            };

        }




        public Attack GetRandomAttack(string element)
        {
            switch (element.ToLower())
            {
                case "grass":
                    return GrassAttacks[rng.Next(GrassAttacks.Count)];
                case "fire":
                    return FireAttacks[rng.Next(FireAttacks.Count)];
                case "water":
                    return WaterAttacks[rng.Next(WaterAttacks.Count)];
                default:
                    return null;
            }
        }
        public IEnumerable<Attack> GetAllElementSpecificAttacks(string element)
        {
            switch (element.ToLowerInvariant())
            {
                case "grass":
                    return GrassAttacks;
                case "fire":
                    return FireAttacks;
                case "water":
                    return WaterAttacks;
                default:
                    return null;
            }
        }
    }
    public class Monster
    {
            public int MonsterDMG { get; set; }
            public int MonsterHP { get; set; }
            public string MonsterElement { get; set; }



            // status effects
            public bool IsPoisoned { get; private set; }
            public int PoisonTurnsRemaining { get; private set; }
            public int PoisonDamagePerTurn { get; private set; }
            public bool IsBleeding { get; private set; }
             public bool IsTrapped { get; private set; }
            public bool IsCursed { get; private set; }
            public int CurseTurnsRemaining { get; private set; }
            public int TrapTurnsRemaining   { get; private set; }
            public bool IsWeakened { get; private set; }
            public int WeaknessTurnsRemaining { get; private set; }


            public Monster(int MonsterDMGRandomizer, int MonsterHPRandomizer, string MonsterElementChosen)
            {
                MonsterDMG = MonsterDMGRandomizer;
                MonsterHP = MonsterHPRandomizer;
                MonsterElement = MonsterElementChosen;
            }

            public void ApplyPoison(int damagePerTurn, int turns)
            {
                IsPoisoned = true;
                PoisonDamagePerTurn = damagePerTurn;
                PoisonTurnsRemaining = turns;
                Console.WriteLine($"The monster has been poisoned by your cool attack!");
                  Thread.Sleep(2000);
            }
            public void ApplyBleed()
            {
                IsBleeding = true;
                Console.WriteLine("The monster has been afflicted by bleeding from that sick katana");
                 Thread.Sleep(2000);
            }
            public void ApplyWeakness(int turns)
            {
                IsWeakened = true;
                WeaknessTurnsRemaining = turns;
                Console.WriteLine("The monster has been weakened owch!");
                Thread.Sleep(2000);
            }
            public void ApplyTrapped(int turns)
            {
              IsTrapped = true;
              TrapTurnsRemaining = turns;
               Console.WriteLine("The monster has been Trapped");
              Thread.Sleep(2000);

            }
            public void ApplyCursed(int turns)
            {
                IsCursed= true;
              CurseTurnsRemaining = turns;
               Console.WriteLine("The monster has been Cursed");
              Thread.Sleep(2000);
 
            }
        public void ProcessEndOfTurnEffects()
        {
                // Handle poison
                if (IsPoisoned)
                {
                    MonsterHP -= PoisonDamagePerTurn;
                    PoisonTurnsRemaining--;
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine($"Poison deals {PoisonDamagePerTurn} damage! Monster HP: {MonsterHP}");
                   Thread.Sleep(2000);

                if (PoisonTurnsRemaining <= 0)
                    {
                        IsPoisoned = false;
                        Console.WriteLine("The poison has worn off");
                      Thread.Sleep(2000);
                    }
                }
                if (IsWeakened)
                {
                    WeaknessTurnsRemaining--;
                    if (WeaknessTurnsRemaining <= 0)
                    {
                        IsWeakened = false;
                        Console.WriteLine("The monster has regained its strength");
                        Thread.Sleep(2000);
                    }
                }
                if (IsCursed)
                {
                 int CurseDmg = 10;
                    MonsterHP -= CurseDmg;
                    CurseTurnsRemaining--;
                    if (CurseTurnsRemaining <= 0)
                    {
                        IsCursed = false;
                        Console.WriteLine("The monster has Broke Free From the curse");
                        Thread.Sleep(2000);
                    }
                }
                if (IsTrapped)
                {
                   int TrappedDmg = 10;
                   MonsterHP -= TrappedDmg;
                   TrapTurnsRemaining--;
                   if (TrapTurnsRemaining <= 0)
                   {
                    IsTrapped = false;
                    Console.WriteLine("The monster has Broke Free from the Trap!");
                    Thread.Sleep(2000);
                   }
                }
        }
            // Adjust damage output if weakened
            public int GetAdjustedDamage()
            {
                return IsWeakened ? (int)(MonsterDMG * 0.75) : MonsterDMG;
            }

    }
    public class Player
    {
        public string name { get; set; }
        public int Health { get; set; }
        public int MaxHP { get; set; }
        public bool IsAlive { get; set; }
        public bool IsShielded { get; set; }
        public bool IsRegenerating { get; set; }
        public int regenTurns { get; set; }
        public int dmgBuffTurns { get; set; }
        public bool IsReflecting { get;  set; }
        public int ReflectionTurns { get; set; }

        public bool IsDmgBuffed { get; set; }
        public Sword Weapon { get; set; }
        public List<Potion> Inventory = new List<Potion>();
        public Player(string playerName, Sword playerSword)
        {
            name = playerName;
            Health = 100;
            MaxHP = 100;
            Weapon = playerSword;
            Inventory = new List<Potion>();
        }
        public static double ElementalModifier(string attackElement, string monsterElement)
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
        public void TakeDamage(int MonsterDMG)
        {
            Console.Clear();
            Health = Health - MonsterDMG;
            if (Health <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"
                ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▒█████   ██▒   █▓▓█████  ██▀███  
               ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒██▒  ██▒▓██░   █▒▓█   ▀ ▓██ ▒ ██▒
              ▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██░  ██▒ ▓██  █▒░▒███   ▓██ ░▄█ ▒
              ░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ▒██   ██░  ▒██ █░░▒▓█  ▄ ▒██▀▀█▄   
              ░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒   ░ ████▓▒░   ▒▀█░  ░▒████▒░██▓ ▒██▒
               ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░   ░ ▒░▒░▒░    ░ ▐░  ░░ ▒░ ░░ ▒▓ ░▒▓░
                 ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ░ ▒ ▒░    ░ ░░   ░ ░  ░  ░▒ ░ ▒░
                  ░ ░   ░   ░   ▒   ░      ░      ░      ░ ░ ░ ▒       ░░     ░     ░░   ░ 
                      ░       ░  ░       ░      ░  ░       ░ ░        ░     ░  ░   ░     
                                                     ░                   
                                         ");
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You died... Try again later");
                Console.ResetColor();
                Thread.Sleep(2000);
                IsAlive = false;
            }
            else
            {
                Console.WriteLine($"Wowzers! {MonsterDMG} damage. Remaining HP: {Health}");
                Thread.Sleep(1500);
            }
        }
        public void AttackMonster(Monster monster, Attack attack)
        {
            Random rng = new Random();
            int baseDamage = attack.GetDamage();
            double modifier = ElementalModifier(attack.Element, monster.MonsterElement);
            int damage = (int)(baseDamage * modifier);

            // Apply bleeding bonus
            if (monster.IsBleeding)
                damage = (int)(damage * 1.25);


         


            if (attack.Effect.Contains("LifeSteal"))
            {
                int LifestealAmount = damage / 2;
                monster.MonsterHP  -= damage;
                Health = Health += LifestealAmount;
                Console.WriteLine($"{name} used {attack.Name}! It dealt {damage} damage and healed {LifestealAmount} HP! Monster HP: {monster.MonsterHP}");


            }
            if(damage <= 0 && attack.Effect.Contains("Heal"))
            {
                int HealingAmount = Health / 2;
                Health = Health + HealingAmount;
                Console.WriteLine($"You Healed {HealingAmount} Health!");
            }
            if (damage <= 0 && attack.Effect.Contains("Reflect"))
            {
               
          
                Console.WriteLine($"You Gained a Reflective Barrier!");
            }
            else
            {
                monster.MonsterHP -= damage;
                Console.WriteLine($"{name} used {attack.Name}! It dealt {damage} damage. Monster HP: {monster.MonsterHP}");
            }
            Console.WriteLine($"{name} used {attack.Name}! It dealt {damage} damage. Monster HP: {monster.MonsterHP}");
            Console.WriteLine($"{name} used {attack.Name}! It dealt {damage} damage. Monster HP: {monster.MonsterHP}");
            if (rng.NextDouble() <= 0.5)
            {
                if (attack.Effect.Contains("Trap"))
                    monster.ApplyTrapped(3);

                    if (attack.Effect.Contains("Curse"))
                        monster.ApplyCursed(3);
            }
            // 25% chance to apply effects
            if (attack.Effect.Contains("Reflect"))
            {
               
            }
            if (rng.NextDouble() <= 0.25)
            {
                if (attack.Effect.Contains("Poison"))
                    monster.ApplyPoison(10, 3); // 5 dmg per turn, 3 turns

                if (attack.Effect.Contains("Bleeding"))
                    monster.ApplyBleed(); // damage bonus applied automatically

                if (attack.Effect.Contains("Weakens"))
                    monster.ApplyWeakness(3); // lasts 3 turns
            }
        }
        public void ReflectDamage(Monster monster,int ReflectionDamage)
        {
            monster.MonsterHP -= ReflectionDamage;
            Console.WriteLine($"{name} Reflected {ReflectionDamage} Onto the monster!");
        }
         public void ApplyReflecting(int turns)
         {
            IsReflecting = true;
            ReflectionTurns = turns;
            Console.WriteLine("The player gained a reflective shield!");
         }
        
        public void ProccessPlayerEffects()
        {
            if (IsRegenerating && regenTurns > 0)
            {

                Health += 10;
                if(Health > MaxHP)
                {
                    Health = MaxHP;
                }
                Console.WriteLine($"{name} Healed 10 HP");
                Thread.Sleep(500);
                regenTurns--;
            }
            if (regenTurns <= 0)
            {
                IsRegenerating = false;
                Console.WriteLine($"{name} stopped regenerating now!");
                Thread.Sleep(500);
            }
            if (IsDmgBuffed && dmgBuffTurns > 0)
            {
                Console.WriteLine("Your Damage buff is still with you!");
                dmgBuffTurns--;
                Thread.Sleep(500);
            }
            if(dmgBuffTurns <= 0)
            {
                IsDmgBuffed = false;
                Console.WriteLine($"{name}'s Damage buff ran out!");
                Thread.Sleep(500);
            }
            if (IsReflecting && ReflectionTurns > 0)
            {
                ReflectionTurns--;
                Console.WriteLine($"{name}'s Reflective Barrier is still around!");
                Thread.Sleep(500);

            }
            if (ReflectionTurns < 0)
            {
                IsReflecting = false;
                Console.WriteLine($"{name}'s Reflective Barrier has Vanished!");
                Thread.Sleep(500);
            }
        }
    }
        public class Sword
        {
            public string element;
            public int damage;
            public string WeaponType;
            public List<Attack> AvailableAttacks;
            private static AttacksList attacksList = new AttacksList();
            public Sword(int DMGRandomizer, string ElementRandomizerStringified, string swordTypeRandomizerStringified)
            {
               WeaponType = swordTypeRandomizerStringified.ToLower();
               element = ElementRandomizerStringified.ToLower();
               damage = DMGRandomizer;
               AvailableAttacks = new List<Attack>();

            // Add weapon-specific attack
                switch (WeaponType)
                {
                   case "dagger":
                      AvailableAttacks.Add(attacksList.WeaponAttacks.First(a => a.Name.Contains("Venom")));
                       break;
                   case "katana":
                      AvailableAttacks.Add(attacksList.WeaponAttacks.First(a => a.Name.Contains("Bleeding")));
                     break;
                   case "claymore":
                       AvailableAttacks.Add(attacksList.WeaponAttacks.First(a => a.Name.Contains("Crushing")));
                    break;
                   case "longsword":
                       AvailableAttacks.Add(attacksList.WeaponAttacks.First(a => a.Name.Contains("Dangerous")));
                    break;
                }

                // If sword has an element, add one elemental attack
                if (!string.Equals(element, "none", StringComparison.OrdinalIgnoreCase))
                {
                 AvailableAttacks.AddRange(attacksList.GetAllElementSpecificAttacks(element));
                
                }

        }
            private Attack GetWeaponSpecificAttack(string weaponType)
            {
               return weaponType.ToLower() switch
               {
                 "dagger" => attacksList.WeaponAttacks.First(a => a.Name.Contains("Venom Slash")),
                 "katana" => attacksList.WeaponAttacks.First(a => a.Name.Contains("Piercing Slash")),
                 "claymore" => attacksList.WeaponAttacks.First(a => a.Name.Contains("Crushing Blow")),
                 "longsword" => attacksList.WeaponAttacks.First(a => a.Name.Contains("Dangerous Slash")),
                _ => null
               };
            }
            public void printsword(Sword sword)
            {
          
                Console.WriteLine($@"You found a {WeaponType} {(element == "None" ? "" : $"with {element} element")}");
                Console.WriteLine("Available Attacks:");
                foreach (var attack in AvailableAttacks)
                {
                    Console.WriteLine($"- {attack}");
                }
            }
        }
        public class Chest
        {
            public string rarity;
            public Chest(string ChestRarityRandomizerStringified)
            {
                rarity = ChestRarityRandomizerStringified;
            }
        }
    
}
