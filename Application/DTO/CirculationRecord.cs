namespace Application.DTO;

public class CirculationRecord
{
    public string BookTitle { get; set; } = null!;
    public DateTime IssueFrom { get; set; }
    public DateTime IssueTo { get; set; }
    public bool IsOverdue => IssueTo > DateTime.Now;
}
