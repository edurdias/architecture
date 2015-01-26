using System;
using System.Web.Mvc;
using AdventureWorks.Apps.Web.ViewModels;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Foundation;
using AdventureWorks.Foundation.Services;
using Foundation.Services;

namespace AdventureWorks.Apps.Web.Controllers
{
    public abstract class ControllerBase<TDomain, TIndexViewModel, TAddViewModel> : Controller, ITranslator<TDomain, TIndexViewModel>
        where TDomain : IPaginable<TDomain>, IDomain<TDomain>
        where TIndexViewModel : class
        where TAddViewModel : class, IDomainConvertible<TDomain>, new()
    {

        protected ControllerBase()
        {
            Log = Ioc.Resolve<ILogService>();
            Message = Ioc.Resolve<IMessageDisplayService>(new { controller = this });
        }

        protected IMessageDisplayService Message { get; set; }

        public ActionResult Index(Pagination<TDomain, TIndexViewModel> model)
        {

            if (Request.IsAjaxRequest())
            {
                model.Paginate(Translate);
                return Json(model, JsonRequestBehavior.AllowGet);
            }

            return View(model);
        }

        protected ILogService Log { get; set; }

        public abstract TIndexViewModel Translate(TDomain instance);

        public abstract TDomain Translate(TIndexViewModel instance);

        [HttpGet]
        public ActionResult Add()
        {
            var model = Activator.CreateInstance<TAddViewModel>();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TAddViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var domain = model.ToDomain();
                    domain.Save();
                    Message.Success("Added with success.");
                }
                catch (Exception e)
                {
                    Message.Error("Ooops. Something went wrong. Please try again.");
                    Log.Error(e);
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}