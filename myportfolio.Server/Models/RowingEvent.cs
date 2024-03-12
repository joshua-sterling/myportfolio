namespace myportfolio.Server.Models
{
    public class RowingEvent
    {
        public int Id { get; set; }
        public int Distance { get; set; }
        public TimeSpan TotalTime { get; set; }
        public DateOnly EventDate { get; set; }
        public int StrokeRate { get; set; }
    }
}
