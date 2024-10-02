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

    public partial class UcDisplayFood : UserControl
    {
        Account accountLogin;
        ProductService foodService;
        public Product foodSelected { get; set; }

        public ObservableCollection<Product> lstFood { get; set; }
        public UcDisplayFood(Account accountLogin, bool flag)
        {
            InitializeComponent();
            CheckBtnVisibility(flag);

            this.accountLogin = accountLogin;
            foodService = new ProductService();
            lstFood = new ObservableCollection<Product>(foodService.GetFoods());
            this.DataContext = this;
        }

        private void CheckBtnVisibility(bool flag)
        {
            if (flag)
                return;

            BtnEditColumn.Visibility = Visibility.Collapsed;
            btnAdd.Visibility = Visibility.Collapsed;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            frmEditProduct frmEditProducts = new frmEditProduct(accountLogin);
            frmEditProducts.myDelegate += GetProduct;
            frmEditProducts.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddProduct frmAddProduct = new frmAddProduct(lstFood, "Food");
            frmAddProduct.DataContext = lstFood;
            frmAddProduct.ShowDialog();
        }

        private Product GetProduct()
        {
            if (foodSelected == null)
                return null;

            var selected = foodSelected;
            return selected;
        }
    }
}
