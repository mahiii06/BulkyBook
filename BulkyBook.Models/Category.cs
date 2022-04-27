using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models 
{
    public class Category
    {   
        [Key]        
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        //https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-6.0
        //use annotation for space
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage = "Display Order must be between 1 and 100 only !!")] //costom error aslo can define
        public int DisplayOrder { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    } 
}
