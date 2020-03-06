using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace _2_UML.Converter
{
    public class CombineNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string FirstName, SecondName, CombinedName;

            if(values[0]!=null && values[1]!=null)
            {
                FirstName = (values[0] is String) ? values[0].ToString() : "";
                SecondName = (values[1] is String) ? values[1].ToString() : "";

                CombinedName = $"{FirstName} {SecondName}";
                return CombinedName;
            }

            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
