using System;
using System.Web;
using System.Web.Http;
using AdventureWorks.Foundation;
using AdventureWorks.Foundation.Data;
using Autofac;

namespace AdventureWorks.Service.Api
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Ioc.ContainerBuilder.RegisterType<IDataContext>().AsImplementedInterfaces();
        }
    }
}