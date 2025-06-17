using Mottu.Locacao.Motos.Api.Consumer;
using Mottu.Locacao.Motos.Api.Middleware;
using Mottu.Locacao.Motos.Api.Swagger;
using Mottu.Locacao.Motos.Application.Configuration;
using Mottu.Locacao.Motos.IoC.Dependency;
using Mottu.Locacao.Motos.IoC.Jwt;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services
    .RegistrarDependencias()
    .AddConfigurationJwt(builder.Configuration)    
    .AddSwagger()
    .AddSwaggerGenNewtonsoftSupport()
    .AddHostedService<MotoCadastradaConsumer>();

builder.Services
    .Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"))
    .Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var app = builder.Build();

app
    .UseMiddleware<ExceptionHandlerMiddleware>()
    .UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottu Locacao V1");
    })
    .UseHttpsRedirection()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();