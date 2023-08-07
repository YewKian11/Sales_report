using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Sales_Report.Data;

namespace Sales_Report.Areas.Identity.Data
{
    public class YearlySalesModel
    {
       
        //A List of Sales Transaction 
        public List<SalesTransaction>? SalesList { get; set; }
       //Allow user to select a Year from List
     public SelectList SalesYear { get; set; }
        //contains the selected year
      public string SelectedYear { get; set; }

    
    }
}
