using Autofac;
using CashTracker.Database;
using CashTracker.Models;
using System;

namespace CashTracker
{
    /// <summary>
    /// Ioc container containing the app's registered mapping of interfaces to concrete types
    /// </summary>
    public class AppContainer
    {
        private static IContainer container;

        public AppContainer()
        {
            // services
            var builder = new ContainerBuilder();
            builder.RegisterType<JobRepository>().As<IAsyncRepository<Job>>();
            builder.RegisterType<IncomeStatRepository>().As<IAsyncRepository<IncomeStat>>();

            // view models // TODO: Look into whether or not it's worth refactoring to set up view models here
            //builder.RegisterType<MainViewModel>().SingleInstance();

            container = builder.Build();
        }

        /// <summary>
        /// Uses predefined registrations to exchange the provided type for the corresponding type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>() => container.Resolve<T>();

        /// <summary>
        /// Uses predefined registrations to exchange the provided type for the corresponding type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type) => container.Resolve(type);
    }

}
