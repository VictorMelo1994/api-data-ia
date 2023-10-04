using System.ComponentModel.DataAnnotations;

public class GifUploadModel
{
    [Required]
    public required IFormFile Gif { get; set; }

    [Required]
    public required string Label { get; set; }
}
