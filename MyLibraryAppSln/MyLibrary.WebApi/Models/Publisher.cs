namespace MyLibrary.WebApi.Models;

public class Publisher : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string WebSite { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}

