// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Shisha {
    public static class Config {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1", "E-Online Client API"),
                // new ApiScope("scope2"),
                // new ApiScope("scope3"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256())},

                    AllowedScopes = {"scope1"}
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "028BED7E-F9CA-4484-8ED7-7DE3A82F40BC",
                    ClientName = "Entities online",
                    ClientSecrets = {new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256())},

                    AllowedGrantTypes = GrantTypes.Code,

                    AlwaysIncludeUserClaimsInIdToken = true,

                    RedirectUris = {"https://localhost:44381/signin-oidc"},
                    FrontChannelLogoutUri = "https://localhost:44381/signout-oidc",
                    PostLogoutRedirectUris = {"https://localhost:44381/signout-callback-oidc"},


                    AllowOfflineAccess = true,
                    AllowedScopes = {"openid", "profile", "scope1", "scope3", "examiner"}
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "f4cc9d9a-e9fd-4c16-ab32-50c19e29492c",
                    ClientName = "Entities online payments",
                    ClientSecrets = {new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256())},

                    AllowedGrantTypes = GrantTypes.Code,

                    AlwaysIncludeUserClaimsInIdToken = true,

                    RedirectUris = {"https://localhost:44375/signin-oidc"},
                    FrontChannelLogoutUri = "https://localhost:44375/signout-oidc",
                    PostLogoutRedirectUris = {"https://localhost:44375/signout-callback-oidc"},


                    AllowOfflineAccess = true,
                    AllowedScopes = {"openid", "profile", "scope1", "scope3", "examiner"}
                },
            };
    }
}