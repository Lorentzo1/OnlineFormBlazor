using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OnlineForm.Models;
using OnlineFormApi.Services.Abstractions;

namespace OnlineFormApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> logger;
        private readonly IContryService contryService;

        public CountryController(ILogger<CountryController> logger, IContryService contryService)
        {
            this.logger = logger;
            this.contryService = contryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync()
        {
            var countries = await contryService.GetCountries();
            if (countries.Count() > 0)
            {
                return Ok(countries);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}