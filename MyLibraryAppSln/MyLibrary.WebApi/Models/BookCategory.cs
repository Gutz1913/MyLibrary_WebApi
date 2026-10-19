namespace MyLibrary.WebApi.Models;

public class BookCategory : BaseModel
{
    public int BookId { get; set; }
    public int CategoryId { get; set; }
    public DateTime RegisteredAt { get; set; }
    public Book Book { get; set; } = null!;
    public Category Category { get; set; } = null!;
}
