using API_Coloramm.Controllers.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
string identityAuthorityUrl = $"https://{builder.Configuration["Auth0:Domain"]}";

// Dependency Injection
builder.Services.AddTransient<IColorammService, ColorammService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Config

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Configuration.AddJsonFile("json.txt");

// Disabilitazione gestione automatica [API CONTROLLER] per le DATA ANNOTATION
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Swagger config

#region Auth & identityServer config

// Autenticazione e Autorizzazione IdentityServer
//builder.Services.AddIdentityServer()
//    .AddDeveloperSigningCredential()
//    .AddInMemoryPersistedGrants();
//builder.Services.AddAuthentication()
//.AddIdentityServerAuthentication(options =>
//{
//    options.RequireHttpsMetadata = false;
//    // base-address of your identityserver
//    options.Authority = identityAuthorityUrl;
//    // name of the API resource
//    options.ApiName = "Coloramm_Api";
//})
//.AddCookie("cookie")
//.AddOpenIdConnect("oidc", options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.Authority = identityAuthorityUrl;
//    options.ClientId = "oauthClient";
//    options.ClientSecret = "SuperSecretPassword";
//    options.ResponseType = "code";
//    options.UsePkce = true;
//    options.ResponseMode = "query";
//    // options.CallbackPath = "/signin-oidc"; // default redirect URI
//    // options.Scope.Add("oidc"); // default scope
//    // options.Scope.Add("profile"); // default scope
//    options.Scope.Add("coloramm_api.read");
//    options.SaveTokens = true;
//});

//.AddJwtBearer(
//JwtBearerDefaults.AuthenticationScheme, options =>
//{
//    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
//    options.RequireHttpsMetadata = false;

//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidAudience = builder.Configuration["Auth0:Audience"],
//        ValidIssuer = $"{builder.Configuration["Auth0:Domain"]}"
//    };
//});

//builder.Services.AddAuthorization(options =>
//{
//    options.DefaultPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});

#endregion

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",new OpenApiInfo
    {
        Title = "Coloramm.Api",
        Version = "v1",
        Description = "Coloramm Api"
    });
    // Permette l'aggiunta di commenti tramite il file di property .xml
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.CustomSchemaIds(x => x.FullName);
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// WebApplication config
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// connString legata all' appsettings.json
//app.Configuration.GetConnectionString("Default");

//app.UseHttpsRedirection();
app.UseRouting();
//app.UseAuthorization();
//app.UseAuthentication();
//app.UseIdentityServer();
app.MapControllers();
app.Run();
