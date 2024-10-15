namespace Application.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description {get; set;}

        public string Subtitle {get; set;}

        public DateTime PublicationDate {get; set;}

        public List<string> BookAuthors { get; set; } = new List<string>();
        public List<string> BookSubjects { get; set; } = new List<string>();
    }
}
