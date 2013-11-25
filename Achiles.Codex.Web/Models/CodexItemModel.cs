using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Achiles.Codex.Model;

namespace Achiles.Codex.Web.Models
{
    public class CodexItemModel<T>  where T : CodexItemBase
    {
        public T CodexItem { get; set; }
        public bool IsNew { get; set; }
    }

}