using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TicketSystemApi.Models;
using TicketSystemApi.Persistance.Data;
using TicketSystemApi.Persistance.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketApiController : ControllerBase
    {
        private readonly ITicketServices _ticketServices;
        public TicketApiController(ITicketServices ticketServices)
        {
            _ticketServices = ticketServices;
        }

        [HttpGet]
        [Route("getTickets")]
        public ActionResult<IEnumerable<Ticket>> GetAllTickets()
        {
            return Ok(_ticketServices.GetAllTickets());
        }

        [HttpPost]
        [Route("CreateTicket")]
        public ActionResult<TicketViewModel> CreateTicket(TicketViewModel ticket)
        {
            if (ModelState.IsValid)
            {
                var result =  _ticketServices.CreatTicket(ticket);
                return (result!=null)? Ok(result):BadRequest("User already have ticket.");
            }
            return BadRequest(ModelState.ValidationState);
        }

        [HttpGet]
        [Route("GetTicketByNum")]
        public ActionResult<TicketViewModel> GetTicket(string MobileNumber)
        {
            if (MobileNumber == null || MobileNumber.Length != 11)
            {
                return BadRequest("Invaild Phonenumber");
            }
            else
            {
                var result = _ticketServices.GetTicket(MobileNumber);
                return (result != null) ? Ok(result) : BadRequest("No ticket was found.");
            }
        }

      
    }
}
