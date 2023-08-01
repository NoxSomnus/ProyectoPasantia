using System.ComponentModel.DataAnnotations.Schema;

namespace UCABPagaloTodoMS.Core.Entities;

public class BaseEntity
{
    [Column(Order = 1)]
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
