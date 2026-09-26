using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ufide.mascotas.Models.Entities
{
    public class Gato : Mascota
    {
        public Gato(
            string nombre,
            int mesNacimiento,
            int anioNacimiento)
            : base(
                    TipoEspecie.FELINO,
                    nombre,
                    mesNacimiento,
                    anioNacimiento
                  )
        {

        }

        public override string Describir()
        {
            return $"Soy un gato llamado {this.Nombre}, " +
                    $"nací en el mes {this.MesNacimiento} " +
                    $"del año {this.AnioNacimiento}.";
        }
    }
}