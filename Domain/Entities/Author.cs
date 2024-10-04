namespace Domain.Entities
{
    public class Author
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string BirthDate { get; set; }
        public string DeathDate { get; set; }
        public string Wikipedia { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}