using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Greeting { get; set; }
        = "Welcome, Velimir";

    [ObservableProperty]
    public partial string Subtitle { get; set; }
        = "Here's what's happening with your workspace today.";
}