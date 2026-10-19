namespace MyLibrary.WebApi.Models;

public class Reservation : BaseModel
{
    public int BookId { get; set; }
    public int UserId { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string State { get; set; } = "Pending";
    public int? IdAssignedCopy { get; set; }
    public Book Book { get; set; } = null!;
    public User User { get; set; } = null!;
    public Copy? AssignedCopy { get; set; }
}

