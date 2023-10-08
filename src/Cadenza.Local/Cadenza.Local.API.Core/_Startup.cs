﻿global using Cadenza.Local.API.Common.Controllers;
global using Cadenza.Local.API.Common.Interfaces;
global using Cadenza.Local.API.Core.Interfaces;
global using Cadenza.Local.API.Core.Services;
global using Cadenza.Local.API.Core.Settings;
global using Cadenza.Local.FileAccess;
global using Cadenza.Common.DTO;

using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Cadenza.Local.API.Core.Tests")]

namespace Cadenza.Local.API.Core
{
    public static class Startup
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            return services.AddInternalServices()
                .AddTransient<ILibraryService, LibraryService>()
                .AddTransient<ISyncService, SyncService>();
        }

        private static IServiceCollection AddInternalServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IFilepathParser, FilepathParser>()
                .AddTransient<IMusicDirectory, MusicDirectory>();
        }
    }
}
