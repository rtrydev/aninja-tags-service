namespace aninja_tags_service.Models;

public class Anime
{
    public int Id { get; set; }
    public int ExternalId { get; set; }
    public string? TranslatedName { get; set; }
    public IEnumerable<Tag>? Tags { get; set; }

}