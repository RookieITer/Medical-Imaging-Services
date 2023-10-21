using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PortFolio_A1_Version2._0.Models;

namespace PortFolio_A1_Version2._0
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeRoles();
        }
        protected void Application_BeginRequest()
        {
            Response.Headers.Add("Content-Security-Policy-Report-Only", "default-src 'self'; report-uri /csp-violation-report-endpoint");
        }

        private void InitializeRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (!roleManager.RoleExists("Doctor"))
            {
                var role = new IdentityRole { Name = "Doctor" };
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Patient"))
            {
                var role = new IdentityRole { Name = "Patient" };
                roleManager.Create(role);
            }
        }
    }
}

