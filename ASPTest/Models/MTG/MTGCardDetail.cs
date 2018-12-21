using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTest.Models.MTG
{
    public class MTGCardDetail
    {
        public string Name { get; set; }
        public string[] Names { get; set; }
        public string ManaCost { get; set; }
        public int CMC { get; set; }
        public string[] Colors { get; set; }
        public string[] ColorIdentities { get; set; }
        public string Type { get; set; }
        public string[] SuperTypes { get; set; }
        public string[] Types { get; set; }
        public string[] SubTypes { get; set; }
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
        public int MultiverseID { get; set; }
        public string ImageURL { get; set; }
        public string Watermark { get; set; }
        public MTGCardRuling[] Rulings { get; set; }
        public MTGCardForeignName[] ForeignNames { get; set; }
        public int[] Variations { get; set; }
        public string[] Printings { get; set; }
        public string OriginalText { get; set; }
        public string OriginalType { get; set; }
        public MTGCardLegality[] Legalities { get; set; }
        public string ID { get; set; }
    }
}
