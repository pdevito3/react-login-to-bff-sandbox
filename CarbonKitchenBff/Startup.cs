namespace CarbonKitchenBff
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.IdentityModel.Protocols.OpenIdConnect;

    public class Startup
  {
      public Startup(IConfiguration configuration)
      {
          Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
          // services.AddCors(options =>
          // {
          //     options.AddPolicy("CorsPolicy",
          //         builder => builder.AllowAnyOrigin()
          //             .AllowAnyMethod()
          //             .AllowAnyHeader()
          //             .WithExposedHeaders("X-Pagination"));
          // });
          
          services.AddHttpContextAccessor();

          services.AddControllersWithViews();

          var domain = "dev-ziza5op9.us.auth0.com";
          var clientid = "x9PBBz6mRYPUK7nATbMe7AcBQaxUxqRF";
          var secret = "UWaajuXkrmUH1ab6udJdIhy3PHk135zKTYuhXadN3M7UtPN1tHysYYYRfKwWhRZs";
          var audience = "auth0.first.api";
          
          
          services.Configure<CookiePolicyOptions>(options => { /*options.MinimumSameSitePolicy = SameSiteMode.None;*/ });
          services.AddAuthentication(options =>
          {
              options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
          })
          .AddCookie(options =>
          {
              options.Cookie.Name = "__OncolensPortal-Bff";
              options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
              options.Cookie.SameSite = SameSiteMode.Strict;
              options.Cookie.HttpOnly = true;
          })
          .AddOpenIdConnect("Auth0", options =>
          {
              // Set the authority to your Auth0 domain
              options.Authority = $"https://{domain}";

              // Configure the Auth0 Client ID and Client Secret
              options.ClientId = clientid;
              options.ClientSecret = secret;

              options.ResponseType = "code";
              options.ResponseMode = "query";

              options.UsePkce = true;

              // Configure the scope
              options.Scope.Clear();
              options.Scope.Add("openid");
              options.Scope.Add("profile");
              options.Scope.Add("email");
              options.Scope.Add("portal.requester");
              
              // Set the callback path, so Auth0 will call back to http://localhost:3000/callback
              // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
              options.CallbackPath = new PathString("/callback");

              // Configure the Claims Issuer to be Auth0
              options.ClaimsIssuer = "Auth0";

              // This saves the tokens in the session cookie
              options.SaveTokens = true;
              
              options.Events = new OpenIdConnectEvents
              {
                  // handle the logout redirection
                  OnRedirectToIdentityProviderForSignOut = (context) =>
                  {
                      var logoutUri = $"https://{domain}/v2/logout?client_id={clientid}";

                      var postLogoutUri = context.Properties.RedirectUri;
                      if (!string.IsNullOrEmpty(postLogoutUri))
                      {
                          if (postLogoutUri.StartsWith("/"))
                          {
                              // transform to absolute
                              var request = context.Request;
                              postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                          }
                          logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
                      }
                      context.Response.Redirect(logoutUri);
                      context.HandleResponse();

                      return Task.CompletedTask;
                  },
                  // OnRedirectToIdentityProvider = context => {
                  //     context.ProtocolMessage.SetParameter("audience", audience);
                  //     return Task.CompletedTask;
                  // }
              };
          });
          
          services.AddSpaStaticFiles(configuration =>
          {
              configuration.RootPath = "ClientApp/build";
          });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
          if (env.IsDevelopment())
          {
              app.UseDeveloperExceptionPage();
          }
          else
          {
              app.UseExceptionHandler("/Error");
              // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
              app.UseHsts();
          }

          app.UseHttpsRedirection();
          app.UseStaticFiles();
          app.UseSpaStaticFiles();

          app.UseRouting();

          app.UseAuthentication();
          app.UseAuthorization();

          app.UseEndpoints(endpoints =>
          {
              endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller}/{action=Index}/{id?}");
          });

          app.UseSpa(spa =>
          {
              spa.Options.SourcePath = "ClientApp";

              if (env.IsDevelopment())
              {
                  spa.UseReactDevelopmentServer(npmScript: "start");
              }
          });
      }
  }
}
