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
    /// Interaction logic for UcDisplayAccount.xaml
    /// </summary>
    public partial class UcSelectedFood : UserControl
    {
        public event EventHandler Add;

        public delegate SelectedProductService DelegateSelectedProductService();
        public event DelegateSelectedProductService delegateSelectedProductService;

        Account accountLogin;
        ProductService foodService;
        ImportDateService importDateService;
        public static SelectedProduct selectedProduct { get; set; }
        public static SelectedProductService selectedProductService { get; set; }

        public Product foodSelected { get; set; }
        public ObservableCollection<Product> lstFood { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (delegateSelectedProductService == null)
                return;

            selectedProductService = delegateSelectedProductService();
        }
        public UcSelectedFood(Account accountLogin)
        {
            InitializeComponent();
            InitVarible();

            this.accountLogin = accountLogin;
            this.DataContext = this;
        }

        private void InitVarible()
        {
            foodService = new ProductService();
            selectedProductService = new SelectedProductService();
            lstFood = new ObservableCollection<Product>(foodService.GetFoods());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            selectedProductService.Inits(foodService.GetFoods());
            selectedProduct = selectedProductService.GetByProduct(foodSelected);
            selectedProduct.nProduct++;
            Add?.Invoke(this, EventArgs.Empty);
        }
    }
}