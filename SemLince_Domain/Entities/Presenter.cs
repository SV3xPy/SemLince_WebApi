using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemLince_Domain.Entities
{
    public class Presenter
    {
        [Key]
        public int Per_ID { get; set; }
        [Required]
        [MinLength(20)]
        public string Per_CV { get; set; }
        public SqlBinary Per_ImgProfile { get; set; }
        public int Per_Persona { get; set; }

    }
}
