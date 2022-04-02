namespace PlatformService.DTOs;

public struct PlatformCreateModel
{
    [Required]
    public string Name { get; init; }
    [Required]
    public string Publisher { get; init; }
    [Required]
    public string Cost { get; init; }
}
