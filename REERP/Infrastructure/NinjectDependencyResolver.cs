using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using log4net;
using NetSqlAzMan;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.Providers;
using REERP.DAL.Repository;


namespace REERP.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.DeclaringType));
            kernel.Bind<IAzManStorage>().To<SqlAzManStorage>().WithConstructorArgument("connectionString", System.Configuration.ConfigurationManager.
                                                                                       ConnectionStrings["SecurityContext"].ConnectionString);
            kernel.Bind<NetSqlAzManRoleProvider>().To<NetSqlAzManRoleProvider>();

            kernel.Bind<REERP.Sales.Services.ISalesInvoiceService>().To<REERP.Sales.Services.SalesInvoiceService>();

            kernel.Bind<REERP.Sales.Services.ICustomerService>().To<REERP.Sales.Services.CustomerService>();

            kernel.Bind<REERP.Product.Services.ICategoryService>().To<REERP.Product.Services.CategoryService>();

            kernel.Bind<REERP.Product.Services.IBranchService>().To<REERP.Product.Services.BranchService>();

            kernel.Bind<REERP.Product.Services.IProductService>().To<REERP.Product.Services.ProductService>();

            kernel.Bind<REERP.Store.Services.IProductReceiveService>().To<REERP.Store.Services.ProductReceiveService>();

            kernel.Bind<REERP.Store.Services.IProductTransferService>().To<REERP.Store.Services.ProductTransferService>();

           kernel.Bind<REERP.DAL.UnitOfWork.IUnitOfWork>().To<REERP.DAL.UnitOfWork.UnitOfWork>();
        }
    }
}