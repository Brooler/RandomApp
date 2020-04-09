using Microsoft.AspNetCore.Mvc;
using RandomApp.Api.Interfaces;

namespace RandomApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private readonly IRandomService _randomService;

        public RandomController(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public IActionResult Get()
        {
            //range from 1 to 100 inclusive as it was not mentioned in task should range be inclusive or exclusive
            return Ok(_randomService.GetRandomNumber(1, 101));
        }
    }
}