using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ufide.mascotas.Binders;
using ufide.mascotas.Models.Entities;
using ufide.mascotas.Models.ViewModels;

namespace ufide.mascotas.Controllers
{
    [RoutePrefix("mascotas")]
    public class HomeController : Controller
    {
        private static IList<Mascota> _mascotas = new List<Mascota>();

        // ------------------------------------------------------------
        // Lista
        // ------------------------------------------------------------
        [HttpGet]
        [Route("")]
        [Route("~/")]
        public ActionResult Index()
        {
            // ViewBag y ViewData: solo viven durante esta solicitud
            ViewBag.Subtitulo = "Administración de mascotas";
            ViewData["FechaConsulta"] = DateTime.Now;

            // Cookie: preferencia no sensible del usuario
            if (Request.Cookies["MascotasVista"] == null)
            {
                var cookie = new HttpCookie("MascotasVista", "tabla")
                {
                    Expires = DateTime.Now.AddDays(7),
                    HttpOnly = true,
                    Secure = Request.IsSecureConnection
                };
                Response.Cookies.Add(cookie);
            }

            return View(new MascotaListaViewModel
            {
                Mascotas = _mascotas.OrderBy(m => m.Nombre).ToList()
            });
        }

        // ------------------------------------------------------------
        // Registrar (GET muestra el formulario, POST lo procesa)
        // ------------------------------------------------------------
        [HttpGet]
        [Route("registrar")]
        public ActionResult Registrar()
        {
            return View(GetRegistrarContent());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("registrar")]
        public ActionResult Registrar(MascotaViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(GetRegistrarContent(modelo));
            }

            // La Factory decide qué subclase crear (ya no hay switch aquí)
            var mascota = MascotaFactory.Crear(
                modelo.TipoEspecie.Value,
                modelo.Nombre.Trim(),
                modelo.MesNacimiento,
                modelo.AnioNacimiento);

            mascota.Id = _mascotas.Count == 0 ? 1 : _mascotas.Max(m => m.Id) + 1;
            _mascotas.Add(mascota);

            // TempData sobrevive a la redirección; Post/Redirect/Get
            TempData["SuccessMessage"] = "Mascota registrada correctamente.";
            return RedirectToAction("Detalle", new { id = mascota.Id });
        }

        // ------------------------------------------------------------
        // Detalle (restricción de ruta: entero >= 1)
        // ------------------------------------------------------------
        [HttpGet]
        [Route("detalle/{id:int:min(1)}", Name = "DetalleMascota")]
        public ActionResult Detalle(int id)
        {
            var mascota = _mascotas.SingleOrDefault(m => m.Id == id);
            if (mascota == null)
            {
                Response.TrySkipIisCustomErrors = true;
                return HttpNotFound("No existe una mascota con ese identificador.");
            }

            return View(new MascotaDetalleViewModel
            {
                Mascota = mascota,
                MensajeEstado = "Registro encontrado."
            });
        }

        // ------------------------------------------------------------
        // Buscar por período (Model Binder personalizado)
        // ------------------------------------------------------------
        [HttpGet]
        [Route("buscar")]
        public ActionResult Buscar(
            [ModelBinder(typeof(PeriodoNacimientoBinder))] PeriodoNacimiento periodo)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                return View("Error");
            }

            var resultado = _mascotas.AsEnumerable();

            if (periodo.TieneAmbosValores)
            {
                resultado = resultado.Where(m =>
                    m.MesNacimiento == periodo.Mes.Value &&
                    m.AnioNacimiento == periodo.Anio.Value);
            }

            ViewBag.Subtitulo = "Resultado de la búsqueda";
            ViewData["FechaConsulta"] = DateTime.Now;

            return View("Index", new MascotaListaViewModel
            {
                Mascotas = resultado.OrderBy(m => m.Nombre).ToList()
            });
        }

        // ------------------------------------------------------------
        // Resultados HTTP (Leccion 02): ContentResult, FileResult, RedirectResult
        // ------------------------------------------------------------
        [HttpGet]
        [Route("estado")]
        public ContentResult Estado()
        {
            return Content("Servicio de mascotas disponible", "text/plain", Encoding.UTF8);
        }

        [HttpGet]
        [Route("reporte")]
        public FileResult DescargarLista()
        {
            var filas = new List<string> { "Id,Nombre,Especie,Tipo,Mes,Año" };
            filas.AddRange(_mascotas.Select(m => string.Format(
                "{0},{1},{2},{3},{4},{5}",
                m.Id, m.Nombre, m.TipoEspecie, m.Tipo, m.MesNacimiento, m.AnioNacimiento)));

            return File(
                Encoding.UTF8.GetBytes(string.Join("\n", filas)),
                "text/csv",
                "mascotas.csv");
        }

        [HttpGet]
        [Route("inicio")]
        public RedirectResult IrAInicio()
        {
            return Redirect(Url.Action("Index", "Home"));
        }

        // ------------------------------------------------------------
        // Página de error segura (destino de customErrors)
        // ------------------------------------------------------------
        [Route("error")]
        public ActionResult Error()
        {
            return View("Error");
        }

        private MascotaViewModel GetRegistrarContent(MascotaViewModel modelo = null)
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

            modelo.Anios = Enumerable.Range(1970, 57)
                .Reverse()
                .Select(anio => new SelectListItem
                {
                    Value = anio.ToString(),
                    Text = anio.ToString()
                });

            return modelo;
        }
    }
}
