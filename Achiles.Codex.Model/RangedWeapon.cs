namespace Achilles.Codex.Model
{
    public class RangedWeapon : Weapon
    {
        public string RangePenalty { get; set; }
        public int WeaponStrenght { get; set; }
        public int MaxRange { get; set; }
    }
}