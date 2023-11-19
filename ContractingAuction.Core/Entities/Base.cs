using System.ComponentModel.DataAnnotations;

namespace ContractingAuction.Core.Entities;

public class Base
{
    [Key] 
    public int Id { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateDate { get; set; }
}