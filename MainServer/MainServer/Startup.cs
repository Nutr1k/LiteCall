using MainServer.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace MainServer
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;
            services.AddEntityFrameworkSqlite().AddDbContext<DB>();
            //services.AddLogging(
            //builder =>
            //{
            //    builder.AddFilter("Microsoft", LogLevel.Warning)
            //           .AddFilter("System", LogLevel.Warning)
            //           .AddFilter("NToastNotify", LogLevel.Warning)
            //           .AddConsole();
            //});
            services.AddOptions();

            services.AddControllers();
            services.AddCors();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);//You can set Time   
            });
            //���������� ��������� �������������
            //AuthOptions.SetKey(Configuration.GetSection("PrivateKey").Value);
            try
            {
                var key = JsonNode.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, @"PrivateKey\PrivateKey.json")));
                AuthOptions.SetKey((string)key["Private"]);
            }
            catch
            {
                Console.WriteLine("Private key not found " + (Path.Combine(AppContext.BaseDirectory, @"PrivateKey\PrivateKey.json")));
                Console.WriteLine("Closing after 10 seconds");
                Thread.Sleep(10000);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }

            //��� �����
            services.AddDistributedMemoryCache();
            #region
            //Added for session state
            //Added for session state


            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(10);
            //});


            //var paramss = new TokenValidationParameters();
            //AuthOptions auth = new AuthOptions(Configuration.GetSection("PrivateKey").Value);
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.RequireHttpsMetadata = false;

            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidIssuer = AuthOptions.Issuer,
            //            RequireAudience = true,
            //            ValidateAudience = true,
            //            ValidAudience = AuthOptions.Audience,
            //            RequireExpirationTime = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            RequireSignedTokens = true,
            //            IssuerSigningKey = auth.PrivateKey
            //        };

            //    });   
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseRouting();
            //app.UseMiddleware<PingMiddleware>();
            app.UseCors(policy =>
            {
                policy
                .SetIsOriginAllowed(origin => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
