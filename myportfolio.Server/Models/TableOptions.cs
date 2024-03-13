namespace myportfolio.Server.Models
{
    public class TableOptions
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
        public string? SortColumn { get; set; }
        public bool SortAscending { get; set; } = true;
    }
}
