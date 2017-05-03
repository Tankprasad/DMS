using DMS.Areas.SetupArea.Models.SetupMasterViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMS.Service.SetupViewModelProviders;
using System.IO;

namespace DMS.Areas.SetupArea.Controllers
{
    public class SetupBranchController : Controller
    {

        private readonly ISetupBranchProvider _setupBranchProvider;
        public SetupBranchController()
        {
            _setupBranchProvider = new SetupBranchProvider();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _GetBranchList(string name, int? page)
        {
            SetupBranchViewModel model = new SetupBranchViewModel();
            model = _setupBranchProvider.GetBranchList(name, page);
            return View(model);
        }
        public ActionResult Create()
        {
            SetupBranchViewModel model = new SetupBranchViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(SetupBranchViewModel model,HttpPostedFileBase file)
        {
            var imagePath = "/DMS/Image/CompanyImage/";
            if(file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath(imagePath), fileName);
                file.SaveAs(path);
                model.ImageName = file.FileName;
                model.ImagePath = imagePath + file.FileName;

            }
            
            int conditionApply = 1;
            if (!ModelState.IsValid)
            {
                //TempData["validationError"] = CommonUtilities.UtilitiesMessage.validationError;

                return Json(CommonUtilities.UtilitiesMessage.validationError, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("Index");
            }
            else
            {
                conditionApply = _setupBranchProvider.Save(model);

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
            SetupBranchViewModel model = new SetupBranchViewModel();
            model = _setupBranchProvider.Edit(id ?? 0);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(SetupBranchViewModel model)
        {
            int condiontApply = 0;
            if (ModelState.IsValid)
            {
                condiontApply = _setupBranchProvider.Save(model);
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
            condiontApply = _setupBranchProvider.Delete(id ?? 0);
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