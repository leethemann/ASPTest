using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTest.Models.MTG
{
    public class MTGColorFilter
    {
        public string Color { get; set; }
        public bool Selected { get; set; }
        public string sourceImage { get; set; }

        public MTGColorFilter(string color, bool selected, string source)
        {
            this.Color = color;
            this.Selected = selected;
            this.sourceImage = source;
        }

        public MTGColorFilter()
        {
            //do nothing
        }
    }
}
