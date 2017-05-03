using DMS.CommonUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.Areas.SetupArea.Models.SetupMasterViewModel
{
    public class SetupBranchViewModel
    {
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public int? HeadOfficeId { get; set; }
        public string BranchName { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string Location { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Metropolitan { get; set; }
        public string SubMetropolitan { get; set; }
        public string Municipality { get; set; }
        public string GauPalika { get; set; }
        public string WardNo { get; set; }
        public long? PhoneNumber { get; set; }
        public string Web { get; set; }
        public string EmailId { get; set; }
        public string PanNo { get; set; }
        public int? VatNo { get; set; }
        public DateTime? EstablishDate { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public Pager pager { get; set; }
        public List<SetupBranchViewModel> SetupBranchViewModelList { get; set; }
    }
}