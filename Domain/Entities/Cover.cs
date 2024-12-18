namespace Domain.Entities
{
    public class Cover
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}