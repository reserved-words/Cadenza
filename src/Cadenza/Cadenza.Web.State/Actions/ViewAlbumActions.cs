using Cadenza.Web.Common.ViewModel;
using System.Collections.Generic;

namespace Cadenza.Web.State.Actions;

public record FetchViewAlbumRequest(int AlbumId);

public record FetchViewAlbumResult(AlbumDetailsVM Album, IReadOnlyCollection<AlbumDiscVM> Tracks);
