using System;
using System.Collections.Generic;
using System.Linq;
using MushroomPocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MushroomPocket
{
    class Program
    {
        static List<Character> pocket = new List<Character>();
        static List<MushroomMaster> mushroomMasters = new List<MushroomMaster>(){
            new MushroomMaster("Daisy", 2, "Peach"),
            new MushroomMaster("Wario", 3, "Mario"),
            new MushroomMaster("Waluigi", 1, "Luigi")
        };

        static void Main(string[] args)
        {
            Console.WriteLine("*******************************");
            Console.WriteLine("Welcome to Mushroom Pocket App");
            Console.WriteLine("*******************************");

            using (var context = new MushroomDatabase())
            {
                while (true)
                {
                    Console.WriteLine("(1). Add Mushroom's Character to my pocket");
                    Console.WriteLine("(2). Add Item to my pocket");
                    Console.WriteLine("(3). List character(s) in my pocket");
                    Console.WriteLine("(4). List Item(s) in my pocket");
                    Console.WriteLine("(5). Use item on a character");
                    Console.WriteLine("(6). Update Mushroom Character's HP & XP");
                    Console.WriteLine("(7). Delete Mushroom Character from my pocket");
                    Console.WriteLine("(8). Check if I can transform my characters");
                    Console.WriteLine("(9). Transform character(s)");
                    Console.WriteLine("(10). Train character(s)");
                    Console.WriteLine("Please only enter [1,2,3,4,5,6,7,8,9,10] or Q to quit: ");
                    string input = Console.ReadLine();
                    Console.WriteLine("---------------------");

                    switch (input)
                    {
                        case "1":
                            AddCharacter();
                            break;
                        case "2":
                            AddItem();
                            break;
                        case "3":
                            ListCharacter();
                            break;
                        case "4":
                            ListItems();
                            break;
                        case "5":
                            UseItems();
                            break;
                        case "6":
                            UpdateCharacter();
                            break;
                        case "7":
                            DeleteCharacter();
                            break;
                        case "8":
                            CheckTransformation();
                            break;
                        case "9":
                            Transformation();
                            break;
                        case "10":
                            TrainCharacter();
                            break;
                        case "Q":
                        case "q":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please enter a number between 1 and 7, or Q to quit.");
                            break;
                    }
                }
            }
        }

        static void AddCharacter()
        {
            Console.WriteLine("Enter character name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter character HP:");
            int hp = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter character EXP:");
            int exp = int.Parse(Console.ReadLine());

            using (var context = new MushroomDatabase())
            {
                switch (name.ToLower())
                {
                    case "waluigi":
                        context.Characters.Add(new Waluigi(name, hp, exp));
                        break;
                    case "daisy":
                        context.Characters.Add(new Daisy(name, hp, exp));
                        break;
                    case "wario":
                        context.Characters.Add(new Wario(name, hp, exp));
                        break;
                    default:
                        Console.WriteLine("Invalid character name. Only Waluigi, Daisy, Wario are prohibited.\n");
                        return;
                }

                context.SaveChanges();
                Console.WriteLine("Character added successfully!");
            }
        }

        static void AddItem()
        {
            using (var context = new MushroomDatabase())
            {
                Console.WriteLine("Enter the item name:");
                string itemName = Console.ReadLine();

                string itemEffect;
                while (true)
                {
                    Console.WriteLine("Enter the item effect (e.g., HP+10 or EXP+20):");
                    itemEffect = Console.ReadLine();

                    // Validation
                    var effectParts = itemEffect.Split('+');
                    if (effectParts.Length == 2 && int.TryParse(effectParts[1], out int value) && (effectParts[0].ToUpper() == "HP" || effectParts[0].ToUpper() == "EXP"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid effect format. Please enter the effect in the format 'TYPE+VALUE' (e.g., HP+10 or EXP+20).");
                    }
                }

                var item = new Item { Name = itemName, Effect = itemEffect };
                context.Items.Add(item);
                context.SaveChanges();

                Console.WriteLine($"Item {itemName} added to the inventory.\n");
            }
        }

        static void ListCharacter()
        {
            using (var context = new MushroomDatabase())
            {
                var characters = context.Characters.ToList();
                var sortedHP = context.Characters.OrderByDescending(c => c.HP);
                if (characters.Count == 0)
                {
                    Console.WriteLine("No characters available.");
                    return;
                }
                foreach (var character in sortedHP)
                {
                    Console.WriteLine($"ID: {character.ID}\nName: {character.Name}\nHP: {character.HP}\nEXP: {character.EXP}\nSkill: {character.Skill}");
                    Console.WriteLine("---------------------");
                }
            }
        }

        static void ListItems()
        {
            using (var context = new MushroomDatabase())
            {
                var items = context.Items.ToList();
                if (items.Count == 0)
                {
                    Console.WriteLine("No items available.");
                    return;
                }
                foreach (var item in items)
                {
                    Console.WriteLine($"ID: {item.Id}\nName: {item.Name}\nEffect: {item.Effect}");
                    Console.WriteLine("---------------------");
                }
            }
        }

        static void UseItems()
        {
            using (var context = new MushroomDatabase())
            {
                // List all characters
                var characters = context.Characters.ToList();
                if (characters.Count == 0)
                {
                    Console.WriteLine("No characters available.");
                    return;
                }

                Console.WriteLine("Select a character by ID:");
                foreach (var character in characters)
                {
                    Console.WriteLine($"ID: {character.ID}, Name: {character.Name}, HP: {character.HP}, EXP: {character.EXP}");
                }
                Console.WriteLine("---------------------");

                int characterID;
                while (!int.TryParse(Console.ReadLine(), out characterID) || !characters.Any(c => c.ID == characterID))
                {
                    Console.WriteLine("Invalid character ID. Please enter a valid character ID:");
                }

                var selectedCharacter = characters.First(c => c.ID == characterID);

                // List all items
                var items = context.Items.ToList();
                if (items.Count == 0)
                {
                    Console.WriteLine("No items available.");
                    return;
                }

                Console.WriteLine("Select an item by ID:");
                foreach (var item in items)
                {
                    Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Effect: {item.Effect}");
                }
                Console.WriteLine("---------------------");

                int itemID;
                while (!int.TryParse(Console.ReadLine(), out itemID) || !items.Any(i => i.Id == itemID))
                {
                    Console.WriteLine("Invalid item ID. Please enter a valid item ID:");
                }

                var selectedItem = items.First(i => i.Id == itemID);

                ApplyItemEffect(selectedCharacter, selectedItem);

                context.Items.Remove(selectedItem);

                context.SaveChanges();
                Console.WriteLine($"Item {selectedItem.Name} used on {selectedCharacter.Name}.");
            }
        }

        static void ApplyItemEffect(Character character, Item item)
        {
            // Parse the item effect
            var effect = item.Effect.Split('+');
            if (effect.Length == 2 && int.TryParse(effect[1], out int value))
            {
                switch (effect[0].ToUpper())
                {
                    case "HP":
                        character.HP += value;
                        Console.WriteLine($"{character.Name}'s HP increased by {value}. New HP: {character.HP}");
                        break;
                    case "EXP":
                        character.EXP += value;
                        Console.WriteLine($"{character.Name}'s EXP increased by {value}. New EXP: {character.EXP}");
                        break;
                    default:
                        Console.WriteLine("Unknown effect type.");
                        break;
                }
            }
        }

        static void TrainCharacter()
        {
            Console.WriteLine("Enter the name of the ID of the character you want to train:");
            int ID = int.Parse(Console.ReadLine());

            using (var context = new MushroomDatabase())
            {
                var character = context.Characters.FirstOrDefault(c => c.ID == ID);
                if (character != null)
                {
                    int currentHP = character.HP;
                    character.HP += 50;
                    int updatedHP = character.HP;
                    Console.WriteLine($"{character.Name} HP: {currentHP} --> {updatedHP}.\n");
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Character not found in pocket.\n");
                }
            }
        }

        static void UpdateCharacter()
        {
            Console.WriteLine("Enter the ID of the character you want to update: ");
            int ID = int.Parse(Console.ReadLine());

            using (var context = new MushroomDatabase())
            {
                var character = context.Characters.FirstOrDefault(c => c.ID == ID);
                if (character != null)
                {
                    Console.WriteLine("Enter new HP:");
                    int newHP = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter new EXP:");
                    int newEXP = int.Parse(Console.ReadLine());

                    character.HP = newHP;
                    character.EXP = newEXP;

                    Console.WriteLine($"{character.Name} has been updated.\n");
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Character not found in pocket.\n");
                }
            }
        }


        static void DeleteCharacter()
        {
            Console.WriteLine("Enter the ID of the character you want to delete: ");
            int ID = int.Parse(Console.ReadLine());

            using (var context = new MushroomDatabase())
            {
                var character = context.Characters.FirstOrDefault(c => c.ID == ID);
                if (character != null)
                {
                    context.Characters.Remove(character);
                    Console.WriteLine($"{character.Name} has been removed from your pocket.\n");
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Character not found in pocket.\n");
                }
            }
        }


        static void CheckTransformation()
        {
            bool canTransform = false;
            using (var context = new MushroomDatabase())
            {
                foreach (var character in mushroomMasters)
                {
                    int count = context.Characters.Count(c => c.Name.ToLower() == character.Name.ToLower());

                    if (count >= character.NoToTransform)
                    {
                        Console.WriteLine($"{character.Name} can be transformed to {character.TransformTo}\n");
                        canTransform = true;
                    }
                }
            }

            if (canTransform == false)
            {
                Console.WriteLine("No characters can be transformed.\n");
            }
        }

        static void Transformation()
        {
            using (var context = new MushroomDatabase())
            {
                foreach (var character in mushroomMasters)
                {
                    int count = context.Characters.Count(c => c.Name.ToLower() == character.Name.ToLower());
                    if (count >= character.NoToTransform)
                    {
                        var charactersToTransform = context.Characters.Where(c => c.Name.ToLower() == character.Name.ToLower()).ToList();
                        foreach (var c in charactersToTransform)
                        {
                            context.Characters.Remove(c);
                        }

                        switch (character.TransformTo.ToLower())
                        {
                            case "peach":
                                context.Characters.Add(new Peach(character.TransformTo, 100, 0));
                                break;
                            case "mario":
                                context.Characters.Add(new Mario(character.TransformTo, 100, 0));
                                break;
                            case "luigi":
                                context.Characters.Add(new Luigi(character.TransformTo, 100, 0));
                                break;
                        }
                        Console.WriteLine($"{character.Name} --> {character.TransformTo}!\n");
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}