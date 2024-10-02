using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class ImportExportReceipt
    {
        public string Id {  get; set; }
        public string Name { get; set; }
        public int Total { get; set; }   
        public List<Receipt> lstReceipts { get; set; }
        public DateTime ReceiptDate { get; set; } = DateTime.Now;

        public ImportExportReceipt(string id, string name, int total, List<Receipt> lstReceipts)
        {
            Id = id;
            Name = name;
            Total = total;
            this.lstReceipts = lstReceipts;
        }

        public ImportExportReceipt()
        {
            lstReceipts = new List<Receipt>();
        }
    }
}
