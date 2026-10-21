namespace MyLibrary.WebApi.DTOs.Request;

public class LoanRequestDTO
{
    public int CopyId { get; set; }
    public int UserId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string State { get; set; } = "Active";
}
