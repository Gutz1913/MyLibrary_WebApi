namespace MyLibrary.WebApi.DTOs.Request;

public class CopyRequestDTO
{
    public string Barcode { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime? AcquisitionDate { get; set; }
    public string? Condition { get; set; }
    public int BookId { get; set; }
}
