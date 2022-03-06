using aninja_tags_service.Models;
using aninja_tags_service.Repositories;
using aninja_tags_service.SyncDataServices;
using Microsoft.EntityFrameworkCore;

namespace aninja_tags_service.Data;

public class DbPrep
{
    public static async Task PrepData(IApplicationBuilder app, bool isProduction)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            if (context is not null)
            {
                await context.Database.MigrateAsync();
            }
            
            if (isProduction)
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IAnimeDataClient>();
                if (grpcClient is not null)
                {
                    var anime = grpcClient.ReturnAllAnime(); 
                    await SeedData(serviceScope.ServiceProvider.GetService<ITagRepository>()!, anime); 
                }
            }
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