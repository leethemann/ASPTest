using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPTest.Models.MTG
{
    public class MTGCardSearchViewModel
    {
        public List<MTGCardSimple> Cards { get; set; }
        public List<MTGColorFilter> ColorFilters { get; set; }
        public SelectList SetFilterList;
        public string TypeFilter;
        public string NameFilter { get; set; }
        public string SetFilter { get; set; }

        public MTGCardSearchViewModel()
        {
            ColorFilters = new List<MTGColorFilter> { new MTGColorFilter("Red", false, "~/images/red.png"), new MTGColorFilter("White", false, "~/images/white.png"),
                                                   new MTGColorFilter("Green", false, "~/images/green.png"), new MTGColorFilter("Black", false, "~/images/black.png"),
                                                   new MTGColorFilter("Blue", false, "~/images/blue.png"), new MTGColorFilter("Colorless", false, "~/images/red.png") };
        }
    }
}
