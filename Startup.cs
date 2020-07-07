using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductApi.Models;
using ProductApi.Models.Auth;
using learn_netcore.Settings;
using learn_netcore.Extensions;
using AutoMapper;
using ProductApi.Mapping;

namespace learn_netcore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string DevelopmentOrigins = "_DevelopmentOrigins";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<ProductContext>(opt =>
               //opt.UseInMemoryDatabase("ProductList")
               opt.UseSqlServer(Configuration.GetConnectionString("ShopContext"))
               );

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<ProductContext>()
            .AddDefaultTokenProviders();
            
            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            services.AddAuth(jwtSettings, Configuration);
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: DevelopmentOrigins,
                                //   options => options.AllowAnyOrigin());
                                builder =>
                                {
                                    builder.WithOrigins("http://localhost:5002") // , "http://www.contoso.com"
                                                        .AllowAnyHeader()
                                                        .AllowAnyMethod()
                                                        ;
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(DevelopmentOrigins);

            app.UseAuth();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    
    }
}
