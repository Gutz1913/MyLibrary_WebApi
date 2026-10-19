namespace MyLibrary.WebApi.Models;

public class Book : BaseModel
{
    public string Isbn { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public int PublicationYear { get; set; }
    public string Language { get; set; } = string.Empty;
    public int? NumPages { get; set; }
    public string? Synopsis { get; set; }
    public bool Active { get; set; } = true;
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; } = null!;
    public ICollection<Copy> Copies { get; set; } = new List<Copy>();
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}

