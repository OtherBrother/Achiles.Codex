using System.Collections.Generic;

namespace Achilles.Codex.Model
{
    public class AttackType
    {
        public AttackType()
        {
            Effects = new List<Effect>();
        }
        
        public string Type { get; set; }
        public int DamageValue { get; set; }
        public int ArmorPiercingModifier { get; set; }
        public List<Effect> Effects { get; set; }
    }
}