namespace aninja_tags_service.Models;

public class Anime
{
    public int Id { get; set; }
    public int ExternalId { get; set; }
    public string? TranslatedTitle { get; set; }
    public ICollection<AnimeTag>? AnimeTags { get; set; }

}