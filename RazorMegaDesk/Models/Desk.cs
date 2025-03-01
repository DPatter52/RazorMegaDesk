using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace RazorMegaDesk.Models
{
    public enum DesktopMaterial
        {
            Laminate, 
            Oak, 
            Rosewood,
            Veneer, 
            Pine
        }
    public enum RushOrder
    {
        [Display(Name = "Standard (14 day)")]
        Standard,
        [Display(Name = "3 Day")]
        ThreeDay,
        [Display(Name = "5 Day")]
        FiveDay,
        [Display(Name = "7 Day")]
        SevenDay
    }
    public class Desk
    {

        

        public const int MIN_WIDTH = 24;
        public const int MAX_WIDTH = 96;
        public const int MIN_DEPTH = 12;
        public const int MAX_DEPTH = 45;
        public const int MIN_DRAWERS = 0;
        public const int MAX_DRAWERS = 7;


        public int Id { get; set; }

        [Required]
        [Range(24, 96)]
        public decimal Width { get; set; }

        [Required]
        [Range(12, 48)]
        public decimal Depth { get; set; }

        [Required]
        [Range(0, 7)]
        public int Drawers { get; set; }

        [Required]
        public DesktopMaterial Material { get; set; }

        [Required]
        [Display(Name="Rush Days")]
        public RushOrder RushDays { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 5)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;


        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date), Display(Name = "Quote Date")]
        public DateTime QuoteDate { get; set; } = DateTime.Now;


        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }


    }
}
