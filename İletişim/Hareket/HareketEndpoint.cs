namespace SudeNaz2048.İletişim.Hareket;

public static class HareketEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/hareket/{direction}", Handle);
    }

    private static IResult Handle(string direction)
    {
        string dir = direction.ToLower();
        if (dir != "left" && dir != "right" && dir != "up" && dir != "down")
        {
            return Results.BadRequest(new {error="Gecersiz Yon"});
        }
        OyunDurumu.Current.Hareket(dir);
        return Results.Ok(OyunDurumu.Current.ToResponse());
    }
}