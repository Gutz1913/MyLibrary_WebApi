namespace MyLibrary.WebApi.DTOs.Request;

public class FineRequestDTO
{
    public int LoanId { get; set; }
    public int UserId { get; set; }
    public DateTime FineDate { get; set; }
    public string? Reason { get; set; }
    public decimal Amount { get; set; }
    public string State { get; set; } = "Pending";
    public DateTime? PaymentDate { get; set; } = null;
}
