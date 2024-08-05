using System.ComponentModel;

namespace SkiaSharpControls.Abstracts;

internal class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
}