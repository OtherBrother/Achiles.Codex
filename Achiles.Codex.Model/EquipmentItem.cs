using System.Collections;
using System.Collections.Generic;

namespace Achilles.Codex.Model
{

    public class Damage
    {
        private List<DamageValue> _damages = new List<DamageValue>();

        public List<DamageValue> Damages
        {
            get { return _damages; }
            set { _damages = value; }
        }
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

    public class MeleeWeapon : Weapon
    {
              
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