var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Get Configuration
var configuration = builder.Configuration;

// Add DbContext
builder.Services.AddPlatformDbContext(configuration);

// Add Dependencies
builder.Services.AddMapster();
builder.Services.AddProviders();
builder.Services.AddSyncDataService();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Register gRPC endpoint
app.MapGrpcService<GrpcPlatformService>();

await app.ApplyPlatformDbContextMigrations();

app.Run();
