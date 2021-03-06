﻿using Encode.Models;
using MySql.Data.Entity;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Encode.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            Database.SetInitializer(new EncodeInitializer());
            EncodeInitializer.DatabaseInitialization();

    
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
