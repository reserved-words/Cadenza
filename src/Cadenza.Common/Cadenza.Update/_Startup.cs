//global using Cadenza.Domain;
//using Cadenza.Update.LibraryUpdaters;
//using Cadenza.Utilities;
//using Microsoft.Extensions.DependencyInjection;
//using System.Runtime.CompilerServices;

//[assembly: InternalsVisibleTo("Cadenza.Library.Tests")]

//namespace Cadenza.Update;

//public static class _Startup
//{

//    public static IServiceCollection AddMergedUpdaters(this IServiceCollection services)
//    {
//        return services
//            .AddTransient<IValueMerger, ValueMerger>()
//            .AddTransient<IMerger, Merger>()
//            .AddTransient<IMergedArtistUpdater, MergedArtistUpdater>();
//    }

//}
