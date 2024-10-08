namespace Application.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description {get; set;}

        public string Subtitle {get; set;}

        public string PublicationDate {get; set;}

        public List<string> BookAuthors { get; set; }
        public List<string> BookSubjects { get; set; }
    }
}
