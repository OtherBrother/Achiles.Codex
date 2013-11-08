using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Achiles.Codex.Web
{
    public class BaseController : Controller
    {
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