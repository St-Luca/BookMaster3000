namespace Application.DTO
{
    public class ClientCardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }

        public List<CirculationRecord> IssuedBooks { get; set; } = [];
        public List<CirculationRecord> ReturnedBooks { get; set; } = [];
    }
}
