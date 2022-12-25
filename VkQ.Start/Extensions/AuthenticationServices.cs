using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using VkQ.Application.Abstractions.Users.Entities;
using VkQ.Application.Abstractions.Users.ServicesInterfaces.UsersAuthentication;
using VkQ.Application.Services.Users.Authentication;
using VkQ.Infrastructure.ApplicationDataStorage;

namespace VkQ.Start.Extensions;

internal static class AuthenticationServices
{
    internal static void AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddIdentity<UserData, RoleData>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ абвгдеёжзийкламнопрстуфхцчшьыъэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЬЫЪЭЮЯ";
                options.User.RequireUniqueEmail = true;
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.Sid;
            })
            .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

        services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

        services.AddAuthentication()
            .AddVkontakte(options =>
            {
                options.ClientId = "7482215";
                options.ClientSecret = "Cq44grlZooUDOvLsJJEh";
                options.Scope.Add("email");
            }).AddYandex(options =>
            {
                options.ClientId = "862fc97020224f29829e9bb333e85091";
                options.ClientSecret = "ccd13995b6f8410e965f357b029b36c5";
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Identity.Application", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddAuthenticationSchemes(IdentityConstants.ApplicationScheme);
                policy.RequireClaim(ClaimTypes.Name);
            });
            options.AddPolicy("Admin", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddAuthenticationSchemes(IdentityConstants.ApplicationScheme);
                policy.RequireClaim(ClaimTypes.Name);
                policy.RequireRole("Admin");
            });
        });
    }
}