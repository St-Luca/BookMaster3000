using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Issue
{
    public int Id { get; set; } 

    public int BookId { get; set; }

    public Book Book { get; set; }

    [ForeignKey("ClientCardId")]
    public int ClientCardId { get; set; }

    public ClientCard ClientCard { get; set; }

    public DateTime IssueFrom { get; set; }

    public DateTime IssueTo { get; set; }

    public DateTime? ReturnDate { get; set; }


    [NotMapped]
    public bool IsRenewed {  get; set; }

    public void RenewReturnDateByWeek()
    {
        IssueTo = IssueTo.AddDays(7);
        IsRenewed = true;
    }
}