namespace Helpers;

/// <summary>
/// Operations with UI units
/// </summary>
public static class UnitsHelper
{
    /// <summary>
    /// Converts the DIU to pixels.
    /// </summary>
    /// <param name="diu">
    /// DIU
    /// </param>
    /// <returns>
    /// pixels
    /// </returns>
    public static float ConvertDIUToPixels(float diu)
    {
        return diu * (float)DeviceDisplay.MainDisplayInfo.Density;
    }
}