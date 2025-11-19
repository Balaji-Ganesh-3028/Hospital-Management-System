namespace DataAccessLayer.Models
{
    public class UserDetailsQuery
    {
        public string? SearchTerm { get; set; }
        public string? UserType { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortByOrder { get; set; }
        public string? SortByColumn { get; set; }
    }
}
