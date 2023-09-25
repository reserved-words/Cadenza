using Cadenza.Web.Common.Interfaces;

namespace Cadenza.Web.Common.Model;

public record ConnectionStartupParameter(IConnectionState State, object ConnectRequest);
