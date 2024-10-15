namespace Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public string Wikipedia { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}