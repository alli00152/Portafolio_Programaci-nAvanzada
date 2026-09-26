using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ufide.mascotas.Models.Entities
{
    public class Tortuga : Mascota
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

        public override string Describir()
        {
            return $"Soy una tortuga llamada {this.Nombre}, " +
                    $"nací en el mes {this.MesNacimiento} " +
                    $"del año {this.AnioNacimiento}.";
        }
    }
}