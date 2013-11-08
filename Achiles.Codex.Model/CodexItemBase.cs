using System.Collections.Generic;

namespace Achiles.Codex.Model
{
    public abstract class CodexItemBase : CodexItem
    {
        private List<string> _relatedCodexItems = new List<string>();
        
        public string IconUrl { get; set; }
        public string ArticleId { get; set; }

        public List<string> RelatedCodexItems
        {
            get { return _relatedCodexItems; }
            set { _relatedCodexItems = value; }
        }
    }
}