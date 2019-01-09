using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASPTest.Models.MTG;
using ASPTest.DataObjects.MTG;
using System.Net;
using System.IO;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPTest.Controllers
{
    public class MTGController : Controller
    {
        private static MTGAPIService _apiService;
        private static List<string> _setList;

        public MTGController()
        {
            if(_apiService is null)
                _apiService = new MTGAPIService();
        }

        // GET: /MTG/
        public async Task<IActionResult> Index(string nameFilter, string setFilter, string colorFilter, string typeFilter)
        {
            MTGCardSearchViewModel data = new MTGCardSearchViewModel();
            //We're not searching off the bat so we need to send in an empty list, or shit blows up.
            data.Cards = new List<MTGCardSimple>();

            if (_setList is null)
                _setList = await GetAllSetsForFilterAsync();

            data.SetFilterList = new SelectList(_setList);
            
            await Task.Run(() =>_apiService.ClearFilters());
            
            data.NameFilter = nameFilter;
             
            return View(data);
        }

        // GET: /MTG/
        public async Task<IActionResult> CardSearch(string nameFilter, string setFilter, string colorFilter, string typeFilter)
        {
            MTGCardSearchViewModel data = new MTGCardSearchViewModel();

            await Task.Run(() => _apiService.ClearFilters());

            data.Cards = await GetCardsForDisplayAsync(nameFilter, setFilter, colorFilter, typeFilter);
            data.SetFilterList = new SelectList(await GetAllSetsForFilterAsync());

            data.NameFilter = nameFilter;
            data.SetFilter = setFilter;
            data.TypeFilter = typeFilter;

            return View(data);
        }

        // GET: /MTG/Details/id
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string card = await _apiService.GetCardByIDAsync(id);

            MTGCardDetail detailCard = await ParseDetailCardJsonAsync(card);

            if (card == null)
            {
                return NotFound();
            }

            return View(detailCard);
        }

        public async Task<List<MTGCardSimple>> GetCardsForDisplayAsync(string nameFilter, string setFilter, string colorFilter, string typeFilter)
        {
            List<MTGCardSimple> data = new List<MTGCardSimple>();
            
            _apiService.NameFilter = nameFilter;
            _apiService.ColorFilter = colorFilter;
            _apiService.SetFilter = setFilter;
            _apiService.TypeFilter = typeFilter;

            string jsonResponse = await _apiService.GetCardsAsync();
            data = await ParseCardsJsonAsync(jsonResponse);

            return data;
        }

        public async Task<List<string>> GetAllSetsForFilterAsync()
        {
            string allSetsJson = await _apiService.GetAllSetsAsync();

            return await ParseSetsJsonAsync(allSetsJson);
        }
        
        private async Task<List<MTGCardSimple>> ParseCardsJsonAsync(string cardsJson)
        {
            List<MTGCardSimple> parsedCards = new List<MTGCardSimple>();

            dynamic jObject = await Task.Run(() => JsonConvert.DeserializeObject(cardsJson));

            foreach(var card in jObject.cards)
            {
                MTGCardSimple newCard = new MTGCardSimple();
                newCard.Name = card.name;

                if(card["colors"] != null)
                {
                    foreach (var color in card.colors)
                    {
                        newCard.Colors.Add((string)color);
                    }
                }

                newCard.SetName = card.setName;
                newCard.Type = card.type;
                newCard.ID = card.id;

                parsedCards.Add(newCard);
            }

            return parsedCards;
        }

        //TODO: Finish manual parse code for detail object
        private async Task<MTGCardDetail> ParseDetailCardJsonAsync(string cardsJson)
        {
            MTGCardDetail parsedCard = new MTGCardDetail();

            dynamic jObject = await Task.Run(() => JsonConvert.DeserializeObject(cardsJson));
            
            if (jObject.card != null)
            {
                jObject = jObject.card;

                parsedCard.Name = jObject.name;
                parsedCard.ManaCost = jObject.manaCost;
                parsedCard.CMC = (int?)jObject.cmc;
                parsedCard.Type = jObject.type;
                parsedCard.Rarity = jObject.rarity;
                parsedCard.Set = jObject.set;
                parsedCard.SetName = jObject.setName;
                parsedCard.Text = jObject.text;
                parsedCard.Flavor = jObject.flavor;
                parsedCard.Artist = jObject.artist;
                parsedCard.Number = jObject.number;
                parsedCard.Power = jObject.power;
                parsedCard.Toughness = jObject.toughness;
                parsedCard.Layout = jObject.layout;
                parsedCard.MultiverseID = (int?)jObject.multiverseid;
                parsedCard.ImageURL = jObject.imageUrl;
                parsedCard.Watermark = jObject.watermark;
                parsedCard.OriginalText = jObject.originalText;
                parsedCard.OriginalType = jObject.originalType;
                parsedCard.ID = jObject.id;

                if (jObject["names"] != null)
                {
                    foreach (var name in jObject.names)
                    {
                        parsedCard.Names.Add(name.ToString());
                    }
                }

                if (jObject["colors"] != null)
                {
                    foreach (var color in jObject.colors)
                    {
                        parsedCard.Colors.Add(color.ToString());
                    }
                }

                if (jObject["colorIdentities"] != null)
                {
                    foreach (var colorID in jObject.colorIdentities)
                    {
                        parsedCard.ColorIdentities.Add(colorID.ToString());
                    }
                }

                if (jObject["superTypes"] != null)
                {
                    foreach (var superType in jObject.superTypes)
                    {
                        parsedCard.SuperTypes.Add(superType.ToString());
                    }
                }

                if (jObject["types"] != null)
                {
                    foreach (var type in jObject.types)
                    {
                        parsedCard.Types.Add(type.ToString());
                    }
                }

                if (jObject["subTypes"] != null)
                {
                    foreach (var subType in jObject.subTypes)
                    {
                        parsedCard.SubTypes.Add(subType.ToString());
                    }
                }

                if (jObject["rulings"] != null)
                {
                    foreach (var ruling in jObject.rulings)
                    {
                        parsedCard.Rulings.Add(new MTGCardRuling(ruling.date.ToString(), ruling.text.ToString()));
                    }
                }

                if (jObject["foreignNames"] != null)
                {
                    foreach (var foreignName in jObject.foreignNames)
                    {
                        parsedCard.ForeignNames.Add(new MTGCardForeignName(foreignName.name.ToString(), foreignName.text.ToString(), foreignName.flavor.ToString(), foreignName.imageUrl.ToString(), foreignName.language.ToString(), (int?)foreignName.multiverseId));
                    }
                }

                if (jObject["variations"] != null)
                {
                    foreach (var variation in jObject.variations)
                    {
                        parsedCard.Variations.Add(variation.ToString());
                    }
                }

                if (jObject["printings"] != null)
                {
                    foreach (var printing in jObject.printings)
                    {
                        parsedCard.Printings.Add(printing.ToString());
                    }
                }

                if (jObject["legalities"] != null)
                {
                    foreach (var legality in jObject.legalities)
                    {
                        parsedCard.Legalities.Add(new MTGCardLegality(legality.format.ToString(), legality.legality.ToString()));
                    }
                }
            }
            
            return parsedCard;
        }

        private async Task<List<string>> ParseSetsJsonAsync(string json)
        {
            List<string> setsList = new List<string>();

            dynamic jObject = await Task.Run(() => JsonConvert.DeserializeObject(json));

            foreach(var set in jObject.sets)
            {
                setsList.Add((string)set.name);
            }

            setsList.Sort();

            return setsList;
        }
    }
}
