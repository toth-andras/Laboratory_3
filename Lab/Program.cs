using Lab;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.MapGet("/", () => "Hello");
app.MapGet("/text", () => "text");
app.MapGet("/regex/", Handler.RegexQuery);
app.MapGet("/random", Handler.Random);
app.MapPost("/initialize", Handler.Initialize);

app.UseSwagger();
app.UseSwaggerUI();

app.Run();