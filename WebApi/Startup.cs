using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using WebApi.Context;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICallCenterService, CallCenterService>();

            string databaseType = this._configuration["DatabaseType"].ToLower();
            if (databaseType == "postgresql")
            {
                services.AddScoped<IAgentRepository, PostgreSqlAgentService>();
                var connectionString = this._configuration.GetConnectionString("PostgreSQLConnection");
                services.AddDbContext<AgentDbContext>(options =>
                            options.UseNpgsql(connectionString));
            }
            else if (databaseType == "mongodb")
            {
                services.AddScoped<IAgentRepository, MongoDbAgentService>();
                services.AddSingleton<IMongoDatabase>(provider =>
                {
                    var mongoClient = new MongoClient(this._configuration.GetConnectionString("MongoDBConnection"));
                    return mongoClient.GetDatabase(this._configuration["testdb"]);
                });
            }
            else
            {
                throw new InvalidOperationException("Invalid database type specified in appsettings.json.");
            }

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
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