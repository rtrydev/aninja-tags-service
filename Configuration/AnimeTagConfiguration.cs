using aninja_tags_service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aninja_tags_service.Configuration;

public class AnimeTagConfiguration : IEntityTypeConfiguration<AnimeTag>
{
    public void Configure(EntityTypeBuilder<AnimeTag> builder)
    {
        builder.ToTable("anime_tag");
        
        builder.HasKey(x => new {x.AnimeId, x.TagId});

        builder.HasOne(x => x.Anime)
            .WithMany(x => x.AnimeTags)
            .HasForeignKey(x => x.AnimeId);

        builder.Property(x => x.AnimeId)
            .HasColumnName("anime_id");

        builder.HasOne(x => x.Tag)
            .WithMany(x => x.AnimeTags)
            .HasForeignKey(x => x.TagId);

        builder.Property(x => x.TagId)
            .HasColumnName("tag_id");
    }
}