using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMS.Areas.SetupArea.Models.SetupMasterViewModel;
using DMS.Service.SetupViewModelProviders;
using System.IO;

namespace DMS.Areas.SetupArea.Controllers
{
    public class SetupHeadOfficeController : Controller
    {
        private readonly ISetupHeadOfficeProvider _setupHeadOfficeProvider;
        public SetupHeadOfficeController()
        {
            _setupHeadOfficeProvider = new SetupHeadOfficeProvider();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _GetHeadOfficeList(string name,int? page)
        {
            SetupHeadOfficeViewModel model = new SetupHeadOfficeViewModel();
            model = _setupHeadOfficeProvider.GetHeadOfficeList(name, page);
            return View(model);
        }
        public ActionResult Create()
        {
            SetupHeadOfficeViewModel model = new SetupHeadOfficeViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(SetupHeadOfficeViewModel model,HttpPostedFileBase file)
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
               // TempData["validationError"] = CommonUtilities.UtilitiesMessage.validationError;

                return Json(CommonUtilities.UtilitiesMessage.validationError, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("Index");
            }
            else
            {
                conditionApply = _setupHeadOfficeProvider.Save(model);

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
            SetupHeadOfficeViewModel model = new SetupHeadOfficeViewModel();
            model = _setupHeadOfficeProvider.Edit(id ?? 0);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(SetupHeadOfficeViewModel model,HttpPostedFileBase file)
        {
            var imagePath = "/DMS/Image/CompanyImage/";
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath(imagePath), fileName);
                file.SaveAs(path);
                model.ImageName = file.FileName;
                model.ImagePath = imagePath + file.FileName;
            }
            int condiontApply = 0;
            if (ModelState.IsValid)
            {
                condiontApply = _setupHeadOfficeProvider.Save(model);
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
            condiontApply = _setupHeadOfficeProvider.Delete(id ?? 0);
            if (condiontApply == 1)
            {
                //TempData["save"] = Utilities.ValidationMessage.delete;
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