using HackathonBackend.Comms;
// GithubApi.SearchGit("FreddyBicandy50", "ft_printf");
// await GeminiApi.SendToAiAsync(
//     "This code is my try to recreate a library containing some basic operations and function of the c language",
//     File.ReadAllBytes("Created/code.txt"));
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseHttpsRedirection();

app.MapControllers();

app.Run();