namespace MyLibrary.WebApi.DTOs.Request;

public class PublisherRequestDTO
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string WebSite { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
}
