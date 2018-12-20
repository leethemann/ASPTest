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
        public List<string> ColorOptions { get; set; }
        public SelectList SetFilterList;
        public string TypeFilter;
        public string NameFilter { get; set; }
        public string SetFilter { get; set; }

        public MTGCardSearchViewModel()
        {
            ColorOptions = new List<string> { "Red", "White", "Green", "Black", "Blue", "Colorless" };
        }
    }
}
