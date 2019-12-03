//библиотеки(libraries)
using BLL.AdditionalModels;
using BLL.Interface;
using System.Linq;
using System.Web.Mvc;

namespace UrlShorted.Controllers
{
    //HomeController для управление Url
    public class HomeController : Controller
    {
        private readonly IUrlMdl urlMdl; 
        public HomeController(IUrlMdl urlMdlR)
        {
            urlMdl = urlMdlR;
        }
        //View добавление моделя 
        public ActionResult Insert()
        {
            return View(new UrlMdl());
        }

        //HttpPost добавление моделя 
        [HttpPost]
        public ActionResult Insert(string FullUrl)
        {
            if (FullUrl is null)
                return View();
            var result = urlMdl.Insert(new UrlMdl
            {
                FullUrl = FullUrl,
            });
            if (result.Success == false)
                return View();
            ViewBag.ShortUrl = result.Value.ShortUrl;
            ViewBag.FullUrl = result.Value.FullUrl;
            ViewBag.Message = result.Message;
            return View();
        }
        //Список моделя 
        public ActionResult Index(string message = null)
        {
            if (message != null)
                ViewBag.Message = message;
            var result = urlMdl.UrlMdlList.ToList();
            return View(result);
        }

        //Удаление моделя 
        public ActionResult Delete(int Id)
        {
            var result = urlMdl.Delete(Id);
            if (result.Success == false)
                return RedirectToAction("Index", new { message = result.Message });

            return RedirectToAction("Index", new { message = result.Message });

        }

        //Перенаправление HttpGet RedirectTo
        [Route("redirect/{path}")]
        public ActionResult RedirectTo(string path)
        {
            var result = urlMdl.RedirectToUrl(path);
            return Redirect("http://" + result.Value);
        }
        //HttpGet редактирование UrlMdl
        public ActionResult Edit(int Id)
        {
            var result = urlMdl.Select(Id);
            return View(result.Value);
        }
        //HttpPost редактирование UrlMdl
        [HttpPost]
        public ActionResult Edit(UrlMdl model)
        {
            var result = urlMdl.Edit(model);
            if (result.Success == false)
                return View(model);
            return RedirectToAction("Index");
        }
    }
}