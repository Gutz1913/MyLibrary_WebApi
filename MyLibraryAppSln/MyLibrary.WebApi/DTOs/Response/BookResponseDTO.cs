namespace MyLibrary.WebApi.DTOs.Response;

public class BookResponseDTO
{
    public int Id { get; set; }
    public string Isbn { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public string Language { get; set; } = string.Empty;
    public int? NumPages { get; set; }
    public string? Synopsis { get; set; }
    public bool Active { get; set; } = true;
    public int CopiesCount { get; set; }
    public int PublisherId { get; set; }
    public string PublisherName { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
