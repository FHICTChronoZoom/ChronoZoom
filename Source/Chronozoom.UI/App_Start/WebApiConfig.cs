﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Chronozoom.Business.Repositories;
using Chronozoom.Business;
using System.Configuration;

namespace Chronozoom.UI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var unityContainer = new UnityContainer();
            RegisterEntityFramework(unityContainer);

            var resolver = new UnityDependencyResolver(unityContainer);
            config.DependencyResolver = resolver;

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("Default API", "api/v2/{controller}/{id}", new { id = RouteParameter.Optional });
        }

        private static void RegisterOptions(IUnityContainer container)
        {
            var applicationSettings = new ApplicationSettings();

            // Add application wide settings here
            //applicationSettings.Set("Identifier key", "Something something");

            container.RegisterInstance<IApplicationSettings>(applicationSettings);
        }

        private static void RegisterEntityFramework(IUnityContainer container)
        {
            container.RegisterType<IUserRepository, Chronozoom.Entities.Repositories.UserRepository>();
            container.RegisterType<ICollectionRepository, Chronozoom.Entities.Repositories.CollectionRepository>();
            container.RegisterType<ITimelineRepository, Chronozoom.Entities.Repositories.TimelineRepository>();
            container.RegisterType<IExhibitRepository, Chronozoom.Entities.Repositories.ExhibitRepository>();
            container.RegisterType<ITourRepository, Chronozoom.Entities.Repositories.TourRepository>();
        }

        private static void RegisterMongoDB(IUnityContainer container)
        {
            // TODO : Lars, add the dependenies for MongoDB in the Unity container, see RegisterEntityFramework method for an example.
        }
    }
}
