using Microsoft.Extensions.DependencyInjection;
using learn_netcore.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace learn_netcore.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, JwtSettings jwtSettings, IConfiguration Configuration)
        {
            services
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                })
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.SignInScheme = "Identity.External";
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                })   
                // .AddGoogle(options =>
                // {
                //     IConfigurationSection googleAuthNSection =
                //         Configuration.GetSection("Authentication:Google"); //configure in appsettings.json ex. jwt

                //     options.ClientId = googleAuthNSection["ClientId"];
                //     options.ClientSecret = googleAuthNSection["ClientSecret"];
                // })
                ;

            return services;
        }

        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }
    }
}
