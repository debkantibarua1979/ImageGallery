﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace ImageGallery.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", 
                "Your role(s)", 
                new[] { "role" }),
            new IdentityResource("country", 
                "The country you are living in",
                new List<string>() { "country" }),
        };

    public static IEnumerable<ApiResource> Apis =>
        new ApiResource[]
        {
            new ApiResource("imagegalleryapi", "Image Gallery Api", new []{ "role", "country" })
            {
                Scopes =
                {
                    "imagegalleryapi.fullaccess", 
                    "imagegalleryapi.read", 
                    "imagegalleryapi.write"
                },
                ApiSecrets = { new Secret("apisecret".Sha256())}
            }
        };
    

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("imagegalleryapi.fullaccess"),
            new ApiScope("imagegalleryapi.read"),
            new ApiScope("imagegalleryapi.write"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client()
            {
                ClientName = "Image Gallery",
                ClientId = "imagegalleryclient",
                AllowedGrantTypes = GrantTypes.Code,
                AccessTokenType = AccessTokenType.Reference,
                
                AllowOfflineAccess = true,
                UpdateAccessTokenClaimsOnRefresh = true,
                AccessTokenLifetime = 30,
                
                // AuthorizationCodeLifetime
                // IdentityTokenLifetime = 
                
                RedirectUris =
                {
                    "https://localhost:7184/signin-oidc",
                },
                PostLogoutRedirectUris =
                {
                    "https://localhost:7184/signout-callback-oidc",    
                },
                    
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles",
                    "imagegalleryapi.read",
                    "imagegalleryapi.write",
                    "country"
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                RequireConsent = true
            },
            new Client()
            {
                ClientName = "Image Gallery BFF",
                ClientId = "imagegallerybff",                    
                AccessTokenType = AccessTokenType.Reference,
                AllowedGrantTypes = GrantTypes.Code,
                AllowOfflineAccess = true,                    
                UpdateAccessTokenClaimsOnRefresh = true,
                RedirectUris =
                {
                    "https://localhost:7119/signin-oidc"
                },
                PostLogoutRedirectUris =
                {
                    "https://localhost:7119/signout-callback-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles", 
                    "imagegalleryapi.read",
                    "imagegalleryapi.write",
                    "country"
                },
                ClientSecrets =
                {
                    new Secret("anothersecret".Sha256())
                },
                RequireConsent = true
            }
        };
}