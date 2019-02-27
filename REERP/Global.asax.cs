using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net.Core;
using REERP.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using REERP.Security;

namespace REERP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();

            DependencyResolver.SetResolver(new NinjectDependencyResolver());

            //log4net.Config.XmlConfigurator.Configure();
            //DependencyResolver.Current.GetService<ILogger>();

            MyIdentityDbContext db = new MyIdentityDbContext();
            RoleStore<MyIdentityRole> roleStore = new RoleStore<MyIdentityRole>(db);
            RoleManager<MyIdentityRole> roleManager = new RoleManager<MyIdentityRole>(roleStore);

            if (!roleManager.RoleExists("Administrator"))
            {
                MyIdentityRole newRole = new MyIdentityRole("Administrator", "Administrators can add, edit and delete data.");
                roleManager.Create(newRole);
            }

            if (!roleManager.RoleExists("SalesPerson"))
            {
                MyIdentityRole newRole = new MyIdentityRole("SalesPerson", "SalesPersons can only add or edit data.");
                roleManager.Create(newRole);
            }

            if (!roleManager.RoleExists("Cashier"))
            {
                MyIdentityRole newRole = new MyIdentityRole("Cashier", "Cashiers can only add or edit data.");
                roleManager.Create(newRole);
            }

            if (!roleManager.RoleExists("StoreKeeper"))
            {
                MyIdentityRole newRole = new MyIdentityRole("StoreKeeper", "StoreKeepers can only add or edit data.");
                roleManager.Create(newRole);
            }
        }
    }
}
