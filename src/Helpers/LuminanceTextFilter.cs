/* Automatically changes the font colour on a taskcard from white to black, 
 * depending on what the underlying background colour is. 
 */
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace App.Helpers {
    public class LuminanceTextFilter : IValueConverter {

        public object? Convert(object? value, 
                               Type targetType, 
                               object? parameter, 
                               CultureInfo culture) {

            if (value is string colorString) {
                // Parse color string to Avalonia.Color
                Color color;
                try {
                    color = Color.Parse(colorString);
                }
                catch {
                    return Brushes.Black; // fallback
                }

                // Calculate brightness: simple formula
                var brightness = (color.R * 0.299 + color.G * 0.587 + color.B * 0.114);

                // Return white for dark colors, black for light colors
                return brightness < 128 ? Brushes.White : Brushes.Black;
            }
            return Brushes.Black;
        }

        public object? ConvertBack(object? value, 
                                   Type targetType, 
                                   object? parameter, 
                                   CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
