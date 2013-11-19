using System.Collections.Generic;
using Achiles.Codex.Model;

namespace Achiles.Codex.Model
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