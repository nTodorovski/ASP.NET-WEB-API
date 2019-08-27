using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business;
using DomainModels;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [Route("createticket")]
        [HttpPost]
        public IActionResult PlaceTicket([FromBody] TicketModel model)
        {
            var userId = GetUserId();
            _ticketService.CreateTicket(userId,model);
            return Ok($"A ticket was placed with combination: {model.Combination}");
        }

        [Authorize]
        [Route("byUser/{id}")]
        [HttpGet]
        public IEnumerable<TicketModel> GetByUser()
        {
            var userId = GetUserId();
            return _ticketService.GetAllByUser(userId);
        }

        private int GetUserId()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out var userId))
                throw new Exception("Invalid user Id");

            return userId;
        }
    }
}