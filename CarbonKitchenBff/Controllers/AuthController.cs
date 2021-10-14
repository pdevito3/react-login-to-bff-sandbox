namespace CarbonKitchenBff.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Dtos;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;

    public class AuthController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public ActionResult Login(string returnUrl = "/")
        {
            return new ChallengeResult("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [Authorize]
        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            // signout from client
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            // signout from server
            await _httpContextAccessor.HttpContext.SignOutAsync("Auth0");
            
            return Redirect(returnUrl); // will be portal homepage
        }

        [Authorize]
        public ActionResult GetUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var claims = ((ClaimsIdentity)this.User.Identity).Claims.Select(c =>
                        new { type = c.Type, value = c.Value })
                    .ToList();
                
                // enrich claims with hospitals and user profile 

                var metadata = new List<Metadata>();
                // http call to get their hospitals, if this one they are logging in from isn't in their list, add it
                metadata.Add(new Metadata { Type = "hospitals", Value = "GC|OSU" });
                //metadata.add name, email, phone, etc. from new user boundary not tied to web oncolens?

                return Json(new { claims = claims, metadata = metadata });
            }

            return Unauthorized();
        }
        
        public IActionResult Callback()
        {
            var test = _httpContextAccessor.HttpContext;

            // register
            test.Request.Query.TryGetValue("onco_hospital_registration", out var hospitalToRegisterWith);
            if(!string.IsNullOrEmpty(hospitalToRegisterWith))
                return Redirect("/requests"); // will be portal homepage
            
            test.Request.Query.TryGetValue("hospital", out var hospital);
            if(!string.IsNullOrEmpty(hospital))
                return Redirect("/requests"); // will be portal homepage
            
            return Redirect("/requests"); // will be portal homepage
        }
    }
}