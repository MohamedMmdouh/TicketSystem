using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TicketSystemApi.Models;
using TicketSystemApi.Models.ApplicationContext;
using TicketSystemApi.Persistance.Interfaces;

namespace TicketSystemApi.Persistance.Services
{
    public class BaseReposiitry<T> : IBaseRepositry<T> where T : class
    {
        protected ApplicationDbContext _context;
        private readonly IUnitOFWork _unitOFWork;

        public BaseReposiitry(ApplicationDbContext context,IUnitOFWork unitOFWork)
        {
            _context = context;
            _unitOFWork = unitOFWork;
        }

        public T AddTicket(T model)
        {
            _context.Set<T>().Add(model);
            _unitOFWork.Complete();
            return model;
        }

        public IEnumerable<T> GetAllTickets(Expression<Func<T, object>> predicate)
        {
            return _context.Set<T>().OrderByDescending(predicate).ToList();
        }

   
        public T getTicket(string userid)
        {
            return _context.Set<T>().Find(userid);
        }

        public User GetTByMobileNumber(string mobilenumber)
        {
            return _context.User.Where(x=>x.PhoneNumber==mobilenumber).Include(x=>x.ticket).FirstOrDefault();
        }

        public T getlastTicketnumber(Expression<Func<T, object>> predicate)
        {
            return _context.Set<T>().OrderByDescending(predicate).FirstOrDefault();
        }

    }
}
