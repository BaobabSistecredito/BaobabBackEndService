using FluentValidation;
using System.Text.Json;
using BaobabBackEndSerice.Data;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using BaobabBackEndService.Extensions;
using SlackNet.AspNetCore;
using SlackNet;
using BaobabBackEndService.ExternalServices.SlackNotificationService;
using BaobabBackEndService.ExternalServices.MailSendService;
using BaobabBackEndService.Models;


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

/* PARTE 5 
    Aca implementaremos la inyeccion de las dependencias para hacer el uso de nuestras validaciones
    como siempre debemos tener los using que seran, using FluentValidation y 
    using FluentValidation.AspNetCore una vez los registremos vamos a inyectar la dependencia
*/
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<CouponValidator>();
/* PARTE 6
    aca te preguntaras, porque solamente estoy inyectando un solo modelo y no inyecto los otros,
    pues veras, al hacer la inyeccion de una sola clase, la libreria automaticamente leera todas 
    las otras clases del mismo tipo dentro DEL MISMO ARCHIVO Validator.cs por lo tanto con solo
    inyectar una ya es sufuciente para leer las otras. 
    Y ya con esto queda todo el proceso de fluent validation, dudas preguntas con Jesus.
*/



// ----------------------------------
// LIBRERÍA 'HealthCheck'
builder.Services.AddHealthChecks();
// ----------------------------------
// DATABASE:
builder.Services.AddDbContext<BaobabDataBaseContext>(Options =>
    Options.UseMySql(
        builder.Configuration.GetConnectionString("BaobabDataBaseConnection"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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

// ------------ // ---------------
// builder.Services.AddSlackNet(c => c
//     .UseApiToken("xoxb-7280173190323-7295334874897-vCGyPfd0WozHueAl6h8wZIia")); // Token
// builder.Services.AddSingleton<ISlackClient>(new SlackClient(new SlackServiceConfiguration
// {
//     WebhookUri = new Uri("https://hooks.slack.com/services/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")
// }));
// - - - - - - 
// builder.Services.AddSlackNet(c => c
//     .UseApiToken("xoxb-7280173190323-7295334874897-vCGyPfd0WozHueAl6h8wZIia")
//     .UseSigningSecret("https://hooks.slack.com/services/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"));
/* 
    Se configura y registra la sección 'SlackSettingsService' del archivo 'appsettings.json' como un servicio de configuración en el contenedor de dependencias de la aplicación:
*/
builder.Services.Configure<SlackSettingsService>(builder.Configuration.GetSection("SlackSettingsService"));
/* 
    Registra un cliente HTTP como servicio para la clase 'SlackNotificationService'.
    Al registrar un cliente HTTP para SlackNotificationService, ASP.NET Core se encarga de crear y administrar una instancia de HttpClient que puede ser inyectada en SlackNotificationService. Esto facilita el envío de solicitudes HTTP al WebHook de Slack desde SlackNotificationService para enviar notificaciones:
*/
builder.Services.AddHttpClient<SlackNotificationService>();
// -------------------------------

// ------------ // ---------------
builder.Services.AddAutoMapper(typeof(Program));
// -------------------------------

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
*/

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddCustomAuthentication(builder.Configuration);


builder.Services.AddHttpClient<IMailSendService, MailSendService>();
builder.Services.Configure<MailerSendOptions>(builder.Configuration.GetSection("MailerSend"));



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaobabBackEndService"); });

app.UseHttpsRedirection();
app.UseCors("AllowAnyOrigin");

app.UseAuthentication();
app.UseAuthorization();

// app.UseSlackNet();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapControllers();
app.Run();