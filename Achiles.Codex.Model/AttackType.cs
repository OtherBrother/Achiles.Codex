using System.Collections.Generic;
using System.Text;

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
        public override string ToString()
        {
            var x = new StringBuilder();
            foreach (var e in Effects)
            {
                x.AppendFormat("{0}{1} ", e.EffectType[0], e.Span);
            }
            return x.ToString();
        }
    }
}