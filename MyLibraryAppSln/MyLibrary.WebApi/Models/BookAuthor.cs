namespace MyLibrary.WebApi.Models;

public class BookAuthor : BaseModel
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }
    public DateTime RegisteredAt { get; set; }
    public Book Book { get; set; } = null!;
    public Author Author { get; set; } = null!;
}

