using System.ComponentModel.DataAnnotations;

namespace P01_2022_CA_652_2022_SU_650.Models
{
    public class reservas
    {
        [Key]
        public int reservaId { get; set; }
        public int usuarioId { get; set; }
        public int espacioId { get; set; }
        public DateTime fechaReservacion { get; set; }
        public int horas {  get; set; }
    }
}
