namespace Domain.Entities
{
    public class BookAuthor
    {
        public int BookKey { get; set; }
        public Book Book { get; set; }

        public int AuthorKey { get; set; }
        public Author Author { get; set; }
    }
}