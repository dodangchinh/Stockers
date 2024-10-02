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
    public partial class UcSelectedCategory : UserControl
    {
        public event EventHandler Add;
        public event EventHandler Import;

        public delegate SelectedProductService MyDelegate();
        public event MyDelegate delegateSelectedProductService;

        public static SelectedProductService selectedProductService {  get; set; } 


        Account accountLogin;
        public string selectedCategory {  get; set; }   
        ObservableCollection<string> lstCategory;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (delegateSelectedProductService == null)
                return;
            selectedProductService = delegateSelectedProductService();
        }

        public UcSelectedCategory(Account accountLogin)
        {
            InitializeComponent();

            lstCategory = new ObservableCollection<string>(UnitOfWork.Instance.lstProductType);
            cbSelectedCategory.ItemsSource = lstCategory;
            this.accountLogin = accountLogin;

            this.DataContext = this;
        }

        private void cbSelectedCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategory = cbSelectedCategory.SelectedItem as string;
            spInfoCategory.Children.Clear();
            switch (selectedCategory)
            {
                case "Electronic":
                    UcSelectedElectronic ucSelectedElectronic = new UcSelectedElectronic(accountLogin);
                    ucSelectedElectronic.delegateSelectedProductService += GetSelectedProductService;
                    ucSelectedElectronic.Add += UcSelected_Add;
                    spInfoCategory.Children.Add(ucSelectedElectronic);
                    break;
                case "Porcelain":
                    UcSelectedPorcelain ucSelectedPorcelain = new UcSelectedPorcelain(accountLogin);
                    ucSelectedPorcelain.delegateSelectedProductService += GetSelectedProductService;
                    ucSelectedPorcelain.Add += UcSelected_Add;
                    spInfoCategory.Children.Add(ucSelectedPorcelain);
                    break;
                case "Food":
                    UcSelectedFood ucSelectedFood = new UcSelectedFood(accountLogin);
                    ucSelectedFood.delegateSelectedProductService += GetSelectedProductService;
                    ucSelectedFood.Add += UcSelected_Add;
                    spInfoCategory.Children.Add(ucSelectedFood);
                    break;
            }
        }

        private SelectedProductService GetSelectedProductService()
        {
            return selectedProductService;
        }

        private void UcSelected_Add(object sender, EventArgs e)
        {
            Add?.Invoke(this, EventArgs.Empty);
        }
    }
}
