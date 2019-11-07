using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Infrastructure.Data;
using Web.Api.Infrastructure.Data.Repositories;
using Web.Api.Settings;


namespace Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=players.db"));

            services.AddOptions();

            IConfigurationSection sec = Configuration.GetSection("OrdersService");
            services.Configure<OrderOptions>(sec);
            services.Configure<MyOptions>(Configuration);

            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ILoggerService, LoggerService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            ServiceProvider sp = services.BuildServiceProvider();
            IOptionsMonitor<MyOptions> iop = sp.GetService<IOptionsMonitor<MyOptions>>();


            MyOptions op = iop.CurrentValue;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
