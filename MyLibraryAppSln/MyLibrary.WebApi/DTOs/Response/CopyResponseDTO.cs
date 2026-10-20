namespace MyLibrary.WebApi.DTOs.Response;

public class CopyResponseDTO
{
    public int Id { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime? AcquisitionDate { get; set; }
    public string? Condition { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
