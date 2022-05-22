using AutoMapper;
using CoreHtmlToImage;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using TicketSystemApi.Helpers.ImageFormat;
using TicketSystemApi.Models;
using TicketSystemApi.Persistance.Data;
using TicketSystemApi.Persistance.Interfaces;

namespace TicketSystemApi.Persistance.Services
{
    public class TicketServices : ITicketServices
    {
        private readonly IBaseRepositry<Ticket> _baseRepositry;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;



        public TicketServices(IUserRepository userRepository, IMapper mapper, IWebHostEnvironment environment, IBaseRepositry<Ticket> baseRepositry)
        {
            _baseRepositry = baseRepositry;
            _mapper = mapper;
            _environment = environment;
            _userRepository = userRepository;
        }

        public TicketViewModel CreatTicket(TicketViewModel ticket)
        {
            if (_userRepository.isexistTicket(ticket.userid))
            {
                var Ticket = _mapper.Map<Ticket>(ticket);
                Ticket.TIcketNumber = returnlastTicketNumber();
                var path = ImageGenerator(ticket, Ticket.TIcketNumber);
                if (path == "")
                {
                    return null;
                }
                Ticket.imagepath = path;
                Ticket=_baseRepositry.AddTicket(Ticket);
                var result = _mapper.Map<TicketViewModel>(Ticket);
                return result;
            }
            return null;
        }

      

        public IEnumerable<Ticket> GetAllTickets()
        {
            return _baseRepositry.GetAllTickets(c => c.TIcketNumber);
        }

        public  TicketViewModel GetTicket(string MobileNumber)
        {
            try
            {
                var ticket = _userRepository.GetUser(MobileNumber);
                if (ticket == null || ticket.ticket == null)
                {
                    return null;
                }
                else
                {
                    var result = _mapper.Map<TicketViewModel>(ticket.ticket);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public string ImageGenerator(TicketViewModel ticket,int ticketnumber)
        {
            try
            {
                var converter = new HtmlConverter();
                var bytes = converter.FromHtmlString(IMageTextFormat.ImageGenerator(ticket,ticketnumber));
                _environment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                File.WriteAllBytes(_environment.WebRootPath + "\\Tickets\\"+ ticketnumber + ".jpeg", bytes);
                return _environment.WebRootPath + "\\Tickets\\" + ticketnumber + ".jpeg";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public int returnlastTicketNumber()
        {
            var lastcode = _baseRepositry.getlastTicketnumber(t=>t.TIcketNumber);
            return (lastcode == null) ? 1 : lastcode.TIcketNumber + 1;
        }

    }
}
