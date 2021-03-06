﻿namespace EspartoWorld.Data
{
    using Microsoft.AspNetCore.Identity;

    public static class IdentityOptionsProvider
    {
        public static void GetIdentityOptions(IdentityOptions options)
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;

            // This lock account after try three times to enter
            // options.Lockout.MaxFailedAccessAttempts = 3;
            // This lock account for 5 minutes
            // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        }
    }
}
