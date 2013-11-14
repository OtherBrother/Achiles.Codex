namespace Achiles.Codex.Model
{
    
    public enum Gear
    {
        Comabt,
        NonCombat
    }

    public class CombatGearItem : CodexItemBase
    {

    }
    public abstract class NonCombatGearItem : CodexItemBase
    {

    }

    public class NcgEquipmentItem : NonCombatGearItem
    {
        public int Price { get; set; }
    }
}