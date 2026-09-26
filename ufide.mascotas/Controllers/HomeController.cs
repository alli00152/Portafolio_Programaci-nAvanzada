using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ufide.mascotas.Models.Entities;
using ufide.mascotas.Models.ViewModels;

namespace ufide.mascotas.Controllers
{
    public class HomeController : Controller
    {
        private static IList<Mascota> _mascotas = new List<Mascota>();

        [HttpGet]
        public ActionResult Index()
        {
            return View(GetIndexContent());
        }

        [HttpPost]
        public ActionResult Index(MascotaViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(GetIndexContent(modelo));
            }

            Mascota mascota;

            switch (modelo.TipoEspecie)
            {
                case TipoEspecie.FELINO:
                    mascota = new Gato(
                        nombre: modelo.Nombre,
                        mesNacimiento: modelo.MesNacimiento,
                        anioNacimiento: modelo.AnioNacimiento
                    );
                    break;
                case TipoEspecie.REPTIL:
                    mascota = new Tortuga(
                        nombre: modelo.Nombre,
                        mesNacimiento: modelo.MesNacimiento,
                        anioNacimiento: modelo.AnioNacimiento
                    );
                    break;
                case TipoEspecie.CANINO:
                default:
                    mascota = new Perro(
                        nombre: modelo.Nombre,
                        mesNacimiento: modelo.MesNacimiento,
                        anioNacimiento: modelo.AnioNacimiento
                    );
                    break;
            }

            _mascotas.Add(mascota);
            TempData["SuccessMessage"] = "Mascota agregada correctamente.";

            return RedirectToAction("Index");
        }

        private MascotaViewModel GetIndexContent(MascotaViewModel modelo = null)
        {
            modelo = modelo ?? new MascotaViewModel();

            modelo.Especies = Enum.GetValues(typeof(TipoEspecie))
                .Cast<TipoEspecie>().Select(especie => new SelectListItem
                {
                    Value = especie.ToString(),
                    Text = especie.ToString()
                });

            modelo.Meses = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Enero" },
                new SelectListItem { Value = "2", Text = "Febrero" },
                new SelectListItem { Value = "3", Text = "Marzo" },
                new SelectListItem { Value = "4", Text = "Abril" },
                new SelectListItem { Value = "5", Text = "Mayo" },
                new SelectListItem { Value = "6", Text = "Junio" },
                new SelectListItem { Value = "7", Text = "Julio" },
                new SelectListItem { Value = "8", Text = "Agosto" },
                new SelectListItem { Value = "9", Text = "Septiembre" },
                new SelectListItem { Value = "10", Text = "Octubre" },
                new SelectListItem { Value = "11", Text = "Noviembre" },
                new SelectListItem { Value = "12", Text = "Diciembre" },
            };

            modelo.Anios = Enumerable.Range(1970, 56)
                .Reverse()
                .Select(anio => new SelectListItem
                {
                    Value = anio.ToString(),
                    Text = anio.ToString()
                });

            modelo.Mascotas = _mascotas.OrderBy(m => m.Nombre).ToList();

            return modelo;
        }
    }
}