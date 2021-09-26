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
          
          services.AddControllersWithViews();

          services.AddBff();

          services.AddAuthentication(options =>
          {
              options.DefaultScheme = "cookie";
              options.DefaultChallengeScheme = "oidc";
              options.DefaultSignOutScheme = "oidc";
          })
          .AddCookie("cookie", options =>
          {
              options.Cookie.Name = "__Host-bff";
              options.Cookie.SameSite = SameSiteMode.Strict;
          })
          .AddOpenIdConnect("oidc", options =>
          {
              options.Authority = "https://localhost:5010";
              options.ClientId = "interactive.bff";
              options.ClientSecret = "secret";
              options.ResponseType = "code";
              options.ResponseMode = "query";

              options.GetClaimsFromUserInfoEndpoint = true;
              options.MapInboundClaims = false;
              options.SaveTokens = true;

              options.Scope.Clear();
              options.Scope.Add("openid");
              options.Scope.Add("profile");
              options.Scope.Add("recipes.read");
              // options.Scope.Add("offline_access");

              options.TokenValidationParameters = new()
              {
                  NameClaimType = "name",
                  RoleClaimType = "role"
              };
          });
          // .AddOpenIdConnect("oidc", options =>
          // {
          //     options.Authority = "https://demo.duendesoftware.com";
          //     options.ClientId = "interactive.confidential";
          //     options.ClientSecret = "secret";
          //     options.ResponseType = "code";
          //     options.ResponseMode = "query";
          //
          //     options.GetClaimsFromUserInfoEndpoint = true;
          //     options.MapInboundClaims = false;
          //     options.SaveTokens = true;
          //
          //     options.Scope.Clear();
          //     options.Scope.Add("openid");
          //     options.Scope.Add("profile");
          //     options.Scope.Add("api");
          //     options.Scope.Add("offline_access");
          //
          //     options.TokenValidationParameters = new()
          //     {
          //         NameClaimType = "name",
          //         RoleClaimType = "role"
          //     };
          // });

          // In production, the React files will be served from this directory
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
          app.UseBff();
          app.UseAuthorization();

          app.UseEndpoints(endpoints =>
          {
              endpoints.MapBffManagementEndpoints();
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
