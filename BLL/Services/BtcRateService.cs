using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Common.Dto;
using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public class BtcRateService
    {
        private const string CoinBaseApi = "https://rest.coinapi.io/v1";
        
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public BtcRateService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        /// <summary>
        /// Returns BTC to UAH exchange rate
        /// </summary>
        /// <returns></returns>
        public Task<BtcUahRate?> GetBtcUahRate()
        {
            var httpClient = _httpClientFactory.CreateClient();
            
            httpClient.DefaultRequestHeaders.Add("X-CoinAPI-Key", _configuration["CoinAPIKey"]);
                
            return httpClient.GetFromJsonAsync<BtcUahRate>($"{CoinBaseApi}/exchangerate/BTC/UAH");
        }
    }
}