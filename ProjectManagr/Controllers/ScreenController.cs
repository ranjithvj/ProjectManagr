using DataTables.Mvc;
using Models;
using ProjectManagr.ViewModels;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace ProjectManagr.Controllers
{
    public class ScreenController : BaseController
    {
        private readonly IScreenService _screenService;

        //TODO:REMOVE
        private static List<ScreenVM> returnData = new List<ScreenVM>();

        public ScreenController(IScreenService screenService)
        {
            _screenService = screenService;
        }


        // GET: Screen
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IEnumerable<Screen> screens = _screenService.GetAll();
            var totalCount = screens.Count();

            //TODO:REMOVE
            if (returnData.Count == 0)
            {
                for (int i = 0; i < 100; i++)
                {
                    Screen screen = new Screen();
                    screen.Name = "Related Well " + i;
                    screen.TechLead = "Selva";
                    screen.EstCompletion = DateTime.UtcNow.AddDays(i);
                    screen.EstRelease = DateTime.UtcNow.AddDays(-i);
                    ScreenVM vm = IMapper.Map<Screen, ScreenVM>(screen);
                    vm.Id = i;
                    returnData.Add(vm);
                }
            }

            // Searching and Sorting
            var result = SearchProject(requestModel, returnData);
            var filteredCount = result.Count();

            // Paging
            result = result.Skip(requestModel.Start).Take(requestModel.Length).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, result, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        private List<ScreenVM> SearchProject(IDataTablesRequest requestModel, List<ScreenVM> query)
        {
            // Search
            if (requestModel.Search.Value != string.Empty)
            {
                //TODO: Change the search criteria
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.Name.Contains(value) ||
                                         p.TechLead.Contains(value)).ToList();
            }

            var filteredCount = query.Count();

            // Sort
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            query = query.OrderBy(orderByString == string.Empty ? "Name asc" : orderByString).ToList(); //TODO: Change sort column name

            return query;
        }

        // GET: Screen/Create  
        public ActionResult Create()
        {
            Screen screen = new Screen();
            ScreenVM vm = IMapper.Map<Screen, ScreenVM>(screen);

            return PartialView("_CreateOrEditProject", vm);
        }

        // POST: Screen/Create  
        [HttpPost]
        public ActionResult Create(ScreenVM assetVM)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreateOrEditProject", assetVM);

            //_screenService.Insert(assetVM);
            //Asset asset = MaptoModel(assetVM);

            //DbContext.Assets.Add(asset);
            //var task = DbContext.SaveChangesAsync();
            //await task;

            //if (task.Exception != null)
            //{
            //    ModelState.AddModelError("", "Unable to add the Asset");
            //    return View("_CreatePartial", assetVM);
            //}

            return Content("success");
        }

        // GET: Screen/Edit 
        public ActionResult Edit(int id)
        {
            Screen screen = new Screen();
            screen.Name = "Related Well";
            screen.TechLead = "Selva";
            screen.EstCompletion = DateTime.UtcNow;
            screen.EstRelease = DateTime.UtcNow;
            ScreenVM vm = IMapper.Map<Screen, ScreenVM>(screen);
            vm.IsEdit = true;
            return PartialView("_CreateOrEditProject", vm);
        }

        // POST: Screen/Edit
        [HttpPost]
        public ActionResult Edit(ScreenVM assetVM)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreateOrEditProject", assetVM);

            //_screenService.Insert(assetVM);
            //Asset asset = MaptoModel(assetVM);

            //DbContext.Assets.Add(asset);
            //var task = DbContext.SaveChangesAsync();
            //await task;

            //if (task.Exception != null)
            //{
            //    ModelState.AddModelError("", "Unable to add the Asset");
            //    return View("_CreatePartial", assetVM);
            //}

            return Content("success");
        }

        // GET: Screen/Details
        public ActionResult Details(int id)
        {
            Screen screen = new Screen();
            screen.Name = "Related Well";
            screen.TechLead = "Selva";
            screen.EstCompletion = DateTime.UtcNow;
            screen.EstRelease = DateTime.UtcNow;
            ScreenVM vm = IMapper.Map<Screen, ScreenVM>(screen);
            vm.IsEdit = true;
            return PartialView("_ViewProject", vm);
        }

        // GET: Screen/Delete
        public ActionResult Delete(int id)
        {
            Screen screen = new Screen();
            screen.Name = "Related Well";
            screen.TechLead = "Selva";
            screen.EstCompletion = DateTime.UtcNow;
            screen.EstRelease = DateTime.UtcNow;
            ScreenVM vm = IMapper.Map<Screen, ScreenVM>(screen);
            vm.IsEdit = true;
            return PartialView("_DeleteProject", vm);
        }

        // POST: Screen/Delete
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteProject(int id)
        {
            //if (!ModelState.IsValid)
            //    return PartialView("_CreateOrEditProject", assetVM);

            //_screenService.Insert(assetVM);
            //Asset asset = MaptoModel(assetVM);

            //DbContext.Assets.Add(asset);
            //var task = DbContext.SaveChangesAsync();
            //await task;

            //if (task.Exception != null)
            //{
            //    ModelState.AddModelError("", "Unable to add the Asset");
            //    return View("_CreatePartial", assetVM);
            //}

            return Content("success");
        }
    }


}