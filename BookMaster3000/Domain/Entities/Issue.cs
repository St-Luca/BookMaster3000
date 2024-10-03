namespace Domain.Entities
{
    public class Issue
    {
        public long Id { get; set; } 
        public string BookKey { get; set; }
        public string ClientId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Book Book { get; set; }
        public Client Client { get; set; }
    }
}