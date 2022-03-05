using aninja_tags_service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aninja_tags_service.Configuration;

public class AnimeConfiguration : IEntityTypeConfiguration<Anime>
{
    public void Configure(EntityTypeBuilder<Anime> builder)
    {
        builder.ToTable("animes");
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ExternalId)
            .HasColumnName("external_id")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.TranslatedTitle)
            .HasColumnName("translated_title")
            .HasColumnType("text")
            .IsRequired();


    }
}