//importa clases y/o dependencias necesarias para la clase
using CrudNet8MVC.Datos;
using CrudNet8MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


//inicia con el namespace, define la pertenencia y pertenencia de un conjunto de clases
namespace CrudNet8MVC.Controllers
{
   
    public class InicioController : Controller //crea la clase InicioController y hereda la clase Controller
    {
        //DbContext es una clase que representa una sesion con la base de datos y se utiliza para realizar 
        //operaciones de consulta, insercion, actualizacion, y eliminacion en la base de datos a traves
        //de modelos de entidad.
        private readonly AplicationDBContext _contexto;//crea el objeto _contexto del tipo AplicationDBContext

        //esta función inicializa el atributo _contexto con un valor que entra por parametro
        public InicioController(AplicationDBContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]//metodo get, tomado en cuenta en el envío del formulario desde la vista
        public async Task<IActionResult> Index() //declaracion del metodo index como asincrono
        {
            return View(await _contexto.Contacto.ToListAsync());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Contacto.Add(contacto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id){
            if(id == null)
            {
                return NotFound();
            }

            var contacto = _contexto.Contacto.Find(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = _contexto.Contacto.Find(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = _contexto.Contacto.Find(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpPost, ActionName("Borrar")]
        public async Task<IActionResult> BorrarContacto(int? id)
        {
            var contacto = await _contexto.Contacto.FindAsync(id);
            if(contacto == null)
            {
                return View();
            }

            // Borrado
            _contexto.Contacto.Remove(contacto);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Contacto.Update(contacto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
