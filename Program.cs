using BaobabBackEndSerice.Data;
using BaobabBackEndService.Repository.Categories;
using BaobabBackEndService.Repository.Coupons;
using BaobabBackEndService.Repository.MassiveCoupons;
using BaobabBackEndService.Services.categories;
using BaobabBackEndService.Services.Coupons;
using BaobabBackEndService.Services.MassiveCoupons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

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
        builder.Configuration.GetConnectionString("BaobabDataBaseConnection"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

/*
Parte 6:

En esta sección, integramos nuestros repositorios en el sistema. Es importante recordar primero registrar la interfaz, 
que podemos identificar por la "I" al principio de su nombre, y luego la clase del repositorio correspondiente.

Pasos para integrar un repositorio:

1. Crear la carpeta en el servicio.
2. Crear nuestra interfaz con el nombre siguiendo el formato "I + Nombre + Repository".
3. Crear nuestra clase del repositorio con el nombre "Nombre + Repository".
4. Declarar el scope del repositorio aquí en el archivo `program.cs`.
5. Declarar el repositorio en el controlador, como vimos anteriormente en la Parte 0 de nuestra documentación.

Por ejemplo, para integrar `CouponsRepository`, seguiríamos estos pasos:

```csharp
// En el archivo program.cs
services.AddScoped<ICouponsRepository, CouponsRepository>();
De esta forma, hemos registrado el repositorio en el contenedor de dependencias, lo que permitirá inyectarlo 
en nuestros controladores y servicios.

¡Eso es todo para esta parte! Ahora, tu sistema está configurado para utilizar el nuevo repositorio.
 Yeeeiii fiesta en la casa del tintero
*/
builder.Services.AddScoped<IMassiveCouponsRepository, MassiveCouponsRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ICouponsRepository, CouponsRepository>();
builder.Services.AddScoped<IMassiveCouponsServices, MassiveCouponsServices>();
builder.Services.AddScoped<ICategoriesServices, CategoryServices>();
builder.Services.AddScoped<ICouponsServices, CouponsServices>();



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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapControllers();

app.Run();