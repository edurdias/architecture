﻿using System;
using System.Web;
using System.Web.Http;

namespace AdventureWorks.Service.Api
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}