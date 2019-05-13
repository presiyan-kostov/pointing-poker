using System.Linq;
using FluentNHibernate.Cfg;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using Poker.Domain.Entities;
using Poker.Domain.Entities.Interfaces;
using Poker.Domain.Factories;
using Poker.Domain.Factories.Interfaces;
using Poker.Service;
using Poker.Service.Interfaces;
using Poker.Transportation.Mapping;
using Poker.Transportation.Repository;
using Poker.Transportation.Repository.Base;
using Poker.Transportation.Repository.Base.Interfaces;
using Poker.Transportation.Repository.Interfaces;

namespace Poker.WebUI
{
    public static class Registry
    {
        public static void Register(IServiceCollection services, string connectionString)
        {
            Transportation(services, connectionString);
            Domain(services);
            Services(services);
        }

        private static void Transportation(IServiceCollection services, string connectionString)
        {
            services.AddSingleton(factory => 
                                  {
                                      return Fluently.Configure()
                                                     .Database(() => FluentNHibernate.Cfg
                                                                                     .Db
                                                                                     .MsSqlConfiguration
                                                                                     .MsSql2012
                                                                                     .ConnectionString(connectionString))
                                                     .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                                                     .BuildSessionFactory();
                                  });

            services.AddScoped(factory => factory.GetServices<ISessionFactory>()
                                                 .First()
                                                 .OpenSession()
                                                 );

            services.AddScoped<IGenericTransaction, GenericTransaction>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIssueEstimationRepository, IssueEstimationRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectUserRepository, ProjectUserRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void Domain(IServiceCollection services)
        {
            // Entities
            services.AddTransient<IProject, Project>();
            services.AddTransient<IUser, User>();

            // Factories
            services.AddSingleton<IIssueFactory, IssueFactory>();
            services.AddSingleton<IProjectFactory, ProjectFactory>();
            services.AddSingleton<IUserFactory, UserFactory>();
        }

        private static void Services(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}