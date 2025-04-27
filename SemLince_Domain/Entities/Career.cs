using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemLince_Domain.Entities
{
    public class Career
    {
        [Key]
        public int Car_ID { get; set; }
        [MaxLength(60)]
        [Required]
        public string Car_Nombre { get; set; }
    }
}
