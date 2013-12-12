using System.Collections.Generic;

namespace Achilles.Codex.Model
{
    public abstract class Armor : EquipmentItem
    {
        public int ArmorValue { get; set; }
        public List<string> Properties { get; set; }
    }
    public class HeadArmor : Armor
    {
    }
    public class BodyArmor : Armor
    {
    }
    public class ArmArmor : Armor
    {
    }
    public class LegArmor : Armor
    {
    }
}