using URA_Twilio.Gateways;

dotenv.net.DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001);
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IRetellGateway, RetellGateway>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
