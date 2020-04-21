using Dna;
using Dna.AspNet;
using FluentValidation.AspNetCore;
using LoremIpsum.Core;
using LoremIpsum.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// The startup class that handles constructing the ASP.Net server services
    /// </summary>
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Add proper cookie request to follow GDPR
            services.Configure<CookiePolicyOptions>(options =>
            {
                //This lambda determines whether user consent for 
                //non -essential cookies is needed for a given request
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add SendGrid email sender
            services.AddSendGridEmailSender();

            // Add general email template sender
            services.AddEmailTemplateSender();

            // Add Task Manager service
            services.AddTransient<ITaskManager, BaseTaskManager>();

            //Add localization manager
            services.AddTransient<ILocalizationManager, LocalizationManager>();

            // Add ApplicationDbContext to DI
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Framework.Construction.Configuration.GetConnectionString("SQLServerConnection")));


            // AddIdentity adds cookie based authentication
            // Adds scoped classes for things like UserManager, SignInManager, PasswordHashers etc..
            // NOTE: Automatically adds the validated user from a cookie to the HttpContext.User
            // https://github.com/aspnet/Identity/blob/85f8a49aef68bf9763cd9854ce1dd4a26a7c5d3c/src/Identity/IdentityServiceCollectionExtensions.cs
            services.AddIdentity<ApplicationUser, IdentityRole>()

                // Adds UserStore and RoleStore from this context
                // That are consumed by the UserManager and RoleManager
                // https://github.com/aspnet/Identity/blob/dev/src/EF/IdentityEntityFrameworkBuilderExtensions.cs
                .AddEntityFrameworkStores<ApplicationDbContext>()

                // Adds a provider that generates unique keys and hashes for things like
                // forgot password links, phone number verification codes etc...
                .AddDefaultTokenProviders();

            // Add JWT Authentication for Api clients
            services.AddAuthentication()
                .AddJwtBearer(options =>                     
                // Set validation parameters
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Validate issuer
                        ValidateIssuer = true,
                        // Validate audience
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        // Validate signature
                        ValidateIssuerSigningKey = true,
                        // Set issuer
                        ValidIssuer = Framework.Construction.Configuration["Jwt:Issuer"],
                        // Set audience
                        ValidAudience = Framework.Construction.Configuration["Jwt:Audience"],
                        // Set signing key
                        IssuerSigningKey = new SymmetricSecurityKey(
                            // Get our secret key from configuration
                            Encoding.UTF8.GetBytes(Framework.Construction.Configuration["Jwt:SecretKey"])),
                    });

            // Change password policy
            services.Configure<IdentityOptions>(options =>
            {
                //Password Settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                //User Settings
                options.User.RequireUniqueEmail = true;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

            });

            //Enable CORS
            services.AddCors(options => options.AddPolicy("AllowAllOrigins",
                    builder => builder
                            .AllowAnyMethod()
                            //.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowCredentials()));

            // Use MVC style website
            services.AddMvc(options =>
                //options.InputFormatters.Add(new XmlSerializerInputFormatter());
                //options.OutputFormatters.Add(new DefaultContractResolver());
                options.EnableEndpointRouting = false)

            //Add Fluent validation all validators
            .AddFluentValidation((x) => x.RegisterValidatorsFromAssemblyContaining<Startup>())

           // State we are a minimum compatibility of 3.0 onwards
           .SetCompatibilityVersion(CompatibilityVersion.Latest);


            // Register the Swagger generator, defining one or more Swagger documents  
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Lorem Ipsum Web Server API",
                    Version = "v1",
                    Description = "Lorem Ipsum Web Server API",
                    Contact = new OpenApiContact { Name = "Nelson Villacruz", Email = "villacruznelson@gmail.com" }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Please insert JWT with Bearer in this field",
                    Type = SecuritySchemeType.ApiKey,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme  {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] { }
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var pathFile = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(pathFile);
            });

            //Register Controllers
            services.AddControllers()
               //Use temporarily the original Newtonsoft.Json
               //TODO: Shift to System.Text.Json
               .AddNewtonsoftJson(options =>
                    // Use the default property (Pascal) casing
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            //Register the Views
            services.AddControllersWithViews();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Store instance of the DI service provider so our application can access it anywhere
            app.UseDnaFramework();

            // Setup Identity
            app.UseAuthentication();

            // Redirect all calls from HTTP to HTTPS
            //app.UseHttpsRedirection();

            // Force non-essential cookies to only store if the user has consented
            app.UseCookiePolicy();

            // Serve static files
            app.UseStaticFiles();

            //Use CORS set up above
            app.UseCors("AllowAllOrigins");

            // If in development...
            if (env.IsDevelopment())
            {
                // Show any exceptions in browser when they crash
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();

            }
            // Otherwise...
            else
            // Just show generic error page
            {

                app.UseExceptionHandler("/Home/Error");

                // In production, tell the browsers (via the HSTS header)
                // to only try and access our site via HTTPS, not HTTP
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "My API V1"));

            // Setup MVC routes
            app.UseMvc(routes =>
                // Default route of /controller/action/info
                routes.MapRoute(
                    "default",
                    "{controller=Seed}/{action=SeedAsync}/{moreInfo?}"));

        }

    }
}
