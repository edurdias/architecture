using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdventureWorks.Apps.Web.Controllers;
using AdventureWorks.Apps.Web.ViewModels;
using AdventureWorks.Apps.Web.ViewModels.Person;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Foundation.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AdventureWorks.Apps.Web.Tests.Controllers
{
    [TestClass]
    public class PersonControllerTest
    {



        [TestMethod]
        public void TestIndex()
        {
            var log = new Mock<ILogService>();
            var message = new Mock<IMessageDisplayService>();

            var controller = new PersonController(log.Object, message.Object)
            {
                ControllerContext = CreateControllerContext("X-Requested-With", string.Empty)
            };

            var domain = CreateDomain(new List<IPerson>
            {
                new Mock<IPerson>().Object
            });

            var model = new Pagination<IPerson, IndexViewModel>(domain);
            var result = controller.Index(model) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.AreEqual(model, result.Model);
            Assert.IsNull(model.rows);
        }


        [TestMethod]
        public void TestIndexWithPagination()
        {
            var log = new Mock<ILogService>();
            var message = new Mock<IMessageDisplayService>();

            var controller = new PersonController(log.Object, message.Object)
            {
                ControllerContext = CreateControllerContext("X-Requested-With", "XMLHttpRequest")
            };

            var expectedPeople = new List<IPerson>
            {
                new Mock<IPerson>().Object
            };

            var domain = CreateDomain(expectedPeople);
            var model = new Pagination<IPerson, IndexViewModel>(domain);
            var result = controller.Index(model) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(model, result.Data);
            Assert.AreEqual(expectedPeople.Count, model.rows.Count());
        }

        [TestMethod]
        public void TestAddingPerson()
        {
            var log = new Mock<ILogService>();
            var message = new Mock<IMessageDisplayService>();

            var domain = new Mock<IPerson>();
            domain.Setup(d => d.Save()).Verifiable();

            var viewModel = new AddViewModel(domain.Object)
            {
                FirstName = "Joe",
                LastName = "Satriani"
            };

            var controller = new PersonController(log.Object, message.Object);
            var result = controller.Add(viewModel) as RedirectToRouteResult;
            Assert.IsNotNull(result);

            domain.VerifyAll();

        }

        [TestMethod]
        public void TestAddingInvalidViewModel()
        {
            var log = new Mock<ILogService>();
            var message = new Mock<IMessageDisplayService>();
            
            var viewModel = new AddViewModel
            {
                FirstName = "Joe"
            };

            var controller = new PersonController(log.Object, message.Object);
            controller.ModelState.AddModelError("LastName", string.Empty);
            var result = controller.Add(viewModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(viewModel, result.Model);
            Assert.AreEqual(1, controller.ModelState["LastName"].Errors.Count);
        }

        [TestMethod]
        public void TestAddingWithError()
        {
            var log = new Mock<ILogService>();
            log.Setup(l => l.Error(It.IsAny<Exception>())).Verifiable();

            var message = new Mock<IMessageDisplayService>();
            message.Setup(m => m.Error(It.IsAny<string>())).Verifiable();

            var domain = new Mock<IPerson>();
            domain.Setup(d => d.Save()).Throws(new Exception());

            var viewModel = new AddViewModel(domain.Object)
            {
                FirstName = "Joe",
                LastName = "Satriani"
            };

            var controller = new PersonController(log.Object, message.Object);
            var result = controller.Add(viewModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(viewModel, result.Model);
            
            log.VerifyAll();
            message.VerifyAll();
        }


        #region Internal Methods

        private static IPerson CreateDomain(IList<IPerson> people)
        {
            var domain = new Mock<IPerson>();
            domain.Setup(d => d.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(people);

            return domain.Object;
        }

        private static ControllerContext CreateControllerContext(string requestKey, string requestValue)
        {
            var request = new Mock<HttpRequestBase>();
            request.Setup(r => r[requestKey])
                .Returns(requestValue);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(ctx => ctx.Request)
                .Returns(request.Object);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(ctx => ctx.HttpContext)
                .Returns(httpContext.Object);

            return controllerContext.Object;
        }

        #endregion

    }
}
