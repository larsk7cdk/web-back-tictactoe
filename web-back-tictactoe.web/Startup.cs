﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using web_back_tictactoe.web.Extensions;
using web_back_tictactoe.web.Models;
using web_back_tictactoe.web.Services;

namespace web_back_tictactoe.web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRouting();

            services.AddSingleton<IUserService, UserService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseWebSockets();

            app.UseCommunicationMiddleware();

            // Call UserRegistration with dedicated route
            // http://localhost:5000/createuser?firstname=Lars&lastName=Larsen&email=lars@k7c.dk&password=123
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapGet("CreateUser", context =>
            {
                var firstName = context.Request.Query["firstName"];
                var lastName = context.Request.Query["lastName"];
                var email = context.Request.Query["email"];
                var password = context.Request.Query["password"];
                var userService = context.RequestServices.GetService<IUserService>();
                userService.RegisterUser(new UserModel
                { FirstName = firstName, LastName = lastName, Email = email, Password = password });
                return context.Response.WriteAsync($"User {firstName} {lastName} has been sucessfully created.");
            });
            var newUserRoutes = routeBuilder.Build();
            app.UseRouter(newUserRoutes);

            // This redelegate http://localhost:5000/newuser to UserRegistration/Index. Shortcut
            var options = new RewriteOptions()
                .AddRewrite("newuser", "/UserRegistration/Index", false);
            app.UseRewriter(options);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseStatusCodePages("text/plain", "HTTP Error - Status Code: {0}");
        }
    }
}