namespace Domain.Entities
{
    public class BookSubject
    {
        public string BookKey { get; set; }  // Foreign Key для книги
        public Book Book { get; set; }

        public long SubjectId { get; set; }  // Foreign Key для темы
        public Subject Subject { get; set; }
    }
}