namespace BookStoreApp.API.Model.Book
{
    public class BookDetailDto : BaseDTO
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }  
        public decimal Price { get; set; }  
        public int AuthorID { get; set; }  
        public string AuthorName { get; set; }  
    }
}
