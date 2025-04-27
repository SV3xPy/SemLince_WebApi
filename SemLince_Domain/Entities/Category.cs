using System.ComponentModel.DataAnnotations;

namespace SemLince_Domain.Entities
{
    public class Category
    {
        [Key]
        public int Cat_ID { get; set; }
        [MaxLength(60)]
        [Required]
        public string Cat_Nombre { get; set; }
    }
}
