﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Models;
using Microsoft.Practices.Unity;
using Raven.Client;

namespace Achilles.Codex.Web
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
      

        protected void SetRelatedItems(CodexItem item)
        {
            //TODO: this is hack and should be redone properly, but because of time contriants it here..
            var relatedItemIds = this.Request.Form["CodexItem.RelatedItems"];
            
            if (string.IsNullOrEmpty(relatedItemIds))
                return;

            var arrayOfIds = relatedItemIds.Split(new char[] {','});
            var relatedItems = DocumentSession.Load<CodexItem>(arrayOfIds).ToArray().Where(x=>x!=null).Select(x=> new RelatedCodexItem { Id = x.Id, Name = x.Name});
            item.RelatedCodexItems.Clear();
            item.RelatedCodexItems.AddRange(relatedItems);

        }

        protected T UpsertCodexItem<T>(T input) where T : CodexItem
        {
            T itemToUpdate = null;

            if (!string.IsNullOrEmpty(input.Id))
                itemToUpdate = DocumentSession.Load<T>(input.Id);

            if (itemToUpdate == null)
            {
                SetRelatedItems(input);
                //make all tags lowercase
                input.Tags = input.Tags.Select(x => x.ToLower()).Distinct().ToList();
                DocumentSession.Store(input);
                return input;
            }
            else
            {
                itemToUpdate.Name = input.Name; //this allows to id and name to differ..
                itemToUpdate.Description = input.Description;
                itemToUpdate.Tags = input.Tags.Select(x=>x.ToLower()).Distinct().ToList();
                SetRelatedItems(itemToUpdate);
                return itemToUpdate;
            }
        }
   }

    public class CodexItemBaseController : CodexItemController
    {
        public CodexItemModel<T> GetModel<T>(string id) where T : CodexItemBase, new()
        {
            return string.IsNullOrEmpty(id) ? new CodexItemModel<T> { IsNew = true, CodexItem = new T()} : CreateModel(DocumentSession.Load<T>(id));
        }

        public CodexItemModel<T> CreateModel<T>(T codexItem) where T : CodexItemBase
        {
            string articleBody = null;
            
            if (codexItem != null && !string.IsNullOrEmpty(codexItem.BodyId))
            {
                var body = DocumentSession.Load<Body>(codexItem.BodyId);
                if (body != null)
                    articleBody = body.Text;
            }

            return new CodexItemModel<T> { CodexItem = codexItem, IsNew = (codexItem == null) || string.IsNullOrEmpty(codexItem.Id), ArticleBody = articleBody };
        }

        /// <summary>
        /// Creates if item does not exists, otherwise updates item
        /// </summary>
        /// <typeparam name="T">Code item type</typeparam>
        /// <param name="model">Input</param>
        /// <returns>Inserted or udpated item</returns>
        protected T UpsertBaseCodexItem<T>(CodexItemModel<T> model) where T : CodexItemBase
        {
            var itemToUpdate = UpsertCodexItem(model.CodexItem);
            itemToUpdate.IconUrl = model.__CodexItemBase.IconUrl;
            
            UpdateItemArticle(model, itemToUpdate);

            return itemToUpdate;
        }

        private void UpdateItemArticle<T>(CodexItemModel<T> model, T itemToUpdate) where T : CodexItemBase
        {
            Body body = null;
            body = !string.IsNullOrEmpty(itemToUpdate.BodyId)
                ? DocumentSession.Load<Body>(itemToUpdate.BodyId)
                : new Body();

            if (!string.IsNullOrEmpty(model.ArticleBody))
            {
                body.Text = model.ArticleBody;
                DocumentSession.Store(body);
            }
            itemToUpdate.BodyId = body.Id;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var ignoreKeys = ModelState.Keys.Where(x => x.StartsWith("__")).ToArray();
            foreach (var ignoreKey in ignoreKeys)
                ModelState.Remove(ignoreKey);

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

    public enum UpserResult
    {
        Insert,
        Update
    }
}