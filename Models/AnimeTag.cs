namespace aninja_tags_service.Models;

public class AnimeTag
{
    public int AnimeId { get; set; }
    public Anime? Anime { get; set; }
    public int TagId { get; set; }
    public Tag? Tag { get; set; }
}