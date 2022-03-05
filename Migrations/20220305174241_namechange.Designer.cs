﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using aninja_tags_service.Data;

#nullable disable

namespace aninja_tags_service.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220305174241_namechange")]
    partial class namechange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("aninja_tags_service.Models.Anime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ExternalId")
                        .HasColumnType("int")
                        .HasColumnName("external_id");

                    b.Property<string>("TranslatedTitle")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("translated_title");

                    b.HasKey("Id");

                    b.ToTable("animes", (string)null);
                });

            modelBuilder.Entity("aninja_tags_service.Models.AnimeTag", b =>
                {
                    b.Property<int>("AnimeId")
                        .HasColumnType("integer")
                        .HasColumnName("anime_id");

                    b.Property<int>("TagId")
                        .HasColumnType("integer")
                        .HasColumnName("tag_id");

                    b.HasKey("AnimeId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("anime_tag", (string)null);
                });

            modelBuilder.Entity("aninja_tags_service.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("tags", (string)null);
                });

            modelBuilder.Entity("aninja_tags_service.Models.AnimeTag", b =>
                {
                    b.HasOne("aninja_tags_service.Models.Anime", "Anime")
                        .WithMany("AnimeTags")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aninja_tags_service.Models.Tag", "Tag")
                        .WithMany("AnimeTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("aninja_tags_service.Models.Anime", b =>
                {
                    b.Navigation("AnimeTags");
                });

            modelBuilder.Entity("aninja_tags_service.Models.Tag", b =>
                {
                    b.Navigation("AnimeTags");
                });
#pragma warning restore 612, 618
        }
    }
}