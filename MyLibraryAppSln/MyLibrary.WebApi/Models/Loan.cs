namespace MyLibrary.WebApi.Models;

public class Loan : BaseModel
{
    public int CopyId { get; set; }
    public int UserId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string State { get; set; } = "Active";
    public Copy Copy { get; set; } = null!;
    public User User { get; set; } = null!;
    public ICollection<Fine> Fines { get; set; } = new List<Fine>();
}

