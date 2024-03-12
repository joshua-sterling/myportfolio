
using myportfolio.Server.Models;

namespace myportfolio.Server.DataAccess
{
    public class RowingEventRepository
    {
        private readonly MyPortfolioContext _context;

        public RowingEventRepository(MyPortfolioContext context)
        {
            _context = context;
        }

        public List<RowingEvent> GetRowingEvents()
        {
            return _context.RowingEvents.ToList();
        }

        public int AddRowingEvent(RowingEvent rowingEvent)
        {
            _context.RowingEvents.Add(rowingEvent);
            return _context.SaveChanges();
        }

        public RowingEvent? GetRowingEvent(int id)
        {
            return _context.RowingEvents.FirstOrDefault(x => x.Id == id);
        }

        public int SaveChanges()
        {            
            return _context.SaveChanges();
        }
    }
}
