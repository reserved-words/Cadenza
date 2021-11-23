using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadenza.Database
{
    public class DbInfo
    {
        public int Id { get; set; }
        public bool AreArtistsPopulated { get; set; }
        public bool AreTracksPopulated { get; set; }
    }
}
