using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemLince_Domain.Entities
{
    public class Building
    {
        [Key]
        public int Edi_ID { get; set; }

        [Required]
        [MaxLength(6)]
        public required string Edi_Nombre { get; set; }

        [Required]
        public int Edi_Campus {  get; set; }
    }
}
