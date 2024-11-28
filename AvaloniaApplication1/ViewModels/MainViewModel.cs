using CommunityToolkit.Mvvm.ComponentModel;
using DotNetBrowser.Browser;
using DotNetBrowser.Engine;
using System;

namespace AvaloniaApplication1.ViewModels;

public class MainViewModel : ObservableObject, IDisposable
{
    private readonly IEngine _engine;
    public IBrowser Browser { get; }

    public MainViewModel()
    {
        // Инициализация браузера
        _engine = EngineFactory.Create(new EngineOptions.Builder
        {
            RenderingMode = RenderingMode.OffScreen,
            LicenseKey = "",
            FileAccessFromFilesAllowed = true
        }.Build());
        Browser = _engine.CreateBrowser();
        OpenFile();
    }

    private void OpenFile()
    {
        var pdfName = "sample.pdf";
        LoadPdf(pdfName);
        Browser.DevTools.Show();
    }

    private void LoadPdf(string pdfName)
    {
        // Запускаем сервер для папки Resources
        var resourcePath = @"";
        LocalHttpServer.Start(resourcePath);

        // Формируем URL для загрузки PDF
        var pdfUrl = $"http://localhost:8080/{pdfName}";
        var viewerUrl = $"http://localhost:8080/pdfjs/web/viewer.html?file={pdfUrl}";

        Browser.Navigation.LoadUrl(viewerUrl);
    }

    public void Dispose()
    {
        LocalHttpServer.Stop();
        Browser.Dispose();
        _engine.Dispose();
    }
}