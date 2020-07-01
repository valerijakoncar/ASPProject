using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPProjekat.ApiApp.Core;
using ASPProjekat.Application;
using ASPProjekat.Application.Commands;
using ASPProjekat.Application.Email;
using ASPProjekat.Application.Queries;
using ASPProjekat.EFDataAccess;
using ASPProjekat.Implementation.Commands;
using ASPProjekat.Implementation.Email;
using ASPProjekat.Implementation.Logging;
using ASPProjekat.Implementation.Queries;
using ASPProjekat.Implementation.Validators;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace ASPProjekat.ApiApp
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
            services.AddTransient<IAuditLogs, EFAuditLogs>();
            services.AddTransient<IChangeOrderStatus, EFChangeOrderStatusCommand>();
            services.AddTransient<IGetOrder, EFGetOrder>();
            services.AddTransient<ICreateOrderCommand, EFCreateOrderCommand>();
            services.AddTransient<IGetUserCart, EFGetUserCartQuery>();
            services.AddTransient<IUpdateQuantityCart, EFUpdateQuantityCartCommand>();
            services.AddTransient<IInsertIntoCart, EFInsertIntoCartCommand>();
            services.AddTransient<IUpdateArticleCommand, EFUpdateArticleCommand>();
            services.AddTransient<IDeleteArticleCommand, EFDeleteArticleCommand>();
            services.AddTransient<ICreateArticleCommand, EFCreateArticleCommand>();
            services.AddTransient<ICreateCategoryCommand, EFCreateCategoryCommand>();
            services.AddTransient<IUpdateCategoryCommand, EFUpdateCategoryCommand>();
            services.AddTransient<IDeleteCategory, EFDeleteCategoryCommand>();           
            services.AddTransient<IDeleteProductFromCart, EFDeleteProductFromCartCommand>();
            services.AddTransient<IGetOneArticleQuery, EFGetOneArticleQuery>();
            services.AddTransient<IGetArticlesQuery, EFGetArticlesQuery>();
            services.AddTransient<IGetCategoriesQuery, EFGetCategoriesQuery>();
            services.AddTransient<IEmailSender, SmtpEmailSender>();
            services.AddTransient<IUseCaseLogger, DatabaseUseCaseLogger>();
            services.AddTransient<IRegisterUserCommand, EFRegisterUserCommand>();
            services.AddTransient<UseCaseExecutor>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddTransient<CreateOrderValidator>();
            services.AddTransient<UpdateQuantityCartValidator>();
            services.AddTransient<InsertIntoCartValidator>();
            services.AddTransient<UpdateCategoryValidator>();
            services.AddTransient<UpdateArticleValidator>();
            services.AddTransient<CreateArticleValidator>();
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<ASPProjekatContext>();
            services.AddTransient<JwtManager>();

            services.AddHttpContextAccessor();
            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                //izvuci token
                //pozicionirati se na payload
                //izvuci ActorData claim
                //Deserijalizovati actorData string u c# objekat

                var user = accessor.HttpContext.User;

                if (user.FindFirst("ActorData") == null)
                {
                    return new AnonymousActor();
                }

                var actorString = user.FindFirst("ActorData").Value;

                var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);

                return actor;

            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "asp_api",
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMyVerySecretKey")),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotStuff Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                    });
            });
            services.AddControllers();         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors(x =>
            //{
            //    x.AllowAnyOrigin();
            //    x.AllowAnyMethod();
            //    x.AllowAnyHeader();
            //});

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
            });

            app.UseRouting();

            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
