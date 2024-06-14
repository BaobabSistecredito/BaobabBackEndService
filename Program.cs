using BaobabBackEndService.Repository.Categories;
using BaobabBackEndService.Repository.Coupons;
using BaobabBackEndService.Repository.MassiveCoupons;
using BaobabBackEndService.Services.categories;
using BaobabBackEndService.Services.Coupons;
using BaobabBackEndService.Services.MassiveCoupons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

//librerias de jwt
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BaobabBackEndService.Services.User;
using BaobabBackEndService.Repository.User;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using BaobabBackEndService.Middleware;
using BaobabBackEndSerice.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<BaobabDataBaseContext>(Options =>
    Options.UseMySql(
        builder.Configuration.GetConnectionString("BaobabDataBaseConnectionTest"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    /* 
        Fue necesario añadir estos servicios para configurar la serialización JSON
        para el método 'EditCoupon':
    */
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(configure =>
    {
        string jwtToken = "3C7HJGIRJIKSOKSDIJFIDJFDJFDJF23234E";
        configure.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Environment.GetEnvironmentVariable("JwtToke"),
            ValidAudience = Environment.GetEnvironmentVariable("JwtToke"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken))
        };

        configure.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                /* context.Response.ContentType = "application/json"; */

                if (context.Exception is SecurityTokenExpiredException)
                {
                   
                    Console.WriteLine("-------------------------------------------------------------------------------");
                    Console.WriteLine("Token Expirado. Por favor, genere un nuevo.");
                }
                else
                {
                    
                    Console.WriteLine("-------------------------------------------------------------------------------");
                    Console.WriteLine("Usuario no Autorizado.");
                }
            }
        };
    });



builder.Services.AddScoped<IMassiveCouponsRepository, MassiveCouponsRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ICouponsRepository, CouponsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IMassiveCouponsServices, MassiveCouponsServices>();
builder.Services.AddScoped<ICategoriesServices, CategoryServices>();
builder.Services.AddScoped<ICouponsServices, CouponsServices>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaobabBackEndService");
});

// Configuración de CORS
app.UseCors("AllowAnyOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Asegúrate de agregar este middleware antes del middleware de autenticación
app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (SecurityTokenExpiredException)
    {
        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"message\": \"Token Expirado. Por favor, genere un nuevo.\"}");
        }
    }
    catch (Exception ex)
    {
        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync($"{{\"message\": \"{ex.Message}\"}}");
        }
        else
        {
            throw;
        }
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();