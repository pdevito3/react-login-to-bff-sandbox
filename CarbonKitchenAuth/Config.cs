// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace CarbonKitchenAuth
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("recipemanagement", "Recipe Management")
                {
                    Scopes = {"recipes.read", "recipes.add", "recipes.delete", "recipes.update"},
                    ApiSecrets = { new Secret("secret".Sha256()) },
                },
            };
        
        // allow access to identity information. client level rules of who can access what (e.g. read:sample, read:order, create:order, read:report)
        // this will be in the audience claim and will be checked by the jwt middleware to grant access or not
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("recipes.read", "CanReadRecipes"),
                new ApiScope("recipes.add", "CanAddRecipes"),
                new ApiScope("recipes.update", "CanUpdateRecipes"),
                new ApiScope("recipes.delete", "CanDeleteRecipes"),
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
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "scope1" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientName = "Interactive Client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {"https://localhost:5375/swagger/oauth2-redirect.html"},
                    PostLogoutRedirectUris = { "http://localhost:5375/" },
                    FrontChannelLogoutUri =    "http://localhost:5375/signout-oidc",
                    AllowedCorsOrigins = {"https://localhost:5375"},
                    
                    AllowOfflineAccess = true,
                    RequirePkce = true,
                    RequireClientSecret = true,
                    AllowedScopes = { "openid", "profile",
                        "recipes.read",
                        "recipes.add",
                        "recipes.update",
                        "recipes.delete" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive.bff",
                    ClientName = "Interactive BFF",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:4301/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:4301/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:4301/signout-callback-oidc" },
                    AllowedCorsOrigins = {"https://localhost:5375", "https://localhost:4301"},
                    
                    AllowOfflineAccess = true,
                    RequirePkce = true,
                    RequireClientSecret = true,
                    AllowedScopes = { "openid", "profile",
                        "recipes.read",
                        "recipes.add",
                        "recipes.update",
                        "recipes.delete" }
                },
            };
    }
}