using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using ReportService.DbAccessor;
using ReportService.Report;
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
            var serviceOptions = new ReportServiceOptions();
            Configuration.GetSection("SalaryReportService").Bind(serviceOptions);
            services.AddMvc();
            services.AddScoped<IDbAccessor>(i => new DbAccessor.DbAccessor(new NpgsqlConnection(serviceOptions.DbConnectionString)));
            services.AddScoped<IBookkeepingDepartment>(i => new BookkeepingDepartment(serviceOptions.BookkeepingDepartment));
            services.AddScoped<IHumanResourcesDepartment>(i => new HumanResourcesDepartment(serviceOptions.HumanResourcesDepartment));
            services.AddScoped<IEmployeesRepository, ActiveEmployeesRepository>();
            services.AddScoped<IEmployeeSalaryReportBuilder>(i => new EmployeeSalaryReportBuilder(
                i.GetService<IBookkeepingDepartment>(),
                i.GetService<IHumanResourcesDepartment>(),
                System.IO.File.ReadAllText(serviceOptions.SalaryReportTemplateFile)));
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
