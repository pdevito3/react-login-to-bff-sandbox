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
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;

    public class AuthController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public ActionResult Login(string returnUrl = "/auth/unique")
        {
            return new ChallengeResult("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // signout from client
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            // signout from server
            await _httpContextAccessor.HttpContext.SignOutAsync("Auth0");
            
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("access_token");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("refresh_token");
            
            return Redirect("/gc"); // will be portal homepage
        }

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

        private class Metadata
        {
            public string Type { get; set; }
            public string Value { get; set; }
        }

        // [HttpGet]
        [Authorize]
        public IActionResult Callback()
        {
            var test = _httpContextAccessor.HttpContext;
            // HttpContext.Response.Cookies.Append("__test", 
            //     "some hospital",
            //     new CookieOptions
            //     {
            //         Secure = true,
            //         SameSite = SameSiteMode.Strict,
            //         HttpOnly = true
            //     }
            // );
            return Redirect("/gc"); // will be portal homepage
        }
        
        public ActionResult Unique()
        {
            var test = _httpContextAccessor.HttpContext;

            // register
            test.Request.Query.TryGetValue("onco_hospital_registration", out var hospitalToRegisterWith);
            if(!string.IsNullOrEmpty(hospitalToRegisterWith))
                return Redirect("/gc"); // will be portal homepage
            
            test.Request.Query.TryGetValue("hospital", out var hospital);
            if(!string.IsNullOrEmpty(hospital))
                return Redirect("/gc"); // will be portal homepage
            
            return Redirect("/gc"); // will be portal homepage
            return Ok("hello world");
        }
        
    }
}