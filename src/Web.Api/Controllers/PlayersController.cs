using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Settings;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ILoggerService _loggerService;

        public PlayersController(IPlayerRepository playerRepository, ILoggerService loggerService, IOptions<OrderOptions> op, IOptionsMonitor<MyOptions> myops)
        {
            _playerRepository = playerRepository;
            _loggerService = loggerService;
        }

        // GET api/players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> Get()
        {
            return await _playerRepository.ListAll();
        }

        // GET api/players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> Get(int id)
        {
            return await _playerRepository.GetById(id);
        }
    }
}
