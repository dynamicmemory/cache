/* The colours for the taskcards, uses Built in Brushes to help change the 
 * background colours of taskcards and convert hexcodes into strings for the user
 */
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
                "Red" => Brush.Parse("#FF0040"),
                "Orange" => Brush.Parse("#F5B727"),
                "Yellow" => Brush.Parse("#CCF527"),
                "Lime" => Brush.Parse("#65F527"),
                "Bulma" => Brush.Parse("#27F5B7"),
                "Trunks" => Brush.Parse("#27CCF5"),
                "Blue" => Brush.Parse("#2765F5"),
                "Purple" => Brush.Parse("#B727F5"),
                "Magenta" => Brush.Parse("#F527CC"),
                "White" => Brushes.White, 
                "Black" => Brushes.Black,
                "Transparent" => Brushes.Transparent,
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
