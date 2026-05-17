namespace SudeNaz2048.İletişim.Hareket;

public static class HareketEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/hareket/{yon}", Handle);
    }

    private static IResult Handle(string yon)
    {
        string dir = yon.ToLower();
        if (dir != "sol" && dir != "sag" && dir != "yukari" && dir != "asagi")
        {
            return Results.BadRequest(new { error = "Gecersiz Yon" });
        }
        OyunDurumu.Current.Hareket(dir);
        return Results.Ok(OyunDurumu.Current.ToResponse());
    }
}