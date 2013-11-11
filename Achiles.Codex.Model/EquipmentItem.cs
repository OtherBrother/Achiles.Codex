namespace Achiles.Codex.Model
{
    public abstract class EquipmentItem : CodexItemBase
    {

    }

    public class HandWeapon : EquipmentItem
    {

    }
    public class RangedWeapon : EquipmentItem
    {

    }
    public class Ammo : EquipmentItem
    {

    }
    public class Shield : EquipmentItem
    {

    }
    public abstract class Armor : EquipmentItem
    {

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

    public class MiscellaneousItem : EquipmentItem
    {

    }
}