using Avalonia.Controls;
using AvaloniaApplication1.ViewModels;
using DotNetBrowser.Browser;
using System;

namespace AvaloniaApplication1.Views;

public partial class MainWindow : Window
{
    private MainViewModel? MainViewModel => DataContext as MainViewModel;

    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if (MainViewModel != null)
        {
            BrowserView.InitializeFrom(MainViewModel.Browser);
        }
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        MainViewModel?.Dispose();
    }
}
