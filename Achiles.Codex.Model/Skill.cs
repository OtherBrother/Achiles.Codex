using System.Collections.Generic;

namespace Achilles.Codex.Model
{
    public class Skill : NonCombatGearItem
    {
        private List<Specialization> _specializations;

        /// <summary>
        /// Outdoor, Social,
        /// </summary>
        public string Category { get; set; }

        public List<Specialization> Specializations
        {
            get { return _specializations; }
            set { _specializations = value; }
        }
    }

    public class Specialization
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
    }
}