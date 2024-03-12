using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace myportfolio.Server.Models
{
    public class RowingEvent
    {
        public int Id { get; set; }
        public int Distance { get; set; }
        public TimeSpan TotalTime { get; set; }
        public DateOnly EventDate { get; set; }
        public int StrokeRate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateAdded { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateUpdated { get; set; }
    }
}
