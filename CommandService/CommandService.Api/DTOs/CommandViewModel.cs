namespace CommandService.Api.DTOs
{
    public struct CommandViewModel
    {
        public int Id { get; init; }
        public string HowTo { get; init; }
        public string CommandLine { get; init; }
        public int PlatformId { get; init; }
    }
}