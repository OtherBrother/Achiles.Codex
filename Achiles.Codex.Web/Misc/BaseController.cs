using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achiles.Codex.Model;
using Microsoft.Practices.Unity;
using Raven.Client;

namespace Achiles.Codex.Web
{
    public class BaseController : Controller
    {
        
        [Dependency]
        public IDocumentSession DocumentSession { get; set; }

        protected List<UserInterfaceMessage> Messages
        {
            get {
                return ViewBag.UserInterfaceMessages ?? (ViewBag.UserInterfaceMessages = new List<UserInterfaceMessage>());
            }
        }

        protected void Info(string strongMessage, string message = null)
        {
            Messages.Add(new UserInterfaceMessage { StrongMessage = strongMessage, Message = message, MessageType = MessageType.Info });
        }

        protected void Warn(string strongMessage, string message = null)
        {
            Messages.Add(new UserInterfaceMessage { StrongMessage = strongMessage, Message = message, MessageType = MessageType.Warning });
        }
        protected void Danger(string strongMessage, string message = null)
        {
            Messages.Add(new UserInterfaceMessage { StrongMessage = strongMessage, Message = message, MessageType = MessageType.Danger });
        }
        
        protected void Success(string strongMessage, string message = null)
        {
            Messages.Add(new UserInterfaceMessage { StrongMessage = strongMessage, Message = message, MessageType = MessageType.Success });
        }
    }

    public class CodexItemController : BaseController
    {
        protected void UpdateProperties(CodexItem itemToUpdate, CodexItem input)
        {
            if (itemToUpdate == null) throw new ArgumentNullException("itemToUpdate");

            itemToUpdate.Description = input.Description;
            itemToUpdate.Tags = input.Tags;
        }
    }

    public class CodexItemBaseController : CodexItemController
    {
        protected void UpdateProperties(CodexItemBase itemToUpdate, CodexItemBase input)
        {
            if (itemToUpdate == null) throw new ArgumentNullException("itemToUpdate");

            itemToUpdate.ArticleId = input.ArticleId;
            itemToUpdate.IconUrl = input.IconUrl;
            itemToUpdate.RelatedCodexItems = input.RelatedCodexItems;
            base.UpdateProperties(itemToUpdate, input);
        }
    }


    public class UserInterfaceMessage
    {
        public MessageType MessageType { get; set; }
        public string StrongMessage { get; set; }
        public string Message { get; set; }
    }

    public enum MessageType
    {
        Success,
        Info,
        Warning,
        Danger
    }
}