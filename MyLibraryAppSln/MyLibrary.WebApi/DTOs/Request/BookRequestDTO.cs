namespace MyLibrary.WebApi.DTOs.Request;

public class BookRequestDTO
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
}
