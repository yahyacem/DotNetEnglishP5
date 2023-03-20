using System.ComponentModel.DataAnnotations;

namespace DotNetEnglishP5.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class YearToNowRangeAttribute : RangeAttribute
    {
        public YearToNowRangeAttribute(int minimum) : base(minimum, DateTime.Now.Year)
        {

        }
    }
}
