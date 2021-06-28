using System.Threading.Tasks;
using BLL.Services;
using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("btcRate")]
    [Authorize]
    public class BtcRateController : ControllerBase
    {
        private readonly BtcRateService _btcRateService;

        public BtcRateController(BtcRateService btcRateService)
        {
            _btcRateService = btcRateService;
        }

        [HttpGet]
        public Task<BtcUahRate?> GetUahRate()
        {
            return _btcRateService.GetBtcUahRate();
        }
    }
}