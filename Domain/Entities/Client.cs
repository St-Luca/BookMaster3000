namespace Domain.Entities
{
    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }
}