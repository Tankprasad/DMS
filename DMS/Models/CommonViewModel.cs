using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.Models
{

    public class DeleteConformationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string PartialActionName { get; set; }
        public string AppendHtmlId { get; set; }
        public string AreaName { get; set; }
    }
    public class DropDownListViewModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

}
