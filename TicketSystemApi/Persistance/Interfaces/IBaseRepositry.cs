using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TicketSystemApi.Models;

namespace TicketSystemApi.Persistance.Interfaces
{
    public interface IBaseRepositry<T> where T:class
    {
        IEnumerable<T> GetAllTickets(Expression<Func<T, object>> predicate);
        T AddTicket(T model);

        T getTicket(string userid);
        T getlastTicketnumber(Expression<Func<T, object>> predicate);
        User GetTByMobileNumber(string mobilenumber);

    }
}
