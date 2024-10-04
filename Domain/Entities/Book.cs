namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsRented { get; set; }

        public string Description {get; set;}

        public string Subtitle {get; set;}

        public string PublicationDate {get; set;}

        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<BookSubject> BookSubjects { get; set; }

    }
}