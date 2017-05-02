using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DMS.CommonUtilities;

namespace DMS.Areas.SetupArea.Models.SetupMasterViewModel
{
    public class SetupDepartmentViewModel
    {
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Department Name is Required")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public List<SetupDepartmentViewModel> SetupDepartmentViewModelList { get; set; }
        public Pager pager { get; set; }
    }
}