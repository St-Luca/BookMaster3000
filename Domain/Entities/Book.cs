namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        public string Description {get; set;}

        public string Subtitle {get; set;}

        public DateTime PublicationDate {get; set;}
        public ICollection<Cover> Covers { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<BookSubject> BookSubjects { get; set; }

    }
}