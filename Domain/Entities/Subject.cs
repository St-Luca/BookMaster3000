namespace Domain.Entities
{
    public class Subject
    {
        public long Id { get; set; }  // Уникальный идентификатор темы
        public string Name { get; set; }

        public ICollection<BookSubject> BookSubjects { get; set; }
    }
}