using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
 
using LMS.API.Data; 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using LMS.API.Dtos;
using LMS.API.Middlewares;
using System.IO;

namespace LMS.API
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
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins(Configuration.GetSection("AppSettings:Origion").Value).AllowAnyMethod().AllowAnyHeader();
            })); 

            services.AddMvc();
            services.AddDbContext<AppDBContext>(item => item.UseSqlServer(Configuration.GetConnectionString("DBConn")));
             
            services.AddHttpContextAccessor(); 
            
            services.AddScoped<IAuthRepository, AuthRepository>(); 
            services.AddScoped<IBaseResponse, BaseResponse>(); 
            

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(Configuration.GetSection("AppSettings:AppKey").Value)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); 
            app.UseCors("ApiCorsPolicy");
           // Exception hadling can use globally like middleware
           // app.UseMiddleware<ErrorHandlerMiddleware>(); 

            app.UseRouting();  
            app.UseAuthentication();
            app.UseAuthorization();  

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
