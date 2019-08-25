using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lotto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundResultsController : ControllerBase
    {
        private readonly IRoundResultService _roundResultService;

        public RoundResultsController(IRoundResultService roundResultService)
        {
            _roundResultService = roundResultService;
        }

        [Route("drawround")]
        [HttpPost]
        public IActionResult DrawRound()
        {
            _roundResultService.Draw();
            return Ok($"A new round has started!");
        }

        [Route("getresult")]
        [HttpGet]
        public IEnumerable<RoundResult> GetResults()
        {
            return _roundResultService.GetAll();
        }
    }
}