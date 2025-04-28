using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotoManager.Domain.Commands.Admin.Motos;
using MotoManager.Domain.Commands.Public.Entregador;
using MotoManager.Domain.Commands.Public.Locacao;
using MotoManager.Domain.Handlers.Motos;
using MotoManager.Domain.Interfaces.Handler;
using MotoManager.Domain.Interfaces.Queries;
using MotoManager.Domain.Interfaces.Repositories;
using MotoManager.Domain.Interfaces.Services;
using MotoManager.Domain.Interfaces.UnitOfWork;
using MotoManager.Infra.Data.Queries;
using MotoManager.Infra.Data.Repositories;
using MotoManager.Infra.Data.UnitOfWork;
using MotoManager.Infra.Services;
using System.Configuration;
using System.Reflection;

namespace MotoManager.IOC
{
    public static class Bootstrapper
    {
        /// <summary>
        /// Adiciona as dependencias via inversão de controle
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        public static void AddDependeciesIOC(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly assembly)
        {

            //infra Data
            services.AddScoped<BaseConnection>();
            services.AddScoped<GenericRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IMotoRespository, MotoRepository>();
            services.AddTransient<ILocacaoRepository, LocacaoRepository>();
            services.AddTransient<IEntregadorRepository, EntregadorRepository>();
            services.AddTransient<IPlanoRepository, PlanoRepository>();

            // Infra Data Queries
            services.AddTransient<IMotoQuery, MotoQuery>();
            services.AddTransient<ILocacaoQuery, LocacaoQuery>();
            services.AddTransient<IPlanoQuery, PlanoQuery>();

            // infra services
            services.AddScoped<IFileService, FileService>();

            // handlers
            services.AddTransient<IHandler<MotosCreateCommand>, MotosCreateHandler>();
            services.AddTransient<IHandler<MotosUpdateCommand>, MotosUpdateHandler>();
            services.AddTransient<IHandler<MotosDeleteCommand>, MotosDeleteHandler>();

            services.AddTransient<IHandler<LocacaoRentCommand>, LocacaoRentHandler>();
            services.AddTransient<IHandler<LocacaoReturnCommand>, LocacaoReturnHandler>();

            services.AddTransient<IHandler<EntregadorCreateCommand>, EntregadorCreateHandler>();
            services.AddTransient<IHandler<EntregadorUpdateCommand>, EntregadorUpdateHandler>();



        }
    }
}