﻿using Amazon.SecretsManager;
using AutoMapper;
using FIAP.TechChallenge.ByteMeBurguer.Application;
using FIAP.TechChallenge.ByteMeBurguer.Application.UseCases;
using FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories;
using FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Configurations;
using FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Extensions;
using FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChallenge.ByteMeBurguer.API.Extensions
{
    public static class DependencyInjection
    {
        public static void AddProjectDependencies(this IServiceCollection services, string connectionString)
        {
  
            //AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperConfig());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Repository
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IFormaPagamentoRepository, FormaPagamentoRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();

            //UseCase
            services.AddTransient<IAtualizarProdutoUseCase, AtualizarProdutoUseCase>();
            services.AddTransient<ICriarClienteUseCase, CriarClienteUseCase>();
            services.AddTransient<ICriarPedidoUseCase, CriarPedidoUseCase>();
            services.AddTransient<ICriarProdutoUseCase, CriarProdutoUseCase>();
            services.AddTransient<IAtualizarStatusPedidoUseCase, AtualizarStatusPedidoUseCase>();
            services.AddTransient<IObterClientePorCpfUseCase, ObterClientePorCpfUseCase>();
            services.AddTransient<IObterClientesUseCase, ObterClientesUseCase>();
            services.AddTransient<IObterPedidoPorIdUseCase, ObterPedidoPorIdUseCase>();
            services.AddTransient<IObterStatusPagamentoPorIdUseCase, ObterStatusPagamentoPorIdUseCase>();
            services.AddTransient<IObterPedidosUseCase, ObterPedidosUseCase>();
            services.AddTransient<IObterPedidosFiltradosUseCase, ObterPedidosFiltradosUseCase>();
            services.AddTransient<IObterProdutoPorCategoriaUseCase, ObterProdutoPorCategoriaUseCase>();
            services.AddTransient<IRemoverProdutoUseCase, RemoverProdutoUseCase>();
            services.AddTransient<IAtualizarStatusPagamentoUseCase, AtualizarStatusPagamentoUseCase>();

            //AWS
            services.AddDefaultAWSOptions(new Amazon.Extensions.NETCore.Setup.AWSOptions()
            {
                Region = Amazon.RegionEndpoint.USEast1
            });
            services.AddAWSService<IAmazonSecretsManager>();

            //DataBase
            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(connectionString!);
            });

            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
        }
    }
}
