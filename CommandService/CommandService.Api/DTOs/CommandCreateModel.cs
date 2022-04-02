namespace CommandService.Api.DTOs
{
    public struct CommandCreateModel
    {
        [Required]
        public string HowTo { get; init; }
        [Required]
        public string CommandLine { get; init; }
    }
}