using System.Web.Mvc;
using DMS.Areas.SetupArea.Models.SetupMasterViewModel;
using DMS.Service.SetupViewModelProviders;


namespace DMS.Areas.SetupArea.Controllers
{
    public class SetupDepartmentController : Controller
    {

        private readonly ISetupDepartmentProvider _setupDepartmentProvider;
        public SetupDepartmentController()
        {
            _setupDepartmentProvider = new SetupDepartmentProvider();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _GetDepartmentList(string name, int? page)
        {
            SetupDepartmentViewModel model = new SetupDepartmentViewModel();
            model = _setupDepartmentProvider.GetDepartmentList(name, page);
            return View(model);
        }
        public ActionResult Create()
        {
            SetupDepartmentViewModel model = new SetupDepartmentViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(SetupDepartmentViewModel model)
        {
            int conditionApply = 1;
            if (!ModelState.IsValid)
            {
                TempData["validationError"] = CommonUtilities.UtilitiesMessage.validationError;

                return Json(CommonUtilities.UtilitiesMessage.validationError, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("Index");
            }
            else
            {
                conditionApply = _setupDepartmentProvider.Save(model);

                if (conditionApply == 2)
                {
                    //TempData["AlreadyExits"] = Utilities.ValidationMessage.AlreadyExits;
                    return Json(CommonUtilities.UtilitiesMessage.AlreadyExits, JsonRequestBehavior.AllowGet);

                }

                else if (conditionApply == 1)
                {
                    //TempData["save"] = Utilities.ValidationMessage.save;

                    return Json(CommonUtilities.UtilitiesMessage.save, JsonRequestBehavior.AllowGet);


                }
                else
                {
                    //TempData["savefailed"] = Utilities.ValidationMessage.savefailed;

                    return Json(CommonUtilities.UtilitiesMessage.savefailed, JsonRequestBehavior.AllowGet);
                }
            }
            // return View(model);

            //return Json(data, JsonRequestBehavior.AllowGet);

            
        }

        public ActionResult Edit(int? id)
        {
            SetupDepartmentViewModel model = new SetupDepartmentViewModel();
            model = _setupDepartmentProvider.Edit(id ?? 0);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(SetupDepartmentViewModel model)
        {
            int condiontApply = 0;
            if (ModelState.IsValid)
            {
                condiontApply = _setupDepartmentProvider.Save(model);
            }
            if (condiontApply == 1)
            {
                // TempData["save"] = Utilities.ValidationMessage.edit;
                return Json(CommonUtilities.UtilitiesMessage.edit, JsonRequestBehavior.AllowGet);
            }
            else if (condiontApply == 2)
            {
                //TempData["AlreadyExits"] = Utilities.ValidationMessage.AlreadyExits;
                return Json(CommonUtilities.UtilitiesMessage.AlreadyExits, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //TempData["savefailed"] = Utilities.ValidationMessage.editfailed;
                return Json(CommonUtilities.UtilitiesMessage.editfailed, JsonRequestBehavior.AllowGet);
            }

            // return RedirectToAction("Index");
            
        }
        public ActionResult Delete(int? id)
        {
            int condiontApply = 0;
            condiontApply = _setupDepartmentProvider.Delete(id ?? 0);
            if (condiontApply == 1)
            {
                return Json(CommonUtilities.UtilitiesMessage.delete, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //TempData["savefailed"] = Utilities.ValidationMessage.deletefailed;
                return Json(CommonUtilities.UtilitiesMessage.deletefailed, JsonRequestBehavior.AllowGet);
            }
        }
    }
}