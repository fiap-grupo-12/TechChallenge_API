namespace FIAP.TechChallenge.ByteMeBurguer.Domain.Entities
{
    public class Cliente : EntityBase
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
    }
}
