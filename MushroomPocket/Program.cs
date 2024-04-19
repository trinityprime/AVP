using System;
using System.Collections.Generic;

namespace MushroomPocket
{

    class Program
    {
        static void Main(string[] args)
        {
            //MushroomMaster criteria list for checking character transformation availability.   
            /*************************************************************************
                PLEASE DO NOT CHANGE THE CODES FROM LINE 15-19
            *************************************************************************/
            List<MushroomMaster> mushroomMasters = new List<MushroomMaster>(){
            new MushroomMaster("Daisy", 2, "Peach"),
            new MushroomMaster("Wario", 3, "Mario"),
            new MushroomMaster("Waluigi", 1, "Luigi")
            };


            Console.WriteLine("*******************************");
            Console.WriteLine("Welcome to Mushroom Pocket App");
            Console.WriteLine("*******************************");

            while (true)
            {
                Console.WriteLine("(1). Add Mushroom's Character to my pocket");
                Console.WriteLine("(2). List character(s) in my pocket");
                Console.WriteLine("(3). Check if I can transform my characters");
                Console.WriteLine("(4). Transform character(s)");
                Console.WriteLine("Please only enter [1,2,3,4] or Q to quit: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddCharacter();
                        break;
                    case "2":
                        ListCharacter();
                        break;
                    case "3":
                        CheckTransformation();
                        break;
                    case "4":
                        Transformation();
                        break;
                    case "Q":
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 4, or Q to quit.");
                        break;
                }
            }
        }

        List<Character> mushroomPocket = new List<Character>();

        static void AddCharacter()
        {
            Console.WriteLine("Please enter the name of the character: ");
            string name = Console.ReadLine();
            Character character_created = null;

            switch (name)
            {
                case "Waluigi":
                    character_created = new Waluigi();
                    break;
                case "Daisy":
                    character_created = new Daisy();
                    break;
                case "Wario":
                    character_created = new Wario();
                    break;
                default:
                    Console.WriteLine("Invalid character name. Please enter Waluigi, Daisy, or Wario.");
                    return;
            }

            Console.WriteLine($"Please enter the HP of {name}: ");

        }

        static void ListCharacter()
        {
            Console.WriteLine("Characters in your pocket:");
            foreach (MushroomMaster mushroomMaster in mushroomPocket)
            {
                Console.WriteLine($"Name: {mushroomMaster.Name}, HP: {mushroomMaster.HP}, EXP: {mushroomMaster.EXP}");
            }
        }

        static void CheckTransformation()
        {
            Console.WriteLine("Enter the name of the character you want to check for transformation: ");
            string name = Console.ReadLine();

            MushroomMaster mushroomMaster = mushroomPocket.Find(m => m.Name == name);
            if (mushroomMaster != null)
            {
                Console.WriteLine($"Character {name} can transform into {mushroomMaster.Transformation}");
            }
            else
            {
                Console.WriteLine($"Character {name} not found in your pocket.");
            }
        }

        static void Transformation()
        {
            Console.WriteLine("Enter the name of the character you want to transform: ");
            string name = Console.ReadLine();

            MushroomMaster mushroomMaster = mushroomMasters.Find(m => m.Name == name);
            if (mushroomMaster != null)
            {
                Console.WriteLine($"Transforming {name} into {mushroomMaster.Transformation}...");
                mushroomMaster.Transform();
                Console.WriteLine($"Transformation complete! {name} is now {mushroomMaster.Transformation}");
            }
            else
            {
                Console.WriteLine($"Character {name} not found in your pocket.");
            }
        }
    }
}



