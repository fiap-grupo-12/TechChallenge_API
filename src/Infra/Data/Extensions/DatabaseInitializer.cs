using FIAP.TechChallenge.ByteMeBurguer.Application;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;
using FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Extensions
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(ApplicationDbContext context, ILogger<DatabaseInitializer> logger)
        {
            dbContext = context;
            _logger = logger;
        }
        public void Initialize()
        {
            try
            {
                _logger.LogInformation("Tentando conexão com o banco de dados.");

                dbContext.Database.Migrate();

                if (!dbContext.Categorias.Any())
                {
                    dbContext.AddRange(
                        new Categoria { Nome = "Lanche" },
                        new Categoria { Nome = "Acompanhamento" },
                        new Categoria { Nome = "Bebida" },
                        new Categoria { Nome = "Sobremesa" },
                        new FormaPagamento { Nome = "Mercado Pago" }
                        );

                    dbContext.SaveChanges();
                }

                _logger.LogInformation("Conexão com o banco de dados OK.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
