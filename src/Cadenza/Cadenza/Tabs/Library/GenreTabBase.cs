﻿using Fluxor;
namespace Cadenza.Tabs.Library;

public class GenreTabBase : FluxorComponent
{
    [Inject] public IState<ViewGenreState> ViewGenreState { get; set; }

    public string Genre => ViewGenreState.Value.Genre;
    public List<Artist> Artists => ViewGenreState.Value.Artists;
}
