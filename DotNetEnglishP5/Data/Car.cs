using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetEnglishP5.Data
{
    public class Car
    {
        public int Id { get; set; }
        public string? VIN { get; set; }
        public int ModelId { get; set; }
        [ForeignKey(nameof(ModelId))]
        public virtual Model Model { get; set; }
        public string? Trim { get; set; }
        public int Year { get; set; }
        public DateTime PurchaseDate { get; set; }
        public float PurchasePrice { get; set; } = 0.00f;
        public string? Repairs { get; set; }
        public float RepairCost { get; set; } = 0.00f;
        public DateTime? LotDate { get; set; }
        public float SellingPrice { get; set; } = 0.00f;
        public DateTime? SaleDate { get; set; }
    }
}
