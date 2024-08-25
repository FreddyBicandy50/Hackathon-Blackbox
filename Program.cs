using HackathonBackend.Comms;
// GithubApi.SearchGit("FreddyBicandy50", "ft_printf");
// await GithubApi.SearchGit("https://github.com/FreddyBicandy50/ft_printf");
// await GeminiApi.SendToAiAsync(
//     "This code is my try to recreate a library containing some basic operations and function of the c language",
//     File.ReadAllBytes("Created/code.txt"));
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", 
        builder => builder.WithOrigins("http://100.123.178.21:5000").AllowAnyHeader().AllowAnyMethod());
    options.AddPolicy("AllowSpecificOrgin2", 
        builder => builder.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");
app.UserCors("AllowSpecificOrgin2");
app.MapControllers();

app.Run();
