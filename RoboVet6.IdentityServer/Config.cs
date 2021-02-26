// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace RoboVet6.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                //new ApiScope("scope1"),
                //new ApiScope("scope2"),
                new ApiScope("test")
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("test", "The Weather API")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "Test",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "https://localhost:44384" },
                    AllowedScopes = { "openid", "profile", "email", "test" },
                    RedirectUris = { "https://localhost:44384/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:44384" },
                    Enabled = true
                },
            };
    }
}