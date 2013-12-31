﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Tuner.Converters
{
    public class LevelToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush b;
            switch ((int)value)
            {
                case 0:
                    b = (SolidColorBrush)Application.Current.FindResource("DELColor-Red");
                    break;
                case 1:
                    b = (SolidColorBrush)Application.Current.FindResource("DELColor-Green");
                    break;
                default:
                    b = (SolidColorBrush)Application.Current.FindResource("DELColor-Green");
                    break;
            }
            return b.Color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // TODO
            return null;
        }
    }
}