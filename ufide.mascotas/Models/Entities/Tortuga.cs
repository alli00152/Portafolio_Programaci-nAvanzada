namespace ufide.mascotas.Models.Entities
{
    // sealed: el diseño decide que no habrá subclases más específicas de Tortuga
    public sealed class Tortuga : Mascota
    {
        public Tortuga(
            string nombre,
            int mesNacimiento,
            int anioNacimiento)
            : base(
                    TipoEspecie.REPTIL,
                    nombre,
                    mesNacimiento,
                    anioNacimiento
                  )
        {

        }

        public override string Tipo
        {
            get { return "TORTUGA"; }
        }

        public override string Describir()
        {
            return $"Soy una tortuga llamada {this.Nombre}, " +
                    $"nací en el mes {this.MesNacimiento} " +
                    $"del año {this.AnioNacimiento}.";
        }
    }
}
