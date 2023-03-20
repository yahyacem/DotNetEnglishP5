using System.ComponentModel.DataAnnotations;

namespace DotNetEnglishP5.Data
{
    public class Make
    {
        public int Id { get; set; }
        [Display(Name = "Make")]
        public string Name { get; set; }
    }
}
