using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sales_Report.Areas.Identity.Data
{
    public class MonthlySalesModel
    {

        public List<SalesTransaction>? SalesList { get; set; }
     
        public SelectList byYear { get; set; }
        public SelectList byMonth { get; set; } 
        public string Year { get; set; }
        public string Month { get; set; }

        public DateTime Date { get; set; }

    }
}
