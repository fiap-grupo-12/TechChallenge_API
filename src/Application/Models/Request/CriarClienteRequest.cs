using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.Models.Request
{
    public class CriarClienteRequest
    {
        [Required(ErrorMessage = "É obrigatório informar o nome.")]
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("cpf")]
        public string CPF { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
