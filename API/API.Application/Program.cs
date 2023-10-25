using Microsoft.OpenApi.Models;
using TechMentor.Persistence;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configurar o CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
      builder.WithOrigins("http://localhost:4200") // Substitua pelo domínio do seu aplicativo Angular
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "127.0.0.1:6379";
});

// builder.Services.AddAuthentication("Bearer")
//     .AddJwtBearer("Bearer", options =>
//     {
//         options.Authority = "http://localhost:8080/auth/realms/TechMentor"; // URL do seu Realm
//         options.Audience = "Tech01"; // Nome do cliente configurado no Keycloak
//         options.RequireHttpsMetadata = false; // Remover quando for produção
//     });

// builder.Services.AddSingleton<GifDataDbContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//     c =>
// {
    // c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechMentor", Version = "v1" });

    // c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    // {
    //     Type = SecuritySchemeType.OAuth2,
    //     Flows = new OpenApiOAuthFlows
    //     {
    //         AuthorizationCode = new OpenApiOAuthFlow
    //         {
    //             AuthorizationUrl = new Uri("http://localhost:8080/auth/realms/TechMentor/protocol/openid-connect/auth"),
    //             TokenUrl = new Uri("http://localhost:8080/auth/realms/TechMentor/protocol/openid-connect/token"),
    //             Scopes = new Dictionary<string, string>
    //             {
    //                 { "openid", "OpenID" },
    //                 { "profile", "Profile" }
    //             }
    //         }
    //     }
    // });

//     c.AddSecurityRequirement(new OpenApiSecurityRequirement
//     {
//         {
//             new OpenApiSecurityScheme
//             {
//                 Reference = new OpenApiReference
//                 {
//                     Type = ReferenceType.SecurityScheme,
//                     Id = "oauth2"
//                 }
//             },
//             new List<string>()
//         }
//     });
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
    //     c =>
    // {
    //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechMentor V1");
    //     c.OAuthClientId("Tech01"); // ID do seu Cliente configurado no Keycloak
    //     c.OAuthRealm("TechMentor"); // Nome do seu Realm
    //     c.OAuthAppName("API.Application"); // Nome da sua aplicação
    // }
    );

}

app.UseHttpsRedirection();

// app.UseAuthentication(); // Adicione isso antes do UseAuthorization

app.UseAuthorization();

// Call the CORS
app.UseCors();

app.MapControllers();

app.Run();
