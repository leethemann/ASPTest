using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTest.Models.MTG
{
    public class MTGCardRuling
    {
        public string Date { get; set; }
        public string Text { get; set; }
        
        public MTGCardRuling(string date, string text)
        {
            this.Date = date;
            this.Text = text;
        }
    }
}
