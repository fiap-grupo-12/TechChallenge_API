using FIAP.TechChallenge.ByteMeBurguer.Application;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;
using FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Extensions
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext dbContext;

        public DatabaseInitializer(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public void Initialize()
        {
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
        }
    }
}
