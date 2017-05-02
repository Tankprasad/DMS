using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMS.Models;

namespace DMS.Controllers
{
    public class CommonAjaxRequestController : Controller
    {
        public ActionResult DeleteConformation(int? id, string name, string actionName, string controllerName, string areaName, string partialActionName, string appendHtmlId)
        {
            DeleteConformationViewModel model = new DeleteConformationViewModel();
            model.ActionName = actionName;
            model.Id = id ?? 0;
            model.ControllerName = controllerName;
            model.Name = name;
            model.AreaName = areaName;
            return View(model);
        }
    }
}