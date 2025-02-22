﻿using Common;
using Database.Contexts;
using Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace WebFramework.Configuration
{
    public static class IdentityConfigurationExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings settings)
        {

            services.AddIdentity<User, Role>(identityOptions =>
                {
                    //Password Settings
                    identityOptions.Password.RequireDigit = settings.PasswordRequireDigit;
                    identityOptions.Password.RequiredLength = settings.PasswordRequiredLength;
                    identityOptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumic; //#@!
                    identityOptions.Password.RequireUppercase = settings.PasswordRequireUppercase;
                    identityOptions.Password.RequireLowercase = settings.PasswordRequireLowercase;

                    //UserName Settings
                    identityOptions.User.RequireUniqueEmail = settings.RequireUniqueEmail;

                    identityOptions.Tokens.PasswordResetTokenProvider = ResetPasswordTokenProvider.ProviderKey;

                    //Singin Settings
                    //identityOptions.SignIn.RequireConfirmedEmail = false;
                    //identityOptions.SignIn.RequireConfirmedPhoneNumber = false;

                    //Lockout Settings
                    identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                    identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                    identityOptions.Lockout.AllowedForNewUsers = true;

                })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<PersianIdentityErrorDescriber>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<ResetPasswordTokenProvider>(ResetPasswordTokenProvider.ProviderKey);



            services.AddAuthorization(options =>
            {
                // options.AddPolicy(Policies.ProvinceUser,
                //     policy => policy.RequireClaim("userType",
                //         Convert.ToString((int)UserType.Province)));
                
            });

        }
    }

    public class ResetPasswordTokenProvider : TotpSecurityStampBasedTokenProvider<User>
    {
        public const string ProviderKey = "ResetPassword";

        public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<User> manager, User user)
        {
            return Task.FromResult(false);
        }
    }


}
