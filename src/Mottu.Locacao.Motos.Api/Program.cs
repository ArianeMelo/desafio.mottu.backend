using Mottu.Locacao.Motos.Api.Consumer;
using Mottu.Locacao.Motos.Api.Middleware;
using Mottu.Locacao.Motos.Application.Configuration;
using Mottu.Locacao.Motos.IoC.Dependency;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services
    .RegistrarDependencias()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSwaggerGenNewtonsoftSupport()
    .AddHostedService<MotoCadastradaConsumer>();

builder.Services
    .Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));

var app = builder.Build();

app
    .UseMiddleware<ExceptionHandlerMiddleware>()
    .UseSwagger()
    .UseSwaggerUI()
    .UseHttpsRedirection()
    .UseAuthorization();

app.MapControllers();

app.Run();