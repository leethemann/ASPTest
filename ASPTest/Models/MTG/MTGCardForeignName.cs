using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTest.Models.MTG
{
    public class MTGCardForeignName
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public int MultiverseID { get; set; }
    }
}
