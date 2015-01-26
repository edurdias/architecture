using System;
using System.Web.Mvc;
using AdventureWorks.Apps.Web.ViewModels;
using AdventureWorks.Apps.Web.ViewModels.Product;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Foundation;
using AdventureWorks.Foundation.Services;

namespace AdventureWorks.Apps.Web.Controllers
{
    public class ProductController : Controller
    {
        public ProductController()
        {
            Log = Ioc.Resolve<ILogService>();
            Message = Ioc.Resolve<IMessageDisplayService>(new { controller = this });
        }

        private ILogService Log { get; set; }

        private IMessageDisplayService Message { get; set; }

        public ActionResult Index(Pagination<IProduct, IndexViewModel> model)
        {

            if (Request.IsAjaxRequest())
            {
                model.Paginate(d => new IndexViewModel
                {
                    Id = d.Id
                });

                return Json(model, JsonRequestBehavior.AllowGet);
            }

            return View(model);
        }

        public ActionResult Add()
        {
            return View(new AddViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var domain = model.ToDomain();
                    domain.Save();
                    Message.Success("Product added with success.");
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

        public ActionResult Edit(int id)
        {
            var domain = Ioc.Resolve<IProduct>();
            domain = domain.Load(id);
            return View(new EditViewModel(domain));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = model.ToDomain();
                    domain.Save();
                    Message.Success("Product edited with success.");
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

        public ActionResult Remove(int id)
        {
            try
            {
                var domain = Ioc.Resolve<IProduct>();
                domain = domain.Load(id);
                domain.Remove();
                Message.Success("Product removed with success.");
            }
            catch (Exception e)
            {
                Message.Error(e.Message);
                Log.Error(e);
            }
            return RedirectToAction("Index");
        }
    }
}