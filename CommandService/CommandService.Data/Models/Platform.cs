namespace CommandService.Data.Models;

public class Platform: BaseModel<Platform>
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int ExternalId { get; set; }
    [Required]
    public string Name { get; set; }
    public ICollection<Command> Commands { get; set; }

}
