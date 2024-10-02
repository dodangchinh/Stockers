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
    public partial class UcListImportInventory : UserControl
    {
        ImportInventoryService importInventoryService;

        public ObservableCollection<ImportExport> lstImportInventory { get; set; }
        public UcListImportInventory()
        {
            InitializeComponent();

            importInventoryService = new ImportInventoryService();
            lstImportInventory = new ObservableCollection<ImportExport>(importInventoryService.Gets());
            this.DataContext = this;
        }
    }
}
