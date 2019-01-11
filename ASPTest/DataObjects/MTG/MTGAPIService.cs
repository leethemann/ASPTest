using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using ASPTest.Models.MTG;

namespace ASPTest.DataObjects.MTG
{
    public class MTGAPIService
    {
        #region Properties
        private const string RootAPIPath = @"https://api.magicthegathering.io/v1";
        private bool _filterSet = false;
        private string _nameFilter;
        private List<MTGColorFilter> _colorFilters;
        private string _setFilter;
        private string _typeFilter;

        public string NameFilter
        {
            get
            {
                return _nameFilter;
            }
            set
            {
                if(!string.IsNullOrEmpty(value))
                {
                    _nameFilter = value;
                    _filterSet = true;
                }
            }
        }

        public List<MTGColorFilter> ColorFilter
        {
            get
            {
                return _colorFilters;
            }
            set
            {
                if (value != null)
                {
                    _colorFilters = value;
                    _filterSet = true;
                }
            }
        }

        public string SetFilter
        {
            get
            {
                return _setFilter;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _setFilter = value;
                    _filterSet = true;
                }
            }
        }

        public string TypeFilter
        {
            get
            {
                return _typeFilter;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _typeFilter = value;
                    _filterSet = true;
                }
            }
        }
        #endregion

        #region Public Methods
        public async Task<string> GetCardsAsync()
        {
            string url = RootAPIPath + "/cards";
            
            url = ApplyFiltersToUrl(url);

            return await SendRequest(url);
        }

        public async Task<string> GetAllSetsAsync()
        {
            string url = RootAPIPath + "/sets";

            return await SendRequest(url);
        }

        public async Task<string> GetCardByIDAsync(string id)
        {
            string url = RootAPIPath + "/cards/" + id;

            return await SendRequest(url);
        }

        public void ClearFilters()
        {
            _nameFilter = null;
            _colorFilters = null;
            _setFilter = null;
            _typeFilter = null;
            _filterSet = false;
        }
        #endregion

        #region Private Methods
        private async Task<string> SendRequest(string url)
        {
            string jsonResponse;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonResponse = reader.ReadToEnd();
            }

            return jsonResponse;
        }

        private string ApplyFiltersToUrl(string url)
        {
            if (_filterSet)
            {
                int filterAppliedCount = 0;
                url += "?";

                if (!string.IsNullOrEmpty(_nameFilter))
                {
                    //This filter is always applied first, so we don't need to check for applied filters
                    url = string.Format(url + "name={0}", _nameFilter);
                    filterAppliedCount += 1;
                }

                if (!string.IsNullOrEmpty(_setFilter))
                {
                    if (filterAppliedCount > 0)
                        url += '&';

                    url = string.Format(url + "setName={0}", _setFilter);
                    filterAppliedCount += 1;
                }

                if (_colorFilters != null)
                {
                    if (filterAppliedCount > 0)
                        url += '&';
                    
                    url = url + "colors=";

                    // We need to get all the colors that were selected to filter on, then create a comma separated string of the colors.
                    string colorString = string.Join(',', _colorFilters.Where(filter => filter.Selected).Select(filter => filter.Color));

                    url = url + colorString;
                    
                    filterAppliedCount += 1;
                }

                if (!string.IsNullOrEmpty(_typeFilter))
                {
                    if (filterAppliedCount > 0)
                        url += '&';

                    url = string.Format(url + "type={0}", _typeFilter);
                    filterAppliedCount += 1;
                }
            }

            return url;
        }
        #endregion
    }
}
