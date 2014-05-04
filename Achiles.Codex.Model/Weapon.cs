using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Achilles.Codex.Model
{
    public class Weapon : EquipmentItem
    {
        public Weapon()
        {
            Properties = new List<string>();
            AttackTypes = new List<AttackType>();
        }

        //public int Reach { get; set; }
        public int ReachMin { get; set; }
        public int ReachMax { get; set; }
        public int MinimumStrenght { get; set; }
        
        /// <summary>
        /// Two handed, consealable, +1 against shields...
        /// </summary>
        public List<string> Properties { get; set; }
        public List<AttackType> AttackTypes { get; set; }
        
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("W:{0}", Weight)
                .AppendLine()
                .AppendFormat("R:{0} - {1}", ReachMin, ReachMax)
                .AppendLine()
                .AppendFormat("St:{0}", MinimumStrenght)
                .AppendLine();

            foreach (var a in AttackTypes)
            {
                sb.AppendFormat("{0} {1}+{2}", a.Type, a.DamageValue, a.ArmorPiercingModifier).AppendLine().Append("Effects:");
                sb.AppendLine(string.Join("/", a.Effects.Select(e => string.Format("{0}{1}", e.Span, e.EffectType.Substring(0, 1)))));

            }
            sb.AppendLine();
            if (Properties != null) {
                foreach (var p in Properties)
                {
                    sb.AppendFormat("{0}", p).AppendLine();
                }
            }
            return sb.ToString();
        }

    }


}