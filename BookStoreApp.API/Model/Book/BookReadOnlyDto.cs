namespace BookStoreApp.API.Model.Book
{
    public class BookReadOnlyDto : BaseDTO
    {
        public string? Title { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }
        public int? AuthorNameAuthorID { get; set; }
        public string? AuthorName { get; set; }
    }
}
