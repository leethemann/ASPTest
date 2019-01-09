using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTest.Models.MTG
{
    public class MTGCardLegality
    {
        public string Format { get; set; }
        public string LegalStatus { get; set; }

        public MTGCardLegality(string format, string legality)
        {
            this.Format = format;
            this.LegalStatus = legality;
        }
    }
}
