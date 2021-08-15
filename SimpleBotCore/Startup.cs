using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using SimpleBotCore.Bot;
using SimpleBotCore.Logic;
using SimpleBotCore.Repositories;
using SimpleBotCore.Repositories.Interfaces;

namespace SimpleBotCore
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
            var flagDatabase = Configuration["DatabaseFlag"];
            
            if(flagDatabase.Equals("M"))
            {
                //mongodb init
                string connectionString = Configuration["MongoDB:ConnectionString"];
                MongoClient client = new MongoClient(connectionString);
                services.AddSingleton<IAskRepository>(new MongoDbAskRepository(client));
                services.AddSingleton<IUserProfileRepository>(new MongoDbUserProfileRepository(client));

            }else if(flagDatabase.Equals("S"))
            {
                //sqlserver init
                services.AddTransient<IAskRepository, SqlAskRepository>();
                services.AddTransient<IUserProfileRepository, SqlUserProfileRepository>();
            }
            else
            {
                //mock init
                services.AddSingleton<IAskRepository, MockAskRepository>();
                services.AddSingleton<IUserProfileRepository>(new MockUserProfileRepository());
            }

            services.AddSingleton<IBotDialogHub, BotDialogHub>();
            services.AddSingleton<BotDialog, SimpleBot>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
