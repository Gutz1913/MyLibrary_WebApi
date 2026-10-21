namespace MyLibrary.WebApi.DTOs.Response;

public class FineResponseDTO
{
    public int Id { get; set; }
    public int LoanId { get; set; }
    public int UserId { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public DateTime FineDate { get; set; }
    public string? Reason { get; set; }
    public decimal Amount { get; set; }
    public string State { get; set; } = "Pending";
    public DateTime? PaymentDate { get; set; } 
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
