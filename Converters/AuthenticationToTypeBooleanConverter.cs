using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Chinh_QuanLyKho
{
    // Chuyển đổi giá trị 0 - Windows Authentication thành false 
    // 1- sẽ trở thành true
    // Dùng cho combobox Authentication và Textbox Username / Pass
    public class AuthenticationToTypeBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int index = (int)value;
            return index != 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
