

namespace SudeNaz2048.İletişim.YeniOyun;

public static class YeniOyunEndpoint

{
    public static void Map(WebApplication app)
    {
        app.MapPost("/new", Handle);
    }

    private static object Handle()
    {
        OyunDurumu.Current.New();
        return OyunDurumu.Current.ToResponse();
    }
}