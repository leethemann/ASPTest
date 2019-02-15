using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTest.Models.MTG
{
    public class MTGCardSimple
    {
        public string Name { get; set; }
        public List<string> Colors { get; set; }
        public string Type { get; set; }
        public string SetName { get; set; }
        public string ID { get; set; }
        public string ImageURL { get; set; }
        public string Text { get; set; }

        public MTGCardSimple()
        {
            Colors = new List<string>();
        }
    }
}
