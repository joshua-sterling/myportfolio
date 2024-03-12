using System.ComponentModel.DataAnnotations;

namespace myportfolio.Server.Controllers.ViewModels
{
    public class RowingEventViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Distance must be greater than 0")]
        public int Distance { get; set; }
        [Required]
        public TimeSpan TotalTime { get; set; }
        [Required]
        public DateOnly EventDate { get; set; }
        [Required]
        [Range(1, 50, ErrorMessage = "Stroke rate must be between 1 and 50")]
        public int StrokeRate { get; set; }
    }
}
