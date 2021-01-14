using Calculus.APIImpl.Abstract;
using Calculus.APIImpl.Realisation;
using Calculus.DTO;
using Calculus.DTO.Singltones;
using Calculus.Servs.Schedulers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculus.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string SpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.

            services.AddCors(opt =>
            {
                opt.AddPolicy(name: SpecificOrigins, builder => {
                    //builder.WithOrigins("http://localhost"); 
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
            services.AddControllers();

            services.AddSingleton(typeof(IGeneralControllerImpl), typeof(GeneralControllerImpl));// имплементация логики контроллера
            services.AddSingleton(typeof(ICalculationsSingl), typeof(CalculationsSingl));// синглтон для рассчетов, в общей памяти для приложения.
            services.AddSingleton(typeof(IWriteQueueSingl), typeof(WriteQueueSingl));// синглтон очереди на запись в файл

            services.AddHostedService<CalcScheduler>();//Служба высчитывает Current из синглтона. И перекачивает данные на жесткий диск                                    
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
            app.UseCors(SpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
