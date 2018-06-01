using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReportService.DbAccessor;
using ReportService.Domain;
using ReportService.Repository;
using ReportService.Services;

namespace ReportService
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
            services.AddMvc();
            services.AddSingleton<IDbAccessor>(i => new NpgsqlAccessor("Host=192.168.99.100;Username=postgres;Password=1;Database=employee"));
            services.AddScoped(i => new BookkeepingDepartment("http://salary.local/api/empcode/"));
            services.AddScoped(i => new HumanResourcesDepartment("http://buh.local/api/inn/"));
            services.AddScoped<IReportRepository<Employee>, ActiveEmployeesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug();
                loggerFactory.AddConsole();
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
