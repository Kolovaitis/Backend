﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using Loliboo.Configuration;
using Loliboo.Models.UserModels;

namespace Loliboo
{
    public class WebServer : IDisposable
    {
        private readonly string _host;
        private IDisposable _server;

        public WebServer(IConfiguraiton configuration)
        {
            _host = configuration.Host;
        }

        public void Start(Func<IKernel> createKernel)
        {
            _server = WebApp.Start(_host, builder =>
            {
                var configuration = new HttpConfiguration();

                configuration.MapHttpAttributeRoutes();
                configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);
                EnsureAuthIndexes.Exist();
                builder.Use(async (context, next) =>
                {
                    var request = context.Request;
                    Console.WriteLine($">> [{request.RemoteIpAddress}] HTTP {request.Method} {request.Uri}");

                    try
                    {
                        await next();
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Unhandled exception: {ex}");
                        throw;
                    }

                    var response = context.Response;
                    Console.WriteLine($"<< [{request.RemoteIpAddress}] HTTP {response.StatusCode} {((HttpStatusCode)response.StatusCode)}: {response.ContentLength} bytes");
                });

                builder.CreatePerOwinContext(ApplicationIdentityContext.Create);
                builder.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

                builder.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    Provider = new CookieAuthenticationProvider
                    {
                        OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                            validateInterval: TimeSpan.FromMinutes(30),
                            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                    }
                });

                builder.UseNinjectMiddleware(createKernel);
                builder.UseNinjectWebApi(configuration);
            });

            Console.WriteLine($"Server started at {_host}");
        }


        public void Stop()
        {
            _server.Dispose();
        }

        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}
