﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace _2_UML.Converter
{
    public class BoolToGridRowHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(GridLength))
                throw new InvalidOperationException("The target must be a GridLength");

            return ((bool)value == true) ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
            //return ((bool)value == true) ? new GridLength(40) : new GridLength(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}
