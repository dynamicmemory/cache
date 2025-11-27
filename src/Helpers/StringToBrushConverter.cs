using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace App.Helpers {

    public class StringToBrushConverter : IValueConverter {
        public object? Convert(object? value, 
                              Type targetType, 
                              object? parameter, 
                              CultureInfo culture) {
            if (value is string s) {
                try {
                    return Brush.Parse(s);
                }
                catch { 
                    return Brushes.Transparent;
                }
            }
            return Brushes.Transparent;
        }

        public object? ConvertBack(object? value, 
                                  Type targetType, 
                                  object? parameter, 
                                  CultureInfo culture) {
            if (value is IBrush b) {
                return b.ToString();
            }
            return "#FFFFFF";
        }
    }
}
