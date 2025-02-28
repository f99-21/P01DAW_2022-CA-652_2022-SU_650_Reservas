using System.ComponentModel.DataAnnotations;

namespace P01_2022_CA_652_2022_SU_650.Models
{
    public class usuarios
    {
        [Key]
        public int usuarioId { get; set; }
        public string nombreUsuario { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string contrasena { get; set; }
        public int rolId { get; set; }
    }
}
