using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tennis_Mate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NamesController: ControllerBase
    {
        private static readonly string[] NamesToPick = new[]
        {
            "Mike", "Emily", "Nate", "Matt"
        };

        private readonly ILogger<NamesController> _logger;

        public NamesController(ILogger<NamesController> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Names> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 4).Select(index => new Names
            {
                Name = NamesToPick[rng.Next(NamesToPick.Length)]
            }).ToArray();
        }

    }
}
