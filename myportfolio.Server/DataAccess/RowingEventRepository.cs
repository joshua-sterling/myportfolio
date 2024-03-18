
using Microsoft.EntityFrameworkCore;
using myportfolio.Server.Models;
using System.Linq;

namespace myportfolio.Server.DataAccess
{
    public class RowingEventRepository
    {
        private readonly MyPortfolioContext _context;

        public RowingEventRepository(MyPortfolioContext context)
        {
            _context = context;
        }

        public List<RowingEvent> GetRowingEvents(TableOptions tableOptions)
        {

            var query = _context.RowingEvents.AsQueryable();
            if (!string.IsNullOrEmpty(tableOptions.SortColumn))
            {
                //Angular properties are camelCase, so we need to convert to PascalCase
                var sortColumnPascalCase = char.ToUpperInvariant(tableOptions.SortColumn[0]) + tableOptions.SortColumn.Substring(1);

                if (tableOptions.SortAscending)
                {
                    query = query.OrderBy(prop => EF.Property<object>(prop, sortColumnPascalCase));
                }
                else
                {
                    query = query.OrderByDescending(prop => EF.Property<object>(prop, sortColumnPascalCase));
                }
            }

            return query.Skip(tableOptions.Skip).Take(tableOptions.Take).ToList();
            
        }

        public int GetRowingEventsCount()
        {
            return _context.RowingEvents.Count();
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

        public IQueryable<RowingEvent> GetRowingEvents()
        {
            return _context.RowingEvents;
        }
    }
}
