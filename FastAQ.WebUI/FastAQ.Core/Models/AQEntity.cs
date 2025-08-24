using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastAQ.Models.AQEntity;

public class AQEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public required string Question { get; set; }
    public required string Answer { get; set; }
}

public class AQEntityItem
{
    public required string Question { get; set; }
    public required string Answer { get; set; }
}