﻿using System;
using System.Collections.Generic;
using System.Linq;

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

        static void AddCharacter()
        {
            Console.WriteLine("Enter character name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter character HP:");
            int hp = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter character EXP:");
            int exp = int.Parse(Console.ReadLine());

            switch (name.ToLower())
            {
                case "waluigi":
                    pocket.Add(new Waluigi(name, hp, exp));
                    break;
                case "daisy":
                    pocket.Add(new Daisy(name, hp, exp));
                    break;
                case "wario":
                    pocket.Add(new Wario(name, hp, exp));
                    break;
                default:
                    Console.WriteLine("Invalid character name. Only Waluigi, Daisy, and Wario are allowed.\n");
                    break;
            }
        }

        static void ListCharacter()
        {
            foreach (var character in pocket)
            {
                Console.WriteLine($"Name: {character.Name}\nHP: {character.HP}\nEXP: {character.EXP}\nSkill: {character.Skill}");
                Console.WriteLine("---------------------");
            }
        }

        static void CheckTransformation()
        {
            bool canTransform = false;
            foreach (var master in mushroomMasters)
            {
                int count = pocket.Count(c => c.Name == master.Name);

                if (count >= master.NoToTransform)
                {
                    Console.WriteLine($"{master.Name} can be transformed to {master.TransformTo}\n");
                    canTransform = true;
                }
            }

            if (canTransform == false)
            {
                Console.WriteLine("No characters can be transformed.\n");
            }
        }

        static void Transformation()
        {
            foreach (var master in mushroomMasters)
            {
                int count = pocket.Count(c => c.Name == master.Name);
                if (count >= master.NoToTransform)
                {
                    pocket.RemoveAll(c => c.Name == master.Name);
                    switch (master.TransformTo)
                    {
                        case "Peach":
                            pocket.Add(new Peach(master.TransformTo, 100, 0));
                            break;
                        case "Mario":
                            pocket.Add(new Mario(master.TransformTo, 100, 0));
                            break;
                        case "Luigi":
                            pocket.Add(new Luigi(master.TransformTo, 100, 0));
                            break;
                    }
                    Console.WriteLine($"{master.Name} --> {master.TransformTo}!\n");
                }
            }
        }
    }
}