using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Achilles.Codex.Model;

namespace Achilles.Codex.Web.Models
{
    public interface ICodexItemModel<out T> where T : CodexItemBase
    {
        CodexItemBase __CodexItemBase { get; set; }
        bool IsNew { get; set; }
        string ArticleBody { get; set; }
    }

    public class CodexItemModel<T> : ICodexItemModel<T> where T : CodexItemBase
    {
        public CodexItemBase __CodexItemBase { get; set; }
  
        public string ArticleBody { get; set; }

        public T CodexItem {
            get
            {
                return __CodexItemBase as T;
            }
             set
            {
                __CodexItemBase = value;
            }
        }
        public bool IsNew { get; set; }
    }

}