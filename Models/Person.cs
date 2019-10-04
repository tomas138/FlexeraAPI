using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexeraAPI.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public double Balance { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
