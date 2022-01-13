using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WaServer.Data;
using WaServer.Data.Entities;
using WaServer.Data.Repositories;
using WaServer.Data.Repositories.Contracts;
using WaServer.Services.Auth;
using WaServer.Services.Security;
using WaServer.Services.Security.Contracts;

namespace WaServer
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
            services.AddDbContext<SimpleEcommerceContext>(builder => 
                builder.UseNpgsql(Configuration.GetConnectionString("SimpleEcommerceContext")));
            services.AddScoped<IBasicRepository<Order>, OrdersRepository>();
            services.AddScoped<IBasicRepository<OrderItem>, OrderItemsRepository>();
            services.AddScoped<IBasicRepository<DeliveryTeam>, DeliveryTeamsRepository>();
            services.AddScoped<IAuthenticationService, Authenticator>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            var key = Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt").GetValue<string>("Key"));
            var tokenParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            services.AddSingleton(s => tokenParameters);
            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = tokenParameters;
                });
            services.AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(opt => opt
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
