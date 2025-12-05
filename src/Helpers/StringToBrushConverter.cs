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
                return s switch {
                "Magenta" => Brush.Parse("#F527CC"),
                "Purple" => Brush.Parse("#B727F5"),
                "Bulma" => Brush.Parse("#27F5B7"),
                "Green" => Brush.Parse("#27F550"),
                "Lime" => Brush.Parse("#65F527"),
                "Blue" => Brush.Parse("#2765F5"),
                "Trunks" => Brush.Parse("#27CCF5"),
                "Yellow" => Brush.Parse("#CCF527"),
                "Orange" => Brush.Parse("#F5B727"),
                "Red" => Brush.Parse("#F55027"),
                "BluPurp" => Brush.Parse("#5027F5"),
                "White" => Brushes.White, 
                _ => Brushes.Transparent
                };
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
