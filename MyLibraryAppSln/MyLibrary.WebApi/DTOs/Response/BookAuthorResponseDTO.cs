namespace MyLibrary.WebApi.DTOs.Response;

public class BookAuthorResponseDTO
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; }
}
