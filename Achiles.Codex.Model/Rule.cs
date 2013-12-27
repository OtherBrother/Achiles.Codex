namespace Achilles.Codex.Model
{
    public class Rule : CodexItemBase
    {
        public Gear? Gear { get; set; }
    }

    public enum Gear
    { 
        Comabt,
        NonComabt
    }

}