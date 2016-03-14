using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AATravel.UWP.Converter
{
    public class NullableDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double? temp = value as double?;
            if(temp==null)
            {
                return "0";
            }
            else
            {
                return temp.Value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double temp = double.Parse(value as string);
            return new double?(temp);
        }
    }
}
