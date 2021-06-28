using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Common.Dto;
using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public class BtcRateService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public BtcRateService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public Task<BtcUahRate?> GetBtcUahRate()
        {
            var httpClient = _httpClientFactory.CreateClient();
            
            httpClient.DefaultRequestHeaders.Add("X-CoinAPI-Key", _configuration["CoinAPIKey"]);
                
            return httpClient.GetFromJsonAsync<BtcUahRate>("https://rest.coinapi.io/v1/exchangerate/BTC/UAH");
        }
    }
}