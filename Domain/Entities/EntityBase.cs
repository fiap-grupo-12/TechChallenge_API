using System.ComponentModel.DataAnnotations;

namespace FIAP.TechChallenge.ByteMeBurguer.Domain.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
