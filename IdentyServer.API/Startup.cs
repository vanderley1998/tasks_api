using LubyTasks.API.Filters;
using LubyTasks.Domain;
using LubyTasks.Domain.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;

namespace LubyTasks.IdentyServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public string Key { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Key = Configuration.GetConnectionString("KeyJwt");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LubyTasksDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<LubyTasksHandler>();
            services.AddScoped<CurrentUser>();
            services.AddScoped<StatusRequestFilter>();
            services.AddScoped<CurrentUserFilter>();
            services.AddServerSideBlazor(o => o.DetailedErrors = true);

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";

            }).AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Key)),
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            Authentication.KeyJwt = Key;

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
