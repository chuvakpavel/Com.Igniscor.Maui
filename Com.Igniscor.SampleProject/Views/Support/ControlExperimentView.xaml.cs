namespace SkiaSharpControls.Views.Support;

public partial class ControlExperimentView
{
    #region Bindable Properties

    #region ControlPreviewView Property

    public static readonly BindableProperty ControlPreviewViewProperty = BindableProperty.Create(
        nameof(ControlPreviewView),
        typeof(View),
        typeof(ControlExperimentView),
        propertyChanged: ControlPreviewView_OnPropertyChanged);

    public View ControlPreviewView
    {
        get => (View)GetValue(ControlPreviewViewProperty);
        set => SetValue(ControlPreviewViewProperty, value);
    }

    private static void ControlPreviewView_OnPropertyChanged(BindableObject bindable, object oldValue,
        object newValue)
    {
        if (bindable is not ControlExperimentView view || newValue is not View controlPreviewView)
        {
            return;
        }

        view.ControlPreviewFrame.Content = controlPreviewView;
    }

    #endregion ControlPreviewView Property

    #region ControlPropertiesView Property

    public static readonly BindableProperty ControlPropertiesViewProperty = BindableProperty.Create(
        nameof(ControlPropertiesView),
        typeof(View),
        typeof(ControlExperimentView),
        propertyChanged: ControlPropertiesView_OnPropertyChanged);

    public View ControlPropertiesView
    {
        get => (View)GetValue(ControlPropertiesViewProperty);
        set => SetValue(ControlPropertiesViewProperty, value);
    }

    private static void ControlPropertiesView_OnPropertyChanged(BindableObject bindable, object oldValue,
        object newValue)
    {
        if (bindable is not ControlExperimentView view || newValue is not View controlPropertiesView)
        {
            return;
        }

        view.ControlPropertiesScrollView.Content = controlPropertiesView;
    }

    #endregion ControlPropertiesView Property
    
    #endregion Bindable Properties

    public double RowHeight => DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

    public ControlExperimentView()
    {
        InitializeComponent();
    }
}