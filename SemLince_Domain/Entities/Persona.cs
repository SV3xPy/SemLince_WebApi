using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemLince_Domain.Entities
{
    public class Persona
    {
        [Key]
        public int Per_ID { get; set; }
        [Required, MaxLength(60)]
        public required string Per_Name { get; set; }
        [MaxLength(60)]
        public string? Per_PaternalSurname { get; set; }
        [MaxLength(60)]
        public string? Per_MaternalSurname { get; set; }
        public int Per_NControl { get; set; }
        [Required, MaxLength(50), DataType(DataType.EmailAddress)]
        public required string Per_Email { get; set; }
        [Required, MaxLength(50), DataType(DataType.Password)]
        public required string Per_Password { get; set; }
        public int Per_Semester { get; set; }
        public int Per_Carrer { get; set; }
    }
}
