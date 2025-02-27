using System.ComponentModel.DataAnnotations;

namespace P01_2022_CA_652_2022_SU_650.Models
{
    public class espacio
    {
        [Key]
        public int espacioId { get; set; }
        public int sucursalId { get; set; }
        public int numeroEspacio { get; set; }
        public int costoHora { get; set; }
        public bool disponible { get; set; }
    }
}
