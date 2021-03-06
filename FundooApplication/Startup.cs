using BusinessLayer.Interface;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooApplication
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
            services.AddDbContext<FundooContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:FundooDB"]));
            services.AddControllers();
            services.AddTransient<IUserBL, UserBL>();
            services.AddTransient<IUserRL, UserRL>();
            services.AddMvc();

            services.AddTransient<INotesBL,NotesBL>();
            services.AddTransient<INotesRL,NotesRL>();

            services.AddTransient<ICollabBL,CollabBL>();
            services.AddTransient<ICollabRL,CollabRL>();

            services.AddTransient<ILabelBL,LabelBL>();
            services.AddTransient<ILabelRL,LabelRL>();

           // services.AddSingleton<ILoggerManager, LoggerManager>();
            // Reset Token Valid for 2 hours 
            //services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(2));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Welcome to FundooNotes" });

                var securitySchema = new OpenApiSecurityScheme

                {

                    Description = "Using the Authorization header with the Bearer scheme.",

                    Name = "Authorization",

                    In = ParameterLocation.Header,

                    Type = SecuritySchemeType.Http,

                    Scheme = "bearer",

                    Reference = new OpenApiReference

                    {

                        Type = ReferenceType.SecurityScheme,

                        Id = "Bearer"

                    }

                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement

                    {

                    { securitySchema, new[] { "Bearer" } }

                });
            });

            //var jwtSection = Configuration.GetSection("Jwt:Key");
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,

                    ValidateAudience = false,

                    ValidateLifetime = false,

                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))

                };

            });
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });
        }
            //    services.AddSwaggerGen(swagger =>
            //    {
            //            //This is to generate the Default UI of Swagger Documentation  
            //            swagger.SwaggerDoc("v1", new OpenApiInfo
            //        {
            //            Version = "v1",
            //            Title = "Fundoo Application",
            //            Description = "ASP.NET Core 3.1 Web API"
            //        });
            //            // To Enable authorization using Swagger (JWT)  
            //            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            //        {
            //            Name = "Authorization",
            //            Type = SecuritySchemeType.ApiKey,
            //            Scheme = "Bearer",
            //            BearerFormat = "JWT",
            //            In = ParameterLocation.Header,
            //            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            //        });
            //        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            //        {
            //                {
            //                      new OpenApiSecurityScheme
            //                        {
            //                            Reference = new OpenApiReference
            //                            {
            //                                Type = ReferenceType.SecurityScheme,
            //                                Id = "Bearer"
            //                            }
            //                        },

            //                        new string[] {}

            //                }
            //        });
            //    });

            //    services.AddAuthentication(option =>
            //    {
            //        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //    }).AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = false,
            //            ValidateAudience = false,
            //            ValidateLifetime = false,
            //            ValidateIssuerSigningKey = true,
            //           // ValidIssuer = Configuration["Jwt:Issuer"],
            //            //ValidAudience = Configuration["Jwt:Issuer"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]  
            //            };
            //    });
            //}

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {

         //   LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            

            if (env.IsDevelopment())
               {
                 app.UseDeveloperExceptionPage();
                
               }

                 app.UseHttpsRedirection();

                 app.UseRouting();
                 app.UseAuthentication();

                 app.UseAuthorization();
        
                 app.UseEndpoints(endpoints =>
                 {
                   endpoints.MapControllers();
                 });
            

                 app.UseSwagger();
                 app.UseSwaggerUI(c =>
                 {
                    c.SwaggerEndpoint("/Swagger/v1/swagger.json", "My API v1");
                 });
            }
    }
}
