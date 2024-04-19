namespace MushroomPocket
{
    public abstract class Character
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int EXP { get; set; }
        public string Skill { get; set; }

        protected Character(string name, int hp, int exp, string skill)
        {
            Name = name;
            HP = hp;
            EXP = exp;
            Skill = skill;
        }
    }

    public class Waluigi : Character
    {
        public Waluigi() : base("Waluigi", 100, 0, "Speed") { }
    }

    public class Daisy : Character
    {
        public Daisy() : base("Daisy", 100, 0, "Leadership") { }
    }

    public class Wario : Character
    {
        public Wario() : base("Wario", 100, 0, "Strength") { }
    }

    public class MushroomMaster : Character
    {
        public int NoToTransform { get; set; }
        public string TransformTo { get; set; }

        public MushroomMaster(string name, int hp, int exp, string skill, int noToTransform, string transformTo)
            : base(name, hp, exp, skill)
        {
            NoToTransform = noToTransform;
            TransformTo = transformTo;
        }
    }
}










