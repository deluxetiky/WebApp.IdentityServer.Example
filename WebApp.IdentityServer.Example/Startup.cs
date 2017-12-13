using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApp.IdentityServer.Example.Helper;

namespace WebApp.IdentityServer.Example
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


			var authUrl = "http://localhost:8000";

			#region IdentityServerSetup

			services.AddIdentityServer()
				.AddDeveloperSigningCredential()
				.AddInMemoryApiResources(MemoryDb.GetApiResources())
				.AddInMemoryClients(MemoryDb.GetClients())
				.AddTestUsers(MemoryDb.GetUsers());

			#endregion

			#region Validation Settings
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.Authority = authUrl;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = authUrl,
					ValidateAudience = false,
					ValidAudience = "sinanbir.com.auth",//test purposes
					ValidateLifetime = true,
				};
			});

			#endregion
			services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			
			app.UseIdentityServer();
			app.UseAuthentication();
			app.UseMvc();
        }
    }
}
