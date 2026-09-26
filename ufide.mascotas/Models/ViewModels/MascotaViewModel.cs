using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ufide.mascotas.Models.Entities;

namespace ufide.mascotas.Models.ViewModels
{
    public class MascotaViewModel
    {
        [Required(ErrorMessage = "Seleccione una especie.")]
        public TipoEspecie? TipoEspecie { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(60, ErrorMessage = "El nombre no puede superar 60 caracteres.")]
        public string Nombre { get; set; }

        [Range(1, 12, ErrorMessage = "Seleccione un mes válido.")]
        public int MesNacimiento { get; set; }

        [Range(1970, 2026, ErrorMessage = "Seleccione un año entre 1970 y 2026.")]
        public int AnioNacimiento { get; set; }

        public IEnumerable<SelectListItem> Especies { get; set; }
        public IEnumerable<SelectListItem> Meses { get; set; }
        public IEnumerable<SelectListItem> Anios { get; set; }
        public IList<Mascota> Mascotas { get; set; }
    }
}