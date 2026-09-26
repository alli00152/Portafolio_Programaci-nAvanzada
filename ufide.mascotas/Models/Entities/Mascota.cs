using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

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
        }

        public string Nombre { get; set; }
        public int MesNacimiento { get; set; }
        public int AnioNacimiento { get; set; }

        public TipoEspecie TipoEspecie
        {
            get { return _tipoEspecie; }
            private set { _tipoEspecie = value; }
        }

        public abstract string Describir();
    }
}