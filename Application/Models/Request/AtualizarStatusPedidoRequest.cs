using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.Models.Request
{
    public class AtualizarStatusPedidoRequest
    {
        [Required(ErrorMessage = "É obrigatório informar o id.")]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "É obrigatório informar o status do pedido.")]
        [JsonPropertyName("statusPedido")]
        public StatusPedido StatusPedido { get; set; }
    }
}
