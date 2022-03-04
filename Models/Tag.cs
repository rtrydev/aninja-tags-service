namespace aninja_tags_service.Models;

public class Tag
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<Anime>? AnimesWithTag { get; set; }
}