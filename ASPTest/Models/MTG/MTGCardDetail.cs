using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTest.Models.MTG
{
    public class MTGCardDetail
    {
        public string Name { get; set; }
        public List<string> Names { get; set; }
        public string ManaCost { get; set; }
        public string CMC { get; set; }
        public List<string> Colors { get; set; }
        public List<string> ColorIdentities { get; set; }
        public string Type { get; set; }
        public List<string> SuperTypes { get; set; }
        public List<string> Types { get; set; }
        public List<string> SubTypes { get; set; }
        public string Rarity { get; set; }
        public string Set { get; set; }
        public string SetName { get; set; }
        public string Text { get; set; }
        public string Flavor { get; set; }
        public string Artist { get; set; }
        public string Number { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public string Layout { get; set; }
        public string MultiverseID { get; set; }
        public string ImageURL { get; set; }
        public string Watermark { get; set; }
        public List<MTGCardRuling> Rulings { get; set; }
        public List<MTGCardForeignName> ForeignNames { get; set; }
        public List<string> Variations { get; set; }
        public List<string> Printings { get; set; }
        public string OriginalText { get; set; }
        public string OriginalType { get; set; }
        public List<MTGCardLegality> Legalities { get; set; }
        public string ID { get; set; }

        public MTGCardDetail()
        {
            Names = new List<string>();
            Colors = new List<string>();
            ColorIdentities = new List<string>();
            SuperTypes = new List<string>();
            Types = new List<string>();
            SubTypes = new List<string>();
            Rulings = new List<MTGCardRuling>();
            ForeignNames = new List<MTGCardForeignName>();
            Variations = new List<string>();
            Printings = new List<string>();
            Legalities = new List<MTGCardLegality>();
        }
    }
}
