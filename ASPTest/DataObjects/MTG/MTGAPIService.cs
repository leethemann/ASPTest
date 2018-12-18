using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace ASPTest.DataObjects.MTG
{
    public class MTGAPIService
    {
        #region Properties
        private const string RootAPIPath = @"https://api.magicthegathering.io/v1";
        private bool _filterSet = false;
        private string _nameFilter;
        private string _colorFilter;
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

        public string ColorFilter
        {
            get
            {
                return _colorFilter;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _colorFilter = value;
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
        public async Task<string> RequestCards()
        {
            string url = RootAPIPath + "/cards";
            
            url = ApplyFiltersToUrl(url);

            return await SendRequest(url);
        }

        public async Task<string> GetAllSets()
        {
            string url = RootAPIPath + "/sets";

            return await SendRequest(url);
        }

        public void ClearFilters()
        {
            _nameFilter = null;
            _colorFilter = null;
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

                if (!string.IsNullOrEmpty(_colorFilter))
                {
                    if (filterAppliedCount > 0)
                        url += '&';

                    url = string.Format(url + "colors={0}", _colorFilter);
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
