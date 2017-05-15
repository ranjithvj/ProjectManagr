using Models;
using ProjectManagr.ViewModels;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagr.Controllers
{
    public class ScreenController : Controller
    {
        private readonly IScreenService _screenService;

        public ScreenController(IScreenService screenService)
        {
            _screenService = screenService;
        }


        // GET: Screen
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllScreens()
        {
            List<ScreenVM> returnData = new List<ScreenVM>();

            IEnumerable<Screen> screens = _screenService.GetAll();

            foreach(var screen in screens)
            {
                returnData.Add(new ScreenVM(screen));
            }

            returnData.Add(new ScreenVM(new Screen { Name = "RelatedWell" ,
            TechLead = "Selva", EstCompletion = DateTime.Now, EstRelease = DateTime.Now}));

            return Json(new { data = returnData }, JsonRequestBehavior.AllowGet);
        }

    }


}