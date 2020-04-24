using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace _2_UML.Converter
{
    public class PresentDateConverter :IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string Day, Month, Year, Date;

            if (values[0] != null && values[1] != null)
            {
                Day = (values[0] is int) ? values[0].ToString() : "";
                Month = (values[1] is int) ? values[1].ToString() : "";
                Year = (values[2] is int) ? values[2].ToString() : "";

                Date = $"{Day}.{Month}.{Year}";
                return Date;
            }

            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
