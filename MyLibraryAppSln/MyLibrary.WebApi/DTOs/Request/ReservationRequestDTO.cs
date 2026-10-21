namespace MyLibrary.WebApi.DTOs.Request;

public class ReservationRequestDTO
{
    public int BookId { get; set; }
    public int UserId { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string State { get; set; } = "Pending";
    public int? IdAssignedCopy { get; set; }
}
