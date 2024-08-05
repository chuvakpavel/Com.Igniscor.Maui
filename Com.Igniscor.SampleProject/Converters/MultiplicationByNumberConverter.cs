using System.Globalization;

namespace SkiaSharpControls.Converters;

internal class MultiplicationByNumberConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var multiplier = GetParameter(parameter);

        return value switch
        {
            double doubleValue => (doubleValue * (double)multiplier),
            float floatValue => (floatValue * (float)multiplier),
            int intValue => (intValue * (int)multiplier),
            long longValue => (longValue * (long)multiplier),
            short shortValue => (shortValue * (short)multiplier),
            _ => null
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static object GetParameter(object? parameter)
    {
        return parameter switch
        {
            double doubleParameter => doubleParameter,
            int intParameter => intParameter,
            string stringParameter => double.Parse(stringParameter, CultureInfo.InvariantCulture),
            float floatParameter => floatParameter,
            short shortParameter => shortParameter,
            long longParameter => longParameter,
            _ => 1
        };
    }
}