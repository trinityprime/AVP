﻿using System;
using System.Collections.Generic;
using System.Linq;
using MushroomPocket.EntityFrameworkCore;

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
                    Console.WriteLine("(2). List character(s) in my pocket");
                    Console.WriteLine("(3). Update Mushroom Character's HP & XP");
                    Console.WriteLine("(4). Delete Mushroom Character from my pocket");
                    Console.WriteLine("(5). Check if I can transform my characters");
                    Console.WriteLine("(6). Transform character(s)");
                    Console.WriteLine("(7). Train character(s)");
                    Console.WriteLine("Please only enter [1,2,3,4,5,6,7] or Q to quit: ");
                    string input = Console.ReadLine();
                    Console.WriteLine("---------------------");

                    switch (input)
                    {
                        case "1":
                            AddCharacter();
                            break;
                        case "2":
                            ListCharacter();
                            break;
                        case "3":
                            UpdateCharacter();
                            break;
                        case "4":
                            DeleteCharacter();
                            break;
                        case "5":
                            CheckTransformation();
                            break;
                        case "6":
                            Transformation();
                            break;
                        case "7":
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


        static void ListCharacter()
        {
            using (var context = new MushroomDatabase())
            {
                var sortedCharacters = context.Characters.OrderByDescending(c => c.HP).ToList();
                foreach (var character in sortedCharacters)
                {
                    Console.WriteLine($"ID: {character.ID}\nName: {character.Name}\nHP: {character.HP}\nEXP: {character.EXP}\nSkill: {character.Skill}");
                    Console.WriteLine("---------------------");
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