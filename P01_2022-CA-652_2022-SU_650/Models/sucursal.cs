using System.ComponentModel.DataAnnotations;

namespace P01_2022_CA_652_2022_SU_650.Models
{
    public class sucursal
    {
        [Key]
        public int sucursalId { get; set; }
        public string sucursalNombre {  get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string administrador { get; set; }
        public int espacios { get; set; }
    }
}
