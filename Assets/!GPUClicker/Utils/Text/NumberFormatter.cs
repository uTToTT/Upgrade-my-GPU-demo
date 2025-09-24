using System;
using System.Globalization;
using UnityEngine;

public static class NumberFormatter
{
    public static string Format(NumberFormat format, double number)
    {
        switch (format)
        {
            case NumberFormat.Default:
                return number.ToString(CultureInfo.InvariantCulture);

            case NumberFormat.WithSeparators:
                return FormatWithSeparators(number);

            case NumberFormat.Compact:
                return FormatCompact(number);

            default:
                throw new ArgumentException(nameof(format));
        }
    }

    public static string FormatWithSeparators(double number)
    {
        NumberFormatInfo nfi = new NumberFormatInfo
        {
            NumberGroupSeparator = ".",
            NumberDecimalSeparator = ",",
            NumberGroupSizes = new int[] { 3 }
        };

        return number.ToString("N2", nfi);
    }

    public static string FormatCompact(double number)
    {
        if (number < 1000d)
            return number.ToString("0.##", CultureInfo.InvariantCulture);

        if (number < 1_000_000d)
            return (number / 1000d).ToString("0.##", CultureInfo.InvariantCulture) + "k";

        if (number < 1_000_000_000d)
            return (number / 1_000_000d).ToString("0.##", CultureInfo.InvariantCulture) + "M";

        if (number < 1_000_000_000_000d)
            return (number / 1_000_000_000d).ToString("0.##", CultureInfo.InvariantCulture) + "B";

        return (number / 1_000_000_000_000d).ToString("0.##", CultureInfo.InvariantCulture) + "T";
    }
}
