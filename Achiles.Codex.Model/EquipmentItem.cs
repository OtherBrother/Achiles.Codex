using System.Collections;
using System.Linq;
using System.Text;

namespace Achilles.Codex.Model
{
  
    public abstract class EquipmentItem : CombatGearItem
    {
        public int Weight { get; set; }
        public int Price { get; set; }
    }
}