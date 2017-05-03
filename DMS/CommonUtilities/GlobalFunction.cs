using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMS.DMS.Entities;

namespace DMS.CommonUtilities
{
    public class GlobalFunction
    {
        public static IEnumerable<SelectListItem> GetHeadOfficeList()
        {
            DMSEntities _ent = new DMSEntities();
            return new SelectList(_ent.SetupHaeadOffices.Where(x => x.Status == true).ToList(), "HeadOfficeId", "HeadOfficeName");
        }
    }
}