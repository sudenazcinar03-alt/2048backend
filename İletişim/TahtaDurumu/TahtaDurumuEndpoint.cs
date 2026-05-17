namespace SudeNaz2048.İletişim.TahtaDurumu;

public static class TahtaDurumuEndpoint
/// tahtanın o anki durumunu webw bildiren yer
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/tahta-durumu", Handle); 
    }

    private static object Handle()
    {
        return OyunDurumu.Current.ToResponse();
    }
}