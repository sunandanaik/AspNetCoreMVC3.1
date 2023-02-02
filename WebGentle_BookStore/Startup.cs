using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebGentle_BookStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) //this method is used to add all dependencies that are used in the application.
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) //this method finds out whether current environment is development from launchsettings.json file.
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); //this is to inform application to use static files.
            //In order to use static files from non-wwwroot folder
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
                RequestPath = "/MyStaticFiles"
            });

            ////1 Middleware created
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello from My First Middleware..!!");
            //    await next(); //this is to call next middleware.
            //    await context.Response.WriteAsync("Hello from My First Middleware..!!"); //then calls this middleware after last middleware.
            //});

            ////2 Middleware created
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello from My Second Middleware..!!");
            //    await next();
            //});

            ////3 Middleware created
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello from My Third Middleware..!!");
            //    await next();
            //});

            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        if (env.IsDevelopment())
            //        {
            //            await context.Response.WriteAsync("Hello developemnt!");
            //        }
            //        else if (env.IsProduction())
            //        {
            //            await context.Response.WriteAsync("Hello Production!");
            //        }
            //        else if (env.IsStaging())
            //        {
            //            await context.Response.WriteAsync("Hello Staging!");
            //        }
            //        else
            //        {
            //            await context.Response.WriteAsync(env.EnvironmentName);
            //        }
            //    });
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/sun", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello Sunanda!");
            //    });
            //});

            //To route to a specific Controller's Action method.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute(); 
            });
        }
    }
}
