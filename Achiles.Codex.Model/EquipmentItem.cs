using System.Collections;
using System.Collections.Generic;

namespace Achiles.Codex.Model
{

    public class Damage
    {
        public Damage(params DamageValue[] damages)
        {

        }
        public IEnumerable<DamageValue> Damages { get; set; }
    }

    

    public class DamageValue
    {
        public DamageType DamageType { get; set; }
        public int Value { get; set; }
    }

    public enum DamageType
    {
        Poison,
        Fire,
        Bash,
        Trauma,
        Bleed,
        Critical,
    }

    public abstract class EquipmentItem : CombatGearItem
    {
        public int Weight { get; set; }
        public int Price { get; set; }
        
    }

    public class Weapon : EquipmentItem
    {
        public int Reach { get; set; }
        public int MinimumStrenght { get; set; }
        /// <summary>
        /// Two handed, consealable, +1 against shields...
        /// </summary>
        public List<string> Properties { get; set; }
        public Damage Damage { get; set; }
    }

    public class HandWeapon : Weapon
    {
        public int SwingBaseDamage { get; set; }
        public int ThrustBaseDamage { get; set; }
        public int ThrustArmorPiercing { get; set; }
        public int SwingArmorPiercing { get; set; }
    }

    public class RangedWeapon : Weapon
    {
        public string RangePenalty { get; set; }
        public int WeaponStrenght { get; set; }
        public int MaxRange { get; set; }
    }
    
    public class Ammo : EquipmentItem
    {
        public int WeaponMinStrenght { get; set; }
        public Damage Damage { get; set; }
        
    }
    public class Shield : Weapon
    {
        public int DefenseBonus { get; set; }
        public int HitPoints { get; set; }
    }
    
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

    public class MiscellaneousItem : EquipmentItem
    {

    }
}