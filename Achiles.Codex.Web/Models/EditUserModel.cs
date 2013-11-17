using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Achiles.Codex.Web.Models
{
    public class EditUserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsContributor { get; set; }

    }
}