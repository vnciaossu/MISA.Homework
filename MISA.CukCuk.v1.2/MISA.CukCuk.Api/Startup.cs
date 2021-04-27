using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using MISA.Core.Service;
using MISA.CukCuk.Api.Middware;
using MISA.Infrastructure.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MISA.CukCuk.Api", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            //addScoped Service
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerGroupService, CustomerGroupService>();
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            //addScoped Repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerGroupRepository, CustomerGroupRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MISA.CukCuk.Api v1"));
            }

            // Hook in the global error-handling middleware
            //app.UseMiddleware(typeof(ErrorHandlingMiddleware));   
            app.UseMiddleware(typeof(ErrorHandling));

           //app.UseExceptionHandler(c => c.Run(async context =>
           //{

           //    var exception = context.Features
           //        .Get<IExceptionHandlerPathFeature>()
           //        .Error;
           //    if (exception is CustomerException)
           //    {
           //        var response = new
           //        {
           //            devMsg = exception.Message,
           //            userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA",
           //            MISACode = "001",
           //            Data = exception.Data
           //        };
           //        var result = JsonConvert.SerializeObject(response);
           //        context.Response.ContentType = "application/json";
           //        //400
           //        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
           //        await context.Response.WriteAsJsonAsync(response);
           //    }
           //    else
           //    {
           //        var response = new
           //        {
           //            devMsg = exception.Message,
           //            userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA",
           //            MISACode = "002",
           //            Data = exception
           //        };
           //        //500
           //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
           //        await context.Response.WriteAsJsonAsync(response);

           //    }

           //}));
           app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
