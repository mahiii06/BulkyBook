using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class CoverType
    {
        [Key]
        public int CoverType_Id { get; set; }
        [Required]
        public string CoverType_Name { get; set; }
    }
}
