using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTest.Models.MTG
{
    public class MTGCardForeignName
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Flavor { get; set; }
        public string ImageURL { get; set; }
        public string Language { get; set; }
        public int? MultiverseID { get; set; }

        public MTGCardForeignName(string name, string text, string flavor, string image, string language, int? multiverse)
        {
            this.Name = name;
            this.Text = text;
            this.Flavor = flavor;
            this.ImageURL = image;
            this.Language = language;
            this.MultiverseID = multiverse;
        }
    }
}
