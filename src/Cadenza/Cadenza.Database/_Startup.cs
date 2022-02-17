//global using Cadenza.Common;
//global using IndexedDB.Blazor;
//global using System.ComponentModel.DataAnnotations;

//using Cadenza.Core;
//using Microsoft.Extensions.DependencyInjection;

//namespace Cadenza.Database;

//public static class Startup
//{
//    public static IServiceCollection AddDatabaseRepositories(this IServiceCollection services)
//    {
//        return services
//            .AddSingleton<IIndexedDbFactory, IndexedDbFactory>()
//            .AddTransient<IMainRepository, MainRepository>()
//            .AddTransient<IArtistRepository, ArtistRepository>()
//            .AddTransient<IAlbumRepository, AlbumRepository>()
//            .AddTransient<IPlayTrackRepository, PlayTrackRepository>();
//    }
//}

