using System.Web.Mvc;
using AdventureWorks.Foundation.Services;

namespace AdventureWorks.Apps.Web.Services
{
    public class MessageDisplayService : IMessageDisplayService
    {
        public MessageDisplayService()
        {
        }

        public MessageDisplayService(ControllerBase controller)
        {
            Controller = controller;
        }

        public ControllerBase Controller { get; set; }

        public void Success(string message)
        {
            SetMessage(message, "success");
        }

        public void Error(string message)
        {
            SetMessage(message, "danger");
        }

        private void SetMessage(string message, string messageType)
        {
            Controller.TempData["message"] = message;
            Controller.TempData["messageType"] = messageType;
        }
    }
}