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
            ColorFilters = new List<MTGColorFilter> { new MTGColorFilter("Red", false), new MTGColorFilter("White", false),
                                                   new MTGColorFilter("Green", false), new MTGColorFilter("Black", false),
                                                   new MTGColorFilter("Blue", false), new MTGColorFilter("Colorless", false) };
        }
    }
}
