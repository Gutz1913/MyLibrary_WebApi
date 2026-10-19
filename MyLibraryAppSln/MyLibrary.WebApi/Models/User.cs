namespace MyLibrary.WebApi.Models;

public class User : BaseModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string AspNetUserId { get; set; } = string.Empty;
    public ApplicationUser AspNetUser { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    public ICollection<Fine> Fines { get; set; } = new List<Fine>();
}

