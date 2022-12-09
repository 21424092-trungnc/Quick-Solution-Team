using Autofac;
using Business.Services;
using Business.Services.Interfaces;
using Data.Core.Repositories;
using Data.Core.Repositories.Base;
using Data.Core.Repositories.Interfaces;
using log4net;

namespace Host.WcfService
{
    public class MainModule
    {
        public static IContainer BuildContainer()
        {
            log4net.Config.XmlConfigurator.Configure();
            var builder = new ContainerBuilder();

            // register services

            builder.RegisterType<DungChungService>().As<IDungChungService>();
            builder.RegisterType<NewsService>().As<INewsService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<AccountServices>().As<IAccountServices>();
            builder.RegisterType<RecruitmentService>().As<IRecruitmentService>();
            builder.RegisterType<GymSetupService>().As<IGymSetupService>();
            builder.RegisterType<CartService>().As<ICartService>();

            // register repositories & log4net
            builder.Register(log => LogManager.GetLogger(typeof(MainModule))).SingleInstance();
            //builder.RegisterInstance(LogManager.GetLogger(typeof(MainModule))).As<ILog>();
            builder.RegisterType<DungChungRepository>().As<IDungChungRepository>();
            builder.RegisterType<WebHelper>().As<IWebHelper>();
            //News
            builder.RegisterType<NewsRepository>().As<INewsRepository>();
            builder.RegisterType<BannerRepository>().As<IBannerRepository>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            //Recruitment
            builder.RegisterType<RecruitmentRepository>().As<IRecruitmentRepository>();
            builder.RegisterType<GymSetupRepository>().As<IGymSetupRepository>();
            builder.RegisterType<CartRepository>().As<ICartRepository>();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            return builder.Build();
        }
    }
}