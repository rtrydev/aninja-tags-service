using aninja_tags_service.Models;
using aninja_tags_service.Repositories;
using aninja_tags_service.SyncDataServices;
using Microsoft.EntityFrameworkCore;

namespace aninja_tags_service.Data;

public class DbPrep
{
    public static async Task PrepData(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var grpcClient = serviceScope.ServiceProvider.GetService<IAnimeDataClient>();
            var anime = grpcClient.ReturnAllAnime();

            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context.Database.Migrate();
            
            await SeedData(serviceScope.ServiceProvider.GetService<ITagRepository>(), anime); 
        }
    }

    private static async Task SeedData(ITagRepository tagRepository, IEnumerable<Anime> anime)
    {
        foreach (var a in anime)
        {
            if (!await tagRepository.ExternalAnimeExists(a.ExternalId))
            {
                await tagRepository.CreateAnime(a);
            }

            await tagRepository.SaveChangesAsync();
        }
    }
}