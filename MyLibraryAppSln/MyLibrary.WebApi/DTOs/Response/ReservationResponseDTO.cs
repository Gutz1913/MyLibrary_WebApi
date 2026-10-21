namespace MyLibrary.WebApi.DTOs.Response;

public class ReservationResponseDTO
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public DateTime ReservationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string State { get; set; } = "Pending";
    public int? IdAssignedCopy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
