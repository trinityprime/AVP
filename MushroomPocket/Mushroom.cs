using System;
using System.Collections.Generic;

namespace MushroomPocket
{
    public abstract class Character
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int EXP { get; set; }
        public string Skill { get; set; }
        public List<Item> Inventory { get; set; } = new List<Item>();

        public Character() { }
        public Character(string name, int hp, int exp, string skill)
        {
            Name = name;
            HP = hp;
            EXP = exp;
            Skill = skill;
        }
    }

    public class Waluigi : Character
    {
        public Waluigi() { }
        public Waluigi(string name, int hp, int exp) : base(name, hp, exp, "Agility") { }
    }

    public class Daisy : Character
    {
        public Daisy() { }
        public Daisy(string name, int hp, int exp) : base(name, hp, exp, "Leadership") { }
    }

    public class Wario : Character
    {
        public Wario() { }
        public Wario(string name, int hp, int exp) : base(name, hp, exp, "Strength") { }
    }

    public class Luigi : Character
    {
        public Luigi() { }
        public Luigi(string name, int hp, int exp) : base(name, hp, exp, "Precision and Accuracy") { }
    }

    public class Peach : Character
    {
        public Peach() { }
        public Peach(string name, int hp, int exp) : base(name, hp, exp, "Magic Abilities") { }
    }

    public class Mario : Character
    {
        public Mario() { }
        public Mario(string name, int hp, int exp) : base(name, hp, exp, "Combat Skills") { }
    }
}

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Effect { get; set; }

}

public class MushroomMaster
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int NoToTransform { get; set; }
    public string TransformTo { get; set; }

    public MushroomMaster(string name, int noToTransform, string transformTo)
    {
        this.Name = name;
        this.NoToTransform = noToTransform;
        this.TransformTo = transformTo;
    }
}
