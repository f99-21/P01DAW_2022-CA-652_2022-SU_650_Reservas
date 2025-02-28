using System.ComponentModel.DataAnnotations;

namespace P01_2022_CA_652_2022_SU_650.Models
{
    public class roles
    {
        [Key]
        public int rolId { get; set; }
        public string nombre { get; set; }
    }
}
