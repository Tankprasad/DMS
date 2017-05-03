using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Areas.SetupArea.Models.SetupMasterViewModel;
using DMS.DMS.Entities;
using System.Data.Entity;
using DMS.CommonUtilities;

namespace DMS.Service.SetupViewModelProviders
{
    public interface ISetupBranchProvider
    {
        int Save(SetupBranchViewModel model);
        SetupBranchViewModel Edit(int branchId);
        SetupBranchViewModel GetBranchList(string branchName, int? page);
        int Delete(int branchId);

    }
    public class SetupBranchProvider:ISetupBranchProvider
    {
        public readonly DMSEntities _ent;
        public SetupBranchProvider()
        {
            this._ent = new DMSEntities();
        }
        public int Save(SetupBranchViewModel model)
        {
            //var user = Userprovider.CustomeUserManager.GetUserDetails();
            SetupBranch setupBranchEntity = new SetupBranch();
            var data = _ent.SetupBranches.Where(x => x.BranchName == model.BranchName.Trim() && x.HeadOfficeId==model.HeadOfficeId&& x.DeletedDate == null).ToList();
            if (data.Count > 0)
            {
                return 2;
            }
            setupBranchEntity.BranchCode = model.BranchName;
            setupBranchEntity.HeadOfficeId = model.HeadOfficeId??0;
            setupBranchEntity.BranchName = model.BranchName;
            setupBranchEntity.ImageName = model.ImageName;
            setupBranchEntity.ImagePath = model.ImagePath;
            setupBranchEntity.Location = model.Location;
            setupBranchEntity.State = model.State;
            setupBranchEntity.District = model.District;
            setupBranchEntity.Metropolitan = model.Metropolitan;
            setupBranchEntity.SubMetropolitan = model.SubMetropolitan;
            setupBranchEntity.Municipality = model.Municipality;
            setupBranchEntity.GauPalika = model.GauPalika;
            setupBranchEntity.WardNo = model.WardNo;
            setupBranchEntity.EmailId = model.EmailId;
            setupBranchEntity.Web = model.Web;
            setupBranchEntity.PhoneNumber = model.PhoneNumber;
            setupBranchEntity.PanNo = model.PanNo;
            setupBranchEntity.VatNo = model.VatNo;
            setupBranchEntity.EstablishDate = model.EstablishDate;
            setupBranchEntity.RegisteredDate = model.RegisteredDate;
            if (model.BranchId> 0)
            {
                setupBranchEntity.Status = model.Status;
                setupBranchEntity.UpdatedBy = 1;// user.UserId;
                setupBranchEntity.BranchId= model.BranchId;
                setupBranchEntity.UpdatedDate = DateTime.UtcNow;
                _ent.Entry(setupBranchEntity).State = EntityState.Modified;
                _ent.Entry(setupBranchEntity).Property(x => x.CreatedBy).IsModified= false;
                _ent.Entry(setupBranchEntity).Property(x => x.CreatedDate).IsModified= false;

            }
            else
            {
                setupBranchEntity.Status = true;
                setupBranchEntity.CreatedBy = 1;// user.UserId;
                setupBranchEntity.CreatedDate = DateTime.UtcNow;
                _ent.Entry(setupBranchEntity).State = EntityState.Added;
            }
            try
            {
                _ent.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public SetupBranchViewModel GetBranchList(string branchName, int? page)
        {

            int pageSize = 2;// Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);


            SetupBranchViewModel model = new SetupBranchViewModel();
            if (branchName != "")
            {
                var pager = new Pager(_ent.SetupBranches.Where(x => x.DeletedDate == null && x.BranchName.Contains(branchName)).Count(), page);
                model.SetupBranchViewModelList = (from b in _ent.SetupBranches
                                                      where b.BranchName.Contains(branchName) && b.DeletedDate == null
                                                      select new SetupBranchViewModel
                                                      {
                                                          BranchId= b.BranchId,
                                                          BranchName = b.BranchName,
                                                          HeadOfficeId = b.HeadOfficeId,
                                                          ImageName = b.ImageName,
                                                          ImagePath = b.ImagePath,
                                                          State = b.State,
                                                          District = b.District,
                                                          Metropolitan = b.Metropolitan,
                                                          SubMetropolitan = b.SubMetropolitan,
                                                          Municipality = b.Municipality,
                                                          GauPalika = b.GauPalika,
                                                          WardNo = b.WardNo,
                                                          Web = b.Web,
                                                          PanNo = b.PanNo,
                                                          VatNo = b.VatNo,
                                                          EstablishDate = b.EstablishDate,
                                                          RegisteredDate = b.RegisteredDate,
                                                          Status = b.Status,
                                                          Location = b.Location,
                                                          PhoneNumber = b.PhoneNumber,
                                                          EmailId = b.EmailId,

                                                      }).OrderBy(x => x.BranchId).
                                      Skip((pager.CurrentPage - 1) * pageSize).Take(pageSize).ToList();
               

                
            }
            else
            {
                var pager = new Pager(_ent.SetupBranches.Where(x => x.DeletedDate == null).Count(), page);

                model.SetupBranchViewModelList = (from b in _ent.SetupBranches
                                                      where b.DeletedDate == null
                                                      select new SetupBranchViewModel
                                                      {
                                                          BranchId = b.BranchId,
                                                          BranchName = b.BranchName,
                                                          HeadOfficeId = b.HeadOfficeId,
                                                          ImageName = b.ImageName,
                                                          ImagePath = b.ImagePath,
                                                          State = b.State,
                                                          District = b.District,
                                                          Metropolitan = b.Metropolitan,
                                                          SubMetropolitan = b.SubMetropolitan,
                                                          Municipality = b.Municipality,
                                                          GauPalika = b.GauPalika,
                                                          WardNo = b.WardNo,
                                                          Web = b.Web,
                                                          PanNo = b.PanNo,
                                                          VatNo = b.VatNo,
                                                          EstablishDate = b.EstablishDate,
                                                          RegisteredDate = b.RegisteredDate,
                                                          Status = b.Status,
                                                          Location = b.Location,
                                                          PhoneNumber = b.PhoneNumber,
                                                          EmailId = b.EmailId,

                                                      }).OrderBy(x => x.BranchId).
                                      Skip((pager.CurrentPage - 1) * pageSize).Take(pageSize).ToList();
                model.pager = pager;
            }

            return model;
        }
        public int Delete(int BranchId)

        {
            //var user = Userprovider.CustomeUserManager.GetUserDetails();
            var data = _ent.SetupBranches.Where(x => x.BranchId== BranchId).FirstOrDefault();
            if (data != null)
            {
                //data.DeletedBy = user.UserId;
                data.DeletedDate = DateTime.UtcNow;
                _ent.Entry(data).State = EntityState.Modified;
                try
                {
                    _ent.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {

                    return 0;
                }

            }
            return 0;

        }

        public SetupBranchViewModel Edit(int BranchId)
        {
            SetupBranchViewModel model = new SetupBranchViewModel();
            var data = _ent.SetupBranches.Where(b=> b.BranchId==BranchId&& b.DeletedDate == null).FirstOrDefault();
            if (data != null)
            {
              
                model.HeadOfficeId = data.HeadOfficeId;
                model.BranchId = data.BranchId;
                model.BranchCode = data.BranchCode;
                model.BranchName = data.BranchName;
                model.ImageName = data.ImageName;
                model.ImagePath = data.ImagePath;
                model.State = data.State;
                model.District = data.District;
                model.Metropolitan = data.Metropolitan;
                model.SubMetropolitan = data.SubMetropolitan;
                model.Municipality = data.Municipality;
                model.GauPalika = data.GauPalika;
                model.WardNo = data.WardNo;
                model.Web = data.Web;
                model.PanNo = data.PanNo;
                model.VatNo = data.VatNo;
                model.EstablishDate = data.EstablishDate;
                model.RegisteredDate = data.RegisteredDate;
                model.Status = data.Status;
                model.Location = data.Location;
                model.PhoneNumber = data.PhoneNumber;
                model.EmailId = data.EmailId;
                model.Status = data.Status;
                

            }
            return model;
        }
    }
}
