namespace MyLibrary.WebApi.Models;

public class Fine : BaseModel
{
    public int LoanId { get; set; }
    public int UserId { get; set; }
    public DateTime FineDate { get; set; }
    public string? Reason { get; set; }
    public decimal Amount { get; set; }
    public string State { get; set; } = "Pending";
    public DateTime? PaymentDate { get; set; }
    public Loan Loan { get; set; } = null!;
    public User User { get; set; } = null!;
}
