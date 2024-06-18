using System.ComponentModel.DataAnnotations;
namespace PokemonTryASP.Entities;

public class Worker
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public int Bobik { get; set; }
}