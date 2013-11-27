using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Versioning;

namespace Achiles.Codex.Model
{
    public abstract class CodexItem
    {
        private static readonly List<Mapping> Mappings = new List<Mapping>();
        private class Mapping
        {
            public Mapping()
            {
                PossibleInputs = new HashSet<string>();
                MappedItemTypes = new HashSet<CodexItemType>();
            }

            public HashSet<string> PossibleInputs { get; set; }
            public HashSet<CodexItemType> MappedItemTypes { get; set; }
        }

        static CodexItem()
        {
            Mappings.Add(new Mapping { PossibleInputs = { "attribute", "at", "attr" }, MappedItemTypes = { CodexItemType.Attribute } });
            Mappings.Add(new Mapping { PossibleInputs = { "article", "ar" }, MappedItemTypes = { CodexItemType.Article } });
            Mappings.Add(new Mapping { PossibleInputs = { "ruleset", "rs" }, MappedItemTypes = { CodexItemType.RuleSet } });
            Mappings.Add(new Mapping { PossibleInputs = { "rule", "r" }, MappedItemTypes = { CodexItemType.Rule } });
            Mappings.Add(new Mapping { PossibleInputs = { "skill", "sk", }, MappedItemTypes = { CodexItemType.Skill } });
            Mappings.Add(new Mapping { PossibleInputs = { "combatskill", "cs" }, MappedItemTypes = { CodexItemType.CombatSkill } });
            Mappings.Add(new Mapping { PossibleInputs = { "misc", "m" }, MappedItemTypes = { CodexItemType.MiscellaneousItem } });
            Mappings.Add(new Mapping { PossibleInputs = { "s" }, MappedItemTypes = { CodexItemType.Skill, CodexItemType.CombatSkill } });
            Mappings.Add(new Mapping { PossibleInputs = { "t" }, MappedItemTypes = { CodexItemType.Talent } });
            Mappings.Add(new Mapping { PossibleInputs = { "cg" }, MappedItemTypes = { CodexItemType.CombatSkill, CodexItemType.Attribute } });
            Mappings.Add(new Mapping { PossibleInputs = { "ncg" }, MappedItemTypes = { CodexItemType.Skill, CodexItemType.Talent } });

            Mappings.Add(new Mapping { PossibleInputs = { "armor", "am" }, MappedItemTypes = { CodexItemType.HeadArmor, CodexItemType.BodyArmor, CodexItemType.ArmArmor, CodexItemType.LegArmor } });
            Mappings.Add(new Mapping { PossibleInputs = { "weapon", "w" }, MappedItemTypes = { CodexItemType.MeleeWeapon, CodexItemType.RangedWeapon, CodexItemType.Ammo, CodexItemType.Shield } });
            Mappings.Add(new Mapping { PossibleInputs = { "shield", "sh" }, MappedItemTypes = { CodexItemType.Shield } });
            Mappings.Add(new Mapping { PossibleInputs = { "equipment", "eq" }, MappedItemTypes = { CodexItemType.HeadArmor, CodexItemType.BodyArmor, CodexItemType.ArmArmor, CodexItemType.LegArmor, CodexItemType.MeleeWeapon, CodexItemType.RangedWeapon, CodexItemType.Ammo, CodexItemType.Shield } });
        }

        private List<string> _tags = new List<string>();
        private List<RelatedCodexItem> _relatedCodexItems = new List<RelatedCodexItem>();

        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set {
                _id = !string.IsNullOrEmpty(value) ? (value.Contains("/") ? value : string.Format("{0}/{1}", this.GetType().Name, value)).ToLower() : value;
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }
        public string IconUrl { get; set; }

        [TypeConverter(typeof(StringConverter))]
        public List<string> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        public List<RelatedCodexItem> RelatedCodexItems
        {
            get { return _relatedCodexItems; }
            set { _relatedCodexItems = value; }
        }
        
        public static CodexItemType DetermineType(string id)
        {
            if(string.IsNullOrEmpty(id))
                return CodexItemType.Unknown;

            var separatorIndex = id.IndexOf('/');
            
            if(separatorIndex==-1)
                return CodexItemType.Unknown;
            
            var typeName = id.Substring(0, separatorIndex);
            
            var itemType = CodexItemType.Unknown;
            Enum.TryParse(typeName, true, out itemType);

            return itemType;
        }

        public static bool GetTypesForQuery(string query, out CodexItemType[] types, out string searchTerm)
        {
            types = new CodexItemType[0];
            searchTerm = null;

            var separatorIndex = query.IndexOf('/');
            if (separatorIndex == -1)
                return false;

            var typeName = query.Substring(0, separatorIndex);

            types = MatchTypes(new string[] { typeName });
            
            if (!types.Any())
                return false;
            
            if (separatorIndex + 1 >= query.Length)
                return false;

            searchTerm = query.Substring(separatorIndex + 1, query.Length - (separatorIndex + 1));
            return true;
        }

        public static CodexItemType[] MatchTypes(IEnumerable<string>  inputTypes)
        {
            return Mappings.Where(x => x.PossibleInputs.Overlaps(inputTypes))
                 .SelectMany(x => x.MappedItemTypes)
                 .Distinct()
                 .ToArray();
        }

        public static string GetTypeLabel(string id)
        {
            return GetLabelForType(DetermineType(id));
        }

        public static string GetLabelForType(CodexItemType itemType)
        {
            
            return Resources.CodexItemLabels.ResourceManager.GetString(itemType.ToString()) ?? itemType.ToString();
        }
    }

    public class RelatedCodexItem
    {
        public string Id{ get; set;}
        public string Name { get; set; }

    }
}