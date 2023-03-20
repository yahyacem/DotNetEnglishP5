using DotNetEnglishP5.Models.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetEnglishP5.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string? VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string? Trim { get; set; }

        [YearToNowRange(1990)]
        public int Year { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DisplayName("Purchase date")]
        public DateTime PurchaseDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        [DisplayName("Purchase price")]
        public float PurchasePrice { get; set; }

        public string? Repairs { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        [DisplayName("Repair cost")]
        public float RepairCost { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DisplayName("Lot date")]
        public DateTime? LotDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        [DisplayName("Selling price")]
        public float SellingPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DisplayName("Sale date")]
        public DateTime? SaleDate { get; set; }

        [DisplayName("Images")]
        public List<ImageViewModel>? Images { get; set; }

        public IFormFile[]? ImagesInput { get; set; }

        public bool Sold { get { return SaleDate != null; } }
    }
}
