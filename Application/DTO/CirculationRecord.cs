namespace Application.DTO;

public class CirculationRecord
{
    public string BookTitle { get; set; }
    public DateTime IssueFrom { get; set; }
    public DateTime IssueTo { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsOverdue => IssueTo > DateTime.Now;
}
