namespace MyLibrary.WebApi.Models;

public class Author : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string? Biography { get; set; }
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
}

