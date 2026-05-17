using SudeNaz2048.İletişim.Hareket;
using SudeNaz2048.İletişim.TahtaDurumu;
using SudeNaz2048.İletişim.YeniOyun;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

app.UseCors();


YeniOyunEndpoint.Map(app);
HareketEndpoint.Map(app);
TahtaDurumuEndpoint.Map(app);
app.Run();


