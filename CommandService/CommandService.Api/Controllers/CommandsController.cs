namespace CommandService.Api.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    public class CommandsController: BaseApiController<CommandsController>
    {
        private readonly ICommandDataProvider _commandDataProvider;
        private readonly IMapper _mapper;

        public CommandsController(
            ILogger<CommandsController> logger,
            IMapper mapper,
            ICommandDataProvider commandDataProvider
        ): base(logger)
        {
            _commandDataProvider = commandDataProvider;
            _mapper = mapper;
        }

    [HttpGet()]
    public async Task<IActionResult> GetCommandsForPlatform(int platformId)
    {
        return await TryAsync(async () =>
        {
            var commands = await _commandDataProvider.GetCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandViewModel>>(commands));
        });
    }

    [HttpGet("{commandId}", Name ="GetCommand")]
    public async Task<IActionResult> GetCommand(int platformId, int commandId)
    {
        return await TryAsync(async () =>
        {
            var command = await _commandDataProvider.GetCommand(platformId, commandId);
            return Ok(_mapper.Map<CommandViewModel>(command));
        });
    }

    [HttpPost()]
    public async Task<IActionResult> CreateCommandForPlatform(int platformId, CommandCreateModel command)
    {
        return await TryAsync(async () =>
        {
            var newCommand = _mapper.Map<Command>(command);
            newCommand = await _commandDataProvider.AddCommand(platformId, newCommand);
            var viewModel = _mapper.Map<CommandViewModel>(newCommand);
            return CreatedAtRoute(nameof(GetCommand), new {platformId = viewModel.PlatformId, commandId = viewModel.Id}, viewModel);
        });
    }
    }
}