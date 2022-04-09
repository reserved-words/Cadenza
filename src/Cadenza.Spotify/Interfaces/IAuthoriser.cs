using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadenza.Spotify.Interfaces;

public interface IAuthoriser
{
    Task<string> GetAuthHeader();
    Task<string> GetAuthUrl(string state, string redirectUri);
    Task<string> GetTokenUrl();
}
