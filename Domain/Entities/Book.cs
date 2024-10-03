namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsRented { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<BookSubject> BookSubjects { get; set; }

    }
}