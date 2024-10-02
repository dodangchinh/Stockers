using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for frmAddMovie.xaml
    /// </summary>
    public partial class frmAddProduct : Window
    {
        string Category;

        ProductService productService;
        ImportInventoryService importInventoryService;
        RemainingProductService remainingProductService;
        ExportInventoryService exportInventoryService;
        InventorySaleService inventorySaleService;
        
        ObservableCollection<Product> lstProduct;

        public double PriceOut {  get; set; }   

        public frmAddProduct(ObservableCollection<Product> lstProduct, string Category)
        {
            InitializeComponent();
            InitVarible();

            this.lstProduct = lstProduct;
            this.Category = Category;

            SelectedCategory();
            DataContext = this;
        }

        private void InitVarible()
        {
            productService = new ProductService();
            importInventoryService = new ImportInventoryService();
            remainingProductService = new RemainingProductService();
            exportInventoryService = new ExportInventoryService();
            inventorySaleService = new InventorySaleService();
        }

        private void TxtDuration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(txtId.Text.Length <= 1)
            {
                MessageBox.Show("Please enter Id!");
                return;
            }
            if(txtName.Text.Length <= 0 || txtProducer.Text.Length <= 0 || txtPriceIn.Text.Length <= 0)
                return;

            Product product = InitProduct();

            if(product == null)
                return;

            productService.ChangeProduct(product);

            if (!productService.Add(product))
            {
                MessageBox.Show($"The ID '{product.Id}' or Name '{product.Name}' is in stock!");
                return;
            }

            importInventoryService.Add(new ImportExport(product));
            remainingProductService.Add(new RemainingProduct(0, 0, 0, product));
            exportInventoryService.Add(new ImportExport(product));
            inventorySaleService.Add(new InventorySale(0, 0, new ImportExport(product), product));

            lstProduct.Add(product);    

            MessageBox.Show("Successful!");
            this.Close();
        }

        private Product InitProduct()
        {
            Product product = null;
            switch (Category)
            {
                case "Electronic":
                    if (txtWarranty.Text.Length <= 0)
                        return null;
                    product = new Electronic(txtId.Text, txtName.Text, txtCategory.Text, txtProducer.Text, double.Parse(txtPriceIn.Text), double.Parse(txtPriceOut.Text), int.Parse(txtWarranty.Text));
                    break;
                case "Porcelain":
                    if (txtMaterial.Text.Length <= 0)
                        return null;
                    product = new Porcelain(txtId.Text, txtName.Text, txtCategory.Text, txtProducer.Text, double.Parse(txtPriceIn.Text), double.Parse(txtPriceOut.Text), txtMaterial.Text);
                    break;
                case "Food":
                    product = new Food(txtId.Text, txtName.Text, txtCategory.Text, txtProducer.Text, double.Parse(txtPriceIn.Text), double.Parse(txtPriceOut.Text));
                    break;
            }
            return product;
        }
        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SelectedCategory()
        {
            switch (Category)
            {
                case "Electronic":
                    txtId.Text += "E";
                    txtCategory.Text += Category;
                    spMaterial.Visibility = Visibility.Collapsed;
                    break;
                case "Porcelain":
                    txtId.Text += "P";
                    txtCategory.Text += Category;    
                    spWarranty.Visibility = Visibility.Collapsed;
                    break;
                case "Food":
                    txtId.Text += "F";
                    txtCategory.Text += Category;    
                    spWarranty.Visibility= Visibility.Collapsed;
                    spMaterial.Visibility= Visibility.Collapsed;
                    break;
            }
            
        }

        private void txtPriceIn_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if(txtPriceIn.Text.Length <= 0)
            {
                txtPriceOut.Text = "";
                return;
            }
                

            double PriceIn = Convert.ToDouble(txtPriceIn.Text);
            PriceOut = PriceIn + PriceIn * 0.1 + PriceIn * 0.3 + PriceIn * (Parameter.nAccount * 0.036);
            txtPriceOut.Text = PriceOut.ToString();
        }

        private void txtPriceIn_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return !new Regex("[^0-9]+").IsMatch(text);
        }

    }
}
