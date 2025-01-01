namespace BookStoreApp.API.Data
{
    public class CLassTable1
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Bio { get; set; }

        public virtual ICollection<CLassTable1> Books { get; set; } = new List<CLassTable1>();
    }
}
