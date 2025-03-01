using System.ComponentModel.DataAnnotations;

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
        public decimal Width { get; set; }

        [Required]
        public decimal Depth { get; set; }

        [Required]
        public int Drawers { get; set; }

        [Required]
        public DesktopMaterial Material { get; set; }

        [Required]
        public int RushDays { get; set; }

        [Required]
        public string CustomerName { get; set; } = string.Empty;


        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime QuoteDate { get; set; } = DateTime.Now;


        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }


    }
}
