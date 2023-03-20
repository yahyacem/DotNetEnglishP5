using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetEnglishP5.Data
{
    public class Model
    {
        public int Id { get; set; }
        [Display(Name = "Model")]
        public string Name { get; set; }
        public int MakeId { get; set; }
        [ForeignKey(nameof(MakeId))]
        public virtual Make Make { get; set; }
    }
}
