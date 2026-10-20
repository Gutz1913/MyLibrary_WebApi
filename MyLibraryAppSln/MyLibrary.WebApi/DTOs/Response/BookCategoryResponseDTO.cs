namespace MyLibrary.WebApi.DTOs.Response;

public class BookCategoryResponseDTO
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; }
}
