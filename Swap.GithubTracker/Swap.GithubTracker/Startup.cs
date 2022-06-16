using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swap.GithubTracker.Domain.Configurations;
using Swap.GithubTracker.Domain.Interfaces.Services;
using Swap.GithubTracker.Infra.CrossCutting.IoC;
using Swap.GithubTracker.Infra.External.Services;
using System;

namespace Swap.GithubTracker
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
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1",
                  new Microsoft.OpenApi.Models.OpenApiInfo
                  {
                      Title = "Swagger Swap.GithubTracker",
                      Version = "v1",
                      Description = "Swagger Swap.GithubTracker API"
                  });
            });

            BootStrapper.RegisterServices(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("swagger/v1/swagger.json", "Swagger Swap.GithubTracker");
                opt.RoutePrefix = "";
            });
        }
    }
}
