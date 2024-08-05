using System.Collections.ObjectModel;
using System.Windows.Input;
using DonutChartControl;
using SkiaSharpControls.Abstracts;
using SkiaSharpControls.Helpers;
using SkiaSharpControls.Models;

namespace SkiaSharpControls.ViewModels;

internal class DonutChartViewModel : BaseViewModel
{
    public ICommand SectorTouchCommand { get; }
    public ICommand HoleTouchCommand { get; }
    public ICommand AddExpenseCommand { get; }
    public ICommand RemoveExpenseCommand { get; }

    public ObservableCollection<BaseDonutChartItem> Expenses { get; }
    public float TotalValue { get; private set; }

    public float HoleRadius { get; set; } = 0.5f;
    public float LineToCircleLength { get; set; } = 20f;
    public float DescriptionCircleRadius { get; set; } = 20f;
    public float SeparatorsWidth { get; set; } = 2f;
    public float InnerMargin { get; set; } = 100f;
    public float HolePrimaryTextScale { get; set; } = 1f;
    public float HoleSecondaryTextScale { get; set; } = 1f;
    public string HolePrimaryText { get; set; } = "Total";

    private readonly IList<(string Name, string Filename)> _possibleExpenses;

    public DonutChartViewModel()
    {
        SectorTouchCommand = new Command(ExpenseTouch);
        HoleTouchCommand = new Command(HoleTouch);
        AddExpenseCommand = new Command(AddExpense);
        RemoveExpenseCommand = new Command(RemoveExpense);

        _possibleExpenses = new List<(string Name, string Filename)>
        {
            ("Everyday Food", Constants.Images.Dish),
            ("Entertainment", Constants.Images.Entertainment),
            ("Fast Food", Constants.Images.FastFood),
            ("Medicine", Constants.Images.Pills),
            ("Repairing", Constants.Images.RepairTools),
            ("Transport", Constants.Images.Transport),
            ("Games", Constants.Images.VideoGame)
        };

        Expenses = new ObservableCollection<BaseDonutChartItem>();
        Expenses.CollectionChanged += (_, _) => { TotalValue = Expenses.Sum(x => x.Value); };

        AddExpense();
        AddExpense();
        AddExpense();
    }

    private static void ExpenseTouch(object? parameter)
    {
        if (parameter is not ExtendChartItem item)
        {
            return;
        }

        Application.Current?.MainPage?.DisplayAlert("Info", $"\"{item.Name}\" was selected.\nValue = {item.Value}",
            "Ok");
    }

    private static void HoleTouch()
    {
        Application.Current?.MainPage?.DisplayAlert("Info", "Hole was touched.", "Ok");
    }

    private void AddExpense()
    {
        var random = new Random();

        var (name, filename) = _possibleExpenses[random.Next(_possibleExpenses.Count)];

        Expenses.Add(new ExtendChartItem
        {
            Value = random.Next(20, 100),
            SectionHexColorStart = Color.FromRgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)).ToHex(),
            SectionHexColorEnd = Color.FromRgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)).ToHex(),
            IconData = GetSKBitmap(filename),
            Name = name
        });
    }

    private void RemoveExpense()
    {
        if (Expenses.Count > 0)
        {
            Expenses.Remove(Expenses.Last());
        }
    }

    private static byte[] GetSKBitmap(string resourceId)
    {
        var iconData = Array.Empty<byte>();
        try
        {
            using var stream = FileSystem.OpenAppPackageFileAsync(resourceId).Result;
            int i;
            while ((i = stream.ReadByte()) != -1)
            {
                iconData = iconData.Append((byte)i).ToArray();
            }
        }
        catch (Exception)
        {
            // ignored
        }

        return iconData;
    }
}