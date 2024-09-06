using System.ComponentModel;

namespace FIAP.TechChallenge.ByteMeBurguer.Domain.Entities.Enum
{
    public enum StatusPedido
    {
        [Description("Recebido")]
        Recebido = 1,

        [Description("Em Preparação")]
        EmPreparacao = 2,

        [Description("Pronto")]
        Pronto = 3,

        [Description("Finalizado")]
        Finalizado = 4
    }
}
