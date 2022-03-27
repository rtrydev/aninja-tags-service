using System.ComponentModel.DataAnnotations;

namespace aninja_tags_service.Dtos;

public class TagWriteDto
{
    [MaxLength(40)]
    public string? Name { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
}