using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    }


}