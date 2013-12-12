using System.Collections.Generic;
using Achilles.Codex.Model;

namespace Achilles.Codex.Model
{
    public class CombatSkill : CombatGearItem
    {
        public List<string> Features 
        {
            get { return _Features; }
            set { _Features = value; }
        }


        private List<string> _Features = new List<string>();
    }

    public class SkillFeature : CombatGearItem
    {
        public int MaxLevel { get; set; }
    }

   

}
