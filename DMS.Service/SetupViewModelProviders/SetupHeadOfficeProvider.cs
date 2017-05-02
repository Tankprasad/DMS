using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Areas.SetupArea.Models.SetupMasterViewModel;
using DMS.DMS.Entities;
using DMS.CommonUtilities;
using System.Data.Entity;

namespace DMS.Service.SetupViewModelProviders
{
    public interface ISetupHeadOfficeProvider
    {
        int Save(SetupHeadOfficeViewModel model);
        SetupHeadOfficeViewModel GetHeadOfficeList(string headOfficeName,int? page);
        SetupHeadOfficeViewModel Edit(int headOfficeId);
        int Delete(int headOfficeId);
    }
    public class SetupHeadOfficeProvider : ISetupHeadOfficeProvider
    {
        private readonly DMSEntities _ent;
        public SetupHeadOfficeProvider()
        {
            this._ent = new DMSEntities();
        }
        public int Save(SetupHeadOfficeViewModel model)
        {
            SetupHaeadOffice headOfficeEntity = new SetupHaeadOffice();
            var data = (_ent.SetupHaeadOffices.Where(h => h.HeadOfficeName == model.HeadOfficeName.Trim() && h.DeletedDate == null)).ToList();
            if(data.Count >0)
            {
                return 2;
            }
            headOfficeEntity.HeadOfficeCode = model.HeadOfficeCode;
            headOfficeEntity.HeadOfficeName = model.HeadOfficeName;
            headOfficeEntity.ImageName = model.ImageName;
            headOfficeEntity.ImagePath = model.ImagePath;
            headOfficeEntity.Location = model.Location;
            headOfficeEntity.State = model.State;
            headOfficeEntity.District = model.District;
            headOfficeEntity.Metropolitan = model.Metropolitan;
            headOfficeEntity.SubMetropolitan = model.SubMetropolitan;
            headOfficeEntity.Municipality = model.Municipality;
            headOfficeEntity.GauPalika = model.GauPalika;
            headOfficeEntity.WardNo = model.WardNo;
            headOfficeEntity.EmailId = model.EmailId;
            headOfficeEntity.Web = model.Web;
            headOfficeEntity.PhoneNumber = model.PhoneNumber;
            headOfficeEntity.PanNo = model.PanNo;
            headOfficeEntity.VatNo = model.VatNo;
            headOfficeEntity.EstablishDate = model.EstablishDate;
            headOfficeEntity.RegisteredDate = model.RegisteredDate;
            if(model.HeadOfficeId > 0)
            {
                headOfficeEntity.HeadOfficeId = model.HeadOfficeId;
                headOfficeEntity.Status = model.Status;
                headOfficeEntity.UpdatedBy = 1;
                headOfficeEntity.UpdatedDate = DateTime.UtcNow;
                _ent.Entry(headOfficeEntity).State = EntityState.Modified;
                _ent.Entry(headOfficeEntity).Property(x => x.CreatedBy).IsModified = false;
                _ent.Entry(headOfficeEntity).Property(x => x.CreatedDate).IsModified = false;
            }
            else
            {
                headOfficeEntity.Status = true;
                headOfficeEntity.CreatedBy = 1;
                headOfficeEntity.CreatedDate = DateTime.UtcNow;
                _ent.Entry(headOfficeEntity).State = EntityState.Added;

            }
            try
            {
                _ent.SaveChanges();
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
                
           

        }
        public SetupHeadOfficeViewModel GetHeadOfficeList(string headOfficeName,int? page)
        {
            int pageSize = 2;
            SetupHeadOfficeViewModel model = new SetupHeadOfficeViewModel();
            if (headOfficeName != "")
            {
                var pager = new Pager(_ent.SetupHaeadOffices.Where(x => x.DeletedDate == null && x.HeadOfficeName.Contains(headOfficeName)).Count(), page);
                model.SetupHeadOfficeViewModelList = (from h in _ent.SetupHaeadOffices
                                                      where h.HeadOfficeName.Contains(headOfficeName) && h.DeletedDate == null
                                                      select new SetupHeadOfficeViewModel
                                                      {
                                                          HeadOfficeId = h.HeadOfficeId,
                                                          HeadOfficeName = h.HeadOfficeName,
                                                          ImageName = h.ImageName,
                                                          ImagePath = h.ImagePath,
                                                          State = h.State,
                                                          District = h.District,
                                                          Metropolitan = h.Metropolitan,
                                                          SubMetropolitan = h.SubMetropolitan,
                                                          Municipality = h.Municipality,
                                                          GauPalika = h.GauPalika,
                                                          WardNo = h.WardNo,
                                                          Web = h.Web,
                                                          PanNo = h.PanNo,
                                                          VatNo = h.VatNo,
                                                          EstablishDate = h.EstablishDate,
                                                          RegisteredDate = h.RegisteredDate,
                                                          Status = h.Status,
                                                          Location = h.Location,
                                                          PhoneNumber = h.PhoneNumber,
                                                          EmailId = h.EmailId,
                                                          //CreatedBy = h.CreatedBy,
                                                          //CreatedDate = h.CreatedDate,
                                                          //UpdatedBy = h.UpdatedBy,
                                                          //UpdatedDate = h.UpdatedDate
                                                      }).OrderBy(x => x.HeadOfficeId).
                                                      Skip((pager.CurrentPage - 1) * pageSize).Take(pageSize).ToList();
                model.pager = pager;
                
            }
            else
            {
                var pager = new Pager(_ent.SetupHaeadOffices.Where(x => x.DeletedDate == null).Count(), page);
                model.SetupHeadOfficeViewModelList = (from h in _ent.SetupHaeadOffices
                                                      where h.DeletedDate == null
                                                      select new SetupHeadOfficeViewModel
                                                      {
                                                          HeadOfficeId = h.HeadOfficeId,
                                                          HeadOfficeName = h.HeadOfficeName,
                                                          ImageName = h.ImageName,
                                                          ImagePath = h.ImagePath,
                                                          State = h.State,
                                                          District = h.District,
                                                          Metropolitan = h.Metropolitan,
                                                          SubMetropolitan = h.SubMetropolitan,
                                                          Municipality = h.Municipality,
                                                          GauPalika = h.GauPalika,
                                                          WardNo = h.WardNo,
                                                          Web = h.Web,
                                                          PanNo = h.PanNo,
                                                          VatNo = h.VatNo,
                                                          EstablishDate = h.EstablishDate,
                                                          RegisteredDate = h.RegisteredDate,
                                                          Status = h.Status,
                                                          Location = h.Location,
                                                          PhoneNumber = h.PhoneNumber,
                                                          EmailId = h.EmailId,
                                                          //CreatedBy = h.CreatedBy,
                                                          //CreatedDate = h.CreatedDate,
                                                          //UpdatedBy = h.UpdatedBy,
                                                          //UpdatedDate = h.UpdatedDate
                                                      }).OrderBy(x => x.HeadOfficeId).
                                                    Skip((pager.CurrentPage - 1) * pageSize).Take(pageSize).ToList();
                model.pager = pager;
            }
            return model;
        }

        public SetupHeadOfficeViewModel Edit(int headOfficeId)
        {
            SetupHeadOfficeViewModel model = new SetupHeadOfficeViewModel();
            var data = _ent.SetupHaeadOffices.Where(x => x.HeadOfficeId == headOfficeId).FirstOrDefault();
            if(data!=null)
            {
                model.HeadOfficeId = data.HeadOfficeId;
                model.HeadOfficeCode = data.HeadOfficeCode;
                model.HeadOfficeName = data.HeadOfficeName;
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
                model.CreatedBy = data.CreatedBy;
                model.CreatedDate = data.CreatedDate;


            }
            return model;
        }

        public int Delete(int headOfficeId)
        {
            var data = _ent.SetupHaeadOffices.Where(x => x.HeadOfficeId == headOfficeId).SingleOrDefault();
            if(data!=null)
            {
                data.DeletedBy = 1;
                data.DeletedDate = DateTime.UtcNow;
                _ent.Entry(data).State = EntityState.Modified;
                
            }
            try
            {
                _ent.SaveChanges();
                return 1;
            }
            catch (Exception ec)
            {

                return 0;
            }
        }
    }
}
