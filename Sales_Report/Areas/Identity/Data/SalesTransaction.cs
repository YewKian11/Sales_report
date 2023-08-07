using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sales_Report.Areas.Identity.Data
{
    public class SalesTransaction 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }


        [Required]
        [Display(Name = "UserId")]
        //FKey relationship
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Sales Item")]
        public string SalesItem { get; set; } 
        
        
        [Required]
        [Display(Name = "Sales Amount")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public double SalesAmount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Sales Date")]
        //Display date
        public DateTime SalesDate { get; set; }

       


        [DataType(DataType.DateTime)]
        [Display(Name = "Sales Date Updated")]
        public DateTime SalesDateUpdated { get; set; } = DateTime.UtcNow;


        public virtual ApplicationUser ApplicationUsers { get; set; }

      
    }
    
}
