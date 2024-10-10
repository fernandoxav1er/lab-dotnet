using Lab.MemoryCache.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Lab.MemoryCache.WebApi.Controllers;

[Route("api/[controller]")]
public class BanksController : ControllerBase
{
    private readonly IDistributedCache _cache;
    private const string BanksKey = "Banks";

    public BanksController(IDistributedCache cache)
    {
        _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> GetBanks()
    {
        const string restBanksUrl = "https://brasilapi.com.br/api/banks/v1";

        var banksObj = await _cache.GetStringAsync(BanksKey);

        if (!string.IsNullOrEmpty(banksObj)) return Content(banksObj, "application/json");

        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(restBanksUrl);
            var responseData = await response.Content.ReadAsStringAsync();
            var banks = JsonConvert.DeserializeObject<List<Bank>>(responseData);

            var memoryCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1200)
            };
            await _cache.SetStringAsync(BanksKey, responseData, memoryCacheEntryOptions);

            return Ok(banks);
        }

    }

}
