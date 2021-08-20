using ElectronicSignage.Domain.Options;
using ElectronicSignage.Repository;
using ElectronicSignage.Service;
using ElectronicSignage.Web.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

namespace ElectronicSignage.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeSerializer(DateTimeKind.Local, BsonType.DateTime));

            var transportOptions = Configuration.GetSection("TransportOptions").Get<TransportOptions>();
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            var mongoDBOptions = Configuration.GetSection("MongoDBOptions").Get<MongoDBOptions>();

            services.AddSingleton<TransportOptions>(provider => transportOptions);
            services.AddSingleton<TokenOptions>(provider => tokenOptions);
            services.AddSingleton<MongoDBOptions>(provider => mongoDBOptions);

            #region Repository

            services.AddScoped<AuthorizationRepository>();
            services.AddScoped<ToDoRepository>();
            services.AddScoped<HeartbeatRepository>();

            #endregion

            #region Service

            services.AddScoped<AuthorizationService>();

            #endregion

            services.AddControllersWithViews();

            services
                .AddMvc()
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("permissionRequirement", policy => policy.Requirements.Add(new PermissionRequirement()));
            }).AddAuthorizationPolicyEvaluator();
            services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();

            services.AddAuthentication(options => options.AddScheme("PermissionHandler", o => o.HandlerType = typeof(PermissionHandler)));

            services.AddHttpClient();
            services.AddHttpClient("Heartbeat", client =>
            {
                client.Timeout = TimeSpan.FromSeconds(3);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (HttpContext context, Func<Task> next) =>
            {
                await next.Invoke();

                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = new PathString("/");
                    await next.Invoke();
                }
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
