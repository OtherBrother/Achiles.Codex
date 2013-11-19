using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Versioning;

namespace Achiles.Codex.Model
{
    public abstract class CodexItem
    {
        private List<string> _tags = new List<string>();
        private List<string> _relatedCodexItems = new List<string>();

        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }
        public string IconUrl { get; set; }

        public List<string> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        public List<string> RelatedCodexItems
        {
            get { return _relatedCodexItems; }
            set { _relatedCodexItems = value; }
        }

        public static CodexItemType DetermineType(string id)
        {
            if(string.IsNullOrEmpty(id))
                return CodexItemType.Unknown;
            
            var separatorIndex = id.IndexOf('/');
            var typeName = id.Substring(0, separatorIndex);
            
            var itemType = CodexItemType.Unknown;
            Enum.TryParse(typeName, true, out itemType);

            return itemType;
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
}