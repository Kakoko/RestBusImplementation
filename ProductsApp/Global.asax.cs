using RestBus.RabbitMQ;
using RestBus.RabbitMQ.Subscription;
using RestBus.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProductsApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        RestBusHost restbusHost = null; //Make this a field in the class

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);




            //*** Start RestBus subscriber/host **//


            var amqpUrl = "amqp://localhost:5672"; //AMQP URI for RabbitMQ server
            var serviceName = "products"; //Uniquely identifies this service

            var msgMapper = new BasicMessageMapper(amqpUrl, serviceName);
            var subscriber = new RestBusSubscriber(msgMapper);
            restbusHost = new RestBusHost(subscriber, GlobalConfiguration.Configuration);
            restbusHost.Start();


            //****//
        }

        protected void Application_End()
        {
            if (restbusHost != null)
                restbusHost.Dispose();
        }
    }
}
