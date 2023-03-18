using System.Web.Mvc;
using MontyHallSimulatorApplication.Models;
using MontyHallSimulatorApplication.Service;

namespace MontyHallSimulatorApplication.Controllers
{
    public class MontyHallController : Controller
    {
        public ActionResult Index()
        {
            return View(new Game());
        }

        [HttpPost]
        public ActionResult Index(Game gameRequest)
        {
            if (ModelState.IsValid)
            {
                SimulatorService simulator = new SimulatorService();
                Game gameResponse = simulator.PlayGame(gameRequest);
                return View(gameResponse);
            }

            return View();
        }
    }
}
