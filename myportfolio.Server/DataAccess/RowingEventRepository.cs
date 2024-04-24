
using Microsoft.EntityFrameworkCore;
using myportfolio.Server.Models;
using System.Linq;

namespace myportfolio.Server.DataAccess
{
    public class RowingEventRepository(MyPortfolioContext context)
    {
        private readonly MyPortfolioContext _context = context;

        public async Task<List<RowingEvent>> GetRowingEventsAsync(TableOptions tableOptions)
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

            return await  query.Skip(tableOptions.Skip).Take(tableOptions.Take).ToListAsync();
            
        }

        public async Task<int> GetRowingEventsCountAsync()
        {
            return await _context.RowingEvents.CountAsync();
        }

        public async Task<int> AddRowingEventAsync(RowingEvent rowingEvent)
        {
            _context.RowingEvents.Add(rowingEvent);  //Microsoft documentation advises against using AddAsync
            return await _context.SaveChangesAsync(); //Always await EF Core asynchronous methods immediately.
        }

        public async Task<RowingEvent?> GetRowingEventAsync(int id)
        {
            return await _context.RowingEvents.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {            
            return await _context.SaveChangesAsync();
        }

        public IQueryable<RowingEvent> GetRowingEvents()
        {
            return _context.RowingEvents;
        }
    }
}
