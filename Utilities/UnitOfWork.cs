using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class UnitOfWork
    {
        public static readonly object Instancelock = new object();
        public static UnitOfWork _Instance;

        public List<string> lstProductType {  get; set; }
        public List<Product> lstProduct = new List<Product>();

        public AccountRepository accountRepository { get; set; }
        public RoleRepository roleRepository { get; set; }
        public AccountRolesRepository accountRolesRepository { get; set; }

        public ElectronicRepository electronicRepository { get; set; }
        public PorcelainRepository porcelainRepository { get; set; }
        public FoodRepository foodRepository { get; set; }

        public ImportInventoryRepository importInventoryRepository { get; set; }
        public RemainingProductRepository remainingProductRepository { get; set; }
        public ExportInventoryRepository exportInventoryRepository { get; set; }

        public ImportReceiptRepository importReceiptRepository { get; set; }
        public OutOfStockRepository outOfStockRepository { get; set; }
        public ExportReceiptRepository exportReceiptRepository { get; set; }

        public ImportDateRepository importDateRepository { get; set; }
        public ExportDateRepository exportDateRepository { get; set; }
        public ExpiredDateRepository expiredDateRepository { get; set; }

        public CardRepository cardRepository { get; set; }
        public CustomerRepository customerRepository { get; set; }

        public InventorySaleRepository inventorySaleRepository { get; set; }
        public SalesSlipRepository saleSlipRepository { get; set; }

        public static UnitOfWork Instance
        {
            get
            {
                lock (Instancelock)
                {
                    if (_Instance == null)
                        _Instance = new UnitOfWork();
                    return _Instance;
                }
            }
        }

        private UnitOfWork()
        {
            InitProductType();
            accountRepository = new AccountRepository();
            roleRepository = new RoleRepository();
            accountRolesRepository = new AccountRolesRepository(accountRepository.lstAccount, roleRepository.lstRole);

            electronicRepository = new ElectronicRepository();
            ChangeLstProduct(electronicRepository.lstElectronic);
            porcelainRepository = new PorcelainRepository();
            ChangeLstProduct(porcelainRepository.lstPorcelain);
            foodRepository = new FoodRepository();
            ChangeLstProduct(foodRepository.lstFood);

            importInventoryRepository = new ImportInventoryRepository(lstProduct);
            remainingProductRepository = new RemainingProductRepository(lstProduct);
            exportInventoryRepository = new ExportInventoryRepository(lstProduct);

            importReceiptRepository = new ImportReceiptRepository(lstProduct);
            outOfStockRepository = new OutOfStockRepository(lstProduct);
            exportReceiptRepository = new ExportReceiptRepository(lstProduct);

            importDateRepository = new ImportDateRepository(lstProduct);
            exportDateRepository = new ExportDateRepository(lstProduct);
            expiredDateRepository = new ExpiredDateRepository(lstProduct);

            inventorySaleRepository = new InventorySaleRepository(exportInventoryRepository.lstExportInventories, lstProduct);
            saleSlipRepository = new SalesSlipRepository(lstProduct);

            cardRepository = new CardRepository();
            customerRepository = new CustomerRepository(cardRepository.lstCard, saleSlipRepository.lstSaleSlip);           
         

            
        }

        private void ChangeLstProduct(List<Product> lstTemp)
        {
            foreach (var item in lstTemp) 
                lstProduct.Add(item);
        }

        private void InitProductType()
        {
            lstProductType = new List<string>();
            lstProductType.Add("Electronic");
            lstProductType.Add("Porcelain");
            lstProductType.Add("Food");
        }

        
    }
}