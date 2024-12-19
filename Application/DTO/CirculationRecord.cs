namespace Application.DTO;

public class CirculationRecord
{
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public string BookSubtitle { get; set; }
    public int ClientId { get; set; }
    public string ClientName { get; set; }
    public DateTime IssueFrom { get; set; }
    public DateTime IssueTo { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsOverdue => IssueTo < DateTime.Now;
}
