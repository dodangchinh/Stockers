using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public static class ProductConvert
    {
        public static ProductType ConvertTo(int type)
        {
            switch (type)
            {
                case 1:
                    return ProductType.Electronic;
                case 2:
                    return ProductType.Porcelain;
                case 3:
                    return ProductType.Food;
            }
            return ProductType.Null;
        }
    }
}
