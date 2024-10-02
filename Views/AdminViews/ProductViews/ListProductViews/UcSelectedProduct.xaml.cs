using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for UcDsplayProduct.xaml
    /// </summary>
    public partial class UcSelectedProduct : UserControl
    {
        bool flag;
        Account accountLogin;
        public string selectedCategory {  get; set; }   
        ObservableCollection<string> lstCategory;

        public UcSelectedProduct(Account accountLogin, bool flag)
        {
            InitializeComponent();

            lstCategory = new ObservableCollection<string>(UnitOfWork.Instance.lstProductType);
            cbSelectedCategory.ItemsSource = lstCategory;
            this.accountLogin = accountLogin;
            this.flag = flag;   
        }

        private void cbSelectedCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategory = cbSelectedCategory.SelectedItem as string;
            
            spInfoCategory.Children.Clear();
            
            switch (selectedCategory)
            {
                case "Electronic":
                    UcDisplayElectronic ucDisplayElectronic = new UcDisplayElectronic(accountLogin, flag);
                    spInfoCategory.Children.Add(ucDisplayElectronic);   
                    break;
                case "Porcelain":
                    UcDisplayPorcelain ucDisplayPorcelain = new UcDisplayPorcelain(accountLogin, flag);
                    spInfoCategory.Children.Add( ucDisplayPorcelain);
                    break;
                case "Food":
                    UcDisplayFood ucDisplayFood = new UcDisplayFood(accountLogin, flag);
                    spInfoCategory.Children.Add(ucDisplayFood); 
                    break;
            }
        }
    }
}
