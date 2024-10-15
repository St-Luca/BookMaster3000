namespace Domain.Entities
{
    public class Issue
    {
        public int Id { get; set; } 
        public int BookId { get; set; }
        public int ClientId { get; set; }
        public int AdminId { get; set; }
        public DateTime IssueFrom { get; set; }
        public DateTime IssueTo { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Book Book { get; set; }
        public Client Client { get; set; }
    }
}