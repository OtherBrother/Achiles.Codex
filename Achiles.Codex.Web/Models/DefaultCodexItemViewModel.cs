using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Achilles.Codex.Model;

namespace Achilles.Codex.Web.Models
{
    public class DefaultCodexItemViewModel
    {
        public CodexItem Item { get; set; }
        public Body Body { get; set; }
    }
}