using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Chinh_QuanLyKho
{
    public class StatusToStringConverter : IValueConverter
    {
        // Chuyển đổi giá trị 0 - Windows Authentication thành false 
        // 1- sẽ trở thành true
        // Dùng cho combobox Authentication và Textbox Username / Pass
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
                return "Sử dụng";
            else
                return "Khóa";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
