using System;

namespace ufide.mascotas.Models.Entities
{
    public abstract class Mascota
    {
        private TipoEspecie _tipoEspecie;

        protected Mascota(
            TipoEspecie tipoEspecie,
            string nombre,
            int mesNacimiento,
            int anioNacimiento)
        {
            this.TipoEspecie = tipoEspecie;
            this.Nombre = nombre;
            this.MesNacimiento = mesNacimiento;
            this.AnioNacimiento = anioNacimiento;
            this.FichaVacunacion = new FichaVacunacion();
        }

        // Lo asigna el controlador al registrar (Leccion 03)
        public int Id { get; internal set; }
        public string Nombre { get; private set; }
        public int MesNacimiento { get; private set; }
        public int AnioNacimiento { get; private set; }

        public TipoEspecie TipoEspecie
        {
            get { return _tipoEspecie; }
            private set { _tipoEspecie = value; }
        }

        // Composición: la mascota delega el detalle de vacunas a su ficha
        public FichaVacunacion FichaVacunacion { get; private set; }

        public DateTime FechaNacimiento
        {
            get { return new DateTime(AnioNacimiento, MesNacimiento, 1); }
        }

        // PERRO, GATO o TORTUGA
        public abstract string Tipo { get; }

        public abstract string Describir();
    }
}
