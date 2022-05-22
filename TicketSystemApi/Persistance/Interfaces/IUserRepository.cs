using System.Threading.Tasks;
using TicketSystemApi.Models;
using TicketSystemApi.Persistance.Data;

namespace TicketSystemApi.Persistance.Interfaces
{
    public interface IUserRepository
    {
        Task<SignIn_Result> RegisterModel(RegisterModel model);
        Task<SignIn_Result> GetTokenAsync(TokenRequestModel model);

        User GetUser(string mobilenum);
        bool isexistTicket(string userid);
    }
}
