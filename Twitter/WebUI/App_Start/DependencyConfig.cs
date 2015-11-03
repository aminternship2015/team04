using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using DAL;
using Services;
using System.Web.Mvc;
using Autofac.Builder;
using Autofac.Integration.Mvc;

namespace WebUI.App_Start
{
    public static class DependencyConfig
    {
        private static IContainer Container { get; set; }

        public static void Config()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            //builder.RegisterType<BookRepository>().As<IRepository<Book>>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<UserDao>().As<IUserDao>();
            builder.RegisterType<TweetService>().As<ITweetService>();
            builder.RegisterType<TweetsDao>().As<ITweetsDao>();
            builder.RegisterType<FollowService>().As<IFollowService>();
            builder.RegisterType<FollowsDao>().As<IFollowsDao>();

            Container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}