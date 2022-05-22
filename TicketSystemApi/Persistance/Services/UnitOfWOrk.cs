using System.Threading.Tasks;
using TicketSystemApi.Models.ApplicationContext;
using TicketSystemApi.Persistance.Interfaces;

namespace TicketSystemApi.Persistance.Services
{
    public class UnitOfWOrk : IUnitOFWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWOrk(ApplicationDbContext Context)
        {
            _context = Context;
        }
        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }
    }
}
