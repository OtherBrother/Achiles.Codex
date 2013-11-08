using System.Collections.Generic;

namespace Achiles.Codex.Model
{
    public abstract class CodexItem
    {
        private List<string> _tags = new List<string>();

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<string> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }
    }
}