namespace TradeSpendDashboard.Models.Pagination
{
    public class PaginationSort
    {
        public string selector { get; set; }
        public bool desc { get; set; }
        public string orderBy => desc ? $"{selector} desc" : $"{selector} asc";
    }
}