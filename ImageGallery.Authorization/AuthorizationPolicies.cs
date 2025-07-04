﻿using Microsoft.AspNetCore.Authorization;

namespace ImageGallery.IDP.Authorization;

public static class AuthorizationPolicies
{
    public static AuthorizationPolicy CanAddImage()
    {
        return new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim("country", "be")
            .RequireRole("PayingUser")
            .Build();
    }
}