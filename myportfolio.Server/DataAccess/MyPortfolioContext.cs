using Microsoft.EntityFrameworkCore;
using myportfolio.Server.Models;

namespace myportfolio.Server.DataAccess
{
    public class MyPortfolioContext : DbContext
    {
        public MyPortfolioContext(DbContextOptions<MyPortfolioContext> options) : base(options)
        {
        }

        public DbSet<RowingEvent> RowingEvents { get; set; }
    }
}
