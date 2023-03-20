using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetEnglishP5.Data
{
    public class Image
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public virtual Car? Car { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Data { get; set; }
    }
}
