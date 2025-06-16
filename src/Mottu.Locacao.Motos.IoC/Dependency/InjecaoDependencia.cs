using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mottu.Locacao.Motos.Application.Service;
using Mottu.Locacao.Motos.Data.Repository;
using Mottu.Locacao.Motos.Domain.Interface.Application;
using Mottu.Locacao.Motos.Domain.Interface.Repository;
using Mottu.Locacao.Motos.Domain.Interface.Service;
using Mottu.Locacao.Motos.Domain.Notification;
using Npgsql;
using System.Data;

namespace Mottu.Locacao.Motos.IoC.Dependency
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection RegistrarDependencias(this IServiceCollection services)
        {  
            services.AddScoped<IMotoService, MotoService>();
            services.AddScoped<IRabbitService, RabbitService>();
            services.AddScoped<IMotoRepository, MotoRepository>();

            services.AddScoped<IDbConnection>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("PostgreDB");
                return new NpgsqlConnection(connectionString);
            });

            services.AddScoped<NotificacaoDominioHandler>();

            return services;
        }
    }
}
