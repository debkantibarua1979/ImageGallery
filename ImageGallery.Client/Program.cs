using ImageGallery.IDP.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()  
    .AddJsonOptions(configure => 
        configure.JsonSerializerOptions.PropertyNamingPolicy = null);

JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAccessTokenManagement();

// create an HttpClient used for accessing the API
builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ImageGalleryAPIRoot"]);
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddUserAccessTokenHandler();

builder.Services.AddHttpClient("IDPClient",
    client =>
    {
        client.BaseAddress = new Uri("https://localhost:5001");
    });


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.AccessDeniedPath = "/Authentication/AccessDenied";
})
    
  .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
  { 
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Authority = "https://localhost:5001"; // IDP URL
        options.ClientId = "imagegalleryclient";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
        
        options.ClaimActions.Remove("aud");
        options.ClaimActions.DeleteClaim("sid");
        options.ClaimActions.DeleteClaim("idp");
        
        options.Scope.Add("roles"); 
        // options.Scope.Add("imagegalleryapi.fullaccess");
        options.Scope.Add("imagegalleryapi.read");
        options.Scope.Add("imagegalleryapi.write");
        options.Scope.Add("country");
        options.Scope.Add("offline_access");
        
        options.ClaimActions.MapJsonKey("role", "role");
        options.ClaimActions.MapUniqueJsonKey("country", "country");

        options.TokenValidationParameters = new()
        {
            NameClaimType = "name",
            RoleClaimType = "role"
        };
  });

builder.Services.AddAuthorization(authorizationOptions =>
    authorizationOptions.AddPolicy("UserCanAddImage", AuthorizationPolicies.CanAddImage()
    )); 
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Gallery}/{action=Index}/{id?}");

app.Run();
