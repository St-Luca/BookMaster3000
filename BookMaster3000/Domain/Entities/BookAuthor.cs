namespace Domain.Entities
{
    public class BookAuthor
    {
        public string BookKey { get; set; }
        public Book Book { get; set; }

        public string AuthorKey { get; set; }
        public Author Author { get; set; }
    }
}