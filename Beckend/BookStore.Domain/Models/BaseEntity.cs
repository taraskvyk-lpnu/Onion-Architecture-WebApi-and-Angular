using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Domain;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}