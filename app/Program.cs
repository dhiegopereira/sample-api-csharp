using Microsoft.OpenApi.Models;

using Microsoft.EntityFrameworkCore;

using sample_api_csharp.Data;
using sample_api_csharp.Abstracts;
using sample_api_csharp.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigureCors(builder);

ConfigureDB(builder);

ConfigureJWT(builder);

ConfigureSwagger(builder);

ConfigureScoped(builder);

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello, world!");

app.Urls.Add("http://*:8080");

var db = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
db.Database.Migrate();

app.UseCors("AllowAllOrigins");

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Guia Local API v1");
});

app.UseRouting();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseAuthorization(); 


app.UseEndpoints(endpoints => { _ = endpoints.MapControllers(); });

app.Run();

void ConfigureDB(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}

void ConfigureJWT(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, 
            ValidateIssuer = false, 
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true, 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });
}

void ConfigureSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen(options =>
    {

        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Dev College Sample API",
            Version = "v1",
            Description = "A simple API for managing CRUD",
            Contact = new OpenApiContact
            {
                Name = "Diego Pereira",
                Email = "dhiegopereira@devcollege.com.br",
                Url = new Uri("https://devcollegeacademy.com.br/")
            }
        });

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Please enter your JWT token",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}

void ConfigureCors(WebApplicationBuilder builder)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    });
}

void ConfigureScoped(WebApplicationBuilder builder)
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies();

    builder.Services.AddHttpClient();

    foreach (var assembly in assemblies)
    {
        var scopeds = assembly.GetTypes().Where(type => type.Name.EndsWith("Repository") || type.Name.EndsWith("Service"));

        foreach (var type in scopeds)
        {
            if (
                type.BaseType != null && 
                type.BaseType.IsGenericType && 
                type.BaseType.GetGenericTypeDefinition() == typeof(AbstractRepository<>)
                )
            {
                builder.Services.AddScoped(type);
            }
            if (type.BaseType == typeof(AbstractService))
            {
                builder.Services.AddScoped(type);
                builder.Services.AddHttpClient(type.GetType().Name);
            }   
        }
    }
}


