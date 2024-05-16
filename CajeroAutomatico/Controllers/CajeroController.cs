using CajeroAutomatico.Models;
using Microsoft.AspNetCore.Mvc;

namespace CajeroAutomatico.Controllers
{
    public class CajeroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DispensarPapeletas(int cantidad, string tipoCajero)
        {
            PapeletasADispensar model = new PapeletasADispensar();

            try
            {
                switch (tipoCajero)
                {
                    case "1000y200":
                        model.Papeletas = ObtenerPapeletas1000y200(cantidad);
                        break;
                    case "100y500":
                        model.Papeletas = ObtenerPapeletas100y500(cantidad);
                        break;
                    case "eficiente":
                        model.Papeletas = ObtenerPapeletasEficiente(cantidad);
                        break;
                    default:
                        throw new ArgumentException("Tipo de cajero no válido.");
                }

                return View("Resultado", model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }



        private static Dictionary<int, int> ObtenerPapeletas1000y200(int cantidad)
        {
            if(cantidad % 200 != 0)
            {
                throw new InvalidOperationException("Este cajero solo dispensa papeletas de 1000 y 200");
            }
             Dictionary<int, int> papeletas = new()
             {
                { 1000, cantidad / 1000 }, // Cantidad de papeletas de 1000
                { 200, (cantidad % 1000) / 200 } // Cantidad de papeletas de 200
             };


            return papeletas;
        }
        private static Dictionary<int, int> ObtenerPapeletas100y500(int cantidad)
        {
            if(cantidad % 100 != 0)
            {
                throw new InvalidOperationException("Este cajero solo dispensa papeletas de 100 y 500");
            }
            Dictionary<int, int> papeletas = new()
             {
                { 500, cantidad / 500 }, // Cantidad de papeletas de 500
                { 100, (cantidad % 500) / 100 } // Cantidad de papeletas de 100
             };


            return papeletas;
        }
        private static Dictionary<int, int> ObtenerPapeletasEficiente(int cantidad)
        {
            if(cantidad % 100 != 0)
            {
                throw new InvalidOperationException("Este cajero solo dispensa papeletas de 100, 200, 500 y 1000");
            }
            Dictionary<int, int> papeletas = new()
             {
                { 1000, cantidad / 1000 }, // Cantidad de papeletas de 1000
                { 500, (cantidad % 1000) / 500}, // Cantidad de papeletas de 500
                {200, ((cantidad % 1000) % 500) / 200 }, // Cantidad de papeletas de 200
                { 100, (((cantidad % 1000) % 500) % 200) / 100 } // Cantidad de papeletas de 100
             };


            return papeletas;
        }


    }
}
