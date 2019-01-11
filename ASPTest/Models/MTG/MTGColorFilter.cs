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

        public MTGColorFilter(string color, bool selected)
        {
            this.Color = color;
            this.Selected = selected;
        }

        public MTGColorFilter()
        {
            //do nothing
        }
    }
}
