namespace ufide.mascotas.Models.ViewModels
{
    public class PeriodoNacimiento
    {
        public int? Mes { get; set; }
        public int? Anio { get; set; }

        public bool TieneAmbosValores
        {
            get { return Mes.HasValue && Anio.HasValue; }
        }
    }
}
