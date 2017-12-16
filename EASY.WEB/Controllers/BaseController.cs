using System.Web.Mvc;

namespace EASY.WEB.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult Root()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}