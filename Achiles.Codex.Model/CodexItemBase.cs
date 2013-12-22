using System.Collections.Generic;

namespace Achilles.Codex.Model
{
    public class CodexItemBase : CodexItem
    {
        public string BodyId { get; set; }
    }

    public class Body
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}