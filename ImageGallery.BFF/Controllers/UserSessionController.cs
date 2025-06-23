using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.IDP.BFF.Controllers;

public class UserSessionController: Controller
{
    [Authorize]
    public IActionResult GivenName()
    {
        var objectToReturn = new
        {
            given_name = User.Claims.FirstOrDefault(c => c.Type == "given_name").Value
        };
        
        return Json(objectToReturn);
    }
    
    public async Task Logout()
    {
        await HttpContext.SignOutAsync("BFFCookieScheme");
        await HttpContext.SignOutAsync("BFFChallengeScheme");
    }
}