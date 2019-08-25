using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Lotto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // POST api/<controller>
        [Route("createticket")]
        [HttpPost]
        public IActionResult PlaceTicket([FromBody] TicketModel model)
        {
            _ticketService.CreateTicket(model);
            return Ok($"A ticket was placed with combination: {model.Combination}");
        }

        [Route("byUser/{id}")]
        [HttpGet]
        public IEnumerable<TicketModel> GetByUser(int id)
        {
            return _ticketService.GetAllByUser(id);
        }
    }
}