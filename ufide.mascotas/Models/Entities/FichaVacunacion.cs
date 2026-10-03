using System;

namespace ufide.mascotas.Models.Entities
{
    // Composición: una Mascota "tiene una" FichaVacunacion (no "es una")
    public class FichaVacunacion
    {
        public bool TieneVacunaAntirrabica { get; private set; }
        public DateTime? FechaUltimaVacuna { get; private set; }

        public void RegistrarVacuna(DateTime fecha)
        {
            TieneVacunaAntirrabica = true;
            FechaUltimaVacuna = fecha;
        }
    }
}
