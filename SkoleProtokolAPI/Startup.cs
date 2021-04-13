using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkoleProtokolAPI.Services;
using SkoleProtokolLibrary.Interfaces;
using SkoleProtokolLibrary.DBModels;

namespace SkoleProtokolAPI
{
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

            services.AddAuthentication(options =>
                {
                    // If authentication cookie is present, use it to get authentication information
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    // If authentication is required, and no cookie is present, use okta
                    options.DefaultChallengeScheme = "okta";
                })
                .AddCookie() // Cookie authentication middleware first
                .AddOAuth("okta", options =>
                {
                    // Oauth authentication middleware is second

                    var oktaDomain = Configuration.GetValue<string>("Okta:OktaDomain");

                    // When a user needs to sign in, they will be redirected to the authorize endpoint
                    options.AuthorizationEndpoint = $"{oktaDomain}/oauth2/default/v1/authorize";

                    // Okta's auth server is openId complient, so request the standard openId
                    // Scopes when redirecting to the authorization endpoint
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");

                    // After the user signs in, an authorization code will be sent to a callback
                    // In this app. The OAuth middleware will intercept it
                    options.CallbackPath = new PathString("/authorization-code/callback");

                    // The OAuth middleware will send the clientId, clientSecret, and the 
                    // Authorization code to the token endpoint, and get an access token in return
                    options.ClientId = Configuration.GetValue<string>("Okta:ClientId");
                    options.ClientSecret = Configuration.GetValue<string>("Okta:ClientSecret");
                    options.TokenEndpoint = $"{oktaDomain}/oauth2/default/v1/token";

                    // Below we call the userinfo endpoint to get information about the user
                    options.UserInformationEndpoint = $"{oktaDomain}/oauth2/default/v1/userinfo";

                    // Describe how to map the userinfo we receive to user claims
                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "given_name");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");

                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = async context =>
                        {
                            // Get user info from the UserInfo endpoint and use it to populate user claims
                            var request =
                                new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            request.Headers.Authorization =
                                new AuthenticationHeaderValue("Bearer", context.AccessToken);

                            var response = await context.Backchannel.SendAsync(request,
                                HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                            response.EnsureSuccessStatusCode();

                            var user = JObject.Parse(await response.Content.ReadAsStringAsync());
                            // Needs to be double checked
                            context.RunClaimActions(user.ToObject<JsonElement>());
                        }
                    };
                });

            // Requires using Microsoft.Extensions.Options

            // Populates the RollCallDatabaseSettings object with data
            // from the RollCallDatabaseSettings section in the appsettings.json file.
            services.Configure<RollCallDatabaseSettings>(
                Configuration.GetSection(nameof(RollCallDatabaseSettings)));

            // The IRollCallDatabaseSettings interface is setup for dependency injection as a singleton
            // and when the IRollCallDatabaseSettings type is injected it will resolve to a RollCallDatabaseSettings object
            services.AddSingleton<IRollCallDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<RollCallDatabaseSettings>>().Value);

            // RollCallUsersService is setup for dependency injection as a singleton.
            services.AddSingleton<RollCallUsersService>();
            // RollCallClassesService is setup for dependency injection as a singleton.
            services.AddSingleton<RollCallClassesService>();

            services.AddControllers();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
