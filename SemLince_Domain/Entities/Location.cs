using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemLince_Domain.Entities
{
    public class Location
    {
        [Key]
        public int Loc_ID { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(60)]
        [Required]
        public required string Loc_Name { get; set; }

        [Required]
        public double Loc_Latitud { get; set; }

        [Required]
        public double Loc_Longitud { get; set; }

        [Required]
        public required int Loc_Capacity { get; set; }

        public int Loc_Building { get; set; }

    }
}
