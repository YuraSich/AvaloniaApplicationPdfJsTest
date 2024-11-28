using EmbedIO;

namespace AvaloniaApplication1.ViewModels;

public static class LocalHttpServer
{
    private static WebServer? _server;

    public static void Start(string rootPath, int port = 8080)
    {
        if (_server != null) return;

        // Создаем сервер с правильными MIME-типами
        _server = new WebServer(o => o
                .WithUrlPrefix($"http://localhost:{port}/")
                .WithMode(HttpListenerMode.EmbedIO))
            .WithLocalSessionManager()
            .WithStaticFolder("/", rootPath, true, m =>
            {
                m.WithCustomMimeType(".mjs", "application/javascript");
            });

        _server.RunAsync();
    }

    public static void Stop()
    {
        _server?.Dispose();
    }
}