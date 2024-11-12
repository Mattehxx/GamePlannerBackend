using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Auth;
using GamePlanner.DTO.ConfigurationDTO;
using GamePlanner.Helpers;
using GamePlanner.Services;
using GamePlanner.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Custom services
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IBlobService, BlobService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<GamePlannerDbContext>(options =>
    options.UseSqlServer(KeyVaultHelper.GetSecrectConnectionString("DbConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<GamePlannerDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var JWTSettings = KeyVaultHelper.GetSecret<JWTSettingsDTO>("JWTSettings");
    if (JWTSettings is null) throw new ArgumentNullException(nameof(JWTSettings));

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = JWTSettings.ValidIssuer,
        ValidAudience = JWTSettings.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Secret)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "GamePlannner", Version = "v1" });
    //option.EnableAnnotations();
    option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        Name = "Authorization",
    }
    );
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },
                new string[] { }
            }
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();