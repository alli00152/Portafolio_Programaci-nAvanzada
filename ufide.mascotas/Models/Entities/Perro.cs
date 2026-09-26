using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ufide.mascotas.Models.Entities
{
    public class Perro : Mascota
    {
        public Perro(
            string nombre,
            int mesNacimiento,
            int anioNacimiento)
            : base(
                    TipoEspecie.CANINO,
                    nombre,
                    mesNacimiento,
                    anioNacimiento
                  )
        {
            
        }

        public override string Describir()
        {
            return $"Soy un perro llamado {this.Nombre}, " + 
                    $"nací en el mes {this.MesNacimiento} " +
                    $"del año {this.AnioNacimiento}.";
        }
    }
}