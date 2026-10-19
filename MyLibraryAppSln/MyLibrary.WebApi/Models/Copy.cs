namespace MyLibrary.WebApi.Models;

public class Copy : BaseModel
{
    public string Barcode { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime? AcquisitionDate { get; set; }
    public string? Condition { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    public ICollection<Reservation> AssignedReservations { get; set; } = new List<Reservation>();
}
