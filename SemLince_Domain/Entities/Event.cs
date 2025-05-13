using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemLince_Domain.Entities
{
    public class Event
    {
        [Key]
        public int Eve_ID { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(500)]
        public required string Eve_Description { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime Eve_DateHourStart { get; set; }
        public int Eve_Duration { get; set; }
        public int Eve_IdLocation { get; set; }
        public int Eve_IdCareer { get; set; }
        public int Eve_IdCategory { get; set; }
    }
}
