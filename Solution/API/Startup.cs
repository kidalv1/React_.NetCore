using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Activities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
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
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("React")));
      //deze is nodig zodat react data kan lezen van api
          services.AddCors(); // Make sure you call this previous to AddMvc
          services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
          services.AddMvc(options => options.EnableEndpointRouting = false);
          services.AddMediatR(typeof(List.Handler).Assembly);
          services.Add(new ServiceDescriptor(typeof(IRepo), new Repo()));
          services.AddScoped<IRepo, Repo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

          app.UseCors(
            options => options.WithOrigins("http://localhost:3000").AllowAnyMethod()
          );
      //app.UseHttpsRedirection();
            app.UseMvc();

            app.UseRouting();

            //app.UseAuthorization();

      app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
