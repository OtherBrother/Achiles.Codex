using System.Collections.Generic;
using Achilles.Codex.Model;

namespace Achilles.Codex.Model
{
    public class CombatSkill : CombatGearItem
    {
        public List<string> Features { get; set; }
    }

    public class SkillFeature : CombatGearItem
    {
        public int MaxLevel { get; set; }
    }



}