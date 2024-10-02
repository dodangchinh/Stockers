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
    public partial class UcDisplayStocker : UserControl
    {
        Account accountLogin;
        public string selected {  get; set; }   
        ObservableCollection<string> lstView;

        public UcDisplayStocker(Account accountLogin)
        {
            InitializeComponent();
            InitSelectView();

            this.accountLogin = accountLogin;
            
            cbSelected.ItemsSource = lstView;
        }

        private void InitSelectView()
        {
            lstView = new ObservableCollection<string>();
            lstView.Add("Import Receipts");
            lstView.Add("Export Receipts");
            lstView.Add("Import Inventorys");
            lstView.Add("Export Inventorys");
            lstView.Add("Remaining Inventorys");
            lstView.Add("Import Expired Date");
        }

        private void cbSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected = cbSelected.SelectedItem as string;
            spInfo.Children.Clear();
            switch (selected)
            {
                case "Import Receipts":
                    UcDisplayImportReceipts ucDisplayImportReceipts = new UcDisplayImportReceipts(accountLogin);
                    spInfo.Children.Add(ucDisplayImportReceipts);   
                    break;
                case "Export Receipts":
                    UcDisplayExportReceipts ucDisplayExportReceipts = new UcDisplayExportReceipts();
                    spInfo.Children.Add(ucDisplayExportReceipts);
                    break;
                case "Import Inventorys":
                    UcListImportInventory ucListImportInventory = new UcListImportInventory();
                    spInfo.Children.Add(ucListImportInventory);
                    break;
                case "Export Inventorys":
                    UcListExportInventory ucListExportInventory = new UcListExportInventory();
                    spInfo.Children.Add(ucListExportInventory);
                    break;
                case "Remaining Inventorys":
                    UcRemainingInventory ucRemainingInventory = new UcRemainingInventory();
                    spInfo.Children.Add(ucRemainingInventory);
                    break;
                case "Import Expired Date":
                    UcImportExpiredDate ucImportExpiredDate = new UcImportExpiredDate();
                    spInfo.Children.Add(ucImportExpiredDate);
                    break;

            }

        }
    }
}
