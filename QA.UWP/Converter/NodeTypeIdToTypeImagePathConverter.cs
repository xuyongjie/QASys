using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AATravel.UWP.Converter
{
    public class NodeTypeIdToTypeImageUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int temp = (int)value;
            switch(temp)
            {
                case 0:
                    return new Uri("ms-appx:///Data/Images/cost.png");
                case 1:
                    return new Uri("ms-appx:///Data/Images/comment.png");
                case 2:
                    return new Uri("ms-appx:///Data/Images/share.png");
                default:
                    return new Uri("ms-appx:///Data/Images/comment.png");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
