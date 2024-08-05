using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DonutChartControl;

/// <summary>
/// Base for <see cref="DonutChartControl"/> segment.
/// </summary>
public abstract class BaseDonutChartItem : INotifyPropertyChanged
{
    private float _value;
    private string _sectionHexColorStart = Color.FromRgb(255, 255, 255).ToHex();
    private string _sectionHexColorEnd = Color.FromRgb(255, 255, 255).ToHex();
    private byte[] _iconBytes = Array.Empty<byte>();

    /// <summary>
    /// Gets or sets the value of the donut chart item segment.
    /// </summary>
    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            OnPropertyChanged();
        }
    }


    /// <summary>
    /// Gets or sets the section initial gradient color in hex format.
    /// </summary>
    public string SectionHexColorStart
    {
        get => _sectionHexColorStart;
        set
        {
            _sectionHexColorStart = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets the section final gradient color in hex format.
    /// </summary>
    public string SectionHexColorEnd
    {
        get => _sectionHexColorEnd;
        set
        {
            _sectionHexColorEnd = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets an byte of array containing icon data.
    /// </summary>
    public byte[] IconData
    {
        get => _iconBytes;
        set
        {
            _iconBytes = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Called when [property changed].
    /// </summary>
    /// <param name="name">The name.</param>
    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
}