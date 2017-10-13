using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Antiforgery;
using ReportAPI.Common;
using Swashbuckle.AspNetCore.Swagger;
using Report.Service.TemplateService;
using Report.Data.Templates;
using Report.Data.ReportItems;
using Report.Data.FilterItems;
using Report.Data.SortItems;
using Report.Service;
using ReportAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ReportAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            AutoMapper.Mapper.Initialize(cfg => {
                cfg.AddProfile < ServiceModelMappings>();
                cfg.AddProfile<ApiModelMappings>();
            });

            ProjectAppSettings appSettings = new ProjectAppSettings();
            ConfigurationBinder.Bind(Configuration, appSettings);
        }

        public IConfigurationRoot Configuration { get; }

       
        public void ConfigureServices( IServiceCollection services)
        {           
            AddCors(services);
            AddAuthorization(services);
            services.AddMvc();
            services.Configure<ProjectAppSettings>(options => Configuration.GetSection("ProjectAppSettings").Bind(options));
            InjectServices(services);
            // services.AddAntiforgery(options => { options.HeaderName = "X-XSRF-TOKEN"; });
            AddSwagger(services);
        }
               
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IAntiforgery antiforgery)
        {            
            app.UseCors("AngularPolicy");
            //app.UseCors("OpenGate");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An Unexpected fault happend. Try again later.");
                    });
                });
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Psc Report API v1");
            });
        }

        #region " Configure Services "
        
        private void InjectServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IReportItemRepository, ReportItemRepository>();
            services.AddScoped<ISortItemRepository, SortItemRepository>();
            services.AddScoped<IFilterItemRepository, FilterItemRepository>();
        }

        private void AddCors(IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("OpenGate",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

                options.AddPolicy("AngularPolicy",
                   builder => builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .WithHeaders("accept", "content-type", "origin", "X-ANGREP")
                   .AllowCredentials());
            });        

        private void AddSwagger(IServiceCollection services) =>
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "ReportAPI",
                    Version = "v1",
                    Description = "Psc Report Templates Test Project API",
                    TermsOfService = "Meh",
                    Contact = new Contact { Name = "SomeGuy", Email = "meh@meh.com", Url = "http://meh.com" }
                });
            });

        private void AddAuthorization(IServiceCollection services) =>
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

        #endregion

        #region " Configure "

        private void UseJwtBearerAuthenticaiton(IApplicationBuilder app)
        {
            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                TokenValidationParameters = new TokenValidationParameters()
                {
                    //IssuerSigningKey =   TokenAuthOption.Key
                }
            });
        }

        #endregion

    }
}
