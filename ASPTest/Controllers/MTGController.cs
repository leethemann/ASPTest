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

// TODO: Need to implement Deck Builder, this will require a DB to store decks in.

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

        // GET: /MTG/CardSearch?searchstuff
        public async Task<IActionResult> CardSearch(string nameFilter, string setFilter, List<MTGColorFilter> colorFilters, string typeFilter)
        {
            MTGCardSearchViewModel data = new MTGCardSearchViewModel();

            await Task.Run(() => _apiService.ClearFilters());

            data.Cards = await GetCardsForDisplayAsync(nameFilter, setFilter, colorFilters, typeFilter);
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

        public async Task<List<MTGCardSimple>> GetCardsForDisplayAsync(string nameFilter, string setFilter, List<MTGColorFilter> colorFilters, string typeFilter)
        {
            List<MTGCardSimple> data = new List<MTGCardSimple>();
            
            _apiService.NameFilter = nameFilter;
            _apiService.ColorFilter = colorFilters;
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
                MTGCardSimple newCard = JsonConvert.DeserializeObject<MTGCardSimple>(card.ToString());

                parsedCards.Add(newCard);
            }

            return parsedCards;
        }
        
        private async Task<MTGCardDetail> ParseDetailCardJsonAsync(string cardsJson)
        {
            MTGCardDetail parsedCard = new MTGCardDetail();

            dynamic jObject = await Task.Run(() => JsonConvert.DeserializeObject(cardsJson));
            
            if (jObject.card != null)
            {
                jObject = jObject.card;

                parsedCard = JsonConvert.DeserializeObject<MTGCardDetail>(jObject.ToString());
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
