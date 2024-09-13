using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces
{
    public interface IObterStatusPagamentoPorIdUseCase : IUseCase<int, StatusPagamentoResponse>
    {
    }
}
